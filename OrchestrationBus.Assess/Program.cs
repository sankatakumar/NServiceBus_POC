using NServiceBus;
using OrchestrationBus.ClaimMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrchestrationBus.Assess
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                AsyncMain().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }            
        }

        static async Task AsyncMain()
        {
            Console.Title = "OrchestrationBus - Assess";

            Thread.Sleep(5000);

            #region ConfigureRabbit

            var endpointConfiguration = new EndpointConfiguration("Q_OB_ASSESS_DEV");
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString("host=localhost;username=developer_test;password=developer_test");

            #endregion
            endpointConfiguration.SendFailedMessagesTo("Q_OB_ASSESS_ERR_DEV");
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();

            endpointConfiguration.TimeToWaitBeforeTriggeringCriticalErrorOnTimeoutOutages(new TimeSpan(0, 0, 60));

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
                        
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            await endpointInstance.Stop()
                .ConfigureAwait(false);

        }
    }
}
