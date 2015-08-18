using System;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TimestampFieldMapping>))]
	public interface ITimestampFieldMapping : ISpecialField
	{
		[JsonProperty("enabled")]
		bool Enabled { get; set; }

		[JsonProperty("path")]
		FieldName Path { get; set; }

		[JsonProperty("format")]
		string Format { get; set; }

		[JsonProperty("default")]
		string Default { get; set; }

		[JsonProperty("ignore_missing")]
		bool? IgnoreMissing { get; set; }
	}

	public class TimestampFieldMapping : ITimestampFieldMapping
	{
		public bool Enabled { get; set; }

		public FieldName Path { get; set; }

		public string Format { get; set; }
		public string Default { get; set; }
		public bool? IgnoreMissing { get; set; }
	}


	public class TimestampFieldMappingDescriptor<T> : ITimestampFieldMapping
	{
		private ITimestampFieldMapping Self => this;

		bool ITimestampFieldMapping.Enabled { get; set;}

		FieldName ITimestampFieldMapping.Path { get; set;}

		string ITimestampFieldMapping.Format { get; set; }
		string ITimestampFieldMapping.Default { get; set; }
		bool? ITimestampFieldMapping.IgnoreMissing { get; set; }

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

		public TimestampFieldMappingDescriptor<T> Default(string defaultValue)
		{
			Self.Default = defaultValue;
			return this;
		}

		public TimestampFieldMappingDescriptor<T> IgnoreMissing(bool ignoreMissing = true)
		{
			Self.IgnoreMissing = ignoreMissing;
			return this;
		}

		
	}
}