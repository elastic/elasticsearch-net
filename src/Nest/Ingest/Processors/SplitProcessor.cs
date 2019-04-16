using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

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

		public SplitProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public SplitProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		public SplitProcessorDescriptor<T> Separator(string separator) => Assign(separator, (a, v) => a.Separator = v);
	}
}
