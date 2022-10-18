// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public partial class AggregationContainer
{
	internal string ContainedVariantName { get; set; }

	internal Action<Utf8JsonWriter, JsonSerializerOptions> SerializeFluent { get; private set; }

	private AggregationContainer(string variant) => ContainedVariantName = variant;

	internal static AggregationContainer CreateWithAction<T>(string variantName, Action<T> configure) where T : new()
	{
		var container = new AggregationContainer(variantName);
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

	public static implicit operator AggregationContainer(Aggregation aggregator) =>
		aggregator == null ? null : new AggregationContainer(aggregator);
}
