using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(ProcessorFormatter<AppendProcessor>))]
	public interface IAppendProcessor : IProcessor
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="value")]
		IEnumerable<object> Value { get; set; }
	}

	public class AppendProcessor : ProcessorBase, IAppendProcessor
	{
		public Field Field { get; set; }
		public IEnumerable<object> Value { get; set; }
		protected override string Name => "append";
	}

	public class AppendProcessorDescriptor<T> : ProcessorDescriptorBase<AppendProcessorDescriptor<T>, IAppendProcessor>, IAppendProcessor
		where T : class
	{
		protected override string Name => "append";
		Field IAppendProcessor.Field { get; set; }
		IEnumerable<object> IAppendProcessor.Value { get; set; }

		public AppendProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public AppendProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public AppendProcessorDescriptor<T> Value<TValue>(IEnumerable<TValue> values) => Assign(a => a.Value = values?.Cast<object>());

		public AppendProcessorDescriptor<T> Value<TValue>(params TValue[] values) => Assign(a =>
		{
			if (values?.Length == 1 && typeof(IEnumerable).IsAssignableFrom(typeof(TValue)) && typeof(TValue) != typeof(string))
				a.Value = (values.First() as IEnumerable)?.Cast<object>();
			else a.Value = values?.Cast<object>();
		});
	}
}
