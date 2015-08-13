using System.Collections.Generic;
using System.Linq;
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
		ElasticsearchServerError ServerError { get; }
	}


	public class BaseResponse : IResponse
	{
		private bool? _forcedIsValid = null;
		public virtual bool IsValid
		{
			get
			{
				return _forcedIsValid ?? this.ApiCall?.Success ?? false;
			}
			internal set { _forcedIsValid = value; }
		}

		IApiCallDetails IBodyWithApiCallDetails.CallDetails { get; set; }
		public IApiCallDetails ApiCall => ((IBodyWithApiCallDetails)this).CallDetails;

		public virtual ElasticsearchServerError ServerError
		{
			get
			{
				if (this.IsValid || this.ApiCall == null || this.ApiCall.OriginalException == null)
					return null;
				var e = this.ApiCall.OriginalException as ElasticsearchServerException;
				if (e == null)
					return null;
				return new ElasticsearchServerError
				{
					Status = e.Status,
					Error = e.Message,
					ExceptionType = e.ExceptionType
				};
			}
		}

	}
}
