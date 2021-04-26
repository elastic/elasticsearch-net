/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using Elasticsearch.Net;

namespace Nest
{
	public class Metrics : IEquatable<Metrics>, IUrlParameter
	{
		internal Metrics(IndicesStatsMetric metric) => Value = metric;

		internal Metrics(NodesStatsMetric metric) => Value = metric;

		internal Metrics(NodesInfoMetric metric) => Value = metric;

		internal Metrics(ClusterStateMetric metric) => Value = metric;

		internal Metrics(WatcherStatsMetric metric) => Value = metric;

		internal Metrics(NodesUsageMetric metric) => Value = metric;

		internal Enum Value { get; }

		public bool Equals(Metrics other) => Value.Equals(other.Value);

		public string GetString(IConnectionConfigurationValues settings) => Value.GetStringValue();

		public static implicit operator Metrics(IndicesStatsMetric metric) => new Metrics(metric);

		public static implicit operator Metrics(NodesStatsMetric metric) => new Metrics(metric);

		public static implicit operator Metrics(NodesInfoMetric metric) => new Metrics(metric);

		public static implicit operator Metrics(ClusterStateMetric metric) => new Metrics(metric);

		public static implicit operator Metrics(WatcherStatsMetric metric) => new Metrics(metric);

		public static implicit operator Metrics(NodesUsageMetric metric) => new Metrics(metric);

		public bool Equals(Enum other) => Value.Equals(other);

		public override bool Equals(object obj) => obj is Enum e ? Equals(e) : obj is Metrics m && Equals(m.Value);

		public override int GetHashCode() => Value != null ? Value.GetHashCode() : 0;

		public static bool operator ==(Metrics left, Metrics right) => Equals(left, right);

		public static bool operator !=(Metrics left, Metrics right) => !Equals(left, right);
	}
}
