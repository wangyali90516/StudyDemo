using System;
using AutoMapper;
using DesignPatternDemo.autoMapper;

namespace DesignPatternDemo
{
    public class AutoMapperInit
    {
        private static readonly AutoMapperInput obj = new AutoMapperInput { Name = "bbb" };

        public static void Main()
        {
            AutoMappingConfig.Register();
            var b = Mapper.Instance.Map<AutoMapperInput, AutoMapperoutPut>(obj);
            Console.WriteLine(b.Name);
            Console.ReadKey();
        }
    }
}