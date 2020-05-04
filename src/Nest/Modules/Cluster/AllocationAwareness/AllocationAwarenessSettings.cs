// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
