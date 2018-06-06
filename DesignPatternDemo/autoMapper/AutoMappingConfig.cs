using AutoMapper;

namespace DesignPatternDemo.autoMapper
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