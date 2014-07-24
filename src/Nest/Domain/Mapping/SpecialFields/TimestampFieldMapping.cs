using System;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<TimestampFieldMapping>))]
	public interface ITimestampFieldMapping : ISpecialField
	{
		[JsonProperty("enabled")]
		bool Enabled { get; set; }

		[JsonProperty("path")]
		PropertyPathMarker Path { get; set; }

		[JsonProperty("format")]
		string Format { get; set; }
	}

	public class TimestampFieldMapping : ITimestampFieldMapping
	{
		public bool Enabled { get; set; }

		public PropertyPathMarker Path { get; set; }

		public string Format { get; set; }
	}


	public class TimestampFieldMappingDescriptor<T> : ITimestampFieldMapping
	{
		private ITimestampFieldMapping Self { get { return this; } }

		bool ITimestampFieldMapping.Enabled { get; set;}

		PropertyPathMarker ITimestampFieldMapping.Path { get; set;}

		string ITimestampFieldMapping.Format { get; set; }

		public TimestampFieldMappingDescriptor<T> Enabled(bool enabled = true)
		{
			Self.Enabled = enabled;
			return this;
		}
		public TimestampFieldMappingDescriptor<T> Path(string path)
		{
			Self.Path = path;
			return this;
		}
		public TimestampFieldMappingDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.Path = objectPath;
			return this;
		}
		public TimestampFieldMappingDescriptor<T> Format(string format)
		{
			Self.Format = format;
			return this;
		}

		
	}
}