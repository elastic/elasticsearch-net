using System.Net;
using Elasticsearch.Net;
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
		internal IndexExistsResponse(IElasticsearchResponse connectionStatus)
		{
			this.ConnectionStatus = connectionStatus;
			this.IsValid =connectionStatus.Success || connectionStatus.HttpStatusCode == 404;
			this.Exists = connectionStatus.Success & connectionStatus.HttpStatusCode == 200;
		}

		public bool Exists { get; internal set; }
	}
}
