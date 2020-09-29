// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
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

		public AppendProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public AppendProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		public AppendProcessorDescriptor<T> Value<TValue>(IEnumerable<TValue> values) => Assign(values, (a, v) => a.Value = v?.Cast<object>());

		public AppendProcessorDescriptor<T> Value<TValue>(params TValue[] values) => Assign(values, (a, v) =>
		{
			if (v?.Length == 1 && typeof(IEnumerable).IsAssignableFrom(typeof(TValue)) && typeof(TValue) != typeof(string))
				a.Value = (v.First() as IEnumerable)?.Cast<object>();
			else a.Value = v?.Cast<object>();
		});
	}
}
