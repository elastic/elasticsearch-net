// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Core.MSearch;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;

namespace Elastic.Clients.Elasticsearch.Core.MSearchTemplate;

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
