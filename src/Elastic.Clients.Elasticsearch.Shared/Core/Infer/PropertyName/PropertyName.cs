// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Serialization;
#if ELASTICSEARCH_SERVERLESS
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
#else
using Elastic.Clients.Elasticsearch.Serialization;
#endif
using Elastic.Transport;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

[DebuggerDisplay("{" + nameof(DebugDisplay) + ",nq}")]
[JsonConverter(typeof(PropertyNameConverter))]
public sealed class PropertyName : IEquatable<PropertyName>, IUrlParameter
{
	private readonly object _comparisonValue;
	private readonly Type _type;

	public PropertyName(string name)
	{
		Name = name;
		_comparisonValue = name;
	}

	public PropertyName(Expression expression)
	{
		Expression = expression;
		_comparisonValue = expression.ComparisonValueFromExpression(out var type, out var cachable);
		CacheableExpression = cachable;
		_type = type;
	}

	public PropertyName(PropertyInfo property)
	{
		Property = property;
		_comparisonValue = property;
		_type = property.DeclaringType;
	}

	public bool CacheableExpression { get; }
	public Expression Expression { get; }

	public string? Name { get; }
	public PropertyInfo Property { get; }

	internal string DebugDisplay =>
		$"{Expression?.ToString() ?? PropertyDebug ?? Name}{(_type == null ? "" : " typeof: " + _type.Name)}";

	private string PropertyDebug => Property == null ? null : $"PropertyInfo: {Property.Name}";
	private static int TypeHashCode { get; } = typeof(PropertyName).GetHashCode();

	public bool Equals(PropertyName other) => EqualsMarker(other);

	string IUrlParameter.GetString(ITransportConfiguration? settings)
	{
		if (settings is not IElasticsearchClientSettings elasticsearchSettings)
		{
			throw new ArgumentNullException(nameof(settings),
				$"Can not resolve {nameof(PropertyName)} if no {nameof(IElasticsearchClientSettings)} is provided");
		}

		return GetInferredString(elasticsearchSettings);
	}

	private string GetInferredString(IElasticsearchClientSettings settings) => settings.Inferrer.PropertyName(this);

	public override string ToString() => DebugDisplay;

	public static implicit operator PropertyName(string name) =>
		name.IsNullOrEmpty() ? null : new PropertyName(name);

	public static implicit operator PropertyName(Expression expression) =>
		expression == null ? null : new PropertyName(expression);

	public static implicit operator PropertyName(PropertyInfo property) =>
		property == null ? null : new PropertyName(property);

	internal static PropertyName FromField(Field field)
	{
		if (field.Property is not null)
			return new PropertyName(field.Property);

		if (field.Expression is not null)
			return new PropertyName(field.Expression);

		if (field.Name is not null)
			return new PropertyName(field.Name);

		return null;
	}

	public override int GetHashCode()
	{
		unchecked
		{
			var result = TypeHashCode;
			result = (result * 397) ^ (_comparisonValue?.GetHashCode() ?? 0);
			result = (result * 397) ^ (_type?.GetHashCode() ?? 0);
			return result;
		}
	}

	public override bool Equals(object obj) =>
		obj is string s ? EqualsString(s) : obj is PropertyName r && EqualsMarker(r);

	private bool EqualsString(string other) => !other.IsNullOrEmpty() && other == Name;

	public bool EqualsMarker(PropertyName other) => _type != null
		? other != null && _type == other._type && _comparisonValue.Equals(other._comparisonValue)
		: other != null && _comparisonValue.Equals(other._comparisonValue);

	public static bool operator ==(PropertyName left, PropertyName right) => Equals(left, right);

	public static bool operator !=(PropertyName left, PropertyName right) => !Equals(left, right);
}
