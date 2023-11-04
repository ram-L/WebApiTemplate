using Autofac;
using WebApiTemplate.Application.Interfaces.Infrastructure;
using WebApiTemplate.Infrastructure.Data;
using WebApiTemplate.Infrastructure.Repositories;
using WebApiTemplate.Infrastructure.Repositories.Common;

namespace WebApiTemplate.Infrastructure.DependencyInjection
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //context provider
            builder.RegisterType<AppDbContextProvider>().As<IAppDbContextProvider>().InstancePerLifetimeScope();

            //repositories            
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ClientRepository>().As<IClientRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ExternalUserRepository>().As<IExternalUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleClaimRepository>().As<IRoleClaimRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AccountRoleRepository>().As<IAccountRoleRepository>().InstancePerLifetimeScope();
        }
    }
}
