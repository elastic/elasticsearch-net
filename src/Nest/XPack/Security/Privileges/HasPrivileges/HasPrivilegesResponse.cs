using Newtonsoft.Json;

namespace Nest
{
	public interface IHasPrivilegesResponse : IResponse
	{
		[JsonProperty("user")]
		HasPrivilegesStatus User { get; }
	}

	public class HasPrivilegesResponse : ResponseBase, IHasPrivilegesResponse
	{
		public HasPrivilegesStatus User { get; internal set; }
	}

	public class HasPrivilegesStatus
	{
		[JsonProperty("created")]
		public bool Created { get; internal set; }
	}
}
