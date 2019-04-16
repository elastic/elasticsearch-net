using System.Runtime.Serialization;

namespace Nest
{
	public interface IExecutePainlessScriptResponse<TResult> : IResponse
	{
		[DataMember(Name ="result")]
		TResult Result { get; }
	}

	public class ExecutePainlessScriptResponse<TResult> : ResponseBase, IExecutePainlessScriptResponse<TResult>
	{
		public TResult Result { get; set; }
	}
}
