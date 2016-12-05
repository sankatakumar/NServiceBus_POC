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
    public class AssessModelHandler : IHandleMessages<AssessModel>
    {

        public Task Handle(AssessModel message, IMessageHandlerContext context)
        {
            Thread.Sleep(1000);

            var settleModel = new SettleModel();

            try
            {
                
                Console.WriteLine(message.ConversationID);

                Console.WriteLine("***************************************************************");
                Console.WriteLine("Message Received");
                Console.WriteLine("***************************************************************");

                Console.WriteLine($"Assess ID : {message.AssessID}");
                Console.WriteLine($"Assess Message : {message.ClaimData}");

                Console.WriteLine("***************************************************************");

                Console.WriteLine("*************** Assessment is being processed ****************");

                Console.WriteLine("***************************************************************");                

                settleModel.ConversationID = message.ConversationID;
                settleModel.SettlementID = 300;
                settleModel.ClaimData = message.ClaimData;
                return context.Send("Q_OB_SETTLE_DEV", settleModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Task.CompletedTask;
            }            
        }
    }
}
