using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<GeoIpProcessor>))]
	public interface IGeoIpProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("target_field")]
		Field TargetField { get; set; }

		[JsonProperty("database_file")]
		string DatabaseFile { get; set; }

		[JsonProperty("properties")]
		IEnumerable<string> Properties { get; set; }
	}

	public class GeoIpProcessor : ProcessorBase, IGeoIpProcessor
	{
		protected override string Name => "geoip";

		public Field Field { get; set; }

		public Field TargetField { get; set; }

		public string DatabaseFile { get; set; }

		public IEnumerable<string> Properties { get; set; }
	}

	public class GeoIpProcessorDescriptor<T>
	: ProcessorDescriptorBase<GeoIpProcessorDescriptor<T>, IGeoIpProcessor>, IGeoIpProcessor
	where T : class
	{
		protected override string Name => "geoip";

		Field IGeoIpProcessor.Field { get; set; }
		Field IGeoIpProcessor.TargetField { get; set; }
		string IGeoIpProcessor.DatabaseFile { get; set; }
		IEnumerable<string> IGeoIpProcessor.Properties { get; set; }


		public GeoIpProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public GeoIpProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public GeoIpProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		public GeoIpProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.TargetField = objectPath);

		public GeoIpProcessorDescriptor<T> DatabaseFile(string file) => Assign(a => a.DatabaseFile = file);

		public GeoIpProcessorDescriptor<T> Properties(IEnumerable<string> properties) => Assign(a => a.Properties = properties);

		public GeoIpProcessorDescriptor<T> Properties(params string[] properties) => Assign(a => a.Properties = properties);
	}
}
