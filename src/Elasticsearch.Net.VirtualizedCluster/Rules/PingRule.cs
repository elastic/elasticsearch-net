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
	public class PingRule : RuleBase<PingRule>
	{
		private IRule Self => this;

		public PingRule Fails(RuleOption<TimesHelper.AllTimes, int> times, RuleOption<Exception, int> errorState = null)
		{
			Self.Times = times;
			Self.Succeeds = false;
			Self.Return = errorState;
			return this;
		}

		public PingRule Succeeds(RuleOption<TimesHelper.AllTimes, int> times, int? validResponseCode = 200)
		{
			Self.Times = times;
			Self.Succeeds = true;
			Self.Return = validResponseCode;
			return this;
		}

		public PingRule SucceedAlways(int? validResponseCode = 200) => Succeeds(TimesHelper.Always, validResponseCode);

		public PingRule FailAlways(RuleOption<Exception, int> errorState = null) => Fails(TimesHelper.Always, errorState);
	}
}
