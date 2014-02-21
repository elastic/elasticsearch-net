using System.Net;
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
		internal IndexExistsResponse(NestElasticsearchResponse connectionStatus)
		{
			this.ConnectionStatus = connectionStatus;
			this.IsValid = connectionStatus.Error == null || connectionStatus.Error.HttpStatusCode == HttpStatusCode.NotFound;
			this.Exists = connectionStatus.Error == null && connectionStatus.Success;
		}

		public bool Exists { get; internal set; }
	}
}
