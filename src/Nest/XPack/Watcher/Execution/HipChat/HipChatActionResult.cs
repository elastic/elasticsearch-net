using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class HipChatActionResult
	{
		[DataMember(Name ="account")]
		public string Account { get; set; }

		[DataMember(Name ="sent_messages")]
		public IEnumerable<HipChatActionMessageResult> SentMessages { get; set; }
	}
}
