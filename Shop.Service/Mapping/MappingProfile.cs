using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Shop.Service.Mapping
{
    public static class MappingProfile
    {
        public static bool Map = false;
        private const string namespaceModel = "Shop.Service.Mapping";
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                Assembly controllerAssembly = typeof(MappingProfile).Assembly;
                Type entitySetControllerType = typeof(MappingBase<,>);
                var types = controllerAssembly.GetTypes()
                        .Where(
                            x =>
                            x.BaseType != null && x.BaseType.IsGenericType
                            && entitySetControllerType == x.BaseType.GetGenericTypeDefinition() && x.Namespace != null
                            && x.Namespace.StartsWith(namespaceModel))
                        .ToList();
                foreach (var type in types)
                {
                    cfg.AddProfile(type);
                }
            });

            Map = true;
            return config;
        }
    }
}
