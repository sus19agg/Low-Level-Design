using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Models
{
    internal class QueueMessage
    {
        public required string Id { get; set; }
        public required string Message { get; set; }
        public Object? AdditionalProperties { get; set; }
    }
}
