using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nest
{
	/// <summary> Settings to control where, when, and how shards are allocated to nodes. </summary>
	public interface IIndicesModuleSettings
	{
		/// <summary><summary>
		string QeueriesCacheSize { get; set; }

		/// <summary><summary>
		ICircuitBreakerSettings CircuitBreakerSettings { get; set; }

		/// <summary><summary>
		FielddataSettings FielddataSettings { get; set; }

		/// <summary><summary>
		IndicesRecoverySettings RecoverySettings { get; set; }

	}

	///<inheritdoc/>
	public class IndicesModuleSettings : IIndicesModuleSettings
	{
	}

	///<inheritdoc/>
	public class IndicesModuleSettingsDescriptor : DescriptorBase<IndicesModuleSettingsDescriptor, IIndicesModuleSettings>, IIndicesModuleSettings
	{

		IAllocationAwarenessSettings IIndicesModuleSettings.AllocationAwareness { get; set; }

		///<inheritdoc/>
		public IndicesModuleSettingsDescriptor AllocationAwareness(Func<AllocationAwarenessSettings, IAllocationAwarenessSettings> selector) => 
			Assign(a => a.AllocationAwareness = selector?.Invoke(new AllocationAwarenessSettings()));


	}

}