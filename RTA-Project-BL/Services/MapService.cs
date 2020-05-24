using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_DAL.Models;
using RTA_Project_DAL.Repositories;

namespace RTA_Project_BL.Services
{
    public interface IMapService : IGenereicService<MapBL>
    {
    }

    public class MapService : GenericService<MapBL, Map>, IMapService
    {

        public MapService(IGenericRepository<Map> repository, IMapper mapper) : base(repository, mapper)
        {
        }

    }
}
