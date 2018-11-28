using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// Sorts the elements of an array ascending or descending. Homogeneous arrays of numbers
	/// will be sorted numerically, while arrays of strings or heterogeneous arrays
	///  of strings and numbers will be sorted lexicographically.
	/// </summary>
	[InterfaceDataContract]
	[JsonFormatter(typeof(ProcessorFormatter<SortProcessor>))]
	public interface ISortProcessor : IProcessor
	{
		/// <summary>
		/// The field to be sorted
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The sort order to use. Default is ascending.
		/// </summary>
		[DataMember(Name ="order")]
		SortOrder? Order { get; set; }
	}

	public class SortProcessor : ProcessorBase, ISortProcessor
	{
		public Field Field { get; set; }

		public SortOrder? Order { get; set; }
		protected override string Name => "sort";
	}

	public class SortProcessorDescriptor<T>
		: ProcessorDescriptorBase<SortProcessorDescriptor<T>, ISortProcessor>, ISortProcessor
		where T : class
	{
		protected override string Name => "sort";

		Field ISortProcessor.Field { get; set; }
		SortOrder? ISortProcessor.Order { get; set; }

		public SortProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public SortProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public SortProcessorDescriptor<T> Order(SortOrder? order = SortOrder.Ascending) =>
			Assign(a => a.Order = order);
	}
}
