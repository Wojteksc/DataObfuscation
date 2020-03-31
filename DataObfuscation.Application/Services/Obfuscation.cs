using AutoMapper;
using Bogus;
using DataObfuscation.Application.Extensions;
using DataObfuscation.Application.Mappers;
using DataObfuscation.Application.Options;
using DataObfuscation.Infrastructure.Repositories;
using EFCore.BulkExtensions;
using Microsoft.Extensions.Configuration;
using ShellProgressBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataObfuscation.Application.Services
{
	public class Obfuscation<TEntity, TFaker>
		where TEntity : class
		where TFaker : class, new()
	{
		protected readonly IEntityRepository<TEntity> _repository;
		protected readonly IMapper _mapper;

		protected readonly ProgressBarOptions _mainOptions;
		protected readonly ProgressBarOptions _childOptions;

		protected readonly int _recordsToUpdate;

		public Obfuscation(IEntityRepository<TEntity> repository,
			IConfiguration configuration,
			IProgressBarOptions progressBarOptions)
		{
			_repository = repository;
			_mainOptions = progressBarOptions.GetMainOptions();
			_childOptions = progressBarOptions.GetChildOptions();
			_recordsToUpdate = int.Parse(configuration.GetSection("ObfuscationSettings:NumberOfRecordsToObfuscate").Value);
		}

		protected virtual IQueryable<TEntity> GetData(int skip, int take)
		{
			return _repository.GetEntities(skip, take);
		}

		protected virtual TEntity GenerateFaker()
		{
			var faker = new TFaker();
			return (faker as Faker<TEntity>).Generate();
		}

		protected virtual void MapData(TEntity faker, TEntity entity)
		{
			DefualtMapper.Map(faker, entity);
		}

		protected virtual void SaveData(IEnumerable<TEntity> entities)
		{
			_repository.BulkUpdate(entities.ToList(), new BulkConfig() { BulkCopyTimeout = 0 });
		}

		public void Execute()
		{
			int totalRecords = _repository.Count();
			int totalMappingIterations = (int)Math.Ceiling(totalRecords / (double)_recordsToUpdate);

			int mappingIterator = 1;
			using (var pBar = new ProgressBar(3, "Obfuscation", _mainOptions))
			{
				for (int updatedRecords = 0; updatedRecords < totalRecords; updatedRecords += _recordsToUpdate)
				{
					pBar.Tick(1, GetStepMessage("Step 1 of 3 - GetData", updatedRecords, totalRecords));
					var entities = GetData(updatedRecords, _recordsToUpdate).ToList();

					pBar.Tick(2, GetStepMessage("Step 2 of 3 - MapData", updatedRecords, totalRecords));
					Mapping(entities, pBar, totalMappingIterations, mappingIterator);

					pBar.Tick(3, GetStepMessage("Step 3 of 3 - SaveData", updatedRecords, totalRecords));
					SaveData(entities);

					++mappingIterator;
				}
				pBar.Tick($"!!Successful!!{GetStepMessage("", totalRecords, totalRecords)}");
			}
		}

		private void Mapping(List<TEntity> entities, ProgressBar pBar, int totalMappingIterations, int mappingIterator)
		{
			int entitiesCount = entities.Count;
			using (var childPBar = pBar.Spawn(entities.Count(), "Mapping", _childOptions))
			{
				Parallel.ForEach(entities.WithIndex(), (entity) =>
				{
					childPBar.Tick($"Mapping {entitiesCount} entities ({mappingIterator}/{totalMappingIterations} iterations)");
					var faker = GenerateFaker();
					MapData(faker, entity.item);
				});
			}
		}

		private string GetStepMessage(string step, int updatedRecords, int totalRecords)
		{
			return $"{step} ({typeof(TEntity).Name} {updatedRecords}/{totalRecords} updated records)";
		}
	}
}
