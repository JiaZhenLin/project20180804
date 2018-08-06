using Autofac;
using Project20180804.Core.BLL.Impl;
using Project20180804.Core.DAL;
using Project20180804.Core.DAL.Repositorys;

namespace Project20180804.Core.BLL.Autofac
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MySqlDbContext>().AsSelf().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EFRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<CategoryBLL>().As<ICategoryBLL>().InstancePerLifetimeScope();
            builder.RegisterType<ProductBLL>().As<IProductBLL>().InstancePerLifetimeScope();
            builder.RegisterType<VideoBLL>().As<IVideoBLL>().InstancePerLifetimeScope();
        }
    }
}
