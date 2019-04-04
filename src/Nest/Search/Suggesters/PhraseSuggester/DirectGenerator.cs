using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(DirectGenerator))]
	public interface IDirectGenerator
	{
		[DataMember(Name = "field")]
		Field Field { get; set; }

		[DataMember(Name = "max_edits")]
		int? MaxEdits { get; set; }

		[DataMember(Name = "max_inspections")]
		decimal? MaxInspections { get; set; }

		[DataMember(Name = "max_term_freq")]
		decimal? MaxTermFrequency { get; set; }

		[DataMember(Name = "min_doc_freq")]
		decimal? MinDocFrequency { get; set; }

		[DataMember(Name = "min_word_length")]
		int? MinWordLength { get; set; }

		[DataMember(Name = "post_filter")]
		string PostFilter { get; set; }

		[DataMember(Name = "pre_filter")]
		string PreFilter { get; set; }

		[DataMember(Name = "prefix_length")]
		int? PrefixLength { get; set; }

		[DataMember(Name = "size")]
		int? Size { get; set; }

		[DataMember(Name = "suggest_mode")]

		SuggestMode? SuggestMode { get; set; }
	}

	public class DirectGenerator : IDirectGenerator
	{
		public Field Field { get; set; }
		public int? MaxEdits { get; set; }
		public decimal? MaxInspections { get; set; }
		public decimal? MaxTermFrequency { get; set; }
		public decimal? MinDocFrequency { get; set; }
		public int? MinWordLength { get; set; }
		public string PostFilter { get; set; }
		public string PreFilter { get; set; }
		public int? PrefixLength { get; set; }
		public int? Size { get; set; }
		public SuggestMode? SuggestMode { get; set; }
	}

	public class DirectGeneratorDescriptor<T> : DescriptorBase<DirectGeneratorDescriptor<T>, IDirectGenerator>, IDirectGenerator
		where T : class
	{
		Field IDirectGenerator.Field { get; set; }
		int? IDirectGenerator.MaxEdits { get; set; }
		decimal? IDirectGenerator.MaxInspections { get; set; }
		decimal? IDirectGenerator.MaxTermFrequency { get; set; }
		decimal? IDirectGenerator.MinDocFrequency { get; set; }
		int? IDirectGenerator.MinWordLength { get; set; }
		string IDirectGenerator.PostFilter { get; set; }
		string IDirectGenerator.PreFilter { get; set; }
		int? IDirectGenerator.PrefixLength { get; set; }
		int? IDirectGenerator.Size { get; set; }
		SuggestMode? IDirectGenerator.SuggestMode { get; set; }

		public DirectGeneratorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public DirectGeneratorDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);

		public DirectGeneratorDescriptor<T> Size(int? size) => Assign(size, (a, v) => a.Size = v);

		public DirectGeneratorDescriptor<T> SuggestMode(SuggestMode? mode) => Assign(mode, (a, v) => a.SuggestMode = v);

		public DirectGeneratorDescriptor<T> MinWordLength(int? length) => Assign(length, (a, v) => a.MinWordLength = v);

		public DirectGeneratorDescriptor<T> PrefixLength(int? length) => Assign(length, (a, v) => a.PrefixLength = v);

		public DirectGeneratorDescriptor<T> MaxEdits(int? maxEdits) => Assign(maxEdits, (a, v) => a.MaxEdits = v);

		public DirectGeneratorDescriptor<T> MaxInspections(decimal? maxInspections) => Assign(maxInspections, (a, v) => a.MaxInspections = v);

		public DirectGeneratorDescriptor<T> MinDocFrequency(decimal? frequency) => Assign(frequency, (a, v) => a.MinDocFrequency = v);

		public DirectGeneratorDescriptor<T> MaxTermFrequency(decimal? frequency) => Assign(frequency, (a, v) => a.MaxTermFrequency = v);

		public DirectGeneratorDescriptor<T> PreFilter(string preFilter) => Assign(preFilter, (a, v) => a.PreFilter = v);

		public DirectGeneratorDescriptor<T> PostFilter(string postFilter) => Assign(postFilter, (a, v) => a.PostFilter = v);
	}
}
