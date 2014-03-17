using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;


namespace Elasticsearch.Net
{
	public class ElasticsearchResponse : ElasticsearchResponse<byte[]>
	{
		private string _result;
		
		public string Result
		{
			get { return _result ?? (_result = this.Response.Utf8String()); }
		}
		
		private static readonly byte _startAccolade = (byte)'{';
		private ElasticsearchDynamic _response;
		public ElasticsearchDynamic DynamicResult
		{
			get
			{
				if (Response == null || Response.Length == 0)
					return null;
				if (Response[0] != _startAccolade)
					return null;

				if (_response == null)
					this._response = ElasticsearchDynamic.Create(this.Deserialize<IDictionary<string, object>>());
				return this._response;
			}
		}
		protected ElasticsearchResponse(IConnectionConfigurationValues settings) : base(settings)
		{
			this.Settings = settings;
			this.Serializer = settings.Serializer; 
		}

		private ElasticsearchResponse(IConnectionConfigurationValues settings, Exception e) : this(settings)
		{
			this.Success = false;
			this.Error = new ConnectionError(e);
			if (this.Error.ResponseReadFromWebException != null)
				this.Response = this.Error.ResponseReadFromWebException;
			if (this.Error.HttpStatusCode != null)
				this.HttpStatusCode = (int) this.Error.HttpStatusCode;
		}
		private ElasticsearchResponse(IConnectionConfigurationValues settings, int statusCode, byte[] response = null) : this(settings)
		{
			this.Success = statusCode >= 200 && statusCode < 300;
			if (!this.Success)
			{
				var exception = new ConnectionException(statusCode);
				this.Error = new ConnectionError(exception);
			}
			this.Response = response;
			this.HttpStatusCode = statusCode;
		}
	
		public static ElasticsearchResponse CreateError(IConnectionConfigurationValues settings, Exception e, string method, string path, byte[] request)
		{
			var cs = new ElasticsearchResponse(settings, e);
			cs.Request = request;
			cs.RequestUrl = path;
			cs.RequestMethod = method;
			return cs;
		}
		public static ElasticsearchResponse Create(IConnectionConfigurationValues settings, int statusCode, string method, string path, byte[] request, byte[] response)
		{
			var cs = new ElasticsearchResponse(settings, statusCode, response);
			cs.Request = request;
			cs.RequestUrl = path;
			cs.RequestMethod = method;
			return cs;
		}
		
		/// <summary>
		/// Returns a response of type R based on the connection status by trying parsing status.Result into R
		/// </summary>
		/// <returns></returns>
		public virtual T Deserialize<T>(bool allow404 = false) where T : class
		{
			return this.Serializer.Deserialize<T>(this.Response);
		}

		public override string ToString()
		{
			var r = this;
			var e = r.Error;
			var print = _printFormat.F(
			  Environment.NewLine,
			  r.HttpStatusCode.HasValue ? r.HttpStatusCode.Value.ToString(CultureInfo.InvariantCulture) : "-1",
			  r.RequestMethod,
			  r.RequestUrl,
			  r.Request,
			  r.Result
			);
			if (!this.Success)
			{
				print += _errorFormat.F(Environment.NewLine, e.ExceptionMessage, e.OriginalException.StackTrace);
			}
			return print;
		}
	}

	public class ElasticsearchResponse<T>
	{
		protected static readonly string _printFormat;
		protected static readonly string _errorFormat;
		
		public bool Success { get; protected internal set; }
		public ConnectionError Error { get; protected internal set; }

		public string RequestMethod { get; protected internal set; }
		public string RequestUrl { get; protected internal set; }
		public IConnectionConfigurationValues Settings { get; protected internal set; }

		public T Response { get; protected internal set; }
		
		public byte[] Request { get; protected internal set; }

		public int? HttpStatusCode { get; protected internal set; }

		public IElasticsearchSerializer Serializer { get; protected internal set; }

		/// <summary>
		/// If the response is succesful or has a known error (400-500 range)
		/// The client should not retry this call
		/// </summary>
		internal bool SuccessOrKnownError
		{
			get
			{
				return this.Success ||
					(this.HttpStatusCode.HasValue 
					&& this.HttpStatusCode.Value != 503 && (this.HttpStatusCode.Value >= 400 && this.HttpStatusCode.Value < 599));
			}
		}


		protected ElasticsearchResponse(IConnectionConfigurationValues settings)
		{
			this.Settings = settings;
			this.Serializer = settings.Serializer; 
		}


		static ElasticsearchResponse()
		{
			_printFormat = "StatusCode: {1}, {0}\tMethod: {2}, {0}\tUrl: {3}, {0}\tRequest: {4}, {0}\tResponse: {5}";
			_errorFormat = "{0}\tExceptionMessage: {1}{0}\t StackTrace: {2}";
		}

		public override string ToString()
		{
			var r = this;
			var e = r.Error;
			var print = _printFormat.F(
			  Environment.NewLine,
			  r.HttpStatusCode.HasValue ? r.HttpStatusCode.Value.ToString(CultureInfo.InvariantCulture) : "-1",
			  r.RequestMethod,
			  r.RequestUrl,
			  r.Request,
			  "RESPONSE STREAM ALREADY READ BY SERIALIZER"
			);
			if (!this.Success)
			{
				print += _errorFormat.F(Environment.NewLine, e.ExceptionMessage, e.OriginalException.StackTrace);
			}
			return print;
		}

	}
}
