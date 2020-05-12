using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_DAL.Models;
using RTA_Project_DAL.Repositories;

namespace RTA_Project_BL.Services
{

    public interface ITournamentGroupService : IGenereicService<TournamentGroupBL>
    {
    }

    public class TournamentGroupService : GenericService<TournamentGroupBL, TournamentGroup>, ITournamentGroupService
    {

        public TournamentGroupService(IGenericRepository<TournamentGroup> repository, IMapper mapper) : base(repository, mapper)
        {
        }

    }
}
