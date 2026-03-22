using Autofac;
using University.Core.Services;

namespace University.API.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentService>()
                   .As<IStudentService>()
                   .InstancePerLifetimeScope();
        }
    }
}
