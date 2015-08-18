using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<MoreLikeThisQueryDescriptor<object>>))]
	public interface IMoreLikeThisQuery : IQuery
	{
		[JsonProperty(PropertyName = "fields")]
		IEnumerable<FieldName> Fields { get; set; }

		[JsonProperty(PropertyName = "like_text")]
		string LikeText { get; set; }

		[JsonProperty(PropertyName = "percent_terms_to_match")]
		double? TermMatchPercentage { get; set; }

		[JsonProperty(PropertyName = "minimum_should_match")]
		string MinimumShouldMatch { get; set; }

		[JsonProperty(PropertyName = "stop_words")]
		IEnumerable<string> StopWords { get; set; }

		[JsonProperty(PropertyName = "min_term_freq")]
		int? MinTermFrequency { get; set; }

		[JsonProperty(PropertyName = "max_query_terms")]
		int? MaxQueryTerms { get; set; }

		[JsonProperty(PropertyName = "min_doc_freq")]
		int? MinDocumentFrequency { get; set; }

		[JsonProperty(PropertyName = "max_doc_freq")]
		int? MaxDocumentFrequency { get; set; }

		[JsonProperty(PropertyName = "min_word_len")]
		int? MinWordLength { get; set; }

		[JsonProperty(PropertyName = "max_word_len")]
		int? MaxWordLength { get; set; }

		[JsonProperty(PropertyName = "boost_terms")]
		double? BoostTerms { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string Analyzer { get; set; }
		
		/// <summary>
		/// A list of document ids. This parameter is required if like_text is not specified. 
		/// The texts are fetched from fields unless specified in each doc, and cannot be set to _all.
		/// <pre>Available from Elasticsearch 1.3.0</pre>
		/// </summary>
		[JsonProperty("ids")]
		IEnumerable<string> Ids { get; set; }
		
		/// <summary>
		/// A list of documents following the same syntax as the Multi GET API. This parameter is required if like_text is not specified. 
		/// The texts are fetched from fields unless specified in each doc, and cannot be set to _all.
		/// <pre>Available from Elasticsearch 1.3.0</pre>
		/// </summary>
		[JsonProperty("docs")]
		IEnumerable<IMultiGetOperation> Documents { get; set; }
		
		[JsonProperty(PropertyName = "include")]
		bool? Include { get; set; }
	}

	public class MoreLikeThisQuery : QueryBase, IMoreLikeThisQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IEnumerable<FieldName> Fields { get; set; }
		public string LikeText { get; set; }
		public double? TermMatchPercentage { get; set; }
		public string MinimumShouldMatch { get; set; }
		public IEnumerable<string> StopWords { get; set; }
		public int? MinTermFrequency { get; set; }
		public int? MaxQueryTerms { get; set; }
		public int? MinDocumentFrequency { get; set; }
		public int? MaxDocumentFrequency { get; set; }
		public int? MinWordLength { get; set; }
		public int? MaxWordLength { get; set; }
		public double? BoostTerms { get; set; }
		public string Analyzer { get; set; }
		public IEnumerable<string> Ids { get; set; }
		public IEnumerable<IMultiGetOperation> Documents { get; set; }
		public bool? Include { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.MoreLikeThis = this;
		internal static bool IsConditionless(IMoreLikeThisQuery q)
		{
			return q.LikeText.IsNullOrEmpty()
				&& (q.Ids == null || !q.Ids.Any())
				&& (q.Documents == null || !q.Documents.Any());
		}
	}

	public class MoreLikeThisQueryDescriptor<T> 
		: QueryDescriptorBase<MoreLikeThisQueryDescriptor<T>, IMoreLikeThisQuery>
		, IMoreLikeThisQuery where T : class
	{
		bool IQuery.Conditionless => MoreLikeThisQuery.IsConditionless(this);
		IEnumerable<FieldName> IMoreLikeThisQuery.Fields { get; set; }
		string IMoreLikeThisQuery.LikeText { get; set; }
		double? IMoreLikeThisQuery.TermMatchPercentage { get; set; }
		string IMoreLikeThisQuery.MinimumShouldMatch { get; set; }
		IEnumerable<string> IMoreLikeThisQuery.StopWords { get; set; }
		int? IMoreLikeThisQuery.MinTermFrequency { get; set; }
		int? IMoreLikeThisQuery.MaxQueryTerms { get; set; }
		int? IMoreLikeThisQuery.MinDocumentFrequency { get; set; }
		int? IMoreLikeThisQuery.MaxDocumentFrequency { get; set; }
		int? IMoreLikeThisQuery.MinWordLength { get; set; }
		int? IMoreLikeThisQuery.MaxWordLength { get; set; }
		double? IMoreLikeThisQuery.BoostTerms { get; set; }
		string IMoreLikeThisQuery.Analyzer { get; set; }
		IEnumerable<string> IMoreLikeThisQuery.Ids { get; set; }
		IEnumerable<IMultiGetOperation> IMoreLikeThisQuery.Documents { get; set; }
		bool? IMoreLikeThisQuery.Include { get; set; }

		public MoreLikeThisQueryDescriptor<T> OnFields(IEnumerable<string> fields) => 
			Assign(a => a.Fields = fields.Select(f => (FieldName)f));

		public MoreLikeThisQueryDescriptor<T> OnFields(params Expression<Func<T, object>>[] objectPaths) =>
			Assign(a => a.Fields = objectPaths.Select(e => (FieldName)e));

		public MoreLikeThisQueryDescriptor<T> LikeText(string likeText) => 
			Assign(a => a.LikeText = likeText);

		public MoreLikeThisQueryDescriptor<T> StopWords(IEnumerable<string> stopWords) => 
			Assign(a => a.StopWords = stopWords);

		public MoreLikeThisQueryDescriptor<T> MaxQueryTerms(int maxQueryTerms) => 
			Assign(a => a.MaxQueryTerms = maxQueryTerms);

		public MoreLikeThisQueryDescriptor<T> MinTermFrequency(int minTermFrequency) => 
			Assign(a => a.MinTermFrequency = minTermFrequency);

		public MoreLikeThisQueryDescriptor<T> MinDocumentFrequency(int minDocumentFrequency) =>
			Assign(a => a.MinDocumentFrequency = minDocumentFrequency);

		public MoreLikeThisQueryDescriptor<T> MaxDocumentFrequency(int maxDocumentFrequency) =>
			Assign(a => a.MaxDocumentFrequency = maxDocumentFrequency);

		public MoreLikeThisQueryDescriptor<T> MinWordLength(int minWordLength) => 
			Assign(a => a.MinWordLength = minWordLength);

		public MoreLikeThisQueryDescriptor<T> MaxWordLength(int maxWordLength) =>
			Assign(a => a.MaxWordLength = maxWordLength);

		public MoreLikeThisQueryDescriptor<T> BoostTerms(double boostTerms) =>
			Assign(a => a.BoostTerms = boostTerms);

		public MoreLikeThisQueryDescriptor<T> TermMatchPercentage(double termMatchPercentage) =>
			Assign(a => a.TermMatchPercentage = termMatchPercentage);

		public MoreLikeThisQueryDescriptor<T> MinimumShouldMatch(string minMatch) =>
			Assign(a => a.MinimumShouldMatch = minMatch);

		public MoreLikeThisQueryDescriptor<T> MinimumShouldMatch(int minMatch) =>
			Assign(a => a.MinimumShouldMatch = minMatch.ToString());

		public MoreLikeThisQueryDescriptor<T> Analyzer(string analyzer) =>
			Assign(a => a.Analyzer = analyzer);

		public MoreLikeThisQueryDescriptor<T> Ids(IEnumerable<long> ids) =>
			Assign(a => a.Ids = ids.Select(i => i.ToString(CultureInfo.InvariantCulture)));

		public MoreLikeThisQueryDescriptor<T> Ids(IEnumerable<string> ids) =>
			Assign(a => a.Ids = ids);

		/// <summary>
		/// Specify multiple documents to suply the more like this like text
		/// </summary>
		public MoreLikeThisQueryDescriptor<T> Documents(Func<MoreLikeThisQueryDocumentsDescriptor<T>, MoreLikeThisQueryDocumentsDescriptor<T>> selector) =>
			Assign(a => a.Documents = selector(new MoreLikeThisQueryDocumentsDescriptor<T>(true)).GetOperations);

		/// <summary>
		/// Specify multiple documents to supply the more like this text, but do not generate index: and type: on the get operations.
		/// Useful if the node has rest.action.multi.allow_explicit_index set to false
		/// </summary>
		/// <param name="selector"></param>
		/// <returns></returns>
		public MoreLikeThisQueryDescriptor<T> DocumentsExplicit(Func<MoreLikeThisQueryDocumentsDescriptor<T>, MoreLikeThisQueryDocumentsDescriptor<T>> selector) =>
			Assign(a => a.Documents = selector(new MoreLikeThisQueryDocumentsDescriptor<T>(false)).GetOperations);
	}
}
