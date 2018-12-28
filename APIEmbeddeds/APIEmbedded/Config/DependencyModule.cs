using Autofac;

namespace APIEmbedded.Config
{
    public class DependencyModule : Module
    {

        public DependencyModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .AsImplementedInterfaces()
                .SingleInstance();// in assembly
            
            base.Load(builder);
        }
    }
}
