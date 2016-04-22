using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<MoreLikeThisQueryDescriptor<object>>))]
	public interface IMoreLikeThisQuery : IQuery
	{
		[JsonProperty(PropertyName = "fields")]
		Fields Fields { get; set; }

		[JsonProperty(PropertyName = "like")]
		IEnumerable<Like> Like { get; set; }

		[JsonProperty(PropertyName = "unlike")]
		IEnumerable<Like> Unlike { get; set; }

		[JsonProperty(PropertyName = "max_query_terms")]
		int? MaxQueryTerms { get; set; }

		[JsonProperty(PropertyName = "min_term_freq")]
		int? MinTermFrequency { get; set; }

		[JsonProperty(PropertyName = "min_doc_freq")]
		int? MinDocumentFrequency { get; set; }

		[JsonProperty(PropertyName = "max_doc_freq")]
		int? MaxDocumentFrequency { get; set; }

		[JsonProperty(PropertyName = "min_word_len")]
		int? MinWordLength { get; set; }

		[JsonProperty(PropertyName = "max_word_len")]
		int? MaxWordLength { get; set; }

		[JsonProperty(PropertyName = "stop_words")]
		StopWords StopWords { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string Analyzer { get; set; }

		[JsonProperty(PropertyName = "minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		[JsonProperty(PropertyName = "boost_terms")]
		double? BoostTerms { get; set; }

		[JsonProperty(PropertyName = "include")]
		bool? Include { get; set; }


	}

	public class MoreLikeThisQuery : QueryBase, IMoreLikeThisQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public Fields Fields { get; set; }
		public double? TermMatchPercentage { get; set; }
		public MinimumShouldMatch MinimumShouldMatch { get; set; }
		public StopWords StopWords { get; set; }
		public int? MinTermFrequency { get; set; }
		public int? MaxQueryTerms { get; set; }
		public int? MinDocumentFrequency { get; set; }
		public int? MaxDocumentFrequency { get; set; }
		public int? MinWordLength { get; set; }
		public int? MaxWordLength { get; set; }
		public double? BoostTerms { get; set; }
		public string Analyzer { get; set; }
		public bool? Include { get; set; }
		public IEnumerable<Like> Like { get; set; }
		public IEnumerable<Like> Unlike { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.MoreLikeThis = this;
		internal static bool IsConditionless(IMoreLikeThisQuery q) => q.Fields.IsConditionless() || (!q.Like.HasAny() || q.Like.All(Nest.Like.IsConditionless));
	}

	public class MoreLikeThisQueryDescriptor<T>
		: QueryDescriptorBase<MoreLikeThisQueryDescriptor<T>, IMoreLikeThisQuery>
		, IMoreLikeThisQuery where T : class
	{
		protected override bool Conditionless => MoreLikeThisQuery.IsConditionless(this);
		Fields IMoreLikeThisQuery.Fields { get; set; }
		MinimumShouldMatch IMoreLikeThisQuery.MinimumShouldMatch { get; set; }
		StopWords IMoreLikeThisQuery.StopWords { get; set; }
		int? IMoreLikeThisQuery.MinTermFrequency { get; set; }
		int? IMoreLikeThisQuery.MaxQueryTerms { get; set; }
		int? IMoreLikeThisQuery.MinDocumentFrequency { get; set; }
		int? IMoreLikeThisQuery.MaxDocumentFrequency { get; set; }
		int? IMoreLikeThisQuery.MinWordLength { get; set; }
		int? IMoreLikeThisQuery.MaxWordLength { get; set; }
		double? IMoreLikeThisQuery.BoostTerms { get; set; }
		string IMoreLikeThisQuery.Analyzer { get; set; }
		bool? IMoreLikeThisQuery.Include { get; set; }
		IEnumerable<Like> IMoreLikeThisQuery.Like { get; set; }
		IEnumerable<Like> IMoreLikeThisQuery.Unlike { get; set; }

		public MoreLikeThisQueryDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public MoreLikeThisQueryDescriptor<T> Fields(Fields fields) => Assign(a => a.Fields = fields);

		public MoreLikeThisQueryDescriptor<T> StopWords(IEnumerable<string> stopWords) =>
			Assign(a => a.StopWords = stopWords.ToListOrNullIfEmpty());

		public MoreLikeThisQueryDescriptor<T> StopWords(params string[] stopWords) =>
			Assign(a => a.StopWords = stopWords);

		public MoreLikeThisQueryDescriptor<T> StopWords(StopWords stopWords) =>
			Assign(a => a.StopWords = stopWords);

		public MoreLikeThisQueryDescriptor<T> MaxQueryTerms(int? maxQueryTerms) => Assign(a => a.MaxQueryTerms = maxQueryTerms);

		public MoreLikeThisQueryDescriptor<T> MinTermFrequency(int? minTermFrequency) => Assign(a => a.MinTermFrequency = minTermFrequency);

		public MoreLikeThisQueryDescriptor<T> MinDocumentFrequency(int? minDocumentFrequency) => Assign(a => a.MinDocumentFrequency = minDocumentFrequency);

		public MoreLikeThisQueryDescriptor<T> MaxDocumentFrequency(int? maxDocumentFrequency) => Assign(a => a.MaxDocumentFrequency = maxDocumentFrequency);

		public MoreLikeThisQueryDescriptor<T> MinWordLength(int? minWordLength) => Assign(a => a.MinWordLength = minWordLength);

		public MoreLikeThisQueryDescriptor<T> MaxWordLength(int? maxWordLength) => Assign(a => a.MaxWordLength = maxWordLength);

		public MoreLikeThisQueryDescriptor<T> BoostTerms(double? boostTerms) => Assign(a => a.BoostTerms = boostTerms);

		public MoreLikeThisQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minMatch) => Assign(a => a.MinimumShouldMatch = minMatch);

		public MoreLikeThisQueryDescriptor<T> Include(bool? include = true) => Assign(a => a.Include = include);

		public MoreLikeThisQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public MoreLikeThisQueryDescriptor<T> Like(Func<LikeDescriptor<T>, IPromise<List<Like>>> selector) =>
			Assign(a => a.Like = selector?.Invoke(new LikeDescriptor<T>())?.Value);

		public MoreLikeThisQueryDescriptor<T> Unlike(Func<LikeDescriptor<T>, IPromise<List<Like>>> selector) =>
			Assign(a => a.Unlike = selector?.Invoke(new LikeDescriptor<T>())?.Value);

	}
}
