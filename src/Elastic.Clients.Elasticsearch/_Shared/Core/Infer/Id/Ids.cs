// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

[DebuggerDisplay("{DebugDisplay,nq}")]
[JsonConverter(typeof(IdsConverter))]
public class Ids :
	IUrlParameter,
	IEquatable<Ids>
#if NET7_0_OR_GREATER
	, IParsable<Ids>
#endif
{
	private readonly IList<Id> _ids;

	public Ids(IEnumerable<Id> ids) => _ids = ids.ToList();

	public Ids(IList<Id> ids) => _ids = ids;

	public Ids(IEnumerable<string> ids) => _ids = ids.Select(i => new Id(i)).ToList();

	public Ids(string value)
	{
		if (!value.IsNullOrEmptyCommaSeparatedList(out var arr))
			_ids = arr.Select(i => new Id(i)).ToList();
	}

	public Ids()
	{
		_ids = [];
	}

	internal IList<Id> IdsToSerialize => _ids;

	private string DebugDisplay => ((IUrlParameter)this).GetString(null);

	public bool Equals(Ids other)
	{
		if (other == null)
			return false;
		if (_ids == null && other._ids == null)
			return true;
		if (_ids == null || other._ids == null)
			return false;

		return _ids.Count == other._ids.Count &&
			   _ids.OrderBy(id => id).SequenceEqual(other._ids.OrderBy(id => id));
	}

	string IUrlParameter.GetString(ITransportConfiguration? settings)
	{
		if (settings is not ElasticsearchClientSettings elasticsearchClientSettings)
		{
			throw new Exception("Unexpected settings type.");
		}

		return string.Join(",", _ids.Select(i => i.GetString(elasticsearchClientSettings)) ?? Enumerable.Empty<string>());
	}

	public override string ToString() => DebugDisplay;

	public static implicit operator Ids(string value) =>
		value.IsNullOrEmptyCommaSeparatedList(out var arr) ? null : new Ids(arr);

	public static implicit operator Ids(string[] value) =>
		value.IsNullOrEmpty() ? null : new Ids(value);

	public override bool Equals(object obj) => obj is Ids other && Equals(other);

	public override int GetHashCode()
	{
		if (_ids == null)
			return 0;
		unchecked
		{
			var hc = 0;
			foreach (var id in _ids.OrderBy(id => id))
				hc = hc * 17 + id.GetHashCode();
			return hc;
		}
	}

	public static bool operator ==(Ids left, Ids right) => Equals(left, right);

	public static bool operator !=(Ids left, Ids right) => !Equals(left, right);

	#region IParsable

	public static Ids Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out Ids? result)
	{
		if (s is null)
		{
			result = null;
			return false;
		}

		if (s.IsNullOrEmptyCommaSeparatedList(out var list))
		{
			result = new Ids();
			return true;
		}

		var ids = new List<Id>();
		foreach (var item in list)
		{
			if (!Id.TryParse(item, provider, out var id))
			{
				result = null;
				return false;
			}

			ids.Add(id);
		}

		result = new Ids(ids);
		return true;
	}

	#endregion IParsable
}
