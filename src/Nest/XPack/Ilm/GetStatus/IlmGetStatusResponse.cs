using Newtonsoft.Json;

namespace Nest
{
	public interface IIlmGetStatusResponse : IResponse
	{
		[JsonProperty("operation_mode")]
		string OperationMode { get; }
	}

	public class IlmGetStatusResponse : ResponseBase, IIlmGetStatusResponse
	{
		public string OperationMode { get; internal set; }
	}
}
