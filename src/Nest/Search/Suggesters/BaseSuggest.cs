using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISuggester
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("size")]
		int? Size { get; set; }
}

	public abstract class SuggesterBase : ISuggester
	{
		public Field Field { get; set; }
		public string Analyzer { get; set; }
		public int? Size { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public abstract class SuggestDescriptorBase<TDescriptor, TInterface, T> : DescriptorBase<TDescriptor, TInterface>, ISuggester
		where TDescriptor : SuggestDescriptorBase<TDescriptor, TInterface, T>, TInterface, ISuggester
		where TInterface : class, ISuggester
	{
		Field ISuggester.Field { get; set; }

		string ISuggester.Analyzer { get; set; }

		int? ISuggester.Size { get; set; }

		public TDescriptor Size(int? size) => Assign(a => a.Size = size);

		public TDescriptor Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public TDescriptor Field(Field field) => Assign(a => a.Field = field);

		public TDescriptor Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);
	}
}
