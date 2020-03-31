using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataObfuscation.Application.Mappers
{
	public static class DefualtMapper
	{
		private static bool ExcludePropertyId(PropertyInfo propertyInfo) => propertyInfo.Name != "Id";

		public static void Map<TSource, TDestination>(TSource source, TDestination destination, 
			Func<PropertyInfo, bool> ignoreWhere = null) 
			where TSource : class 
			where TDestination : class
		{
			var ignoreCodition = ignoreWhere ?? ExcludePropertyId;
			List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList();
			List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList();

			foreach (PropertyInfo sourceProperty in sourceProperties.Where(ignoreCodition))
			{
				PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

				if (destinationProperty != null)
				{
					destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
				}
			}
		}
	}
}
