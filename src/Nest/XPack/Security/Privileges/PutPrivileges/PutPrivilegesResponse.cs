using Newtonsoft.Json;

namespace Nest
{
	public interface IPutPrivilegesResponse : IResponse
	{
		[JsonProperty("user")]
		PutPrivilegesStatus Privileges { get; }
	}

	public class PutPrivilegesResponse : ResponseBase, IPutPrivilegesResponse
	{
		public PutPrivilegesStatus Privileges { get; internal set; }
	}

	public class PutPrivilegesStatus
	{
		[JsonProperty("created")]
		public bool Created { get; internal set; }
	}
}
