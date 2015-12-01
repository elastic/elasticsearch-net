using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	[JsonObject]
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

	public abstract class SuggestContextBaseDescriptor<TDescriptor, TInterface, T> : DescriptorBase<TDescriptor, TInterface>, ISuggestContext
		where TDescriptor : SuggestContextBaseDescriptor<TDescriptor, TInterface, T>, TInterface, ISuggestContext
		where TInterface : class, ISuggestContext
	{
		protected abstract string Type { get; }
		string ISuggestContext.Type => this.Type;
		Field ISuggestContext.Path { get; set; }

		public TDescriptor Field(Field field) => Assign(a => a.Path = field);

		public TDescriptor Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Path = objectPath);
	}


}
