using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Elasticsearch.Net
{
	internal static class ResponseStatics
	{
		public static readonly string PrintFormat = "StatusCode: {1}, {0}\tMethod: {2}, {0}\tUrl: {3}, {0}\tRequest: {4}, {0}\tResponse: {5}";
		public static readonly string ErrorFormat = "{0}\tExceptionMessage: {1}{0}\t StackTrace: {2}";
		public static readonly string AlreadyCaptured = "<Response stream not captured or already read to completion by serializer, set ExposeRawResponse() on connectionsettings to force it to be set on>";
	}

	public class ElasticsearchResponse<T> : IApiCallDetails
	{
		public bool Success { get; }

		public HttpMethod HttpMethod { get; internal set; }

		public Uri Uri { get; internal set; }

		/// <summary>The raw byte request message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] RequestBodyInBytes { get; internal set; }

		/// <summary>The raw byte response message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] ResponseBodyInBytes { get; internal set; }

		public T Body { get; protected internal set; }

		public int? HttpStatusCode { get; }

		public List<Audit> AuditTrail { get; internal set; }

		/// <summary>
		/// The response is succesful or has a response code between 400-509 the call should not be retried.
		/// Only on 502 and 503 will this return false;
		/// </summary>
		public bool SuccessOrKnownError =>
			this.Success || (HttpStatusCode >= 400 && HttpStatusCode < 599
				&& HttpStatusCode != 503 //service unavailable needs to be retried
				&& HttpStatusCode != 502 //bad gateway needs to be retried 
			);

		public Exception OriginalException { get; protected internal set; }

		public ServerError ServerError { get; internal set; }

		public ElasticsearchResponse(Exception e)
		{
			this.Success = false;
			this.OriginalException = e;
		}

		public ElasticsearchResponse(int statusCode, IEnumerable<int> allowedStatusCodes)
		{
			this.Success = statusCode >= 200 && statusCode < 300 || allowedStatusCodes.Contains(statusCode);
			this.HttpStatusCode = statusCode;
		}

		public override string ToString()
		{
			var r = this;
			var e = r.OriginalException;
			var response = this.ResponseBodyInBytes?.Utf8String() ?? ResponseStatics.AlreadyCaptured;

			var requestJson = r.RequestBodyInBytes?.Utf8String();

			var print = string.Format(ResponseStatics.PrintFormat,
				Environment.NewLine,
				r.HttpStatusCode.HasValue ? r.HttpStatusCode.Value.ToString(CultureInfo.InvariantCulture) : "-1",
				r.HttpMethod,
				r.Uri,
				requestJson,
				response
			);
			if (!this.Success && e != null)
				print += string.Format(ResponseStatics.ErrorFormat,Environment.NewLine, e.Message, e.StackTrace);
			return print;
		}
	}
}
