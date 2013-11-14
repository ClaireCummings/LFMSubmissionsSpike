using Autofac;

namespace LFM.Submissions.AgentServices
{
    public class AgentServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ObjectSerializer>().As<IObjectSerializer>()
                .InstancePerLifetimeScope();
        }
    }
}
