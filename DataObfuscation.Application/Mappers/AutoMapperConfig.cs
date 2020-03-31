using AutoMapper;

namespace DataObfuscation.Application.Mappers
{
	public static class AutoMapperConfig
	{
		public static IMapper Initialize()
		{
			return new MapperConfiguration(cfg =>
			{

			}).CreateMapper();
		}
	}
}
