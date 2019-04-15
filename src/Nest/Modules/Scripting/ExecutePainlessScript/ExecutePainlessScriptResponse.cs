using System.Runtime.Serialization;

namespace Nest
{
	public interface IExecutePainlessScriptResponse<out TResult> : IResponse
	{
		TResult Result { get; }
	}

	public class ExecutePainlessScriptResponse<TResult> : ResponseBase, IExecutePainlessScriptResponse<TResult>
	{
		[DataMember(Name ="result")]
		public TResult Result { get; set; }
	}
}
