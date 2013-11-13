using Autofac;

namespace LFM.MessageService
{
    public class MessageServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(LandRegistry.SubmissionsService.Lrap1Processor).Assembly)
                .AsImplementedInterfaces();
            builder.RegisterType<MessageSender>().As<ISendMessages>().InstancePerLifetimeScope();

        }
    }
}