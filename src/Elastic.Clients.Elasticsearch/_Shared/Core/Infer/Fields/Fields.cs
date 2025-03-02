// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Serialization;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(FieldsConverter))]
[DebuggerDisplay($"{{{nameof(DebuggerDisplay)},nq}}")]
public sealed class Fields :
	IEquatable<Fields>,
	IEnumerable<Field>,
	IUrlParameter
#if NET7_0_OR_GREATER
	, IParsable<Fields>
#endif
{
	internal readonly List<Field> ListOfFields;

	#region Constructors

	internal Fields() => ListOfFields = [];

	internal Fields(IEnumerable<Field> fields)
	{
		if (fields is null)
			throw new ArgumentNullException(nameof(fields));

		ListOfFields = [.. fields];
	}

	#endregion Constructors

	#region Factory Methods

	public static Fields FromField(Field field) => new Fields([field]);

	public static Fields FromFields(Field[]? fields) => new Fields(fields ?? []);

	public static Fields FromString(string name) => name.IsNullOrEmptyCommaSeparatedList(out var split)
		? new Fields()
		: new Fields(split.Select(f => new Field(f)));

	public static Fields FromStrings(string[]? names) => names.IsNullOrEmpty()
		? new Fields()
		: new Fields(names!.Select(f => new Field(f)));

	public static Fields FromExpression(Expression expression) => new Fields([new Field(expression)]);

	public static Fields FromExpressions(Expression[]? expressions) => expressions.IsNullOrEmpty()
		? new Fields()
		: new Fields(expressions!.Select(f => new Field(f)));

	public static Fields FromExpression<T, TValue>(Expression<Func<T, TValue>> expression) => new Fields([new Field(expression)]);

	public static Fields FromExpressions<T>(Expression<Func<T, object?>>[] expressions) => new Fields(expressions!.Select(f => new Field(f)));

	public static Fields FromProperty(PropertyInfo property) => new Fields([property]);

	public static Fields FromProperties(PropertyInfo[]? properties) => properties.IsNullOrEmpty()
		? new Fields()
		: new Fields(properties!.Select(f => new Field(f)));

	#endregion Factory Methods

	#region Conversion Operators

	public static implicit operator Fields(Field field) => FromField(field);

	public static implicit operator Fields(Field[]? fields) => FromFields(fields);

	public static implicit operator Fields(string name) => FromString(name);

	public static implicit operator Fields(string[]? names) => FromStrings(names);

	public static implicit operator Fields(Expression expression) => FromExpression(expression);

	public static implicit operator Fields(Expression[]? expressions) => FromExpressions(expressions);

	public static implicit operator Fields(PropertyInfo property) => FromProperty(property);

	public static implicit operator Fields(PropertyInfo[]? properties) => FromProperties(properties);

	#endregion Conversion Operators

	#region Combinator Methods

	public Fields And(params Field[] fields)
	{
		if (fields is null)
			throw new ArgumentNullException(nameof(fields));

		ListOfFields.AddRange(fields);

		return this;
	}

	public Fields And(params string[] names)
	{
		if (names is null)
			throw new ArgumentNullException(nameof(names));

		ListOfFields.AddRange(names.Select(f => new Field(f)));

		return this;
	}

	public Fields And<T>(params Expression<Func<T, object>>[] expressions)
	{
		if (expressions is null)
			throw new ArgumentNullException(nameof(expressions));

		ListOfFields.AddRange(expressions.Select(f => new Field(f)));

		return this;
	}

	public Fields And<T, TValue>(Expression<Func<T, TValue>> expression, double? boost = null)
		where T : class
	{
		if (expression is null)
			throw new ArgumentNullException(nameof(expression));

		ListOfFields.Add(new Field(expression, boost));

		return this;
	}

	public Fields And(params PropertyInfo[] properties)
	{
		if (properties is null)
			throw new ArgumentNullException(nameof(properties));

		ListOfFields.AddRange(properties.Select(f => new Field(f)));

		return this;
	}

	#endregion Combinator Methods

	#region Equality

	public static bool operator ==(Fields? left, Fields? right) => Equals(left, right);

	public static bool operator !=(Fields? left, Fields? right) => !Equals(left, right);

	public override bool Equals(object? obj) =>
		obj switch
		{
			Fields f => Equals(f),
			Field[] f => Equals(f),
			Field f => Equals(f),
			string s => Equals(s),
			string[] s => Equals(s),
			Expression e => Equals(e),
			Expression[] e => Equals(e),
			PropertyInfo p => Equals(p),
			PropertyInfo[] p => Equals(p),
			_ => false
		};

	public override int GetHashCode() => ListOfFields.GetHashCode();

	public bool Equals(Fields? other) => (ListOfFields, other?.ListOfFields) switch
	{
		(null, null) => true,
		({ } a, { } b) => (a.Count == b.Count) && !a.Except(b).Any(),
		_ => false
	};

	#endregion Equality

	#region IEnumerable

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public IEnumerator<Field> GetEnumerator() => ListOfFields.GetEnumerator();

	#endregion IEnumerable

	#region IParsable

	public static Fields Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out Fields? result)
	{
		if (s is null)
		{
			result = null;
			return false;
		}

		if (s.IsNullOrEmptyCommaSeparatedList(out var list))
		{
			result = new Fields();
			return true;
		}

		var fields = new List<Field>();
		foreach (var item in list)
		{
			if (!Field.TryParse(item, provider, out var field))
			{
				result = null;
				return false;
			}

			fields.Add(field);
		}

		result = new Fields(fields);
		return true;
	}

	#endregion IParsable

	#region IUrlParameter

	string IUrlParameter.GetString(ITransportConfiguration? settings)
	{
		if (settings is not IElasticsearchClientSettings elasticsearchClientSettings)
		{
			throw new ArgumentNullException(nameof(settings),
				$"Can not resolve {nameof(Fields)} if no {nameof(IElasticsearchClientSettings)} is provided");
		}

		return string.Join(",", ListOfFields.Select(f => ((IUrlParameter)f).GetString(elasticsearchClientSettings)));
	}

	#endregion IUrlParameter

	#region Debugging

	public override string ToString() =>
		$"Count: {ListOfFields.Count} [" +
		string.Join(",", ListOfFields.Select((t, i) => $"({i + 1}: {t?.DebuggerDisplay})")) + "]";

	private string DebuggerDisplay => ToString();

	#endregion Debugging
}
