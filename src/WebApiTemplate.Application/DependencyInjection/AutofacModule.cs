using Autofac;
using WebApiTemplate.Application.Interfaces;
using WebApiTemplate.Application.Services;

namespace WebApiTemplate.Application.DependencyInjection
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //services
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();

            // Register AutoMapper module
            builder.RegisterModule(new AutoMapperModule(this.GetType().Assembly));
        }
    }
}
