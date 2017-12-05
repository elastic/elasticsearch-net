using System;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	internal class HttpDetailsProxy : IApiCallDetails
	{
		private readonly IApiCallDetails _original;

		public bool Success => this._original.Success;
		public bool SuccessOrKnownError => this._original.SuccessOrKnownError;

		public Exception OriginalException => this._original.OriginalException;
		public HttpMethod HttpMethod => this._original.HttpMethod;
		public Uri Uri => this._original.Uri;
		public int? HttpStatusCode => this._original.HttpStatusCode;
		public byte[] ResponseBodyInBytes => this._original.ResponseBodyInBytes;
		public byte[] RequestBodyInBytes => this._original.RequestBodyInBytes;

		public List<Audit> AuditTrail
		{
			get => this._original.AuditTrail;
			set { }
		}

		public string DebugInformation => this._original.DebugInformation;
		public IEnumerable<string> DeprecationWarnings => this._original.DeprecationWarnings;

		public HttpDetailsProxy(IApiCallDetails original)
		{
			_original = original;
		}
	}
}
