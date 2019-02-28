using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(MoreLikeThisQuery))]
	public interface IMoreLikeThisQuery : IQuery
	{
		[DataMember(Name = "analyzer")]
		string Analyzer { get; set; }

		[DataMember(Name = "boost_terms")]
		double? BoostTerms { get; set; }

		[DataMember(Name = "fields")]
		Fields Fields { get; set; }

		[DataMember(Name = "include")]
		bool? Include { get; set; }

		[DataMember(Name = "like")]
		IEnumerable<Like> Like { get; set; }

		[DataMember(Name = "max_doc_freq")]
		int? MaxDocumentFrequency { get; set; }

		[DataMember(Name = "max_query_terms")]
		int? MaxQueryTerms { get; set; }

		[DataMember(Name = "max_word_length")]
		int? MaxWordLength { get; set; }

		[DataMember(Name = "min_doc_freq")]
		int? MinDocumentFrequency { get; set; }

		[DataMember(Name = "minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		[DataMember(Name = "min_term_freq")]
		int? MinTermFrequency { get; set; }

		[DataMember(Name = "min_word_length")]
		int? MinWordLength { get; set; }

		/// <summary>
		/// Provide a different analyzer than the one at the field.
		/// This is useful in order to generate term vectors in any fashion, especially when using artificial documents.
		/// </summary>
		[DataMember(Name = "per_field_analyzer")]
		IPerFieldAnalyzer PerFieldAnalyzer { get; set; }

		[DataMember(Name = "routing")]
		Routing Routing { get; set; }

		[DataMember(Name = "stop_words")]
		StopWords StopWords { get; set; }

		[DataMember(Name = "unlike")]
		IEnumerable<Like> Unlike { get; set; }

		[DataMember(Name = "version")]
		long? Version { get; set; }

		[DataMember(Name = "version_type")]
		VersionType? VersionType { get; set; }
	}

	public class MoreLikeThisQuery : QueryBase, IMoreLikeThisQuery
	{
		public string Analyzer { get; set; }
		public double? BoostTerms { get; set; }
		public Fields Fields { get; set; }
		public bool? Include { get; set; }
		public IEnumerable<Like> Like { get; set; }
		public int? MaxDocumentFrequency { get; set; }
		public int? MaxQueryTerms { get; set; }
		public int? MaxWordLength { get; set; }
		public int? MinDocumentFrequency { get; set; }
		public MinimumShouldMatch MinimumShouldMatch { get; set; }
		public int? MinTermFrequency { get; set; }
		public int? MinWordLength { get; set; }

		/// <inheritdoc />
		public IPerFieldAnalyzer PerFieldAnalyzer { get; set; }

		public Routing Routing { get; set; }
		public StopWords StopWords { get; set; }
		public double? TermMatchPercentage { get; set; }
		public IEnumerable<Like> Unlike { get; set; }

		public long? Version { get; set; }
		public VersionType? VersionType { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.MoreLikeThis = this;

		internal static bool IsConditionless(IMoreLikeThisQuery q) =>
			q.Fields.IsConditionless() || !q.Like.HasAny() || q.Like.All(Nest.Like.IsConditionless);
	}

	public class MoreLikeThisQueryDescriptor<T>
		: QueryDescriptorBase<MoreLikeThisQueryDescriptor<T>, IMoreLikeThisQuery>
			, IMoreLikeThisQuery where T : class
	{
		protected override bool Conditionless => MoreLikeThisQuery.IsConditionless(this);
		string IMoreLikeThisQuery.Analyzer { get; set; }
		double? IMoreLikeThisQuery.BoostTerms { get; set; }
		Fields IMoreLikeThisQuery.Fields { get; set; }
		bool? IMoreLikeThisQuery.Include { get; set; }
		IEnumerable<Like> IMoreLikeThisQuery.Like { get; set; }
		int? IMoreLikeThisQuery.MaxDocumentFrequency { get; set; }
		int? IMoreLikeThisQuery.MaxQueryTerms { get; set; }
		int? IMoreLikeThisQuery.MaxWordLength { get; set; }
		int? IMoreLikeThisQuery.MinDocumentFrequency { get; set; }
		MinimumShouldMatch IMoreLikeThisQuery.MinimumShouldMatch { get; set; }
		int? IMoreLikeThisQuery.MinTermFrequency { get; set; }
		int? IMoreLikeThisQuery.MinWordLength { get; set; }
		IPerFieldAnalyzer IMoreLikeThisQuery.PerFieldAnalyzer { get; set; }
		Routing IMoreLikeThisQuery.Routing { get; set; }
		StopWords IMoreLikeThisQuery.StopWords { get; set; }
		IEnumerable<Like> IMoreLikeThisQuery.Unlike { get; set; }
		long? IMoreLikeThisQuery.Version { get; set; }
		VersionType? IMoreLikeThisQuery.VersionType { get; set; }

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

		public MoreLikeThisQueryDescriptor<T> MinDocumentFrequency(int? minDocumentFrequency) =>
			Assign(a => a.MinDocumentFrequency = minDocumentFrequency);

		public MoreLikeThisQueryDescriptor<T> MaxDocumentFrequency(int? maxDocumentFrequency) =>
			Assign(a => a.MaxDocumentFrequency = maxDocumentFrequency);

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

		public MoreLikeThisQueryDescriptor<T> PerFieldAnalyzer(Func<PerFieldAnalyzerDescriptor<T>, IPromise<IPerFieldAnalyzer>> analyzerSelector) =>
			Assign(a => a.PerFieldAnalyzer = analyzerSelector?.Invoke(new PerFieldAnalyzerDescriptor<T>())?.Value);

		public MoreLikeThisQueryDescriptor<T> Version(long? version) => Assign(a => a.Version = version);

		public MoreLikeThisQueryDescriptor<T> VersionType(VersionType? versionType) => Assign(a => a.VersionType = versionType);

		public MoreLikeThisQueryDescriptor<T> Routing(Routing routing) => Assign(a => a.Routing = routing);
	}
}
