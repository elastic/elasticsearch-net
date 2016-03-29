using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(SuggestContextJsonConverter))]
	public interface ISuggestContext
	{
		[JsonProperty("name")]
		string Name { get; set; }

		[JsonProperty("type")]
		string Type { get; }

		[JsonProperty("path")]
		Field Path { get; set; }
	}

	public abstract class SuggestContextBase : ISuggestContext
	{
		public string Name { get; set; }
		public abstract string Type { get; }
		public Field Path { get; set; }
	}

	public abstract class SuggestContextDescriptorBase<TDescriptor, TInterface, T> : DescriptorBase<TDescriptor, TInterface>, ISuggestContext
		where TDescriptor : SuggestContextDescriptorBase<TDescriptor, TInterface, T>, TInterface, ISuggestContext
		where TInterface : class, ISuggestContext
	{
		string ISuggestContext.Name { get; set; }
		protected abstract string Type { get; }
		string ISuggestContext.Type => this.Type;
		Field ISuggestContext.Path { get; set; }

		public TDescriptor Name(string name) => Assign(a => a.Name = name);

		public TDescriptor Path(Field field) => Assign(a => a.Path = field);

		public TDescriptor Path(Expression<Func<T, object>> objectPath) => Assign(a => a.Path = objectPath);
	}
}
