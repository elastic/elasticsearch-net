// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.Utf8Json;


namespace Nest
{
	/// <summary>
	/// Converts a field in the currently ingested document to a different type,
	/// such as converting a string to an integer.
	/// If the field value is an array, all members will be converted.
	/// </summary>
	[InterfaceDataContract]
	public interface IConvertProcessor : IProcessor
	{
		/// <summary>
		/// The field whose value is to be converted
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The field to assign the converted value to, by default field is updated in-place
		/// </summary>
		[DataMember(Name ="target_field")]
		Field TargetField { get; set; }

		/// <summary>
		/// The type to convert the existing value to
		/// </summary>
		[DataMember(Name ="type")]
		ConvertProcessorType? Type { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field" /> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[DataMember(Name = "ignore_missing")]
		bool? IgnoreMissing { get; set; }
	}

	public class ConvertProcessor : ProcessorBase, IConvertProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public Field TargetField { get; set; }
		/// <inheritdoc />
		public ConvertProcessorType? Type { get; set; }
		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }
		/// <inheritdoc />
		protected override string Name => "convert";
	}

	public class ConvertProcessorDescriptor<T> : ProcessorDescriptorBase<ConvertProcessorDescriptor<T>, IConvertProcessor>, IConvertProcessor
		where T : class
	{
		protected override string Name => "convert";
		Field IConvertProcessor.Field { get; set; }
		Field IConvertProcessor.TargetField { get; set; }
		bool? IConvertProcessor.IgnoreMissing { get; set; }
		ConvertProcessorType? IConvertProcessor.Type { get; set; }

		/// <inheritdoc cref="IConvertProcessor.Field" />
		public ConvertProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IConvertProcessor.Field" />
		public ConvertProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IConvertProcessor.TargetField" />
		public ConvertProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IConvertProcessor.TargetField" />
		public ConvertProcessorDescriptor<T> TargetField<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IConvertProcessor.Type" />
		public ConvertProcessorDescriptor<T> Type(ConvertProcessorType? type) => Assign(type, (a, v) => a.Type = v);

		/// <inheritdoc cref="IConvertProcessor.IgnoreMissing" />
		public ConvertProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);
	}

	[StringEnum]
	public enum ConvertProcessorType
	{
		[EnumMember(Value = "integer")]
		Integer,

		[EnumMember(Value = "long")]
		Long,

		[EnumMember(Value = "float")]
		Float,

		[EnumMember(Value = "double")]
		Double,

		[EnumMember(Value = "string")]
		String,

		[EnumMember(Value = "boolean")]
		Boolean,

		[EnumMember(Value = "auto")]
		Auto
	}
}
