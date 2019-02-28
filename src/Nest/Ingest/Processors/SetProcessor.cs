using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface ISetProcessor : IProcessor
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="value")]
		[JsonFormatter(typeof(SourceWriteFormatter<>))]
		object Value { get; set; }
	}

	public class SetProcessor : ProcessorBase, ISetProcessor
	{
		public Field Field { get; set; }
		public object Value { get; set; }
		protected override string Name => "set";
	}

	public class SetProcessorDescriptor<T> : ProcessorDescriptorBase<SetProcessorDescriptor<T>, ISetProcessor>, ISetProcessor
		where T : class
	{
		protected override string Name => "set";
		Field ISetProcessor.Field { get; set; }
		object ISetProcessor.Value { get; set; }

		public SetProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public SetProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public SetProcessorDescriptor<T> Value<TValue>(TValue value) => Assign(a => a.Value = value);
	}
}
