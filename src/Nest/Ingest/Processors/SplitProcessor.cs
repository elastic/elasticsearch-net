using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Splits a field into an array using a separator character. Only works on string fields
	/// </summary>
	[InterfaceDataContract]
	public interface ISplitProcessor : IProcessor
	{
		/// <summary>
		/// The field to split
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field" /> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[DataMember(Name = "ignore_missing")]
		bool? IgnoreMissing { get; set; }

		/// <summary>
		/// A regex which matches the separator, eg , or \s+
		/// </summary>
		[DataMember(Name ="separator")]
		string Separator { get; set; }

		/// <summary>
		/// The field to assign the split value to, by default field is updated in-place
		/// </summary>
		[DataMember(Name = "target_field")]
		Field TargetField { get; set; }
	}

	/// <inheritdoc cref="SplitProcessor" />
	public class SplitProcessor : ProcessorBase, ISplitProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }

		/// <inheritdoc />
		public string Separator { get; set; }

		/// <inheritdoc />
		public Field TargetField { get; set; }
		protected override string Name => "split";
	}

	/// <inheritdoc cref="SplitProcessor" />
	public class SplitProcessorDescriptor<T>
		: ProcessorDescriptorBase<SplitProcessorDescriptor<T>, ISplitProcessor>, ISplitProcessor
		where T : class
	{
		protected override string Name => "split";

		Field ISplitProcessor.Field { get; set; }
		bool? ISplitProcessor.IgnoreMissing { get; set; }
		string ISplitProcessor.Separator { get; set; }
		Field ISplitProcessor.TargetField { get; set; }

		/// <inheritdoc cref="SplitProcessor.Field" />
		public SplitProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="SplitProcessor.Field" />
		public SplitProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="SplitProcessor.TargetField" />
		public SplitProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="SplitProcessor.TargetField" />
		public SplitProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="SplitProcessor.Separator" />
		public SplitProcessorDescriptor<T> Separator(string separator) => Assign(separator, (a, v) => a.Separator = v);

		/// <inheritdoc cref="SplitProcessor.IgnoreMissing" />
		public SplitProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);
	}
}
