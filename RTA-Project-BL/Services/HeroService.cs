using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_DAL.Models;
using RTA_Project_DAL.Repositories;
using System.Collections.Generic;

namespace RTA_Project_BL.Services
{
    public interface IHeroService : IGenereicService<HeroBL>
    {
        IEnumerable<HeroBL> GetAllByFaction(int factionId);
    }

    public class HeroService : GenericService<HeroBL, Hero>, IHeroService
    {

        public HeroService(IGenericRepository<Hero> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public IEnumerable<HeroBL> GetAllByFaction(int factionId)
        {
            var heroes = _repository.Get(h => h.FactionId == factionId);
            return Map(heroes);
        }
    }
}
