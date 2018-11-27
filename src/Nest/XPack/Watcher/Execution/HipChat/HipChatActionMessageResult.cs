using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class HipChatActionMessageResult
	{
		[DataMember(Name ="message")]
		public IHipChatMessage Message { get; set; }

		[DataMember(Name ="reason")]
		public string Reason { get; set; }

		[DataMember(Name ="request")]
		public HttpInputRequestResult Request { get; set; }

		[DataMember(Name ="response")]
		public HttpInputResponseResult Response { get; set; }

		[DataMember(Name ="room")]
		public string Room { get; set; }

		[DataMember(Name ="status")]
		public Status Status { get; set; }

		[DataMember(Name ="user")]
		public string User { get; set; }
	}
}
