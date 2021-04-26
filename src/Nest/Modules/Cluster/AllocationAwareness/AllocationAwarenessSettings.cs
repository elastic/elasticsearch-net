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
using System.Collections.Generic;

namespace Nest
{
	public interface IAllocationAwarenessSettings
	{
		/// <summary>Determines which node attributes are taken into account when balancing shards across the shards</summary>
		IEnumerable<string> Attributes { get; set; }

		/// <summary>With forced awareness shard copies are NEVER allicated within the same attribute</summary>
		IAllocationAttributes Forced { get; set; }
	}

	public class AllocationAwarenessSettings : IAllocationAwarenessSettings
	{
		/// <inheritdoc />
		public IEnumerable<string> Attributes { get; set; }

		/// <inheritdoc />
		public IAllocationAttributes Forced { get; set; }
	}

	public class AllocationAwarenessSettingsDescriptor
		: DescriptorBase<AllocationAwarenessSettingsDescriptor, IAllocationAwarenessSettings>, IAllocationAwarenessSettings
	{
		/// <inheritdoc />
		IEnumerable<string> IAllocationAwarenessSettings.Attributes { get; set; }

		/// <inheritdoc />
		IAllocationAttributes IAllocationAwarenessSettings.Forced { get; set; }

		/// <inheritdoc />
		public AllocationAwarenessSettingsDescriptor Attributes(IEnumerable<string> attributes) => Assign(attributes, (a, v) => a.Attributes = v);

		/// <inheritdoc />
		public AllocationAwarenessSettingsDescriptor Attributes(params string[] attributes) => Assign(attributes, (a, v) => a.Attributes = v);

		/// <inheritdoc />
		public AllocationAwarenessSettingsDescriptor Force(IAllocationAttributes forceValues) => Assign(forceValues, (a, v) => a.Forced = v);

		/// <inheritdoc />
		public AllocationAwarenessSettingsDescriptor Force(Func<AllocationAttributesDescriptor, IAllocationAttributes> selector) =>
			Assign(selector, (a, v) => a.Forced = v?.Invoke(new AllocationAttributesDescriptor()));
	}
}
