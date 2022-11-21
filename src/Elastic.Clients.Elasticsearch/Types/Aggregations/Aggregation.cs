// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public partial class Aggregation
{
	internal string ContainedVariantName { get; set; }

	internal Action<Utf8JsonWriter, JsonSerializerOptions> SerializeFluent { get; private set; }

	private Aggregation(string variant) => ContainedVariantName = variant;

	internal static Aggregation CreateWithAction<T>(string variantName, Action<T> configure) where T : new()
	{
		var container = new Aggregation(variantName);
		container.SetAction(configure);
		return container;
	}

	private void SetAction<T>(Action<T> configure) where T : new()
		=> SerializeFluent = (writer, options) =>
			{
				var descriptor = new T();
				configure(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
			};

	public static implicit operator Aggregation(SearchAggregation aggregator) =>
		aggregator == null ? null : new Aggregation(aggregator);
}
