using System;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<TimestampField>))]
	public interface ITimestampField : ISpecialField
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

	public class TimestampField : ITimestampField
	{
		public bool Enabled { get; set; }

		public FieldName Path { get; set; }

		public string Format { get; set; }
		public string Default { get; set; }
		public bool? IgnoreMissing { get; set; }
	}


	public class TimestampFieldDescriptor<T> : ITimestampField
	{
		private ITimestampField Self => this;

		bool ITimestampField.Enabled { get; set;}

		FieldName ITimestampField.Path { get; set;}

		string ITimestampField.Format { get; set; }
		string ITimestampField.Default { get; set; }
		bool? ITimestampField.IgnoreMissing { get; set; }

		public TimestampFieldDescriptor<T> Enabled(bool enabled = true)
		{
			Self.Enabled = enabled;
			return this;
		}
		public TimestampFieldDescriptor<T> Path(string path)
		{
			Self.Path = path;
			return this;
		}
		public TimestampFieldDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.Path = objectPath;
			return this;
		}

		public TimestampFieldDescriptor<T> Format(string format)
		{
			Self.Format = format;
			return this;
		}

		public TimestampFieldDescriptor<T> Default(string defaultValue)
		{
			Self.Default = defaultValue;
			return this;
		}

		public TimestampFieldDescriptor<T> IgnoreMissing(bool ignoreMissing = true)
		{
			Self.IgnoreMissing = ignoreMissing;
			return this;
		}

		
	}
}