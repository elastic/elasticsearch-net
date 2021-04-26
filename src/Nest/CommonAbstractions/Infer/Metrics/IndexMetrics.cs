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
using Elastic.Transport;
using Elasticsearch.Net;

namespace Nest
{
	public class IndexMetrics : IEquatable<IndexMetrics>, IUrlParameter
	{
		private readonly NodesStatsIndexMetric _enumValue;

		internal IndexMetrics(NodesStatsIndexMetric metric) => _enumValue = metric;

		internal Enum Value => _enumValue;

		public bool Equals(IndexMetrics other) => Value.Equals(other.Value);

		public string GetString(ITransportConfiguration settings) => _enumValue.GetStringValue();

		public static implicit operator IndexMetrics(NodesStatsIndexMetric metric) => new IndexMetrics(metric);

		public bool Equals(Enum other) => Value.Equals(other);

		public override bool Equals(object obj) => obj is Enum e ? Equals(e) : obj is IndexMetrics m && Equals(m.Value);

		public override int GetHashCode() => _enumValue.GetHashCode();

		public static bool operator ==(IndexMetrics left, IndexMetrics right) => Equals(left, right);

		public static bool operator !=(IndexMetrics left, IndexMetrics right) => !Equals(left, right);
	}
}
