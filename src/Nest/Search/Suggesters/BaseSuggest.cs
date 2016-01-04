using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISuggester
	{
		string Text { get; set; }

		[JsonProperty(PropertyName = "field")]
		Field Field { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string Analyzer { get; set; }

		[JsonProperty(PropertyName = "size")]
		int? Size { get; set; }

		[JsonProperty(PropertyName = "shard_size")]
		int? ShardSize { get; set; }
	}

	public abstract class SuggesterBase : ISuggester
	{
		public string Text { get; set; }
		public Field Field { get; set; }
		public string Analyzer { get; set; }
		public int? Size { get; set; }
		public int? ShardSize { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public abstract class SuggesterBaseDescriptor<TDescriptor, TInterface, T> : DescriptorBase<TDescriptor, TInterface>, ISuggester
		where TDescriptor : SuggesterBaseDescriptor<TDescriptor, TInterface, T>, TInterface, ISuggester
		where TInterface : class, ISuggester
	{
		string ISuggester.Text { get; set; }

		Field ISuggester.Field { get; set; }

		string ISuggester.Analyzer { get; set; }

		int? ISuggester.Size { get; set; }

		int? ISuggester.ShardSize { get; set; }

		public TDescriptor Size(int? size) => Assign(a => a.Size = size);

		public TDescriptor ShardSize(int? size) => Assign(a => a.ShardSize = size);

		public TDescriptor Text(string text) => Assign(a => a.Text = text);

		public TDescriptor Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public TDescriptor Field(Field field) => Assign(a => a.Field = field);

		public TDescriptor Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);
	}
}
