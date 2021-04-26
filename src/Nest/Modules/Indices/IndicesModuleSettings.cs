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
