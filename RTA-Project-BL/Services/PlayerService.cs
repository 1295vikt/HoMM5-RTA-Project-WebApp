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
        public PlayerService(IGenericRepository<Player> repository, IMapper mapper) : base(repository, mapper)
        {
            
        }



    }
}
