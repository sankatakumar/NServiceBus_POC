using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchestrationBus.ClaimMessages
{
    public class AssessModel : ICommand
    {
        public Guid ConversationID { get; set; }

        public int AssessID { get; set; }

        public string ClaimData { get; set; }

    }
}
