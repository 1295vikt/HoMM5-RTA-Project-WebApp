using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_DAL.Models;
using RTA_Project_DAL.Repositories;

namespace RTA_Project_BL.Services
{
    public interface IPlayerService : IGenereicService<PlayerBL>
    {
        PlayerBL GetPlayer(string userId);
    }

    public class PlayerService : GenericService<PlayerBL, Player>, IPlayerService
    {

        public PlayerService(IGenericRepository<Player> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public PlayerBL GetPlayer(string userId)
        {
            var player = _repository.GetFirstOrDefault(p => p.AccountId == userId);
            return Map(player);
        }
    }
}
