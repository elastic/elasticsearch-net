using System;
using System.Collections.Generic;

namespace Nest
{
	public interface IAllocationAwarenessSettings
	{
		/// <summary>Determines which node attributes are taken into account when balancing shards across the shards<summary>
		IEnumerable<string> Attributes { get; set; }
		
		/// <summary>With forced awareness shard copies are NEVER allicated within the same attribute<summary>
		IAllocationAttributes Forced { get; set; }
	}

	public class AllocationAwarenessSettings : IAllocationAwarenessSettings
	{
		///<inheritdoc/>
		public IEnumerable<string> Attributes { get; set; }
		
		///<inheritdoc/>
		public IAllocationAttributes Forced { get; set; }

	}

	public class AllocationAwarenessSettingsDescriptor 
		: DescriptorBase<AllocationAwarenessSettingsDescriptor, IAllocationAwarenessSettings>, IAllocationAwarenessSettings
	{

		///<inheritdoc/>
		IEnumerable<string> IAllocationAwarenessSettings.Attributes { get; set; }
		
		///<inheritdoc/>
		IAllocationAttributes IAllocationAwarenessSettings.Forced { get; set; }

		///<inheritdoc/>
		public AllocationAwarenessSettingsDescriptor Attributes(IEnumerable<string> attributes) => Assign(a => a.Attributes = attributes);

		///<inheritdoc/>
		public AllocationAwarenessSettingsDescriptor Attributes(params string[] attributes) => Assign(a => a.Attributes = attributes);

		///<inheritdoc/>
		public AllocationAwarenessSettingsDescriptor Force(IAllocationAttributes forceValues) => Assign(a => a.Forced = forceValues);

		///<inheritdoc/>
		public AllocationAwarenessSettingsDescriptor Force(Func<AllocationAttributesDescriptor, IAllocationAttributes> selector) => 
			Assign(a => a.Forced = selector?.Invoke(new AllocationAttributesDescriptor()));

	}
}