using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A response from elasticsearch including details about the request/response life cycle
	/// </summary>
	/// <typeparam name="T">
	/// <para> - T, an object you own that the elasticsearch response will be deserialized to </para>
	/// <para> - byte[], no deserialization, but the response stream will be closed </para>
	/// <para> - Stream, no deserialization, response stream is your responsibility </para>
	/// <para> - VoidResponse, no deserialization, response stream never read and closed </para>
	/// <para> - DynamicDictionary, a dynamic aware dictionary that can be safely traversed to any depth </para>
	/// </typeparam>
	public class ElasticsearchResponse<T> : IApiCallDetails, IElasticsearchResponse
	{
		public IApiCallDetails ApiCall { get; set; }

		public bool Success { get; }

		public HttpMethod HttpMethod { get; internal set; }

		public Uri Uri { get; internal set; }

		/// <summary>The raw byte request message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] RequestBodyInBytes { get; internal set; }

		/// <summary>The raw byte response message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] ResponseBodyInBytes { get; internal set; }

		public T Body { get; protected internal set; }

		public int? HttpStatusCode { get; }

		public List<Audit> AuditTrail { get; set; }

		public IEnumerable<string> DeprecationWarnings { get; internal set; } = Enumerable.Empty<string>();

		public bool SuccessOrKnownError =>
			this.Success || (HttpStatusCode >= 400 && HttpStatusCode < 599
			                 && HttpStatusCode != 504 //Gateway timeout needs to be retried
			                 && HttpStatusCode != 503 //service unavailable needs to be retried
			                 && HttpStatusCode != 502 //bad gateway needs to be retried
			);

		public Exception OriginalException { get; protected internal set; }

		public ServerError ServerError { get; internal set; }

		public ElasticsearchResponse()
		{
		}

//		//TODO REMOVE
//		public  ElasticsearchResponse(Exception e)
//		{
//			this.Success = false;
//			this.OriginalException = e;
//		}
//
//		//TODO REMOVE
//		public ElasticsearchResponse(int statusCode, bool success)
//		{
//			this.Success = success;
//			this.HttpStatusCode = statusCode;
//		}

		public string DebugInformation
		{
			get
			{
				var sb = new StringBuilder();
				sb.AppendLine(this.ToString());
				return ResponseStatics.DebugInformationBuilder(this, sb);
			}
		}

		public override string ToString() =>
			$"{(Success ? "S" : "Uns")}uccessful low level call on {HttpMethod.GetStringValue()}: {Uri.PathAndQuery}";

	}
}
