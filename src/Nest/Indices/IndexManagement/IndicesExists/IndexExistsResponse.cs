using Elasticsearch.Net_5_2_0;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IExistsResponse : IResponse
	{
		bool Exists { get; }
	}

	[JsonObject]
	public class ExistsResponse : ResponseBase, IExistsResponse
	{
		internal ExistsResponse(IApiCallDetails apiCallDetails)
		{
			this.Exists = apiCallDetails.Success & apiCallDetails.HttpStatusCode == 200;
		}
		public ExistsResponse()
		{
			this.Exists = false;
		}

		public bool Exists { get; internal set; }
	}
}
