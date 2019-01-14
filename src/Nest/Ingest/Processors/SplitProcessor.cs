using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface ISplitProcessor : IProcessor
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="separator")]
		string Separator { get; set; }
	}

	public class SplitProcessor : ProcessorBase, ISplitProcessor
	{
		public Field Field { get; set; }

		public string Separator { get; set; }
		protected override string Name => "split";
	}

	public class SplitProcessorDescriptor<T>
		: ProcessorDescriptorBase<SplitProcessorDescriptor<T>, ISplitProcessor>, ISplitProcessor
		where T : class
	{
		protected override string Name => "split";

		Field ISplitProcessor.Field { get; set; }
		string ISplitProcessor.Separator { get; set; }

		public SplitProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public SplitProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public SplitProcessorDescriptor<T> Separator(string separator) => Assign(a => a.Separator = separator);
	}
}
