using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class PagerDutyActionResult
	{
		[DataMember(Name ="sent_event")]
		public PagerDutyActionEventResult SentEvent { get; set; }
	}
}
