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

        public TournamentService(IGenericRepository<Tournament> repository, IMapper mapper) : base(repository, mapper)
        {

        }


    }

}
