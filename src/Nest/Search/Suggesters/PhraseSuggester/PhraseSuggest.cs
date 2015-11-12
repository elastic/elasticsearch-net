using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PhraseSuggester>))]
	public interface IPhraseSuggester : ISuggester
	{
		[JsonProperty(PropertyName = "gram_size")]
		int? GramSize { get; set; }

		[JsonProperty(PropertyName = "real_word_error_likelihood")]
		decimal? RealWordErrorLikelihood { get; set; }

		[JsonProperty(PropertyName = "confidence")]
		decimal? Confidence { get; set; }

		[JsonProperty(PropertyName = "max_errors")]
		decimal? MaxErrors { get; set; }

		[JsonProperty(PropertyName = "separator")]
		char? Separator { get; set; }

		[JsonProperty(PropertyName = "direct_generator")]
		IEnumerable<IDirectGenerator> DirectGenerator { get; set; }

		[JsonProperty(PropertyName = "highlight")]
		IPhraseSuggestHighlight Highlight { get; set; }

		[JsonProperty(PropertyName = "collate")]
		IPhraseSuggestCollate Collate { get; set; }
	}

	public class PhraseSuggester : Suggester, IPhraseSuggester
	{
		public int? GramSize { get; set; }
		public decimal? RealWordErrorLikelihood { get; set; }
		public decimal? Confidence { get; set; }
		public decimal? MaxErrors { get; set; }
		public char? Separator { get; set; }
		public IEnumerable<IDirectGenerator> DirectGenerator { get; set; }
		public IPhraseSuggestHighlight Highlight { get; set; }
		public IPhraseSuggestCollate Collate { get; set; }
	}

	public class PhraseSuggestDescriptor<T> : BaseSuggestDescriptor<T>, IPhraseSuggester where T : class
	{
		protected IPhraseSuggester Self => this;

		int? IPhraseSuggester.GramSize { get; set; }

		decimal? IPhraseSuggester.RealWordErrorLikelihood { get; set; }

		decimal? IPhraseSuggester.Confidence { get; set; }

		decimal? IPhraseSuggester.MaxErrors { get; set; }

		char? IPhraseSuggester.Separator { get; set; }

		IEnumerable<IDirectGenerator> IPhraseSuggester.DirectGenerator { get; set; }

		IPhraseSuggestHighlight IPhraseSuggester.Highlight { get; set; }

		IPhraseSuggestCollate IPhraseSuggester.Collate { get; set; }

		public PhraseSuggestDescriptor<T> Text(string text)
		{
			Self.Text = text;
			return this;
		}

		public PhraseSuggestDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public PhraseSuggestDescriptor<T> Field(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public PhraseSuggestDescriptor<T> Analyzer(string analyzer)
		{
			Self.Analyzer = analyzer;
			return this;
		}

		public PhraseSuggestDescriptor<T> Size(int size)
		{
			Self.Size = size;
			return this;
		}

		public PhraseSuggestDescriptor<T> ShardSize(int size)
		{
			Self.ShardSize = size;
			return this;
		}

		public PhraseSuggestDescriptor<T> GramSize(int gramSize)
		{
			Self.GramSize = gramSize;
			return this;
		}

		public PhraseSuggestDescriptor<T> Confidence(decimal confidence)
		{
			Self.Confidence = confidence;
			return this;
		}

		public PhraseSuggestDescriptor<T> MaxErrors(decimal maxErrors)
		{
			Self.MaxErrors = maxErrors;
			return this;
		}

		public PhraseSuggestDescriptor<T> Separator(char separator)
		{
			Self.Separator = separator;
			return this;
		}

		public PhraseSuggestDescriptor<T> DirectGenerator(params Func<DirectGeneratorDescriptor<T>, DirectGeneratorDescriptor<T>>[] generators)
		{
			Self.DirectGenerator = generators.Select(g => g(new DirectGeneratorDescriptor<T>())).ToList();
			return this;
		}

		public PhraseSuggestDescriptor<T> Highlight(Func<PhraseSuggestHighlightDescriptor, PhraseSuggestHighlightDescriptor> highlightDescriptor)
		{
			var selector = highlightDescriptor(new PhraseSuggestHighlightDescriptor());
			Self.Highlight = selector.Highlight;
			return this;
		}

		public PhraseSuggestDescriptor<T> Collate(Func<PhraseSuggestCollateDescriptor<T>, PhraseSuggestCollateDescriptor<T>> collateDescriptor)
		{
			var selector = collateDescriptor(new PhraseSuggestCollateDescriptor<T>());
			Self.Collate = selector.Collate;
			return this;
		}
	}
}
