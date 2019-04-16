using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	public interface ISuggester
	{
		[DataMember(Name ="analyzer")]
		string Analyzer { get; set; }

		/// <summary>
		/// The name of the field on which to run the query
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The number of suggestions to return. Defaults to 5
		/// </summary>
		[DataMember(Name ="size")]
		int? Size { get; set; }
	}

	public abstract class SuggesterBase : ISuggester
	{
		public string Analyzer { get; set; }
		public Field Field { get; set; }
		public int? Size { get; set; }
	}

	[DataContract]
	public abstract class SuggestDescriptorBase<TDescriptor, TInterface, T> : DescriptorBase<TDescriptor, TInterface>, ISuggester
		where TDescriptor : SuggestDescriptorBase<TDescriptor, TInterface, T>, TInterface, ISuggester
		where TInterface : class, ISuggester
	{
		string ISuggester.Analyzer { get; set; }
		Field ISuggester.Field { get; set; }

		int? ISuggester.Size { get; set; }

		public TDescriptor Size(int? size) => Assign(a => a.Size = size);

		public TDescriptor Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public TDescriptor Field(Field field) => Assign(a => a.Field = field);

		public TDescriptor Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);
	}
}
