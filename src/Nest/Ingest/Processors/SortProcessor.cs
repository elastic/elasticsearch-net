using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Sorts the elements of an array ascending or descending. Homogeneous arrays of numbers
	/// will be sorted numerically, while arrays of strings or heterogeneous arrays
	///  of strings and numbers will be sorted lexicographically.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<SortProcessor>))]
	public interface ISortProcessor : IProcessor
	{
		/// <summary>
		/// The field to be sorted
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// The sort order to use. Default is ascending.
		/// </summary>
		[JsonProperty("order")]
		SortOrder? Order { get; set; }
	}

	public class SortProcessor : ProcessorBase, ISortProcessor
	{
		protected override string Name => "sort";

		public Field Field { get; set; }

		public SortOrder? Order { get; set; }
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
