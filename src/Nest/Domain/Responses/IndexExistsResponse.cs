using Newtonsoft.Json;

namespace Nest
{
	public interface IIndexExistsResponse : IResponse
	{
		bool Exists { get; }
	}

	[JsonObject]
	public class IndexExistsResponse : BaseResponse, IIndexExistsResponse
	{	
		public bool Exists { get; internal set; }
	}
}
