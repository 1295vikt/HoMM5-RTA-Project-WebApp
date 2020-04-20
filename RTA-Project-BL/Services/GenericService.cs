using RTA_Project_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTA_Project_BL.Services
{
    public interface IGenereicService<BLModel>
        where BLModel : class
    {
        BLModel FindById(int id);

        IEnumerable<BLModel> GetAll();
        IQueryable<BLModel> QueryAll();

        void Create(BLModel modelBL);
        void Delete(int id);
        void Edit(BLModel modelBL);
    }

    public abstract class GenericService<BLModel, DModel> : IGenereicService<BLModel>
        where BLModel : class
        where DModel : class
    {
        private readonly IGenericRepository<DModel> _repositroy;
        public GenericService(IGenericRepository<DModel> repository)
        {
            _repositroy = repository;
        }

        public virtual BLModel FindById(int id)
        {
            var entity = _repositroy.FindById(id);
            return Map(entity);
        }

        public IEnumerable<BLModel> GetAll()
        {
            var listEntity = _repositroy.GetAll();
            return Map(listEntity);
        }

        public IQueryable<BLModel> QueryAll()
        {
            var listEntity = _repositroy.QueryAll();
            return Project(listEntity);
        }

        public void Create(BLModel modelBL)
        {
            var entity = Map(modelBL);
            _repositroy.Create(entity);
        }

        public void Delete(int id)
        {
            _repositroy.RemoveById(id);
        }

        public void Edit(BLModel modelBL)
        {
            var entity = Map(modelBL);
            _repositroy.Update(entity);
        }

        public abstract BLModel Map(DModel entity);
        public abstract DModel Map(BLModel modelBL);

        public abstract IEnumerable<BLModel> Map(IEnumerable<DModel> modelBL);
        public abstract IEnumerable<DModel> Map(IEnumerable<BLModel> modelBL);
        public abstract IQueryable<BLModel> Project(IQueryable<DModel> entity);
    }
}
