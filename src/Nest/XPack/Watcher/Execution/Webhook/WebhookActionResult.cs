using System.Runtime.Serialization;

namespace Nest
{
	public class WebhookActionResult
	{
		[DataMember(Name ="request")]
		public HttpInputRequestResult Request { get; set; }

		[DataMember(Name ="response")]
		public HttpInputResponseResult Response { get; set; }
	}
}
