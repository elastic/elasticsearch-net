// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

[DebuggerDisplay("{DebugDisplay,nq}")]
[JsonConverter(typeof(IdConverter))]
public class Id :
	IEquatable<Id>,
	IUrlParameter
#if NET7_0_OR_GREATER
	, IParsable<Id>
#endif
{
	public Id(string id)
	{
		Tag = 0;
		StringValue = id;
	}

	public Id(long id)
	{
		Tag = 1;
		LongValue = id;
	}

	public Id(object document)
	{
		Tag = 2;
		Document = document;
	}

	internal object Document { get; }
	internal long? LongValue { get; }
	internal string StringOrLongValue => StringValue ?? LongValue?.ToString(CultureInfo.InvariantCulture);
	internal string StringValue { get; }
	internal int Tag { get; }

	private string DebugDisplay => StringOrLongValue ?? "Id from instance typeof: " + Document?.GetType().Name;

	private static int TypeHashCode { get; } = typeof(Id).GetHashCode();

	public bool Equals(Id other)
	{
		if (Tag + other.Tag == 1)
			return StringOrLongValue == other.StringOrLongValue;
		else if (Tag != other.Tag)
			return false;

		return Tag switch
		{
			0 or 1 => StringOrLongValue == other.StringOrLongValue,
			_ => Document?.Equals(other.Document) ?? false,
		};
	}

	string IUrlParameter.GetString(ITransportConfiguration? settings)
	{
		var elasticClientSettings = (IElasticsearchClientSettings)settings;
		return GetString(elasticClientSettings);
	}

	internal string GetString(IElasticsearchClientSettings settings) => settings.Inferrer.Id(Document) ?? StringOrLongValue;

	public static implicit operator Id(string id) => id.IsNullOrEmpty() ? null : new Id(id);

	public static implicit operator Id(long id) => new(id);

	public static implicit operator Id(Guid id) => new(id.ToString("D"));

	public static Id From<T>(T document) => new(document);

	public override string ToString() => DebugDisplay;

	public override bool Equals(object obj)
	{
		switch (obj)
		{
			case Id r:
				return Equals(r);

			case string s:
				return Equals(s);

			case int l:
				return Equals(l);

			case long l:
				return Equals(l);

			case Guid g:
				return Equals(g);
		}
		return Equals(new Id(obj));
	}

	public override int GetHashCode()
	{
		unchecked
		{
			var result = TypeHashCode;
			result = (result * 397) ^ (StringValue?.GetHashCode() ?? 0);
			result = (result * 397) ^ (LongValue?.GetHashCode() ?? 0);
			result = (result * 397) ^ (Document?.GetHashCode() ?? 0);
			return result;
		}
	}

	public static bool operator ==(Id left, Id right) => Equals(left, right);

	public static bool operator !=(Id left, Id right) => !Equals(left, right);

	#region IParsable

	public static Id Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out Id? result)
	{
		if (s is null)
		{
			result = null;
			return false;
		}

		result = new Id(s);
		return true;
	}

	#endregion IParsable
}
