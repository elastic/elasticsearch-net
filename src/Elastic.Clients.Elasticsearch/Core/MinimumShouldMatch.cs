// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch;

public sealed class MinimumShouldMatch : Union<int?, string>
{
	public MinimumShouldMatch(int count) : base(count) { }

	public MinimumShouldMatch(string percentage) : base(percentage) { }

	public static MinimumShouldMatch Fixed(int count) => count;

	public static MinimumShouldMatch Percentage(double percentage) => $"{percentage}%";

	public static implicit operator MinimumShouldMatch(string first) => new(first);

	public static implicit operator MinimumShouldMatch(int second) => new(second);

	public static implicit operator MinimumShouldMatch(double second) => Percentage(second);
}
