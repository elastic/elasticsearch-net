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

		public SetProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public SetProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		public SetProcessorDescriptor<T> Value<TValue>(TValue value) => Assign(value, (a, v) => a.Value = v);
	}
}
