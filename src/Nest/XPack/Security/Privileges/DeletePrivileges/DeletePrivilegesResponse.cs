using Newtonsoft.Json;

namespace Nest
{
	public interface IDeletePrivilegesResponse : IResponse
	{
		[JsonProperty("found")]
		bool Found { get; }
	}

	public class DeletePrivilegesResponse : ResponseBase, IDeletePrivilegesResponse
	{
		public bool Found { get; internal set; }
	}
}
