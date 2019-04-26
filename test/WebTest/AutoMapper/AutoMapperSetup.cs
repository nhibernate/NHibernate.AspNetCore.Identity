using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace WebTest.AutoMapper {
    public static class AutoMapperSetup {
        public static void AddAutoMapperSetup(this IServiceCollection services) {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper();

            RegisterMappings();
        }

        public static MapperConfiguration RegisterMappings() {
            return new MapperConfiguration(cfg => { cfg.AddProfile(new DtoToDoMappingProfile()); });
        }
    }
}
