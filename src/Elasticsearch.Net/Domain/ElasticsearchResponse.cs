using System;
using System.Collections.Generic;
using System.Configuration;
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
	public interface IElasticsearchResponse
	{
		bool Success { get; }
		IConnectionConfigurationValues Settings { get; }
		ConnectionError Error { get; }
		string RequestMethod { get; }
		string RequestUrl { get; }
		byte[] Request { get; }
		int? HttpStatusCode { get; }
		
		/// <summary>
		/// The raw byte response, only set when IncludeRawResponse() is set on Connection configuration
		/// </summary>
		byte[] ResponseRaw { get; }
	}

	public class ElasticsearchResponse<T> : IElasticsearchResponse
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
		
		/// <summary>
		/// The raw byte response, only set when IncludeRawResponse() is set on Connection configuration
		/// </summary>
		public byte[] ResponseRaw { get; protected internal set; }

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
		private ElasticsearchResponse(IConnectionConfigurationValues settings, Exception e) : this(settings)
		{
			this.Success = false;
			this.Error = new ConnectionError(e);
			if (this.Error.HttpStatusCode != null)
				this.HttpStatusCode = (int) this.Error.HttpStatusCode;
		}
		private ElasticsearchResponse(IConnectionConfigurationValues settings, int statusCode) : this(settings)
		{
			this.Success = statusCode >= 200 && statusCode < 300;
			if (!this.Success)
			{
				var exception = new ConnectionException(statusCode);
				this.Error = new ConnectionError(exception);
			}
			this.HttpStatusCode = statusCode;
		}
		public static ElasticsearchResponse<T> CreateError(IConnectionConfigurationValues settings, Exception e, string method, string path, byte[] request)
		{
			var cs = new ElasticsearchResponse<T>(settings, e);
			cs.Request = request;
			cs.RequestUrl = path;
			cs.RequestMethod = method;
			return cs;
		}
		public static ElasticsearchResponse<T> Create(IConnectionConfigurationValues settings, int statusCode, string method, string path, byte[] request)
		{
			var cs = new ElasticsearchResponse<T>(settings, statusCode);
			cs.Request = request;
			cs.RequestUrl = path;
			cs.RequestMethod = method;
			return cs;
		}

		public static ElasticsearchResponse<T> Create(
			IConnectionConfigurationValues settings, int statusCode, string method, string path, byte[] request, Stream stream, object deserializeState = null)
		{
			var cs = new ElasticsearchResponse<T>(settings, statusCode);
			cs.Request = request;
			cs.RequestUrl = path;
			cs.RequestMethod = method;
			var s = stream;
			if (settings.KeepRawResponse) 
			{
				using (var ms = new MemoryStream())
				{
					stream.CopyTo(ms);
					cs.ResponseRaw = ms.ToArray();
					s = ms;
				}
			}
			var customConverter = deserializeState as Func<IElasticsearchResponse, Stream, T>;
			if (customConverter != null)
			{
				var t = customConverter(cs, stream);
				cs.Response = t;
			}
			else cs.Response = settings.Serializer.Deserialize<T>(cs, s, deserializeState);


			

			return cs;
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
