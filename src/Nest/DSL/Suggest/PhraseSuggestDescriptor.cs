using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class PhraseSuggestDescriptor<T> : BaseSuggestDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "gram_size")]
		internal int? _GramSize { get; set; }

		[JsonProperty(PropertyName = "real_word_error_likelihood")]
		internal decimal? _RealWordErrorLikelyhood { get; set; }

		[JsonProperty(PropertyName = "confidence")]
		internal decimal? _Confidence { get; set; }

		[JsonProperty(PropertyName = "max_errors")]
		internal decimal? _MaxErrors { get; set; }

		[JsonProperty(PropertyName = "separator")]
		internal char? _Separator { get; set; }

		[JsonProperty(PropertyName = "direct_generator")]
		internal DirectGeneratorDescriptor<T>[] _DirectGenerator { get; set; }

		public PhraseSuggestDescriptor<T> Text(string text)
		{
			this._Text = text;
			return this;
		}

		public PhraseSuggestDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}

		public PhraseSuggestDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			this._Field = objectPath;
			return this;
		}

		public PhraseSuggestDescriptor<T> Analyzer(string analyzer)
		{
			this._Analyzer = analyzer;
			return this;
		}

		public PhraseSuggestDescriptor<T> Size(int size)
		{
			this._Size = size;
			return this;
		}

		public PhraseSuggestDescriptor<T> ShardSize(int size)
		{
			this._ShardSize = size;
			return this;
		}

		public PhraseSuggestDescriptor<T> GramSize(int gramSize)
		{
			this._GramSize = gramSize;
			return this;
		}

		public PhraseSuggestDescriptor<T> Confidence(decimal confidence)
		{
			this._Confidence = confidence;
			return this;
		}

		public PhraseSuggestDescriptor<T> MaxErrors(decimal maxErrors)
		{
			this._MaxErrors = maxErrors;
			return this;
		}

		public PhraseSuggestDescriptor<T> Separator(char separator)
		{
			this._Separator = separator;
			return this;
		}

		public PhraseSuggestDescriptor<T> DirectGenerator(params Func<DirectGeneratorDescriptor<T>, DirectGeneratorDescriptor<T>>[] generators)
		{
			List<DirectGeneratorDescriptor<T>> gens = new List<DirectGeneratorDescriptor<T>>();
			foreach (var generator in generators)
			{
				DirectGeneratorDescriptor<T> gen = new DirectGeneratorDescriptor<T>();
				gen = generator(gen);
				gens.Add(gen);
			}
			this._DirectGenerator = gens.ToArray();
			return this;
		}
	}
}
