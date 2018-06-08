using AutoMapper;
using DesignPatternDemo.autoMapper;

namespace DesignPatternDemo.quartz
{
    public class AutoMappingConfig
    {
        /// <summary>
        /// </summary>
        public static void Register()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(AutoMapperProfile).Assembly));
        }
    }
}