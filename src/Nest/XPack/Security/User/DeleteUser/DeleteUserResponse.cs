using Newtonsoft.Json;

namespace Nest
{
	public interface IDeleteUserResponse : IResponse
	{
		[JsonProperty("found")]
		bool Found { get; }
	}

	public class DeleteUserResponse : ResponseBase, IDeleteUserResponse
	{
		public bool Found { get; internal set; }
	}
}
