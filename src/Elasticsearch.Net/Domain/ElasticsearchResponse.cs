using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
using Elasticsearch.Net.Serialization;


namespace Elasticsearch.Net
{
	//TODO document and possibly rename some of the properties

	public interface IElasticsearchResponse
	{
		bool Success { get; }
		bool SuccessOrKnownError { get; }
		IConnectionConfigurationValues Settings { get; }
		Exception OriginalException { get; }
		string RequestMethod { get; }
		string RequestUrl { get; }
		[DebuggerDisplay("{Request != null ? System.Text.Encoding.UTF8.GetString(Request) : null,nq}")]
		byte[] Request { get; }
		int? HttpStatusCode { get; }
		int NumberOfRetries { get; }

		/// <summary>
		/// The raw byte response, only set when IncludeRawResponse() is set on Connection configuration
		/// </summary>
		[DebuggerDisplay("{ResponseRaw != null ? System.Text.Encoding.UTF8.GetString(ResponseRaw) : null,nq}")]
		byte[] ResponseRaw { get; }
	}

	public static class ElasticsearchResponse
	{
		internal static Task<ElasticsearchResponse<DynamicDictionary>> WrapAsync(Task<ElasticsearchResponse<Dictionary<string, object>>> responseTask)
		{
			return responseTask
				.ContinueWith(t =>
				{
					if (t.IsFaulted)
						throw t.Exception.Flatten().InnerException;

					return ToDynamicResponse(t.Result);
				});
		}

		internal static ElasticsearchResponse<DynamicDictionary> Wrap(ElasticsearchResponse<Dictionary<string, object>> response)
		{
			return ToDynamicResponse(response);
		}

		public static ElasticsearchResponse<TTo> CloneFrom<TTo>(IElasticsearchResponse from, TTo to)
		{
			return new ElasticsearchResponse<TTo>(from.Settings)
			{
				OriginalException = from.OriginalException,
				HttpStatusCode = from.HttpStatusCode,
				Request = from.Request,
				RequestMethod = from.RequestMethod,
				RequestUrl = from.RequestUrl,
				Response = to,
				ResponseRaw = from.ResponseRaw,
				Serializer = from.Settings.Serializer,
				Settings = from.Settings,
				Success = from.Success
				
			};
		}

		private static ElasticsearchResponse<DynamicDictionary> ToDynamicResponse(ElasticsearchResponse<Dictionary<string, object>> response)
		{
			return CloneFrom(response, response.Response != null ? DynamicDictionary.Create(response.Response) : null);
		}
	}


	public class ElasticsearchResponse<T> : IElasticsearchResponse
	{
		protected static readonly string _printFormat;
		protected static readonly string _errorFormat;

		public bool Success { get; protected internal set; }

		public Exception OriginalException { get; protected internal set; }

		public string RequestMethod { get; protected internal set; }

		public string RequestUrl { get; protected internal set; }

		public IConnectionConfigurationValues Settings { get; protected internal set; }

		public T Response { get; protected internal set; }

		public byte[] Request { get; protected internal set; }

		public int NumberOfRetries { get; protected internal set; }

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
		public bool SuccessOrKnownError
		{
			get
			{
				return this.Success ||
					(this.HttpStatusCode.HasValue
					&& this.HttpStatusCode.Value != 503 //service unavailable needs to be retried
					&& this.HttpStatusCode.Value != 502 //bad gateway needs to be retried 
					&& ((this.HttpStatusCode.Value >= 400 && this.HttpStatusCode.Value < 599)));
			}
		}

		protected internal ElasticsearchResponse(IConnectionConfigurationValues settings)
		{
			this.Settings = settings;
			this.Serializer = settings.Serializer;
		}

		private ElasticsearchResponse(IConnectionConfigurationValues settings, Exception e)
			: this(settings)
		{
			this.Success = false;
			this.OriginalException = e;
		}

		private ElasticsearchResponse(IConnectionConfigurationValues settings, int statusCode)
			: this(settings)
		{
			this.Success = statusCode >= 200 && statusCode < 300;
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

		public static ElasticsearchResponse<T> Create(IConnectionConfigurationValues settings, int statusCode, string method, string path, byte[] request, T response)
		{
			var cs = new ElasticsearchResponse<T>(settings, statusCode);
			cs.Request = request;
			cs.RequestUrl = path;
			cs.RequestMethod = method;
			cs.Response = response;
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
			var e = r.OriginalException;
			string response = "<Response stream not captured or already read to completion by serializer>";
			if (typeof(T) == typeof(string))
				response = this.Response as string;
			else if (this.Settings.KeepRawResponse)
				response = this.ResponseRaw.Utf8String();
			else if (typeof(T) == typeof(byte[]))
				response = (this.Response as byte[]).Utf8String();

			string requestJson = null;
		    
            if (r.Request != null)
            {
                requestJson = r.Request.Utf8String();
            }
				
			var print = _printFormat.F(
			  Environment.NewLine,
			  r.HttpStatusCode.HasValue ? r.HttpStatusCode.Value.ToString(CultureInfo.InvariantCulture) : "-1",
			  r.RequestMethod,
			  r.RequestUrl,
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
