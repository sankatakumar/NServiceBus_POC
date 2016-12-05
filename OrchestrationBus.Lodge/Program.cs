using NServiceBus;
using OrchestrationBus.ClaimMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrchestrationBus.Lodge
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
            Console.Title = "OrchestrationBus - Lodge";

            Thread.Sleep(5000);

            #region ConfigureRabbit

            var endpointConfiguration = new EndpointConfiguration("Q_OB_LODGE_DEV");
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString("host=localhost;username=developer_test;password=developer_test");

            #endregion

            endpointConfiguration.SendFailedMessagesTo("Q_OB_LODGE_ERR_DEV");
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();

            var recover = endpointConfiguration.Recoverability();
            recover.Immediate(immediate => immediate.NumberOfRetries(3));

            endpointConfiguration.TimeToWaitBeforeTriggeringCriticalErrorOnTimeoutOutages(new TimeSpan(0, 0, 60));

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);


            #region Lodgement Process

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("***************************************************************");
            Console.WriteLine("Finished with Lodgment");
            Console.WriteLine("***************************************************************");
            Console.WriteLine("");
            Console.WriteLine("");            

            #endregion

            var assessModel = new AssessModel();

            assessModel.ConversationID = Guid.NewGuid();

            Console.WriteLine($"Sending Claim converstaion : {assessModel.ConversationID} for Assessment");

            assessModel.AssessID = 200;
            assessModel.ClaimData = "<ClaimData><AssessData><Condition>Assess - Condition</Condition></AssessData></ClaimData>";

            await endpointInstance.Send("Q_OB_ASSESS_DEV", assessModel)
                .ConfigureAwait(false);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            await endpointInstance.Stop()
                .ConfigureAwait(false);

        }
    }
}
