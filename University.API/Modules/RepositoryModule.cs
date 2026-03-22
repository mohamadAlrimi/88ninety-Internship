using Autofac;
using University.Data.Repositories;

namespace University.API.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentRepository>()
                   .As<IStudentRepository>()
                   .InstancePerLifetimeScope();
        }
    }
}
