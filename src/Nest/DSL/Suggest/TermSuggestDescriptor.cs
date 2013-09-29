using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class TermSuggestDescriptor<T> : BaseSuggestDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "prefix_len")]
		internal int? _PrefixLen { get; set; }

		[JsonProperty(PropertyName = "suggest_mode")]
		internal string _SuggestMode { get; set; }

		[JsonProperty(PropertyName = "min_word_len")]
		internal int? _MinWordLen { get; set; }

		[JsonProperty(PropertyName = "max_edits")]
		internal int? _MaxEdits { get; set; }

		[JsonProperty(PropertyName = "max_inspections")]
		internal int? _MaxInspections { get; set; }

		[JsonProperty(PropertyName = "min_doc_freq")]
		internal decimal? _MinDocFrequency { get; set; }

		[JsonProperty(PropertyName = "max_term_freq")]
		internal decimal? _MaxTermFrequency { get; set; }

		public TermSuggestDescriptor<T> Text(string text)
		{
			this._Text = text;
			return this;
		}

		public TermSuggestDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}

		public TermSuggestDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			var fieldName = new PropertyNameResolver().Resolve(objectPath);
			return this.OnField(fieldName);
		}

		public TermSuggestDescriptor<T> Analyzer(string analyzer)
		{
			this._Analyzer = analyzer;
			return this;
		}

		public TermSuggestDescriptor<T> Size(int size)
		{
			this._Size = size;
			return this;
		}
		
		public TermSuggestDescriptor<T> ShardSize(int size)
		{
			this._ShardSize = size;
			return this;
		}

		public TermSuggestDescriptor<T> SuggestMode(SuggestMode mode)
		{
			this._SuggestMode = Enum.GetName(typeof(SuggestMode), mode).ToLower();
			return this;
		}

		public TermSuggestDescriptor<T> MinWordLength(int length)
		{
			this._MinWordLen = length;
			return this;
		}

		public TermSuggestDescriptor<T> PrefixLength(int length)
		{
			this._PrefixLen = length;
			return this;
		}

		public TermSuggestDescriptor<T> MaxEdits(int maxEdits)
		{
			this._MaxEdits = maxEdits;
			return this;
		}

		public TermSuggestDescriptor<T> MaxInspections(int maxInspections)
		{
			this._MaxInspections = maxInspections;
			return this;
		}

		public TermSuggestDescriptor<T> MinDocFrequency(decimal frequency)
		{
			this._MinDocFrequency = frequency;
			return this;
		}

		public TermSuggestDescriptor<T> MaxTermFrequency(decimal frequency)
		{
			this._MaxTermFrequency = frequency;
			return this;
		}

	}
}
