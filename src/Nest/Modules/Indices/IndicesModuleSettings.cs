// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Nest
{
	/// <summary> Settings to control where, when, and how shards are allocated to nodes. </summary>
	public interface IIndicesModuleSettings
	{
		/// <summary></summary>
		ICircuitBreakerSettings CircuitBreakerSettings { get; set; }

		/// <summary></summary>
		FielddataSettings FielddataSettings { get; set; }

		/// <summary></summary>
		string QeueriesCacheSize { get; set; }

		/// <summary></summary>
		IIndicesRecoverySettings RecoverySettings { get; set; }
	}

	/// <inheritdoc />
	public class IndicesModuleSettings : IIndicesModuleSettings
	{
		/// <inheritdoc />
		public ICircuitBreakerSettings CircuitBreakerSettings { get; set; }

		/// <inheritdoc />
		public FielddataSettings FielddataSettings { get; set; }

		/// <inheritdoc />
		public string QeueriesCacheSize { get; set; }

		/// <inheritdoc />
		public IIndicesRecoverySettings RecoverySettings { get; set; }
	}

	/// <inheritdoc />
	public class IndicesModuleSettingsDescriptor : DescriptorBase<IndicesModuleSettingsDescriptor, IIndicesModuleSettings>, IIndicesModuleSettings
	{
		/// <inheritdoc />
		ICircuitBreakerSettings IIndicesModuleSettings.CircuitBreakerSettings { get; set; }

		/// <inheritdoc />
		FielddataSettings IIndicesModuleSettings.FielddataSettings { get; set; }

		/// <inheritdoc />
		string IIndicesModuleSettings.QeueriesCacheSize { get; set; }

		/// <inheritdoc />
		IIndicesRecoverySettings IIndicesModuleSettings.RecoverySettings { get; set; }

		/// <inheritdoc />
		public IndicesModuleSettingsDescriptor CircuitBreaker(Func<CircuitBreakerSettingsDescriptor, ICircuitBreakerSettings> selector) =>
			Assign(selector, (a, v) => a.CircuitBreakerSettings = v?.Invoke(new CircuitBreakerSettingsDescriptor()));

		//fielddatasettings are static

		/// <inheritdoc />
		public IndicesModuleSettingsDescriptor IndicesRecovery(Func<IndicesRecoverySettingsDescriptor, IIndicesRecoverySettings> selector) =>
			Assign(selector, (a, v) => a.RecoverySettings = v?.Invoke(new IndicesRecoverySettingsDescriptor()));
	}
}
