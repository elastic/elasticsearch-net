// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public interface ICircuitBreakerSettings
	{
		/// <summary></summary>
		string FielddataLimit { get; set; }

		/// <summary></summary>
		float? FielddataOverhead { get; set; }

		/// <summary></summary>
		string RequestLimit { get; set; }

		/// <summary></summary>
		float? RequestOverhead { get; set; }

		/// <summary></summary>
		string TotalLimit { get; set; }
	}

	public class CircuitBreakerSettings : ICircuitBreakerSettings
	{
		/// <inheritdoc />
		public string FielddataLimit { get; set; }

		/// <inheritdoc />
		public float? FielddataOverhead { get; set; }

		/// <inheritdoc />
		public string RequestLimit { get; set; }

		/// <inheritdoc />
		public float? RequestOverhead { get; set; }

		/// <inheritdoc />
		public string TotalLimit { get; set; }
	}

	public class CircuitBreakerSettingsDescriptor
		: DescriptorBase<CircuitBreakerSettingsDescriptor, ICircuitBreakerSettings>, ICircuitBreakerSettings
	{
		string ICircuitBreakerSettings.FielddataLimit { get; set; }
		float? ICircuitBreakerSettings.FielddataOverhead { get; set; }
		string ICircuitBreakerSettings.RequestLimit { get; set; }
		float? ICircuitBreakerSettings.RequestOverhead { get; set; }
		string ICircuitBreakerSettings.TotalLimit { get; set; }

		/// <inheritdoc />
		public CircuitBreakerSettingsDescriptor TotalLimit(string limit) => Assign(limit, (a, v) => a.TotalLimit = v);

		/// <inheritdoc />
		public CircuitBreakerSettingsDescriptor FielddataLimit(string limit) => Assign(limit, (a, v) => a.FielddataLimit = v);

		/// <inheritdoc />
		public CircuitBreakerSettingsDescriptor RequestLimit(string limit) => Assign(limit, (a, v) => a.RequestLimit = v);

		/// <inheritdoc />
		public CircuitBreakerSettingsDescriptor FielddataOverhead(float? overhead) => Assign(overhead, (a, v) => a.FielddataOverhead = v);

		/// <inheritdoc />
		public CircuitBreakerSettingsDescriptor RequestOverhead(float? overhead) => Assign(overhead, (a, v) => a.RequestOverhead = v);
	}
}
