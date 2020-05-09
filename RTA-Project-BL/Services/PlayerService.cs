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
    public interface IPlayerService : IGenereicService<PlayerBL>
    {
    }

    public class PlayerService : GenericService<PlayerBL, Player>, IPlayerService
    {
        private readonly IMapper _mapper;
        public PlayerService(IGenericRepository<Player> repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        public override PlayerBL Map(Player model)
        {
            return _mapper.Map<PlayerBL>(model);
        }

        public override Player Map(PlayerBL model)
        {
            return _mapper.Map<Player>(model);
        }

        public override IEnumerable<PlayerBL> Map(IEnumerable<Player> entitiesList)
        {
            return _mapper.Map<IEnumerable<PlayerBL>>(entitiesList);
        }

        public override IEnumerable<Player> Map(IEnumerable<PlayerBL> entitiesList)
        {
            return _mapper.Map<IEnumerable<Player>>(entitiesList);
        }

        public override IQueryable<PlayerBL> Project(IQueryable<Player> entitiesList)
        {
            return _mapper.ProjectTo<PlayerBL>(entitiesList);
        }

    }
}
