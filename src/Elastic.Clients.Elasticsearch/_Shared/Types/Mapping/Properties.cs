// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Mapping;

[JsonConverter(typeof(Json.PropertiesConverter))]
public partial class Properties
{
	private readonly IElasticsearchClientSettings _settings;

	internal Properties(IElasticsearchClientSettings values) => _settings = values;

	public void Add<TDocument>(Expression<Func<TDocument, object>> propertyName, IProperty property) => BackingDictionary.Add(Sanitize(propertyName), property);

	protected override PropertyName Sanitize(PropertyName key) => _settings?.Inferrer.PropertyName(key) ?? key;
}

public sealed class Properties<TDocument> : Properties
{
	public void Add<TValue>(Expression<Func<TDocument, TValue>> name, IProperty property) => BackingDictionary.Add(name, property);
}
