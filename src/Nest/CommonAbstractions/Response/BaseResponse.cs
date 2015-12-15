using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IResponse : IBodyWithApiCallDetails
	{
		[JsonIgnore]
		bool IsValid { get; }

		[JsonIgnore]
		IApiCallDetails ApiCall { get; }
	}

	public class BaseResponse : IResponse
	{
		public virtual bool IsValid => this.ApiCall?.Success ?? false;

		IApiCallDetails IBodyWithApiCallDetails.CallDetails { get; set; }
		public IApiCallDetails ApiCall => ((IBodyWithApiCallDetails)this).CallDetails;
	}
}
