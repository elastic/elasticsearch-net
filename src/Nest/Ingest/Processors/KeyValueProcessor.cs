// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Processor to automatically parse messages (or specific event fields) which are of the key=value variety.
	/// </summary>
	[InterfaceDataContract]
	public interface IKeyValueProcessor : IProcessor
	{
		/// <summary> List of keys to exclude from document </summary>
		[DataMember(Name = "exclude_keys")]
		IEnumerable<string> ExcludeKeys { get; set; }
		/// <summary> The field to be parsed </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary> Regex pattern to use for splitting key-value pairs </summary>
		[DataMember(Name ="field_split")]
		string FieldSplit { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field" /> does not exist or is `null`,
		/// the processor quietly exits without modifying the document
		/// </summary>
		[DataMember(Name ="ignore_missing")]
		bool? IgnoreMissing { get; set; }

		/// <summary> List of keys to filter and insert into document. Defaults to including all keys </summary>
		[DataMember(Name ="include_keys")]
		IEnumerable<string> IncludeKeys { get; set; }

		/// <summary> Prefix to be added to extracted keys </summary>
		[DataMember(Name = "prefix")]
		string Prefix { get; set; }

		/// <summary> If true strip brackets (), &lt;&gt;, [] as well as quotes ' and " from extracted values </summary>
		[DataMember(Name ="strip_brackets")]
		bool? StripBrackets { get; set; }

		/// <summary> The field to insert the extracted keys into. Defaults to the root of the document </summary>
		[DataMember(Name ="target_field")]
		Field TargetField { get; set; }

		/// <summary> String of characters to trim from extracted keys </summary>
		[DataMember(Name ="trim_key")]
		string TrimKey { get; set; }

		/// <summary> String of characters to trim from extracted values </summary>
		[DataMember(Name ="trim_value")]
		string TrimValue { get; set; }

		/// <summary> Regex pattern to use for splitting the key from the value within a key-value pair </summary>
		[DataMember(Name ="value_split")]
		string ValueSplit { get; set; }
	}

	/// <inheritdoc cref="IKeyValueProcessor" />
	public class KeyValueProcessor : ProcessorBase, IKeyValueProcessor
	{
		/// <inheritdoc />
		public IEnumerable<string> ExcludeKeys { get; set; }
		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public string FieldSplit { get; set; }

		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> IncludeKeys { get; set; }

		/// <inheritdoc />
		public string Prefix { get; set; }

		/// <inheritdoc />
		public bool? StripBrackets { get; set; }

		/// <inheritdoc />
		public Field TargetField { get; set; }

		/// <inheritdoc />
		public string TrimKey { get; set; }

		/// <inheritdoc />
		public string TrimValue { get; set; }

		/// <inheritdoc />
		public string ValueSplit { get; set; }

		protected override string Name => "kv";
	}

	/// <inheritdoc cref="IKeyValueProcessor" />
	public class KeyValueProcessorDescriptor<T> : ProcessorDescriptorBase<KeyValueProcessorDescriptor<T>, IKeyValueProcessor>, IKeyValueProcessor
		where T : class
	{
		protected override string Name => "kv";
		IEnumerable<string> IKeyValueProcessor.ExcludeKeys { get; set; }
		Field IKeyValueProcessor.Field { get; set; }
		string IKeyValueProcessor.FieldSplit { get; set; }
		bool? IKeyValueProcessor.IgnoreMissing { get; set; }
		IEnumerable<string> IKeyValueProcessor.IncludeKeys { get; set; }
		string IKeyValueProcessor.Prefix { get; set; }
		bool? IKeyValueProcessor.StripBrackets { get; set; }

		Field IKeyValueProcessor.TargetField { get; set; }
		string IKeyValueProcessor.TrimKey { get; set; }
		string IKeyValueProcessor.TrimValue { get; set; }
		string IKeyValueProcessor.ValueSplit { get; set; }

		/// <inheritdoc cref="IKeyValueProcessor.Field" />
		public KeyValueProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IKeyValueProcessor.Field" />
		public KeyValueProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IKeyValueProcessor.TargetField" />
		public KeyValueProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IKeyValueProcessor.TargetField" />
		public KeyValueProcessorDescriptor<T> TargetField<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IKeyValueProcessor.FieldSplit" />
		public KeyValueProcessorDescriptor<T> FieldSplit(string split) => Assign(split, (a, v) => a.FieldSplit = v);

		/// <inheritdoc cref="IKeyValueProcessor.ValueSplit" />
		public KeyValueProcessorDescriptor<T> ValueSplit(string split) => Assign(split, (a, v) => a.ValueSplit = v);

		/// <inheritdoc cref="IKeyValueProcessor.Prefix" />
		public KeyValueProcessorDescriptor<T> Prefix(string prefix) => Assign(prefix, (a, v) => a.Prefix = v);

		/// <inheritdoc cref="IKeyValueProcessor.IgnoreMissing" />
		public KeyValueProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);

		/// <inheritdoc cref="IKeyValueProcessor.IncludeKeys" />
		public KeyValueProcessorDescriptor<T> IncludeKeys(IEnumerable<string> includeKeys) => Assign(includeKeys, (a, v) => a.IncludeKeys = v);

		/// <inheritdoc cref="IKeyValueProcessor.IncludeKeys" />
		public KeyValueProcessorDescriptor<T> IncludeKeys(params string[] includeKeys) => Assign(includeKeys, (a, v) => a.IncludeKeys = v);

		/// <inheritdoc cref="IKeyValueProcessor.ExcludeKeys" />
		public KeyValueProcessorDescriptor<T> ExcludeKeys(IEnumerable<string> excludeKeys) => Assign(excludeKeys, (a, v) => a.ExcludeKeys = v);

		/// <inheritdoc cref="IKeyValueProcessor.ExcludeKeys" />
		public KeyValueProcessorDescriptor<T> ExcludeKeys(params string[] excludeKeys) => Assign(excludeKeys, (a, v) => a.ExcludeKeys = v);

		/// <inheritdoc cref="IKeyValueProcessor.TrimKey" />
		public KeyValueProcessorDescriptor<T> TrimKey(string trimKeys) => Assign(trimKeys, (a, v) => a.TrimKey = v);

		/// <inheritdoc cref="IKeyValueProcessor.TrimValue" />
		public KeyValueProcessorDescriptor<T> TrimValue(string trimValues) => Assign(trimValues, (a, v) => a.TrimValue = v);

		/// <inheritdoc cref="IKeyValueProcessor.StripBrackets" />
		public KeyValueProcessorDescriptor<T> StripBrackets(bool? skip = true) => Assign(skip, (a, v) => a.StripBrackets = v);
	}
}
