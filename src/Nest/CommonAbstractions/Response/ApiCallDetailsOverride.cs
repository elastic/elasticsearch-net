using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	internal class ApiCallDetailsOverride : IApiCallDetails
	{
		private readonly IApiCallDetails _original;
		private readonly ServerError _error;
		private readonly bool? _isValid;

		public bool Success => _isValid ?? this._original.Success;
		public ServerError ServerError => this._error ?? this._original.ServerError;

		public Exception OriginalException => this._original.OriginalException;
		public HttpMethod HttpMethod => this._original.HttpMethod;
		public Uri Uri => this._original.Uri;
		public int? HttpStatusCode => this._original.HttpStatusCode;
		public byte[] ResponseBodyInBytes => this._original.ResponseBodyInBytes;
		public byte[] RequestBodyInBytes => this._original.RequestBodyInBytes;
		public List<Audit> AuditTrail => this._original.AuditTrail;
		public string DebugInformation => this._original.DebugInformation;

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
	}
}
