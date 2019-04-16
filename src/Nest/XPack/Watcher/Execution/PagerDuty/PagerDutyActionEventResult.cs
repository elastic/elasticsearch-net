using System.Runtime.Serialization;

namespace Nest
{
	public class PagerDutyActionEventResult
	{
		[DataMember(Name ="event")]
		public PagerDutyEvent Event { get; set; }

		[DataMember(Name ="reason")]
		public string Reason { get; set; }

		[DataMember(Name ="request")]
		public HttpInputRequestResult Request { get; set; }

		[DataMember(Name ="response")]
		public HttpInputResponseResult Response { get; set; }
	}
}
