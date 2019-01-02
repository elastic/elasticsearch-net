using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The GeoIP processor adds information about the geographical location of IP addresses,
	/// based on data from the Maxmind databases.
	/// This processor adds this information by default under the geoip field.
	/// The geoip processor can resolve both IPv4 and IPv6 addresses.
	/// </summary>
	/// <remarks>
	/// Requires the Ingest Geoip Processor Plugin to be installed on the cluster.
	/// </remarks>
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<GeoIpProcessor>))]
	public interface IGeoIpProcessor : IProcessor
	{
		[JsonProperty("database_file")]
		string DatabaseFile { get; set; }

		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// If `true` and `field` does not exist, the processor quietly exits without modifying the document
		/// </summary>
		[JsonProperty("ignore_missing")]
		bool? IgnoreMissing { get; set; }

		[JsonProperty("properties")]
		IEnumerable<string> Properties { get; set; }

		[JsonProperty("target_field")]
		Field TargetField { get; set; }
	}

	/// <summary>
	/// The GeoIP processor adds information about the geographical location of IP addresses,
	/// based on data from the Maxmind databases.
	/// This processor adds this information by default under the geoip field.
	/// The geoip processor can resolve both IPv4 and IPv6 addresses.
	/// </summary>
	/// <remarks>
	/// Requires the Ingest Geoip Processor Plugin to be installed on the cluster.
	/// </remarks>
	public class GeoIpProcessor : ProcessorBase, IGeoIpProcessor
	{
		public string DatabaseFile { get; set; }

		public Field Field { get; set; }

		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }

		public IEnumerable<string> Properties { get; set; }

		public Field TargetField { get; set; }
		protected override string Name => "geoip";
	}

	/// <summary>
	/// The GeoIP processor adds information about the geographical location of IP addresses,
	/// based on data from the Maxmind databases.
	/// This processor adds this information by default under the geoip field.
	/// The geoip processor can resolve both IPv4 and IPv6 addresses.
	/// </summary>
	/// <remarks>
	/// Requires the Ingest Geoip Processor Plugin to be installed on the cluster.
	/// </remarks>
	public class GeoIpProcessorDescriptor<T>
		: ProcessorDescriptorBase<GeoIpProcessorDescriptor<T>, IGeoIpProcessor>, IGeoIpProcessor
		where T : class
	{
		protected override string Name => "geoip";
		string IGeoIpProcessor.DatabaseFile { get; set; }

		Field IGeoIpProcessor.Field { get; set; }
		bool? IGeoIpProcessor.IgnoreMissing { get; set; }
		IEnumerable<string> IGeoIpProcessor.Properties { get; set; }
		Field IGeoIpProcessor.TargetField { get; set; }

		public GeoIpProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public GeoIpProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		/// <inheritdoc />
		public GeoIpProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(a => a.IgnoreMissing = ignoreMissing);

		public GeoIpProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		public GeoIpProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.TargetField = objectPath);

		public GeoIpProcessorDescriptor<T> DatabaseFile(string file) => Assign(a => a.DatabaseFile = file);

		public GeoIpProcessorDescriptor<T> Properties(IEnumerable<string> properties) => Assign(a => a.Properties = properties);

		public GeoIpProcessorDescriptor<T> Properties(params string[] properties) => Assign(a => a.Properties = properties);
	}
}
