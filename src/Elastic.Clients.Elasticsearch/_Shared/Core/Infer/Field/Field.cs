// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Serialization;

using Elastic.Transport;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else

namespace Elastic.Clients.Elasticsearch;
#endif

[JsonConverter(typeof(FieldConverter))]
[DebuggerDisplay($"{{{nameof(DebuggerDisplay)},nq}}")]
public sealed class Field :
	IEquatable<Field>,
	IUrlParameter
#if NET7_0_OR_GREATER
	, IParsable<Field>
#endif
{
	private readonly object _comparisonValue;
	private readonly Type? _type;

	// Pseudo and metadata fields

	public static Field IdField = new("_id");
	public static Field ScoreField = new("_score");
	public static Field KeyField = new("_key");
	public static Field CountField = new("_count");

	/// <summary>
	///     The name of the field
	/// </summary>
	public string? Name { get; }

	/// <summary>
	///     An expression from which the name of the field can be inferred
	/// </summary>
	public Expression? Expression { get; }

	/// <summary>
	///     A property from which the name of the field can be inferred
	/// </summary>
	public PropertyInfo? Property { get; }

	/// <summary>
	///     A boost to apply to the field
	/// </summary>
	public double? Boost { get; set; }

	internal bool CachableExpression { get; }

	#region Constructors

	public Field(string name, double? boost = null)
	{
		if (string.IsNullOrEmpty(name))
			throw new ArgumentException($"{name} can not be null or empty.", nameof(name));

		Name = ParseFieldName(name, out var b);
		Boost = b ?? boost;

		_comparisonValue = Name;
	}

	public Field(Expression expression, double? boost = null)
	{
		Expression = expression ?? throw new ArgumentNullException(nameof(expression));
		Boost = boost;

		_comparisonValue = expression.ComparisonValueFromExpression(out var type, out var cachable);
		_type = type;

		CachableExpression = cachable;
	}

	public Field(PropertyInfo property, double? boost = null)
	{
		Property = property ?? throw new ArgumentNullException(nameof(property));
		Boost = boost;

		_comparisonValue = property;
		_type = property.DeclaringType;
	}

	public Field(string name) : this(name, null)
	{
		// Used internally by `KeyValuePairConverter`.
	}

	#endregion Constructors

	#region Factory Methods

	public static Field FromString(string name) => new Field(name);

	public static Field FromExpression(Expression expression) => new Field(expression);

	public static Field FromProperty(PropertyInfo property) => new Field(property);

	#endregion Factory Methods

	#region Conversion Operators

	public static implicit operator Field(string name) => FromString(name);

	public static implicit operator Field(Expression expression) => FromExpression(expression);

	public static implicit operator Field(PropertyInfo property) => FromProperty(property);

	#endregion Conversion Operators

	#region Combinator Methods

	public Fields And(Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));

		return new([this, field]);
	}

	public Fields And<T, TValue>(Expression<Func<T, TValue>> expression, double? boost = null)
	{
		if (expression is null)
			throw new ArgumentNullException(nameof(expression));

		return new([this, new Field(expression, boost)]);
	}

	public Fields And<T>(Expression<Func<T, object>> expression, double? boost = null)
	{
		if (expression is null)
			throw new ArgumentNullException(nameof(expression));

		return new([this, new Field(expression, boost)]);
	}

	public Fields And(string field, double? boost = null)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));

		return new([this, new Field(field, boost)]);
	}

	public Fields And(PropertyInfo property, double? boost = null)
	{
		if (property is null)
			throw new ArgumentNullException(nameof(property));

		return new([this, new Field(property, boost)]);
	}

	#endregion Combinator Methods

	#region Equality

	public static bool operator ==(Field? a, Field? b) => Equals(a, b);

	public static bool operator !=(Field? a, Field? b) => !Equals(a, b);

	public bool Equals(Field? other) =>
		other switch
		{
			not null when _type is not null => (_type == other._type) && _comparisonValue.Equals(other._comparisonValue),
			not null when _type is null => _comparisonValue.Equals(other._comparisonValue),
			_ => false
		};

	public override bool Equals(object? obj) =>
		obj switch
		{
			Field f => Equals(f),
			string s => Equals(s),
			Expression e => Equals(e),
			PropertyInfo p => Equals(p),
			_ => false
		};

	public override int GetHashCode()
	{
		unchecked
		{
			var hashCode = _comparisonValue?.GetHashCode() ?? 0;
			hashCode = (hashCode * 397) ^ (_type?.GetHashCode() ?? 0);
			return hashCode;
		}
	}

	#endregion Equality

	#region IParsable

	public static Field Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out Field? result)
	{
		if (s is null)
		{
			result = null;
			return false;
		}

		result = new Field(s);
		return true;
	}

	#endregion IParsable

	#region IUrlParameter

	string IUrlParameter.GetString(ITransportConfiguration settings)
	{
		if (settings is not IElasticsearchClientSettings elasticsearchSettings)
		{
			throw new ArgumentNullException(nameof(settings),
				$"Can not resolve {nameof(Field)} if no {nameof(IElasticsearchClientSettings)} is provided");
		}

		return elasticsearchSettings.Inferrer.Field(this);
	}

	#endregion IUrlParameter

	#region Debugging

	public override string ToString() =>
		$"{Expression?.ToString() ?? PropertyDebug ?? Name}{(Boost.HasValue ? "^" + Boost.Value : string.Empty)}" +
		$"{(_type == null ? string.Empty : " typeof: " + _type.Name)}";

	internal string DebuggerDisplay => ToString();

	private string? PropertyDebug => Property is null ? null : $"PropertyInfo: {Property.Name}";

	#endregion Debugging

	private static string ParseFieldName(string name, out double? boost)
	{
		boost = null;

		var caretIndex = name.IndexOf('^');
		if (caretIndex == -1)
			return name;

		var parts = name.Split(new[] { '^' }, 2, StringSplitOptions.RemoveEmptyEntries);
		name = parts[0];
		boost = double.Parse(parts[1], CultureInfo.InvariantCulture);
		return name;
	}
}
