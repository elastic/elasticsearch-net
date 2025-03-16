// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public partial struct Refresh : IStringable
{
	public static Refresh WaitFor = new("wait_for");
	public static Refresh True = new("true");
	public static Refresh False = new("false");

	public Refresh(string value) => Value = value;

	public string Value { get; }

	public string GetString() => Value ?? string.Empty;
}
