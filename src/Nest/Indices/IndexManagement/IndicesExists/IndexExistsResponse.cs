using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IExistsResponse : IResponse
	{
		bool Exists { get; }
	}

	[JsonObject]
	public class ExistsResponse : BaseResponse, IExistsResponse
	{
		//TODO I think .Exists should proxy IsValid or be removed completely
		internal ExistsResponse(IApiCallDetails connectionStatus)
		{
			this.Exists = connectionStatus.Success & connectionStatus.HttpStatusCode == 200;
		}
		public ExistsResponse()
		{
			this.Exists = false;
		}

		public bool Exists { get; internal set; }
	}
}
