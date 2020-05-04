// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using Elasticsearch.Net;

namespace Nest
{
	public class IndexMetrics : IEquatable<IndexMetrics>, IUrlParameter
	{
		private readonly NodesStatsIndexMetric _enumValue;

		internal IndexMetrics(NodesStatsIndexMetric metric) => _enumValue = metric;

		internal Enum Value => _enumValue;

		public bool Equals(IndexMetrics other) => Value.Equals(other.Value);

		public string GetString(IConnectionConfigurationValues settings) => _enumValue.GetStringValue();

		public static implicit operator IndexMetrics(NodesStatsIndexMetric metric) => new IndexMetrics(metric);

		public bool Equals(Enum other) => Value.Equals(other);

		public override bool Equals(object obj) => obj is Enum e ? Equals(e) : obj is IndexMetrics m && Equals(m.Value);

		public override int GetHashCode() => _enumValue.GetHashCode();

		public static bool operator ==(IndexMetrics left, IndexMetrics right) => Equals(left, right);

		public static bool operator !=(IndexMetrics left, IndexMetrics right) => !Equals(left, right);
	}
}
