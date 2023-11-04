using Autofac;
using WebApiTemplate.Api.Authorization;
using WebApiTemplate.Application.Interfaces.Api;

namespace WebApiTemplate.Api.DependencyInjection
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            builder.RegisterType<HttpCurrentUserContext>().As<ICurrentUserContext>().SingleInstance();
            builder.RegisterType<JwtAuthOptions>().As<IAuthOptions>().SingleInstance();
        }
    }
}
