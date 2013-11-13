using Autofac;
using LFM.LandRegistry.CommsService;
using LFM.Submissions.AgentServices.LandRegistry;

namespace LFM.LandRegistry.SubmissionsService
{
    public class SubmissionsServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommsService.CommsService>().As<ICommsService>().InstancePerLifetimeScope();
            builder.RegisterType<EdrsCommunicator>().As<IEdrsCommunicator>().InstancePerLifetimeScope();
        }
    }
}
