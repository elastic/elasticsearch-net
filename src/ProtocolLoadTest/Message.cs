using System;
using System.Runtime.Serialization;

namespace ProtocolLoadTest
{
    public class Message
    {
        // Already included by NEST as ID property, so don't store it again as a seperate field in the
        // index
        [IgnoreDataMember]
        public Guid Id { get; set; }

        public DateTime Timestamp { get; set; }
        public string Body { get; set; }
    }
}
