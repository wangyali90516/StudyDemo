using AutoMapper;

namespace DesignPatternDemo.autoMapper
{
    /// <summary>
    ///     平台与银行交互map关系
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<AutoMapperInput, AutoMapperoutPut>();
        }
    }
}