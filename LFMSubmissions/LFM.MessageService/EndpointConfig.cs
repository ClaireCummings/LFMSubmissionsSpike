using Autofac;
using LFM.LandRegistry.SubmissionsService;
using LFM.Submissions.AgentServices;

namespace LFM.MessageService
{
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
	    public void Init()
	    {
	        var builder = new ContainerBuilder();
            builder.RegisterModule<MessageServiceModule>();
            builder.RegisterModule<SubmissionsServiceModule>();
            builder.RegisterModule<AgentServicesModule>();

	        Configure.With()
	            .MessageConventions()
	            .AutofacBuilder(builder.Build());
	    }
    }
}
