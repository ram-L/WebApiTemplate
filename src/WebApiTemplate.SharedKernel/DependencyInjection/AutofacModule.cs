using Autofac;
using WebApiTemplate.SharedKernel.Interfaces;
using WebApiTemplate.SharedKernel.Loggers;

namespace WebApiTemplate.SharedKernel.DependencyInjection
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Register logger
            builder.Register(c => new DefaultLogger(c.Resolve<Serilog.ILogger>())).As<ICustomLogger>().SingleInstance();
        }
    }
}
