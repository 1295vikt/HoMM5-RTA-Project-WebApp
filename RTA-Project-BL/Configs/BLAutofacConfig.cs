using Autofac;
using RTA_Project_DAL;
using RTA_Project_DAL.Repositories;
using System.Data.Entity;

namespace RTA_Project_BL.Configs
{
    public class BLAutofacConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.Register<DbContext>(factory => new RTADatabaseContext());
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));

        }
    }
}
