// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal static partial class JsonSerializerOptionsExtensions
{
	internal static bool TryGetClientSettings(this JsonSerializerOptions options,
		[NotNullWhen(true)] out IElasticsearchClientSettings? settings)
	{
		return ContextProvider<IElasticsearchClientSettings>.TryGetContext(options, out settings);
	}
}
