using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_DAL.Models;
using RTA_Project_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RTA_Project_BL.Services
{
    public interface ITournamentService : IGenereicService<TournamentBL>
    {
    }

    public class TournamentService : GenericService<TournamentBL, Tournament>, ITournamentService
    {
        private readonly IMapper _mapper;
        public TournamentService(IGenericRepository<Tournament> repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public override TournamentBL Map(Tournament model)
        {
            return _mapper.Map<TournamentBL>(model);
        }

        public override Tournament Map(TournamentBL model)
        {
            return _mapper.Map<Tournament>(model);
        }

        public override IEnumerable<TournamentBL> Map(IEnumerable<Tournament> entitiesList)
        {
            return _mapper.Map<IEnumerable<TournamentBL>>(entitiesList);
        }

        public override IEnumerable<Tournament> Map(IEnumerable<TournamentBL> entitiesList)
        {
            return _mapper.Map<IEnumerable<Tournament>>(entitiesList);
        }

        public override IQueryable<TournamentBL> Project(IQueryable<Tournament> entitiesList)
        {
            return _mapper.ProjectTo<TournamentBL>(entitiesList);
        }
    }

}
