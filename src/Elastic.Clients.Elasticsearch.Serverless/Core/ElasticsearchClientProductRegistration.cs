// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;

using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch;

namespace Elastic.Clients.Elasticsearch.Serverless;

internal sealed class ElasticsearchClientProductRegistration : ElasticsearchProductRegistration
{
	private readonly MetaHeaderProvider _metaHeaderProvider;

	public ElasticsearchClientProductRegistration(Type markerType) : base(markerType)
	{
		var identifier = ServiceIdentifier;
		if (!string.IsNullOrEmpty(identifier))
			_metaHeaderProvider = new ServerlessMetaHeaderProvider(markerType, identifier);
	}

	public static ElasticsearchClientProductRegistration DefaultForElasticsearchClientsElasticsearch { get; } = new(typeof(ElasticsearchClient));

	public override string ServiceIdentifier => "esv";

	public override string DefaultMimeType => null; // Prevent base 'ElasticsearchProductRegistration' from sending the compatibility header

	public override MetaHeaderProvider MetaHeaderProvider => _metaHeaderProvider;

	/// <summary>
	///     Elastic.Clients.Elasticsearch handles 404 in its <see cref="ElasticsearchResponse.IsValidResponse" />, we do not want the low level client throwing
	///     exceptions
	///     when <see cref="ITransportConfiguration.ThrowExceptions" /> is enabled for 404's. The client is in charge of
	///     composing paths
	///     so a 404 never signals a wrong URL but a missing entity.
	/// </summary>
	public override bool HttpStatusCodeClassifier(HttpMethod method, int statusCode) =>
		statusCode is >= 200 and < 300 or 404;

	/// <summary>
	///     Makes the low level transport aware of Elastic.Clients.Elasticsearch's <see cref="ElasticsearchResponse" />
	///     so that it can peek in to its exposed error when reporting failures.
	/// </summary>
	public override bool TryGetServerErrorReason<TResponse>(TResponse response, [NotNullWhen(returnValue: true)] out string? reason)
	{
		if (response is not ElasticsearchResponse r)
			return base.TryGetServerErrorReason(response, out reason);
		reason = r.ElasticsearchServerError?.Error?.ToString();
		return !string.IsNullOrEmpty(reason);
	}
}

public sealed class ServerlessMetaHeaderProvider : MetaHeaderProvider
{
	private readonly MetaHeaderProducer[] _producers;

	/// <inheritdoc cref="MetaHeaderProvider.Producers"/>
	public override MetaHeaderProducer[] Producers => _producers;

	public ServerlessMetaHeaderProvider(Type clientType, string serviceIdentifier)
	{
		var version = ReflectionVersionInfo.Create(clientType);

		_producers = new MetaHeaderProducer[]
		{
			new DefaultMetaHeaderProducer(version, serviceIdentifier),
			new ApiVersionMetaHeaderProducer(version)
		};
	}
}

public class ApiVersionMetaHeaderProducer : MetaHeaderProducer
{
	private readonly string _apiVersion;

	public override string HeaderName => "Elastic-Api-Version";

	public override string ProduceHeaderValue(RequestData requestData) => _apiVersion;

	public ApiVersionMetaHeaderProducer(VersionInfo version)
	{
		var meta = version.Metadata;

		if (meta is null || meta.Length != 8)
		{
			_apiVersion = "2023-10-31"; // Fall back to the earliest version
			return;
		}

		// Metadata format: 20231031

		_apiVersion = $"{meta.Substring(0, 4)}-{meta.Substring(4, 2)}-{meta.Substring(6, 2)}";
	}
}
