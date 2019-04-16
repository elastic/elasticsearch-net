using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class SlackActionResult
	{
		[DataMember(Name ="account")]
		public string Account { get; set; }

		[DataMember(Name ="sent_messages")]
		public IEnumerable<SlackActionMessageResult> SentMessages { get; set; }
	}
}
