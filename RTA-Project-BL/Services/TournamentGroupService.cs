using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_DAL.Models;
using RTA_Project_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_BL.Services
{

    public interface ITournamentGroupService : IGenereicService<TournamentGroupBL>
    {
    }

    public class TournamentGroupService : GenericService<TournamentGroupBL, TournamentGroup>, ITournamentGroupService
    {
        private readonly IMapper _mapper;
        public TournamentGroupService(IGenericRepository<TournamentGroup> repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public override TournamentGroupBL Map(TournamentGroup model)
        {
            return _mapper.Map<TournamentGroupBL>(model);
        }

        public override TournamentGroup Map(TournamentGroupBL model)
        {
            return _mapper.Map<TournamentGroup>(model);
        }

        public override IEnumerable<TournamentGroupBL> Map(IEnumerable<TournamentGroup> entitiesList)
        {
            return _mapper.Map<IEnumerable<TournamentGroupBL>>(entitiesList);
        }

        public override IEnumerable<TournamentGroup> Map(IEnumerable<TournamentGroupBL> entitiesList)
        {
            return _mapper.Map<IEnumerable<TournamentGroup>>(entitiesList);
        }

        public override IQueryable<TournamentGroupBL> Project(IQueryable<TournamentGroup> entitiesList)
        {
            return _mapper.ProjectTo<TournamentGroupBL>(entitiesList);
        }
    }
}
