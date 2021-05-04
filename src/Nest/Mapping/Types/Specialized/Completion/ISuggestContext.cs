// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(SuggestContextFormatter))]
	public interface ISuggestContext
	{
		[DataMember(Name = "name")]
		string Name { get; set; }

		[DataMember(Name = "path")]
		Field Path { get; set; }

		[DataMember(Name = "type")]
		string Type { get; }
	}

	public abstract class SuggestContextBase : ISuggestContext
	{
		public string Name { get; set; }
		public Field Path { get; set; }
		public abstract string Type { get; }
	}

	public abstract class SuggestContextDescriptorBase<TDescriptor, TInterface, T> : DescriptorBase<TDescriptor, TInterface>, ISuggestContext
		where TDescriptor : SuggestContextDescriptorBase<TDescriptor, TInterface, T>, TInterface, ISuggestContext
		where TInterface : class, ISuggestContext
	{
		[IgnoreDataMember]
		protected abstract string Type { get; }
		string ISuggestContext.Name { get; set; }
		Field ISuggestContext.Path { get; set; }
		string ISuggestContext.Type => Type;

		public TDescriptor Name(string name) => Assign(name, (a, v) => a.Name = v);

		public TDescriptor Path(Field field) => Assign(field, (a, v) => a.Path = v);

		public TDescriptor Path<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Path = v);
	}
}
