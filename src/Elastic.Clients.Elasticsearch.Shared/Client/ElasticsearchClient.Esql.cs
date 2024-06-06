// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if !ELASTICSEARCH_SERVERLESS

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Threading;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Esql.Serverless;
#else

namespace Elastic.Clients.Elasticsearch.Esql;
#endif

public partial class EsqlNamespacedClient
{
	public virtual async Task<IEnumerable<TDocument>> QueryAsObjectsAsync<TDocument>(
		Action<EsqlQueryRequestDescriptor<TDocument>> configureRequest,
		CancellationToken cancellationToken = default)
	{
		if (configureRequest is null)
			throw new ArgumentNullException(nameof(configureRequest));

		var response = await QueryAsync<TDocument>(Configure, cancellationToken).ConfigureAwait(false);

		return EsqlToObject<TDocument>(Client, response);

		void Configure(EsqlQueryRequestDescriptor<TDocument> descriptor)
		{
			configureRequest(descriptor);
			descriptor.Format("JSON");
			descriptor.Columnar(false);
		}
	}

	private static IEnumerable<T> EsqlToObject<T>(ElasticsearchClient client, EsqlQueryResponse response)
	{
		// TODO: Improve performance

		using var doc = JsonSerializer.Deserialize<JsonDocument>(response.Data) ?? throw new JsonException();

		if (!doc.RootElement.TryGetProperty("columns"u8, out var columns) || (columns.ValueKind is not JsonValueKind.Array))
			throw new JsonException("");

		if (!doc.RootElement.TryGetProperty("values"u8, out var values) || (values.ValueKind is not JsonValueKind.Array))
			yield break;

		var names = columns.EnumerateArray()
			.Select(x =>
			{
				if (!x.TryGetProperty("name"u8, out var prop))
				{
					throw new JsonException();
				}

				var result = prop.GetString() ?? throw new JsonException();

				return result;
			})
			.ToArray();

		var obj = new JsonObject();
		using var ms = new MemoryStream();
		using var writer = new Utf8JsonWriter(ms);

		foreach (var document in values.EnumerateArray())
		{
			obj.Clear();
			ms.SetLength(0);
			writer.Reset();

			var properties = names.Zip(document.EnumerateArray(),
				(key, value) => new KeyValuePair<string, JsonNode?>(key, JsonValue.Create(value)));
			foreach (var property in properties)
				obj.Add(property);

			obj.WriteTo(writer);
			writer.Flush();
			ms.Position = 0;

			var result = client.SourceSerializer.Deserialize<T>(ms) ?? throw new JsonException("");

			yield return result;
		}
	}
}

#endif
