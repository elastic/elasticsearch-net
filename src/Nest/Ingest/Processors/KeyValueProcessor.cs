using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<KeyValueProcessor>))]
	public interface IKeyValueProcessor : IProcessor
	{
		/// <summary>
		/// The field to be parsed
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// The field to insert the extracted keys into. Defaults to the root of the document
		/// </summary>
		[JsonProperty("target_field")]
		Field TargetField { get; set; }

		/// <summary>
		/// Regex pattern to use for splitting key-value pairs
		/// </summary>
		[JsonProperty("field_split")]
		string FieldSplit { get; set; }

		/// <summary>
		/// Regex pattern to use for splitting the key from the value within a key-value pair
		/// </summary>
		[JsonProperty("value_split")]
		string ValueSplit { get; set; }

		/// <summary>
		/// List of keys to filter and insert into document. Defaults to including all keys
		/// </summary>
		[JsonProperty("include_keys")]
		IEnumerable<string> IncludeKeys { get; set; }

		/// <summary>
		/// If `true` and `field` does not exist or is `null`, the processor quietly exits without modifying the document
		/// </summary>
		[JsonProperty("ignore_missing")]
		bool? IgnoreMissing { get; set; }
	}

	public class KeyValueProcessor : ProcessorBase, IKeyValueProcessor
	{
		protected override string Name => "kv";

		/// <inheritdoc/>
		public Field Field { get; set; }

		/// <inheritdoc/>
		public Field TargetField { get; set; }

		/// <inheritdoc/>
		public string FieldSplit { get; set; }

		/// <inheritdoc/>
		public string ValueSplit { get; set; }

		/// <inheritdoc/>
		public IEnumerable<string> IncludeKeys { get; set; }

		/// <inheritdoc/>
		public bool? IgnoreMissing { get; set; }
	}

	public class KeyValueProcessorDescriptor<T> : ProcessorDescriptorBase<KeyValueProcessorDescriptor<T>, IKeyValueProcessor>, IKeyValueProcessor
		where T : class
	{
		protected override string Name => "kv";
		Field IKeyValueProcessor.Field { get; set; }

		Field IKeyValueProcessor.TargetField { get; set; }
		string IKeyValueProcessor.FieldSplit { get; set; }
		string IKeyValueProcessor.ValueSplit { get; set; }
		IEnumerable<string> IKeyValueProcessor.IncludeKeys { get; set; }
		bool? IKeyValueProcessor.IgnoreMissing { get; set; }

		/// <inheritdoc/>
		public KeyValueProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc/>
		public KeyValueProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);

		/// <inheritdoc/>
		public KeyValueProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		/// <inheritdoc/>
		public KeyValueProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) => Assign(a => a.TargetField = objectPath);

		/// <inheritdoc/>
		public KeyValueProcessorDescriptor<T> FieldSplit(string split) => Assign(a => a.FieldSplit = split);

		/// <inheritdoc/>
		public KeyValueProcessorDescriptor<T> ValueSplit(string split) => Assign(a => a.ValueSplit = split);

		/// <inheritdoc/>
		public KeyValueProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(a => a.IgnoreMissing = ignoreMissing);

		/// <inheritdoc/>
		public KeyValueProcessorDescriptor<T> IncludeKeys(IEnumerable<string> includeKeys) => Assign(a => a.IncludeKeys = includeKeys);

		/// <inheritdoc/>
		public KeyValueProcessorDescriptor<T> IncludeKeys(params string[] includeKeys) => Assign(a => a.IncludeKeys = includeKeys);
	}

}
