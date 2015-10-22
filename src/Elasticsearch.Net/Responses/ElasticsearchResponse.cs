using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Serialization;

namespace Elasticsearch.Net
{
	public class ElasticsearchResponse<T> : IApiCallDetails
	{
		private static readonly string _printFormat = "StatusCode: {1}, {0}\tMethod: {2}, {0}\tUrl: {3}, {0}\tRequest: {4}, {0}\tResponse: {5}";
		private static readonly string _errorFormat = "{0}\tExceptionMessage: {1}{0}\t StackTrace: {2}";

		public bool Success { get; internal set; } = true;

		public HttpMethod HttpMethod { get; internal set; }

		public Uri Uri { get; internal set; }

		/// <summary>The raw byte request message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] RequestBodyInBytes { get; internal set; }

		/// <summary>The raw byte response message body, only set when DisableDirectStreaming() is set on Connection configuration</summary>
		public byte[] ResponseBodyInBytes { get; internal set; }

		public T Body { get; protected internal set; }

		public int? HttpStatusCode { get; internal set; }

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

		/// <summary>
		/// This property returns the mapped elasticsearch server exception
		/// </summary>
		public ElasticsearchServerError ServerError
		{
			get
			{
				var esException = this.OriginalException as ElasticsearchServerException;
				if (esException == null) return null;
				return new ElasticsearchServerError
				{
					Error = esException.Message,
					ExceptionType = esException.ExceptionType,
					Status = esException.Status
				};
			}
		}

		public ElasticsearchResponse(Exception e)
		{
			this.Success = false;
			this.OriginalException = e;
		}

		public ElasticsearchResponse(int statusCode)
		{
			this.Success = statusCode >= 200 && statusCode < 300;
			this.HttpStatusCode = statusCode;
		}

		public override string ToString()
		{
			var r = this;
			var e = r.OriginalException;
			string response = "<Response stream not captured or already read to completion by serializer, set ExposeRawResponse() on connectionsettings to force it to be set on>";
			if (this.ResponseBodyInBytes != null)
				response = this.ResponseBodyInBytes.Utf8String();

			string requestJson = null;
			if (r.RequestBodyInBytes != null)
				requestJson = r.RequestBodyInBytes.Utf8String();

			var print = _printFormat.F(
				Environment.NewLine,
				r.HttpStatusCode.HasValue ? r.HttpStatusCode.Value.ToString(CultureInfo.InvariantCulture) : "-1",
				r.HttpMethod,
				r.Uri,
				requestJson,
				response
			);
			if (!this.Success && e != null)
			{
				print += _errorFormat.F(Environment.NewLine, e.Message, e.StackTrace);
			}
			return print;
		}

	}
}
