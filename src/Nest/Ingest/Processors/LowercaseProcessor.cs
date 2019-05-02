using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface ILowercaseProcessor : IProcessor
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }
	}

	public class LowercaseProcessor : ProcessorBase, ILowercaseProcessor
	{
		public Field Field { get; set; }
		protected override string Name => "lowercase";
	}

	public class LowercaseProcessorDescriptor<T>
		: ProcessorDescriptorBase<LowercaseProcessorDescriptor<T>, ILowercaseProcessor>, ILowercaseProcessor
		where T : class
	{
		protected override string Name => "lowercase";

		Field ILowercaseProcessor.Field { get; set; }

		public LowercaseProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public LowercaseProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);
	}
}
