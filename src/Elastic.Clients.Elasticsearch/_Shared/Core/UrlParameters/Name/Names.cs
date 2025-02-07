// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

[DebuggerDisplay("{DebugDisplay,nq}")]
[JsonConverter(typeof(NamesConverter))]
public sealed class Names :
	IEquatable<Names>,
	IUrlParameter
#if NET7_0_OR_GREATER
	, IParsable<Names>
#endif
{
	public Names()
	{
		Values = [];
	}

	public Names(IEnumerable<Name> names)
	{
		Values = names?.ToList();
	}

	public Names(IEnumerable<string> names) :
		this(names?.Select(n => (Name)n).ToList())
	{
	}

	internal IList<Name> Values { get; init; }

	private string DebugDisplay => ((IUrlParameter)this).GetString(null);

	public override string ToString() => DebugDisplay;

	public bool Equals(Names other) => EqualsAllIds(Values, other.Values);

	string IUrlParameter.GetString(ITransportConfiguration? settings) =>
		string.Join(",", Values.Cast<IUrlParameter>().Select(n => n.GetString(settings)));

	public static Names Parse(string names) => names.IsNullOrEmptyCommaSeparatedList(out var list) ? new Names() : new Names(list);

	public static implicit operator Names(Name name) => name == null ? null : new Names(new[] { name });

	public static implicit operator Names(string names) => Parse(names);

	public static implicit operator Names(string[] names) => names.IsNullOrEmpty() ? null : new Names(names);

	public static bool operator ==(Names left, Names right) => Equals(left, right);

	public static bool operator !=(Names left, Names right) => !Equals(left, right);

	private static bool EqualsAllIds(ICollection<Name> thisIds, ICollection<Name> otherIds)
	{
		if (thisIds == null && otherIds == null)
			return true;
		if (thisIds == null || otherIds == null)
			return false;
		if (thisIds.Count != otherIds.Count)
			return false;

		return thisIds.Count == otherIds.Count && !thisIds.Except(otherIds).Any();
	}

	public override bool Equals(object obj) => obj is string s ? Equals(Parse(s)) : obj is Names i && Equals(i);

	public override int GetHashCode() => Values.GetHashCode();

	#region IParsable

	public static Names Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out Names? result)
	{
		if (s is null)
		{
			result = null;
			return false;
		}

		if (s.IsNullOrEmptyCommaSeparatedList(out var list))
		{
			result = new Names();
			return true;
		}

		var names = new List<Name>();
		foreach (var item in list)
		{
			if (!Name.TryParse(item, provider, out var name))
			{
				result = null;
				return false;
			}

			names.Add(name);
		}

		result = new Names { Values = names };
		return true;
	}

	#endregion IParsable
}

internal sealed class NamesConverter :
	JsonConverter<Names>
{
	public override Names Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.TokenType switch
		{
			JsonTokenType.String => new Names([reader.ReadValue<Name>(options)]),
			JsonTokenType.StartArray => new Names(reader.ReadValue<List<Name>>(options)),
			_ => throw reader.UnexpectedTokenException(JsonTokenType.String, JsonTokenType.StartArray)
		};
	}

	public override void Write(Utf8JsonWriter writer, Names value, JsonSerializerOptions options)
	{
		if (value.Values is [{ } single])
		{
			writer.WriteValue(options, single);
			return;
		}

		writer.WriteValue(options, value.Values);
	}
}
