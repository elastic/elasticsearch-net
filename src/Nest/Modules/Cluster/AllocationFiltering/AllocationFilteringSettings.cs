// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
