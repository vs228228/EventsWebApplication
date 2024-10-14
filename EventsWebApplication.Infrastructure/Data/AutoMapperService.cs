using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventsWebApplication.Application.Services;

namespace EventsWebApplication.Infrastructure.Data
{
   

    namespace Infrastructure.Services
    {
        public class AutoMapperService : IMapperService
        {
            private readonly IMapper _mapper;

            public AutoMapperService(IMapper mapper)
            {
                _mapper = mapper;
            }

            public async Task<TDestination> Map<TSource, TDestination>(TSource source)
            {
                return _mapper.Map<TDestination>(source);
            }

            public async Task<TDestination> Update<TSource, TDestination>(TSource source, TDestination destination)
            {
                return _mapper.Map(source, destination); 
            }
        }
    }

}
