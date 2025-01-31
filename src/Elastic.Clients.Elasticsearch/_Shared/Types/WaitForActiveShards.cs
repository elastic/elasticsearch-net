// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public partial struct WaitForActiveShards : IStringable
{
	public static WaitForActiveShards All = new("all");

	public WaitForActiveShards(string value) => Value = value;

	public string Value { get; }

	public static implicit operator WaitForActiveShards(int v) => new(v.ToString());
	public static implicit operator WaitForActiveShards(string v) => new(v);

	public string GetString() => Value ?? string.Empty;
}
