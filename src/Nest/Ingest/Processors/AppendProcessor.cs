using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAppendProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }
		[JsonProperty("values")]
		IEnumerable<object> Values { get; set; }
	}

	public class AppendProcessor : ProcessorBase, IAppendProcessor
	{
		protected override string Name => "append";
		public Field Field { get; set; }
		public IEnumerable<object> Values { get; set; }
	}

	public class AppendProcessorDescriptor<T> : ProcessorDescriptorBase<AppendProcessorDescriptor<T>, IAppendProcessor>, IAppendProcessor
		where T : class
	{
		protected override string Name => "append";
		Field IAppendProcessor.Field { get; set; }
		IEnumerable<object> IAppendProcessor.Values { get; set; }

		public AppendProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public AppendProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public AppendProcessorDescriptor<T> Terms<TValue>(IEnumerable<TValue> terms) => Assign(a => a.Values = terms?.Cast<object>());

		public AppendProcessorDescriptor<T> Terms<TValue>(params TValue[] terms) => Assign(a => {
			if(terms?.Length == 1 && typeof(IEnumerable).IsAssignableFrom(typeof(TValue)) && typeof(TValue) != typeof(string))
				a.Values = (terms.First() as IEnumerable)?.Cast<object>();
			else a.Values = terms?.Cast<object>();
		});
	}

}
