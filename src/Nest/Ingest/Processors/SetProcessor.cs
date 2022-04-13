// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Sets one field and associates it with the specified value.
	/// If the field already exists, its value will be replaced with the provided one.
	/// </summary>
	[InterfaceDataContract]
	public interface ISetProcessor : IProcessor
	{
		/// <summary>
		/// The field to insert, upsert, or update. Supports template snippets.
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The value to be set for the field. Supports template snippets.
		/// </summary>
		[DataMember(Name ="value")]
		[JsonFormatter(typeof(SourceWriteFormatter<>))]
		object Value { get; set; }

		/// <summary>
		/// If processor will update fields with pre-existing non-null-valued field.
		/// When set to false, such fields will not be touched.
		/// Default is <c>true</c>
		/// </summary>
		[DataMember(Name = "override")]
		bool? Override { get; set; }

		/// <summary>
		/// If <c>true</c> and value is a template snippet that evaluates to null or the
		/// empty string, the processor quietly exits without modifying the document.
		/// Defaults to <c>false</c>.
		/// <para />
		/// Valid in Elasticsearch 7.9.0+
		/// </summary>
		[DataMember(Name = "ignore_empty_value")]
		bool? IgnoreEmptyValue { get; set; }

		/// <summary>
		/// The origin field which will be copied to field, cannot set value simultaneously.
		/// </summary>
		[DataMember(Name = "copy_from")]
		Field CopyFrom { get; set; }
	}

	/// <inheritdoc cref="ISetProcessor" />
	public class SetProcessor : ProcessorBase, ISetProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public object Value { get; set; }
		/// <inheritdoc />
		public bool? Override { get; set; }
		/// <inheritdoc />
		public bool? IgnoreEmptyValue { get; set; }
		/// <inheritdoc />
		public Field CopyFrom { get; set; }

		protected override string Name => "set";
	}

	/// <inheritdoc cref="ISetProcessor" />
	public class SetProcessorDescriptor<T> : ProcessorDescriptorBase<SetProcessorDescriptor<T>, ISetProcessor>, ISetProcessor
		where T : class
	{
		protected override string Name => "set";
		Field ISetProcessor.Field { get; set; }
		object ISetProcessor.Value { get; set; }
		bool? ISetProcessor.Override { get; set; }
		bool? ISetProcessor.IgnoreEmptyValue { get; set; }
		Field ISetProcessor.CopyFrom { get; set; }

		/// <inheritdoc cref="ISetProcessor.Field"/>
		public SetProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISetProcessor.Field"/>
		public SetProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISetProcessor.Value"/>
		public SetProcessorDescriptor<T> Value<TValue>(TValue value) => Assign(value, (a, v) => a.Value = v);

		/// <inheritdoc cref="ISetProcessor.Override"/>
		public SetProcessorDescriptor<T> Override(bool? @override = true) => Assign(@override, (a, v) => a.Override = v);

		/// <inheritdoc cref="ISetProcessor.IgnoreEmptyValue"/>
		public SetProcessorDescriptor<T> IgnoreEmptyValue(bool? ignoreEmptyValue = true) =>
			Assign(ignoreEmptyValue, (a, v) => a.IgnoreEmptyValue = v);

		/// <inheritdoc cref="ISetProcessor.CopyFrom"/>
		public SetProcessorDescriptor<T> CopyFrom(Field field) => Assign(field, (a, v) => a.CopyFrom = v);

		/// <inheritdoc cref="ISetProcessor.CopyFrom"/>
		public SetProcessorDescriptor<T> CopyFrom<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.CopyFrom = v);
	}
}
