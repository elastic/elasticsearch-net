// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport.VirtualizedCluster.Components;

namespace Elastic.Transport.VirtualizedCluster.Rules
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
