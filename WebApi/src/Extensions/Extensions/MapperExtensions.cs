using AutoMapper;

namespace Extensions.Extensions
{
    public static class MapperExtensions
    {
        private static IMapper _mapper;

        public static void Configure(IMapper mapper)
        {
            _mapper = mapper;
        }

        public static TDestination Map<TDestination>(this object obj)
        {
            return _mapper.Map<TDestination>(obj);
        }
    }
}
