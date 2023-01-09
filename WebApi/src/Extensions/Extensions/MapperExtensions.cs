using AutoMapper;

namespace Extensions.Extensions
{
    public static class MapperExtensions
    {
        public static TDestination Map<TDestination>(this object obj, IMapper mapper)
        {
            return mapper.Map<TDestination>(obj);
        }
    }
}
