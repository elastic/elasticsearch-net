using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<AppendProcessor>))]
	public interface IAppendProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("value")]
		IEnumerable<object> Value { get; set; }
	}

	public class AppendProcessor : ProcessorBase, IAppendProcessor
	{
		protected override string Name => "append";
		public Field Field { get; set; }
		public IEnumerable<object> Value { get; set; }
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

		public AppendProcessorDescriptor<T> Value<TValue>(params TValue[] values) => Assign(a => {
			if(values?.Length == 1 && typeof(IEnumerable).IsAssignableFrom(typeof(TValue)) && typeof(TValue) != typeof(string))
				a.Value = (values.First() as IEnumerable)?.Cast<object>();
			else a.Value = values?.Cast<object>();
		});
	}

}
