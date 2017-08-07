using Newtonsoft.Json;

namespace Nest
{
	public interface IDeleteExpiredDataResponse : IResponse
	{
		[JsonProperty("deleted")]
		bool Deleted { get; }
	}

	public class DeleteExpiredDataResponse : ResponseBase, IDeleteExpiredDataResponse
	{
		public bool Deleted { get; internal set; }
	}
}
