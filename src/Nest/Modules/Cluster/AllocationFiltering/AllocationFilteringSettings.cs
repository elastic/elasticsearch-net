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
	public interface IAllocationFilteringSettings
	{
		/// <summary>Assign the index to a node whose {attribute} has all of the comma-separated values.</summary>
		IAllocationAttributes Exclude { get; set; }

		/// <summary>Assign the index to a node whose {attribute} has at least one of the comma-separated values.</summary>
		IAllocationAttributes Include { get; set; }

		/// <summary>Assign the index to a node whose {attribute} has none of the comma-separated values.</summary>
		IAllocationAttributes Require { get; set; }
	}

	public class AllocationFilteringSettings : IAllocationFilteringSettings
	{
		/// <inheritdoc />
		public IAllocationAttributes Exclude { get; set; }

		/// <inheritdoc />
		public IAllocationAttributes Include { get; set; }

		/// <inheritdoc />
		public IAllocationAttributes Require { get; set; }
	}

	public class AllocationFilteringSettingsDescriptor
		: DescriptorBase<AllocationFilteringSettingsDescriptor, IAllocationFilteringSettings>, IAllocationFilteringSettings
	{
		/// <inheritdoc />
		IAllocationAttributes IAllocationFilteringSettings.Exclude { get; set; }

		/// <inheritdoc />
		IAllocationAttributes IAllocationFilteringSettings.Include { get; set; }

		/// <inheritdoc />
		IAllocationAttributes IAllocationFilteringSettings.Require { get; set; }

		/// <inheritdoc />
		public AllocationFilteringSettingsDescriptor Include(IAllocationAttributes include) => Assign(include, (a, v) => a.Include = v);

		/// <inheritdoc />
		public AllocationFilteringSettingsDescriptor Include(Func<AllocationAttributesDescriptor, IAllocationAttributes> selector) =>
			Assign(selector, (a, v) => a.Include = v?.Invoke(new AllocationAttributesDescriptor()));

		/// <inheritdoc />
		public AllocationFilteringSettingsDescriptor Exlude(IAllocationAttributes include) => Assign(include, (a, v) => a.Exclude = v);

		/// <inheritdoc />
		public AllocationFilteringSettingsDescriptor Exclude(Func<AllocationAttributesDescriptor, IAllocationAttributes> selector) =>
			Assign(selector, (a, v) => a.Exclude = v?.Invoke(new AllocationAttributesDescriptor()));

		/// <inheritdoc />
		public AllocationFilteringSettingsDescriptor Require(IAllocationAttributes include) => Assign(include, (a, v) => a.Require = v);

		/// <inheritdoc />
		public AllocationFilteringSettingsDescriptor Require(Func<AllocationAttributesDescriptor, IAllocationAttributes> selector) =>
			Assign(selector, (a, v) => a.Require = v?.Invoke(new AllocationAttributesDescriptor()));
	}
}
