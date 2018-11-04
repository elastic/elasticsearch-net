using System;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	internal class ApiCallDetailsOverride : IApiCallDetails
	{
		private readonly ServerError _error;
		private readonly bool? _isValid;
		private readonly IApiCallDetails _original;

		public ApiCallDetailsOverride(IApiCallDetails original, bool isValid)
		{
			_original = original;
			_isValid = isValid;
		}

		public ApiCallDetailsOverride(IApiCallDetails original, ServerError error)
		{
			_original = original;
			_error = error;
			_isValid = false;
		}

		public List<Audit> AuditTrail => _original.AuditTrail;
		public string DebugInformation => _original.DebugInformation;
		public IEnumerable<string> DeprecationWarnings => _original.DeprecationWarnings;
		public HttpMethod HttpMethod => _original.HttpMethod;
		public int? HttpStatusCode => _original.HttpStatusCode;

		public Exception OriginalException => _original.OriginalException;
		public byte[] RequestBodyInBytes => _original.RequestBodyInBytes;
		public byte[] ResponseBodyInBytes => _original.ResponseBodyInBytes;
		public ServerError ServerError => _error ?? _original.ServerError;

		public bool Success => _isValid ?? _original.Success;
		public Uri Uri => _original.Uri;
	}
}
