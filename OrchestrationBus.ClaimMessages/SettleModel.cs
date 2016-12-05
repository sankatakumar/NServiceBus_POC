using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchestrationBus.ClaimMessages
{    
    public class SettleModel : ICommand
    {
        public Guid ConversationID { get; set; }

        public int SettlementID { get; set; }
        
        public string ClaimData { get; set; }

    }
}
