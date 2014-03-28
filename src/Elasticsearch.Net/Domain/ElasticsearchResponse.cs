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
	public interface IElasticsearchResponse
	{
		bool Success { get; }
		IConnectionConfigurationValues Settings { get; }
		ConnectionError Error { get; }
		string RequestMethod { get; }
		string RequestUrl { get; }
        [DebuggerDisplay("{Request != null ? System.Text.Encoding.UTF8.GetString(Request) : null,nq}")]
		byte[] Request { get; }
		int? HttpStatusCode { get; }
		
		/// <summary>
		/// The raw byte response, only set when IncludeRawResponse() is set on Connection configuration
		/// </summary>
        [DebuggerDisplay("{ResponseRaw != null ? System.Text.Encoding.UTF8.GetString(ResponseRaw) : null,nq}")]
		byte[] ResponseRaw { get; }
	}

	internal static class ElasticsearchResponse
	{

		public static Task<ElasticsearchResponse<DynamicDictionary>> WrapAsync(Task<ElasticsearchResponse<Dictionary<string, object>>> responseTask)
		{
			return responseTask
				.ContinueWith(t=>ToDynamicResponse(t.Result));
		}

		public static ElasticsearchResponse<DynamicDictionary> Wrap(ElasticsearchResponse<Dictionary<string, object>> response)
		{
			return ToDynamicResponse(response);
		}

		private static ElasticsearchResponse<DynamicDictionary> ToDynamicResponse(ElasticsearchResponse<Dictionary<string, object>> response)
		{
			return new ElasticsearchResponse<DynamicDictionary>(response.Settings)
			{
				Error = response.Error,
				HttpStatusCode = response.HttpStatusCode,
				Request = response.Request,
				RequestMethod = response.RequestMethod,
				RequestUrl = response.RequestUrl,
				Response = response.Response != null ? DynamicDictionary.Create(response.Response) : null,
				ResponseRaw = response.ResponseRaw,
				Serializer = response.Serializer,
				Settings = response.Settings,
				Success = response.Success
			};
		}
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
		private ElasticsearchResponse(IConnectionConfigurationValues settings, Exception e) : this(settings)
		{
			this.Success = false;
			this.Error = new ConnectionError(e);
			if (this.Error.HttpStatusCode != null)
				this.HttpStatusCode = (int) this.Error.HttpStatusCode;
			this.ResponseRaw = this.Error.ResponseReadFromWebException;
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
			using (var ms = new MemoryStream())
			{
				if (settings.KeepRawResponse)
				{
					stream.CopyTo(ms);
					cs.ResponseRaw = ms.ToArray();
					ms.Position = 0;
					s = ms;
				}
				var customConverter = deserializeState as Func<IElasticsearchResponse, Stream, T>;
				if (customConverter != null)
				{
					var t = customConverter(cs, s);
					cs.Response = t;
				}
				else cs.Response = settings.Serializer.Deserialize<T>(cs, s, deserializeState);

				return cs;
			}
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
			string response = "<Response stream not captured or already read to completion by serializer>";
			if (typeof(T) == typeof(string))
				response = this.Response as string;
			else if (this.Settings.KeepRawResponse)
				response = this.ResponseRaw.Utf8String();
			else if (typeof(T) == typeof(byte[]))
				response = (this.Response as byte[]).Utf8String();

			var print = _printFormat.F(
			  Environment.NewLine,
			  r.HttpStatusCode.HasValue ? r.HttpStatusCode.Value.ToString(CultureInfo.InvariantCulture) : "-1",
			  r.RequestMethod,
			  r.RequestUrl,
			  r.Request,
			  response
			);
			if (!this.Success)
			{
				print += _errorFormat.F(Environment.NewLine, e.ExceptionMessage, e.OriginalException.StackTrace);
			}
			return print;
		}

	}
}
