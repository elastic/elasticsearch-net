using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(SuggestContextJsonConverter))]
	public interface ISuggestContext
	{
		[JsonProperty("type")]
		string Type { get; }

		[JsonProperty("path")]
		Field Path { get; set; }
	}

	public abstract class SuggestContextBase : ISuggestContext
	{
		public abstract string Type { get; }
		public Field Path { get; set; }
	}

	public abstract class SuggestContextDescriptorBase<TDescriptor, TInterface, T> : DescriptorBase<TDescriptor, TInterface>, ISuggestContext
		where TDescriptor : SuggestContextDescriptorBase<TDescriptor, TInterface, T>, TInterface, ISuggestContext
		where TInterface : class, ISuggestContext
	{
		protected abstract string Type { get; }
		string ISuggestContext.Type => this.Type;
		Field ISuggestContext.Path { get; set; }

		public TDescriptor Field(Field field) => Assign(a => a.Path = field);

		public TDescriptor Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Path = objectPath);
	}
}
