using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IExistsResponse : IResponse
	{
		bool Exists { get; }
	}

	[JsonObject]
	public class ExistsResponse : ResponseBase, IExistsResponse
	{
		public bool Exists => this.ApiCall != null && this.ApiCall.Success && this.ApiCall.HttpStatusCode == 200;
	}
}
