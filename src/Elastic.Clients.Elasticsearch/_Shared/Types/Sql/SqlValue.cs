// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Sql;

[JsonConverter(typeof(Json.SqlValueConverter))]
public readonly struct SqlValue
{
	private readonly LazyJson _lazyDocument;

	internal SqlValue(LazyJson lazyDocument) => _lazyDocument = lazyDocument;

	public T? As<T>() => _lazyDocument.As<T>();
}
