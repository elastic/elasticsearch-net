using Newtonsoft.Json;

namespace Nest
{
	public interface IGetIlmStatusResponse : IResponse
	{
		[JsonProperty("operation_mode")]
		LifecycleOperationMode OperationMode { get; }
	}

	public class GetIlmStatusResponse : ResponseBase, IGetIlmStatusResponse
	{
		public LifecycleOperationMode OperationMode { get; internal set; }
	}
}
