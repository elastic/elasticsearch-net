using Newtonsoft.Json;

namespace Nest
{
	public interface IIlmGetStatusResponse : IResponse
	{
		[JsonProperty("operation_mode")]
		OperationMode OperationMode { get; }
	}

	public class IlmGetStatusResponse : ResponseBase, IIlmGetStatusResponse
	{
		public OperationMode OperationMode { get; internal set; }
	}
}
