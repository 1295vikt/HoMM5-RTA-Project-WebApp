﻿using AutoMapper;
using RTA_Project_DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

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
        void Update(BLModel modelBL);
    }

    public abstract class GenericService<BLModel, DModel> : IGenereicService<BLModel>
        where BLModel : class
        where DModel : class
    {
        protected readonly IGenericRepository<DModel> _repository;
        protected readonly IMapper _mapper;

        public GenericService(IGenericRepository<DModel> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual BLModel FindById(int id)
        {
            var entity = _repository.FindById(id);
            return Map(entity);
        }

        public IEnumerable<BLModel> GetAll()
        {
            var listEntity = _repository.GetAll();
            return Map(listEntity);
        }

        public IQueryable<BLModel> QueryAll()
        {
            var listEntity = _repository.QueryAll();
            return Project(listEntity);
        }


        public void Create(BLModel modelBL)
        {
            var entity = Map(modelBL);
            _repository.Create(entity);
        }

        public void Delete(int id)
        {
            _repository.RemoveById(id);
        }

        public void Update(BLModel modelBL)
        {
            var entity = Map(modelBL);
            _repository.Update(entity);
        }


        public BLModel Map(DModel model)
        {
            return _mapper.Map<BLModel>(model);
        }

        public DModel Map(BLModel model)
        {
            return _mapper.Map<DModel>(model);
        }

        public IEnumerable<BLModel> Map(IEnumerable<DModel> entitiesList)
        {
            return _mapper.Map<IEnumerable<BLModel>>(entitiesList);
        }

        public IEnumerable<DModel> Map(IEnumerable<BLModel> entitiesList)
        {
            return _mapper.Map<IEnumerable<DModel>>(entitiesList);
        }

        public IQueryable<BLModel> Project(IQueryable<DModel> entitiesList)
        {
            return _mapper.ProjectTo<BLModel>(entitiesList);
        }


    }
}
