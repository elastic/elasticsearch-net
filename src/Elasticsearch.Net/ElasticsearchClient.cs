using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Serialization;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Low level client that exposes all of elasticsearch API endpoints but leaves you in charge of building request and handling the response
	/// </summary>
	public partial class ElasticsearchClient : IElasticsearchClient
	{
		private UrlFormatProvider _formatter;

		public IConnectionConfigurationValues Settings { get { return this.Transport.Settings; } }
		public IElasticsearchSerializer Serializer { get { return this.Transport.Settings.Serializer; } }

		protected ITransport<IConnectionConfigurationValues> Transport { get; set; }

		/// <summary>Instantiate a new low level elasticsearch client to http://localhost:9200</summary>
		public ElasticsearchClient() : this(new Transport<IConnectionConfigurationValues>(new ConnectionConfiguration())) { }

		/// <summary>Instantiate a new low level elasticsearch client using the specified settings</summary>
		public ElasticsearchClient(IConnectionConfigurationValues settings) : this(new Transport<IConnectionConfigurationValues>(settings ?? new ConnectionConfiguration())) { }

		/// <summary>
		/// Instantiate a new low level elasticsearch client explicitly specifying a custom transport setup
		/// </summary>
		public ElasticsearchClient(ITransport<IConnectionConfigurationValues> transport)
		{
			transport.ThrowIfNull(nameof(transport));
			transport.Settings.ThrowIfNull(nameof(transport.Settings));
			transport.Settings.Serializer.ThrowIfNull(nameof(transport.Settings.Serializer));

			this.Transport = transport;
			this._formatter = new UrlFormatProvider(this.Transport.Settings);
		}

		string Url(FormattableString formattable)
		{
			return formattable.ToString(_formatter);
		}

		private TRequestParams CreateRequestParams<TRequestParams>(Func<TRequestParams, TRequestParams> requestParameters, bool allow404, string contentType)
			where TRequestParams : class, IRequestParameters, new()
		{
			var requestParams = requestParameters?.Invoke(new TRequestParams());
			if (!allow404 && contentType.IsNullOrEmpty()) return requestParams;

			requestParams = requestParams ?? new TRequestParams();
			if (requestParams.RequestConfiguration == null) requestParams.RequestConfiguration = new RequestConfiguration();
			if (allow404)
				requestParams.RequestConfiguration.AllowedStatusCodes = new[] { 404 };
			if (!contentType.IsNullOrEmpty())
				requestParams.RequestConfiguration.ContentType = contentType;
			return requestParams;
		}

		//TODO these are just proxy methods, remove??

		/// <summary>
		/// Perform any request you want over the configured IConnection synchronously while taking advantage of the cluster failover.
		/// </summary>
		/// <typeparam name="T">The type representing the response JSON</typeparam>
		/// <param name="method">the HTTP Method to use</param>
		/// <param name="path">The path of the the url that you would like to hit</param>
		/// <param name="data">The body of the request, string and byte[] are posted as is other types will be serialized to JSON</param>
		/// <param name="requestParameters">Optionally configure request specific timeouts, headers</param>
		/// <returns>An ElasticsearchResponse of T where T represents the JSON response body</returns>
		public ElasticsearchResponse<T> DoRequest<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
		{
			return this.Transport.DoRequest<T>(method, path, data, requestParameters);
		}

		public ElasticsearchResponse<T> DoRequest<T, TRequestParams>(
			HttpMethod method,
			string path,
			Func<TRequestParams, TRequestParams> requestParameters = null,
			PostData<object> data = null,
			bool allow404 = false,
			string contentType = null
			)
			where TRequestParams : class, IRequestParameters, new()
		{
			return this.Transport.DoRequest<T>(method, path, data, CreateRequestParams(requestParameters, allow404, contentType));
		}

		/// <summary>
		/// Perform any request you want over the configured IConnection asynchronously while taking advantage of the cluster failover.
		/// </summary>
		/// <typeparam name="T">The type representing the response JSON</typeparam>
		/// <param name="method">the HTTP Method to use</param>
		/// <param name="path">The path of the the url that you would like to hit</param>
		/// <param name="data">The body of the request, string and byte[] are posted as is other types will be serialized to JSON</param>
		/// <param name="requestParameters">Optionally configure request specific timeouts, headers</param>
		/// <returns>A task of ElasticsearchResponse of T where T represents the JSON response body</returns>
		public Task<ElasticsearchResponse<T>> DoRequestAsync<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
		{
			return this.Transport.DoRequestAsync<T>(method, path, data, requestParameters);
		}

		public Task<ElasticsearchResponse<T>> DoRequestAsync<T, TRequestParams>(
			HttpMethod method,
			string path,
			Func<TRequestParams, TRequestParams> requestParameters = null,
			PostData<object> data = null,
			bool allow404 = false,
			string contentType = null
			)
			where TRequestParams : class, IRequestParameters, new()
		{
			return this.Transport.DoRequestAsync<T>(method, path, data, CreateRequestParams(requestParameters, allow404, contentType));
		}

	}
}
