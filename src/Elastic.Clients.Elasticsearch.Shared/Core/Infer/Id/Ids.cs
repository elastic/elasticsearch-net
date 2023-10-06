// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

[DebuggerDisplay("{DebugDisplay,nq}")]
[JsonConverter(typeof(IdsConverter))]
public partial class Ids : IUrlParameter, IEquatable<Ids>
{
	private readonly IList<Id> _ids;

	public Ids(IEnumerable<Id> ids) => _ids = ids.ToList();

	public Ids(IList<Id> ids) => _ids = ids;

	public Ids(IEnumerable<string> ids) => _ids = ids.Select(i  => new Id(i)).ToList();

	public Ids(string value)
	{
		if (!value.IsNullOrEmptyCommaSeparatedList(out var arr))
			_ids = arr.Select(i => new Id(i)).ToList();
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
		value.IsEmpty() ? null : new Ids(value);

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
}
