// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
#if ELASTICSEARCH_SERVERLESS
using Elastic.Clients.Elasticsearch.Serverless.Core.MSearch;
#else
using Elastic.Clients.Elasticsearch.Core.MSearch;
#endif
#if ELASTICSEARCH_SERVERLESS
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
#else
using Elastic.Clients.Elasticsearch.Serialization;
#endif
using Elastic.Transport;
using Elastic.Transport.Extensions;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Core.MSearchTemplate;
#else
namespace Elastic.Clients.Elasticsearch.Core.MSearchTemplate;
#endif

public sealed class SearchTemplateRequestItem : IStreamSerializable
{
	public SearchTemplateRequestItem(TemplateConfig body) => Body = body;

	public SearchTemplateRequestItem(MultisearchHeader header, TemplateConfig body)
	{
		Header = header;
		Body = body;
	}

	public MultisearchHeader Header { get; init; }
	public TemplateConfig Body { get; init; }

	void IStreamSerializable.Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
	{
		if (Body is null)
			return;

		settings.RequestResponseSerializer.Serialize(Header, stream, formatting);
		stream.WriteByte((byte)'\n');
		settings.RequestResponseSerializer.Serialize(Body, stream, formatting);
		stream.WriteByte((byte)'\n');
	}

	async Task IStreamSerializable.SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
	{
		if (Body is null)
			return;

		await settings.RequestResponseSerializer.SerializeAsync(Header, stream, formatting).ConfigureAwait(false);
		stream.WriteByte((byte)'\n');
		await settings.RequestResponseSerializer.SerializeAsync(Body, stream, formatting).ConfigureAwait(false);
		stream.WriteByte((byte)'\n');
	}
}
