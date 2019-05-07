using Newtonsoft.Json;

namespace Nest
{
	public interface IIlmGetStatusResponse : IResponse
	{
		[JsonProperty("operation_mode")]
		LifecycleOperationMode OperationMode { get; }
	}

	public class IlmGetStatusResponse : ResponseBase, IIlmGetStatusResponse
	{
		public LifecycleOperationMode OperationMode { get; internal set; }
	}
}
