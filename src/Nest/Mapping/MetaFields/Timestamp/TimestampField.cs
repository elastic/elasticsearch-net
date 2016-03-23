using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TimestampField>))]
	public interface ITimestampField : IFieldMapping
	{
		[JsonProperty("enabled")]
		bool Enabled { get; set; }

		[JsonProperty("path")]
		Field Path { get; set; }

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
		public Field Path { get; set; }
		public string Format { get; set; }
		public string Default { get; set; }
		public bool? IgnoreMissing { get; set; }
	}


	public class TimestampFieldDescriptor<T> : DescriptorBase<TimestampFieldDescriptor<T>, ITimestampField>, ITimestampField
	{
		bool ITimestampField.Enabled { get; set;}
		Field ITimestampField.Path { get; set;}
		string ITimestampField.Format { get; set; }
		string ITimestampField.Default { get; set; }
		bool? ITimestampField.IgnoreMissing { get; set; }

		public TimestampFieldDescriptor<T> Enabled(bool enabled = true) => Assign(a => a.Enabled = enabled);

		public TimestampFieldDescriptor<T> Path(Field path) => Assign(a => a.Path = path);

		public TimestampFieldDescriptor<T> Path(Expression<Func<T, object>> objectPath) => Assign(a => a.Path = objectPath);

		public TimestampFieldDescriptor<T> Format(string format) => Assign(a => a.Format = format);

		public TimestampFieldDescriptor<T> Default(string defaultValue) => Assign(a => a.Default = defaultValue);

		public TimestampFieldDescriptor<T> IgnoreMissing(bool ignoreMissing = true) => Assign(a => a.IgnoreMissing = ignoreMissing);
	}
}
