using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(SuggestContextJsonConverter))]
	public interface ISuggestContext
	{
		[JsonProperty("name")]
		string Name { get; set; }

		[JsonProperty("path")]
		Field Path { get; set; }

		[JsonProperty("type")]
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
		protected abstract string Type { get; }
		string ISuggestContext.Name { get; set; }
		Field ISuggestContext.Path { get; set; }
		string ISuggestContext.Type => Type;

		public TDescriptor Name(string name) => Assign(a => a.Name = name);

		public TDescriptor Path(Field field) => Assign(a => a.Path = field);

		public TDescriptor Path(Expression<Func<T, object>> objectPath) => Assign(a => a.Path = objectPath);
	}
}
