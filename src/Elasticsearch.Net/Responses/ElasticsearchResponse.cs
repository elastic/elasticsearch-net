using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	public class ElasticsearchResponse<T> : IApiCallDetails
	{
		public ElasticsearchResponse(Exception e)
		{
			Success = false;
			OriginalException = e;
		}

		public ElasticsearchResponse(int statusCode, IEnumerable<int> allowedStatusCodes)
		{
			var statusCodes = allowedStatusCodes as int[] ?? allowedStatusCodes.ToArray();
			AllowAllStatusCodes = statusCodes.Contains(-1);
			Success = statusCode >= 200 && statusCode < 300 || statusCodes.Contains(statusCode) || AllowAllStatusCodes;
			HttpStatusCode = statusCode;
		}

		public List<Audit> AuditTrail { get; internal set; }

		public T Body { get; protected internal set; }

		public string DebugInformation
		{
			get
			{
				var sb = new StringBuilder();
				sb.AppendLine(ToString());
				return ResponseStatics.DebugInformationBuilder(this, sb);
			}
		}

		public IEnumerable<string> DeprecationWarnings { get; internal set; } = Enumerable.Empty<string>();

		public HttpMethod HttpMethod { get; internal set; }

		public int? HttpStatusCode { get; }

		public Exception OriginalException { get; protected internal set; }

		/// <summary>The raw byte request message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] RequestBodyInBytes { get; internal set; }

		/// <summary>The raw byte response message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] ResponseBodyInBytes { get; internal set; }

		public string ResponseMimeType { get; internal set; }

		public ServerError ServerError { get; internal set; }
		public bool Success { get; }

		/// <summary>
		/// The response is successful or has a response code between 400-599, the call should not be retried.
		/// Only on 502,503 and 504 will this return false;
		/// </summary>
		public bool SuccessOrKnownError =>
			Success || HttpStatusCode >= 400 && HttpStatusCode < 599
			&& HttpStatusCode != 504 //Gateway timeout needs to be retried
			&& HttpStatusCode != 503 //service unavailable needs to be retried
			&& HttpStatusCode != 502;

		public Uri Uri { get; internal set; }

		internal bool AllowAllStatusCodes { get; }


		public override string ToString() =>
			$"{(Success ? "S" : "Uns")}uccessful low level call on {HttpMethod.GetStringValue()}: {Uri.PathAndQuery}";
	}
}
