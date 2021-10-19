// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public class Metrics : IEquatable<Metrics>, IUrlParameter
{
	// TODO: Complete this

	//internal Metrics(IndicesStatsMetric metric) => Value = metric;

	//internal Metrics(NodesStatsMetric metric) => Value = metric;

	//internal Metrics(NodesInfoMetric metric) => Value = metric;

	//internal Metrics(ClusterStateMetric metric) => Value = metric;

	//internal Metrics(WatcherStatsMetric metric) => Value = metric;

	//internal Metrics(NodesUsageMetric metric) => Value = metric;

	internal Enum Value { get; }

	public bool Equals(Metrics other) => Value.Equals(other.Value);

	public string GetString(ITransportConfiguration settings) => string.Empty; // TODO Value.GetStringValue();

	//public static implicit operator Metrics(IndicesStatsMetric metric) => new Metrics(metric);

	//public static implicit operator Metrics(NodesStatsMetric metric) => new Metrics(metric);

	//public static implicit operator Metrics(NodesInfoMetric metric) => new Metrics(metric);

	//public static implicit operator Metrics(ClusterStateMetric metric) => new Metrics(metric);

	//public static implicit operator Metrics(WatcherStatsMetric metric) => new Metrics(metric);

	//public static implicit operator Metrics(NodesUsageMetric metric) => new Metrics(metric);

	public bool Equals(Enum other) => Value.Equals(other);

	public override bool Equals(object obj) => obj is Enum e ? Equals(e) : obj is Metrics m && Equals(m.Value);

	public override int GetHashCode() => Value != null ? Value.GetHashCode() : 0;

	public static bool operator ==(Metrics left, Metrics right) => Equals(left, right);

	public static bool operator !=(Metrics left, Metrics right) => !Equals(left, right);
}
