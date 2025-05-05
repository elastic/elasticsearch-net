// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

[DebuggerDisplay($"{{{nameof(DebuggerDisplay)},nq}}")]
public sealed class Fields :
	IEquatable<Fields>,
	IEnumerable<Field>,
	IUrlParameter
{
	internal readonly List<Field> ListOfFields;

	#region Constructors

	internal Fields() => ListOfFields = [];

	internal Fields(IEnumerable<Field?> fields)
	{
		if (fields is null)
			throw new ArgumentNullException(nameof(fields));

		ListOfFields = [.. fields.Where(f => f is not null)];
	}

	#endregion Constructors

	#region Factory Methods

	public static Fields? FromField(Field? field) => field is null
		? null
		: new Fields([field]);

	public static Fields? FromFields(Field[]? fields) => fields.IsNullOrEmpty()
		? null
		: new Fields(fields!);

	public static Fields? FromString(string? name) => name.IsNullOrEmptyCommaSeparatedList(out var split)
		? null
		: new Fields(split.Select(f => new Field(f)));

	public static Fields? FromStrings(string[]? names) => names.IsNullOrEmpty()
		? null
		: new Fields(names!.Select(f => new Field(f)));

	public static Fields? FromExpression(Expression? expression) => expression is null
		? null
		: new Fields([new Field(expression)]);

	public static Fields? FromExpressions(Expression[]? expressions) => expressions.IsNullOrEmpty()
		? null
		: new Fields(expressions!.Select(f => new Field(f)));

	public static Fields? FromExpression<T, TValue>(Expression<Func<T, TValue>>? expression) => expression is null
		? null
		: new Fields([new Field(expression)]);

	public static Fields? FromExpressions<T>(Expression<Func<T, object?>>[]? expressions) => expressions.IsNullOrEmpty()
		? null
		: new Fields(expressions!.Select(f => new Field(f)));

	public static Fields? FromProperty(PropertyInfo? property) => property is null
		? null
		: new Fields([property]);

	public static Fields? FromProperties(PropertyInfo[]? properties) => properties.IsNullOrEmpty()
		? null
		: new Fields(properties!.Select(f => new Field(f)));

	#endregion Factory Methods

	#region Conversion Operators

	public static implicit operator Fields?(Field? field) => FromField(field);

	public static implicit operator Fields?(Field[]? fields) => FromFields(fields);

	public static implicit operator Fields?(string? name) => FromString(name);

	public static implicit operator Fields?(string[]? names) => FromStrings(names);

	public static implicit operator Fields?(Expression? expression) => FromExpression(expression);

	public static implicit operator Fields?(Expression[]? expressions) => FromExpressions(expressions);

	public static implicit operator Fields?(PropertyInfo? property) => FromProperty(property);

	public static implicit operator Fields?(PropertyInfo[]? properties) => FromProperties(properties);

	#endregion Conversion Operators

	#region Combinator Methods

	public Fields And(params Field?[] fields)
	{
		if (fields is null)
			throw new ArgumentNullException(nameof(fields));

		ListOfFields.AddRange(fields.Where(f => f is not null));

		return this;
	}

	public Fields And(params string?[] names)
	{
		if (names is null)
			throw new ArgumentNullException(nameof(names));

		ListOfFields.AddRange(names.Where(f => f is not null).Select(f => new Field(f)));

		return this;
	}

	public Fields And<T>(params Expression<Func<T, object>>?[] expressions)
	{
		if (expressions is null)
			throw new ArgumentNullException(nameof(expressions));

		ListOfFields.AddRange(expressions.Where(f => f is not null).Select(f => new Field(f)));

		return this;
	}

	public Fields And<T, TValue>(Expression<Func<T, TValue>> expression, double? boost = null, string? format = null)
		where T : class
	{
		if (expression is null)
			throw new ArgumentNullException(nameof(expression));

		ListOfFields.Add(new Field(expression, boost, format));

		return this;
	}

	public Fields And(params PropertyInfo?[] properties)
	{
		if (properties is null)
			throw new ArgumentNullException(nameof(properties));

		ListOfFields.AddRange(properties.Where(x => x is not null).Select(f => new Field(f)));

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
