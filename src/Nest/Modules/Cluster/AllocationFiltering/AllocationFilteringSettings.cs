using System;

namespace Nest
{
	public interface IAllocationFilteringSettings
	{
		/// <summary>Assign the index to a node whose {attribute} has at least one of the comma-separated values.<summary>
		IAllocationAttributes Include { get; set; }

		/// <summary>Assign the index to a node whose {attribute} has all of the comma-separated values.<summary>
		IAllocationAttributes Exclude { get; set; }

		/// <summary>Assign the index to a node whose {attribute} has none of the comma-separated values.<summary>
		IAllocationAttributes Require { get; set; }

	}

	public class AllocationFilteringSettings : IAllocationFilteringSettings
	{
		///<inheritdoc/>
		public IAllocationAttributes Include { get; set; }

		///<inheritdoc/>
		public IAllocationAttributes Exclude { get; set; }

		///<inheritdoc/>
		public IAllocationAttributes Require { get; set; }
		

	}

	public class AllocationFilteringSettingsDescriptor 
		: DescriptorBase<AllocationFilteringSettingsDescriptor, IAllocationFilteringSettings>, IAllocationFilteringSettings
	{

		///<inheritdoc/>
		IAllocationAttributes IAllocationFilteringSettings.Include { get; set; }

		///<inheritdoc/>
		IAllocationAttributes IAllocationFilteringSettings.Exclude { get; set; }

		///<inheritdoc/>
		IAllocationAttributes IAllocationFilteringSettings.Require { get; set; }
		
		///<inheritdoc/>
		public AllocationFilteringSettingsDescriptor Include(IAllocationAttributes include) => Assign(a => a.Include = include);

		///<inheritdoc/>
		public AllocationFilteringSettingsDescriptor Include(Func<AllocationAttributesDescriptor, IAllocationAttributes> selector) => 
			Assign(a => a.Include = selector?.Invoke(new AllocationAttributesDescriptor()));

		///<inheritdoc/>
		public AllocationFilteringSettingsDescriptor Exlude(IAllocationAttributes include) => Assign(a => a.Exclude = include);

		///<inheritdoc/>
		public AllocationFilteringSettingsDescriptor Exclude(Func<AllocationAttributesDescriptor, IAllocationAttributes> selector) => 
			Assign(a => a.Exclude = selector?.Invoke(new AllocationAttributesDescriptor()));

		///<inheritdoc/>
		public AllocationFilteringSettingsDescriptor Require(IAllocationAttributes include) => Assign(a => a.Require = include);

		///<inheritdoc/>
		public AllocationFilteringSettingsDescriptor Require(Func<AllocationAttributesDescriptor, IAllocationAttributes> selector) => 
			Assign(a => a.Require = selector?.Invoke(new AllocationAttributesDescriptor()));
	}
}