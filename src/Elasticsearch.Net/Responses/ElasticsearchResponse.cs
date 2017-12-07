using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A response from elasticsearch including details about the request/response life cycle
	/// </summary>
	public class ElasticsearchResponseBase : IApiCallDetails, IElasticsearchResponse
	{
		public IApiCallDetails ApiCall { get; set; }
		//ignored
		List<Audit> IApiCallDetails.AuditTrail { get; set; }

		public bool Success => this.ApiCall.Success;
		public string ResponseMimeType => this.ApiCall.ResponseMimeType;
		public HttpMethod HttpMethod => this.ApiCall.HttpMethod;
		public Uri Uri => this.ApiCall.Uri;
		public int? HttpStatusCode => this.ApiCall.HttpStatusCode;
		public List<Audit> AuditTrail => this.ApiCall.AuditTrail;
		public IEnumerable<string> DeprecationWarnings => this.ApiCall.DeprecationWarnings;
		public bool SuccessOrKnownError => this.ApiCall.SuccessOrKnownError;
		public Exception OriginalException => this.ApiCall.OriginalException;

		/// <summary>The raw byte request message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] RequestBodyInBytes => this.ApiCall.RequestBodyInBytes;
		/// <summary>The raw byte response message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] ResponseBodyInBytes => this.ApiCall.ResponseBodyInBytes;

		public string DebugInformation => this.ApiCall.DebugInformation;

		public override string ToString() => this.ApiCall.ToString();

	}

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
	public class ElasticsearchResponse<T> : ElasticsearchResponseBase
	{
		public T Body { get; protected internal set; }
	}
}
