using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetScriptResponse : IResponse
	{
		IStoredScript Script { get; }
	}

	[DataContract]
	public class GetScriptResponse : ResponseBase, IGetScriptResponse
	{
		[DataMember(Name ="script")]
		public IStoredScript Script { get; set; }
	}
}
