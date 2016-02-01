using System;
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

		[JsonIgnore]
		ServerError ServerError { get; }

		[JsonIgnore]
		Exception OriginalException { get; }

	}

	public abstract class ResponseBase : IResponse
	{
		public virtual bool IsValid => (this.ApiCall?.Success ?? false) && (this.ServerError == null);

		IApiCallDetails IBodyWithApiCallDetails.CallDetails { get; set; }

		public virtual IApiCallDetails ApiCall => ((IBodyWithApiCallDetails)this).CallDetails;
		
		public virtual ServerError ServerError  => this.ApiCall?.ServerError;

		public Exception OriginalException  => this.ApiCall?.OriginalException;
	}
}
