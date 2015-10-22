using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetIndexResponse : IResponse
	{
		IDictionary<IndexName, IndexState> Indices { get; set; }
	}

	[JsonObject]
	public class GetIndexResponse : BaseResponse, IGetIndexResponse
	{
		public IDictionary<IndexName, IndexState> Indices { get; set; } = new Dictionary<IndexName, IndexState>();
	}
}
