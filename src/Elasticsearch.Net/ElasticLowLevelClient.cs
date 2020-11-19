// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;
using Elasticsearch.Net.Extensions;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Low level client that exposes all of Elasticsearch API endpoints but leaves you in charge of building request and handling the response
	/// </summary>
	// ReSharper disable once RedundantExtendsListEntry
	public partial class ElasticLowLevelClient : IElasticLowLevelClient
	{
		/// <summary>Instantiate a new low level Elasticsearch client to http://localhost:9200</summary>
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public ElasticLowLevelClient() : this(new Transport<IConnectionConfigurationValues>(new ConnectionConfiguration())) { }

		/// <summary>Instantiate a new low level Elasticsearch client using the specified settings</summary>
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public ElasticLowLevelClient(IConnectionConfigurationValues settings) : this(
			new Transport<IConnectionConfigurationValues>(settings ?? new ConnectionConfiguration())) { }

		/// <summary>
		/// Sets up the client to communicate to Elastic Cloud using <paramref name="cloudId"/>,
		/// <para><see cref="CloudConnectionPool"/> documentation for more information on how to obtain your Cloud Id</para>
		/// <para></para>If you want more control use the <see cref="ElasticLowLevelClient(IConnectionConfigurationValues)"/> constructor and pass an instance of
		/// <see cref="ConnectionConfiguration" /> that takes <paramref name="cloudId"/> in its constructor as well
		/// </summary>
		public ElasticLowLevelClient(string cloudId, IAuthenticationHeader credentials) : this(new ConnectionConfiguration(cloudId, credentials)) { }

		/// <summary>
		/// Instantiate a new low level Elasticsearch client explicitly specifying a custom transport setup
		/// </summary>
		public ElasticLowLevelClient(ITransport<IConnectionConfigurationValues> transport)
		{
			transport.ThrowIfNull(nameof(transport));
			transport.Settings.ThrowIfNull(nameof(transport.Settings));
			transport.Settings.RequestResponseSerializer.ThrowIfNull(nameof(transport.Settings.RequestResponseSerializer));

			Transport = transport;
			UrlFormatter = Transport.Settings.UrlFormatter;
			SetupNamespaces();
		}

		partial void SetupNamespaces();

		public ITransportSerializer Serializer => Transport.Settings.RequestResponseSerializer;

		public IConnectionConfigurationValues Settings => Transport.Settings;

		protected ITransport<IConnectionConfigurationValues> Transport { get; set; }

		private UrlFormatter UrlFormatter { get; }

		public TResponse DoRequest<TResponse>(HttpMethod method, string path, PostData data = null, IRequestParameters requestParameters = null)
			where TResponse : class, ITransportResponse, new() =>
			Transport.Request<TResponse>(method, path, data, requestParameters);

		public Task<TResponse> DoRequestAsync<TResponse>(HttpMethod method, string path, CancellationToken cancellationToken, PostData data = null,
			IRequestParameters requestParameters = null
		)
			where TResponse : class, ITransportResponse, new() =>
			Transport.RequestAsync<TResponse>(method, path, cancellationToken, data, requestParameters);

		protected internal string Url(FormattableString formattable) => formattable.ToString(UrlFormatter);

		protected internal TRequestParams RequestParams<TRequestParams>(TRequestParams requestParams, string contentType = null, string accept = null)
			where TRequestParams : class, IRequestParameters, new()
		{
			if (contentType.IsNullOrEmpty()) return requestParams;

			requestParams ??= new TRequestParams();
			if (requestParams.RequestConfiguration == null) requestParams.RequestConfiguration = new RequestConfiguration();
			if (!contentType.IsNullOrEmpty() && requestParams.RequestConfiguration.ContentType.IsNullOrEmpty())
				requestParams.RequestConfiguration.ContentType = contentType;
			if (!accept.IsNullOrEmpty() && requestParams.RequestConfiguration.Accept.IsNullOrEmpty())
				requestParams.RequestConfiguration.Accept = accept;
			return requestParams;
		}
	}
}
