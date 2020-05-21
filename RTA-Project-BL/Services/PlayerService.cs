using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_DAL.Models;
using RTA_Project_DAL.Repositories;

namespace RTA_Project_BL.Services
{
    public interface IPlayerService : IGenereicService<PlayerBL>
    {
        PlayerBL GetByName(string name);
        PlayerBL GetByAccountId(string userId);
        void LinkAccountToPlayer(string userId, string playerKey);
    }

    public class PlayerService : GenericService<PlayerBL, Player>, IPlayerService
    {

        public PlayerService(IGenericRepository<Player> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public PlayerBL GetByName(string name)
        {
            var player = _repository.GetFirstOrDefault(p => p.Name == name);
            return Map(player);
        }

        public PlayerBL GetByAccountId(string userId)
        {
            var player = _repository.GetFirstOrDefault(p => p.AccountId != null && p.AccountId == userId);
            return Map(player);
        }

        public void LinkAccountToPlayer(string userId, string playerKey)
        {
            var player = _repository.GetFirstOrDefault(p => p.GuidKey == playerKey);
            if (player != null)
            {
                player.AccountId = userId;
                _repository.Update(player);
            }

        }
    }
}
