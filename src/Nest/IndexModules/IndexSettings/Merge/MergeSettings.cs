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

namespace Nest
{
	public interface IMergeSettings
	{
		IMergePolicySettings Policy { get; set; }
		IMergeSchedulerSettings Scheduler { get; set; }
	}

	public class MergeSettings : IMergeSettings
	{
		/// <inheritdoc />
		public IMergePolicySettings Policy { get; set; }

		/// <inheritdoc />
		public IMergeSchedulerSettings Scheduler { get; set; }
	}

	public class MergeSettingsDescriptor
		: DescriptorBase<MergeSettingsDescriptor, IMergeSettings>, IMergeSettings
	{
		IMergePolicySettings IMergeSettings.Policy { get; set; }
		IMergeSchedulerSettings IMergeSettings.Scheduler { get; set; }

		/// <inheritdoc />
		public MergeSettingsDescriptor Policy(Func<MergePolicySettingsDescriptor, IMergePolicySettings> selector) =>
			Assign(selector, (a, v) => a.Policy = v?.Invoke(new MergePolicySettingsDescriptor()));

		/// <inheritdoc />
		public MergeSettingsDescriptor Scheduler(Func<MergeSchedulerSettingsDescriptor, IMergeSchedulerSettings> selector) =>
			Assign(selector, (a, v) => a.Scheduler = v?.Invoke(new MergeSchedulerSettingsDescriptor()));
	}
}
