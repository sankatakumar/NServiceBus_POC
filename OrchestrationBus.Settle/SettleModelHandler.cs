using NServiceBus;
using OrchestrationBus.ClaimMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrchestrationBus.Settle
{   
    public class SettleModelHandler : IHandleMessages<SettleModel>
    {
        public Task Handle(SettleModel message, IMessageHandlerContext context)
        {
            Thread.Sleep(1000);

            //try
            //{
            Console.WriteLine(message.ConversationID);

            Console.WriteLine("***************************************************************");
            Console.WriteLine("Message Received");
            Console.WriteLine("***************************************************************");

            Console.WriteLine($"Settlement ID : {message.SettlementID}");
            Console.WriteLine($"Settle Message : {message.ClaimData}");

            Console.WriteLine("***************************************************************");

            Console.WriteLine("*************** Settlement is being processed ****************");

            int temp = 1;
            if (1 == temp)
            {
                throw new Exception("Unhandled exception");
            }

            return Task.CompletedTask;

            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}

        }
    }
}
