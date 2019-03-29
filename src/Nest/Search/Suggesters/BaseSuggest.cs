using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISuggester
	{
		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		/// <summary>
		/// The name of the field on which to run the query
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// The number of suggestions to return. Defaults to 5
		/// </summary>
		[JsonProperty("size")]
		int? Size { get; set; }
	}

	public abstract class SuggesterBase : ISuggester
	{
		public string Analyzer { get; set; }
		public Field Field { get; set; }
		public int? Size { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public abstract class SuggestDescriptorBase<TDescriptor, TInterface, T> : DescriptorBase<TDescriptor, TInterface>, ISuggester
		where TDescriptor : SuggestDescriptorBase<TDescriptor, TInterface, T>, TInterface, ISuggester
		where TInterface : class, ISuggester
	{
		string ISuggester.Analyzer { get; set; }
		Field ISuggester.Field { get; set; }

		int? ISuggester.Size { get; set; }

		public TDescriptor Size(int? size) => Assign(size, (a, v) => a.Size = v);

		public TDescriptor Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		public TDescriptor Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public TDescriptor Field(Expression<Func<T, object>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);
	}
}
