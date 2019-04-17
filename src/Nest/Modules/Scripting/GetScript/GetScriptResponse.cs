using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class GetScriptResponse : ResponseBase
	{
		[DataMember(Name ="script")]
		public IStoredScript Script { get; set; }
	}
}
