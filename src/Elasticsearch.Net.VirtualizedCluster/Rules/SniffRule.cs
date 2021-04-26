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

namespace Elasticsearch.Net.VirtualizedCluster.Rules
{
	public interface ISniffRule : IRule
	{
		VirtualCluster NewClusterState { get; set; }
	}

	public class SniffRule : RuleBase<SniffRule>, ISniffRule
	{
		VirtualCluster ISniffRule.NewClusterState { get; set; }
		private ISniffRule Self => this;

		public SniffRule Fails(RuleOption<TimesHelper.AllTimes, int> times, RuleOption<Exception, int> errorState = null)
		{
			Self.Times = times;
			Self.Succeeds = false;
			Self.Return = errorState;
			return this;
		}

		public SniffRule Succeeds(RuleOption<TimesHelper.AllTimes, int> times, VirtualCluster cluster = null)
		{
			Self.Times = times;
			Self.Succeeds = true;
			Self.NewClusterState = cluster;
			Self.Return = 200;
			return this;
		}

		public SniffRule SucceedAlways(VirtualCluster cluster = null) => Succeeds(TimesHelper.Always, cluster);

		public SniffRule FailAlways(RuleOption<Exception, int> errorState = null) => Fails(TimesHelper.Always, errorState);
	}
}
