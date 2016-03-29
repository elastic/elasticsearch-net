using System;

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
		IIndicesRecoverySettings RecoverySettings { get; set; }

	}

	///<inheritdoc/>
	public class IndicesModuleSettings : IIndicesModuleSettings
	{
		///<inheritdoc/>
		public string QeueriesCacheSize { get; set; }

		///<inheritdoc/>
		public ICircuitBreakerSettings CircuitBreakerSettings { get; set; }

		///<inheritdoc/>
		public FielddataSettings FielddataSettings { get; set; }

		///<inheritdoc/>
		public IIndicesRecoverySettings RecoverySettings { get; set; }

	}

	///<inheritdoc/>
	public class IndicesModuleSettingsDescriptor : DescriptorBase<IndicesModuleSettingsDescriptor, IIndicesModuleSettings>, IIndicesModuleSettings
	{

		///<inheritdoc/>
		string IIndicesModuleSettings.QeueriesCacheSize { get; set; }

		///<inheritdoc/>
		ICircuitBreakerSettings IIndicesModuleSettings.CircuitBreakerSettings { get; set; }

		///<inheritdoc/>
		FielddataSettings IIndicesModuleSettings.FielddataSettings { get; set; }

		///<inheritdoc/>
		IIndicesRecoverySettings IIndicesModuleSettings.RecoverySettings { get; set; }

		///<inheritdoc/>
		public IndicesModuleSettingsDescriptor CircuitBreaker(Func<CircuitBreakerSettingsDescriptor, ICircuitBreakerSettings> selector) => 
			Assign(a => a.CircuitBreakerSettings = selector?.Invoke(new CircuitBreakerSettingsDescriptor()));

		//fielddatasettings are static 

		///<inheritdoc/>
		public IndicesModuleSettingsDescriptor IndicesRecovery(Func<IndicesRecoverySettingsDescriptor, IIndicesRecoverySettings> selector) => 
			Assign(a => a.RecoverySettings = selector?.Invoke(new IndicesRecoverySettingsDescriptor()));

	}

}