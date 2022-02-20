using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AutoMapper
{
    public static class Mapper
    {
        private static IMapper instance { get; set; }

        public static IMapper Instance()
        {
            if (instance == null)
            {
                MapperConfiguration mapConfig = new MapperConfiguration(config => { config.AddProfile(new MappingProfile()); });
                instance = mapConfig.CreateMapper();
            }

            return instance;
        }

    }
}
