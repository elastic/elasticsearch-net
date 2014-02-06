using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class DirectGeneratorDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "field")]
		internal PropertyPathMarker _Field { get; set; }

		[JsonProperty(PropertyName = "size")]
		internal int? _Size { get; set; }

		[JsonProperty(PropertyName = "prefix_len")]
		internal int? _PrefixLen { get; set; }

		[JsonProperty(PropertyName = "suggest_mode")]
		internal string _SuggestMode { get; set; }

		[JsonProperty(PropertyName = "min_word_len")]
		internal int? _MinWordLen { get; set; }

		[JsonProperty(PropertyName = "max_edits")]
		internal int? _MaxEdits { get; set; }

		[JsonProperty(PropertyName = "max_inspections")]
		internal decimal? _MaxInspections { get; set; }

		[JsonProperty(PropertyName = "min_doc_freq")]
		internal decimal? _MinDocFrequency { get; set; }

		[JsonProperty(PropertyName = "max_term_freq")]
		internal decimal? _MaxTermFrequency { get; set; }

		[JsonProperty(PropertyName = "pre_filter")]
		internal string _PreFilter { get; set; }

		[JsonProperty(PropertyName = "post_filter")]
		internal string _PostFilter { get; set; }

		public DirectGeneratorDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}

		public DirectGeneratorDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			this._Field = objectPath;
			return this;
		}

		public DirectGeneratorDescriptor<T> Size(int size)
		{
			this._Size = size;
			return this;
		}

		public DirectGeneratorDescriptor<T> SuggestMode(SuggestMode mode)
		{
			this._SuggestMode = Enum.GetName(typeof(SuggestMode), mode).ToLower();
			return this;
		}

		public DirectGeneratorDescriptor<T> MinWordLength(int length)
		{
			this._MinWordLen = length;
			return this;
		}

		public DirectGeneratorDescriptor<T> PrefixLength(int length)
		{
			this._PrefixLen = length;
			return this;
		}

		public DirectGeneratorDescriptor<T> MaxEdits(int maxEdits)
		{
			this._MaxEdits = maxEdits;
			return this;
		}

		public DirectGeneratorDescriptor<T> MaxInspections(decimal maxInspections)
		{
			this._MaxInspections = maxInspections;
			return this;
		}

		public DirectGeneratorDescriptor<T> MinDocFrequency(decimal frequency)
		{
			this._MinDocFrequency = frequency;
			return this;
		}

		public DirectGeneratorDescriptor<T> MaxTermFrequency(decimal frequency)
		{
			this._MaxTermFrequency = frequency;
			return this;
		}

		public DirectGeneratorDescriptor<T> PreFilter(string analyzer)
		{
			this._PreFilter = analyzer;
			return this;
		}

		public DirectGeneratorDescriptor<T> PostFilter(string analyzer)
		{
			this._PostFilter = analyzer;
			return this;
		}

	}
}
