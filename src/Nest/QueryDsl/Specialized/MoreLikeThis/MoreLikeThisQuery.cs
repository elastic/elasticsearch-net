using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<MoreLikeThisQueryDescriptor<object>>))]
	public interface IMoreLikeThisQuery : IQuery
	{
		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> Fields { get; set; }

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

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

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


	public class MoreLikeThisQuery : PlainQuery, IMoreLikeThisQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IEnumerable<PropertyPathMarker> Fields { get; set; }
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
		public double? Boost { get; set; }
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

	public class MoreLikeThisQueryDescriptor<T> : IMoreLikeThisQuery where T : class
	{
		private IMoreLikeThisQuery Self { get { return this; }}
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => MoreLikeThisQuery.IsConditionless(this);
		IEnumerable<PropertyPathMarker> IMoreLikeThisQuery.Fields { get; set; }
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
		double? IMoreLikeThisQuery.Boost { get; set; }
		string IMoreLikeThisQuery.Analyzer { get; set; }
		IEnumerable<string> IMoreLikeThisQuery.Ids { get; set; }
		IEnumerable<IMultiGetOperation> IMoreLikeThisQuery.Documents { get; set; }
		bool? IMoreLikeThisQuery.Include { get; set; }

		public MoreLikeThisQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public MoreLikeThisQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			this.Self.Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}

		public MoreLikeThisQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			this.Self.Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}

		public MoreLikeThisQueryDescriptor<T> LikeText(string likeText)
		{
			this.Self.LikeText = likeText;
			return this;
		}

		public MoreLikeThisQueryDescriptor<T> StopWords(IEnumerable<string> stopWords)
		{
			this.Self.StopWords = stopWords;
			return this;
		}
		
		public MoreLikeThisQueryDescriptor<T> MaxQueryTerms(int maxQueryTerms)
		{
			this.Self.MaxQueryTerms = maxQueryTerms;
			return this;
		}

		public MoreLikeThisQueryDescriptor<T> MinTermFrequency(int minTermFrequency)
		{
			this.Self.MinTermFrequency = minTermFrequency;
			return this;
		}

		public MoreLikeThisQueryDescriptor<T> MinDocumentFrequency(int minDocumentFrequency)
		{
			this.Self.MinDocumentFrequency = minDocumentFrequency;
			return this;
		}

		public MoreLikeThisQueryDescriptor<T> MaxDocumentFrequency(int maxDocumentFrequency)
		{
			this.Self.MaxDocumentFrequency = maxDocumentFrequency;
			return this;
		}

		public MoreLikeThisQueryDescriptor<T> MinWordLength(int minWordLength)
		{
			this.Self.MinWordLength = minWordLength;
			return this;
		}

		public MoreLikeThisQueryDescriptor<T> MaxWordLength(int maxWordLength)
		{
			this.Self.MaxWordLength = maxWordLength;
			return this;
		}

		public MoreLikeThisQueryDescriptor<T> BoostTerms(double boostTerms)
		{
			this.Self.BoostTerms = boostTerms;
			return this;
		}

		public MoreLikeThisQueryDescriptor<T> TermMatchPercentage(double termMatchPercentage)
		{
			this.Self.TermMatchPercentage = termMatchPercentage;
			return this;
		}

		public MoreLikeThisQueryDescriptor<T> MinimumShouldMatch(string minMatch)
		{
			this.Self.MinimumShouldMatch = minMatch;
			return this;
		}

		public MoreLikeThisQueryDescriptor<T> MinimumShouldMatch(int minMatch)
		{
			this.Self.MinimumShouldMatch = minMatch.ToString();
			return this;
		}

		public MoreLikeThisQueryDescriptor<T> Boost(double boost)
		{
			this.Self.Boost = boost;
			return this;
		}

		public MoreLikeThisQueryDescriptor<T> Analyzer(string analyzer)
		{
			this.Self.Analyzer = analyzer;
			return this;
		}
		
		public MoreLikeThisQueryDescriptor<T> Ids(IEnumerable<long> ids)
		{
			this.Self.Ids = ids.Select(i=>i.ToString(CultureInfo.InvariantCulture));
			return this;
		}
	
		public MoreLikeThisQueryDescriptor<T> Ids(IEnumerable<string> ids)
		{
			this.Self.Ids = ids;
			return this;
		}

		/// <summary>
		/// Specify multiple documents to suply the more like this like text
		/// </summary>
		public MoreLikeThisQueryDescriptor<T> Documents(Func<MoreLikeThisQueryDocumentsDescriptor<T>, MoreLikeThisQueryDocumentsDescriptor<T>> getDocumentsSelector)
		{
			getDocumentsSelector.ThrowIfNull("getDocumentsSelector");

			var descriptor = getDocumentsSelector(new MoreLikeThisQueryDocumentsDescriptor<T>(true));
			descriptor.ThrowIfNull("descriptor");
			Self.Documents = descriptor.GetOperations;
			return this;
		}
		
		/// <summary>
		/// Specify multiple documents to supply the more like this text, but do not generate index: and type: on the get operations.
		/// Useful if the node has rest.action.multi.allow_explicit_index set to false
		/// </summary>
		/// <param name="getDocumentsSelector"></param>
		/// <returns></returns>
		public MoreLikeThisQueryDescriptor<T> DocumentsExplicit(Func<MoreLikeThisQueryDocumentsDescriptor<T>, MoreLikeThisQueryDocumentsDescriptor<T>> getDocumentsSelector)
		{
			getDocumentsSelector.ThrowIfNull("getDocumentsSelector");

			var descriptor = getDocumentsSelector(new MoreLikeThisQueryDocumentsDescriptor<T>(false));
			descriptor.ThrowIfNull("descriptor");
			Self.Documents = descriptor.GetOperations;
			return this;
		}
	}

	public class MoreLikeThisQueryDocumentsDescriptor<T> where T : class
	{
		private readonly bool _allowExplicitIndex;
		internal IList<IMultiGetOperation> GetOperations { get; set; }

		public MoreLikeThisQueryDocumentsDescriptor(bool allowExplicitIndex = true)
		{
			_allowExplicitIndex = allowExplicitIndex;
			this.GetOperations = new List<IMultiGetOperation>();
		}

		/// <summary>
		/// Describe a get operation for the mlt_query docs property
		/// </summary>
		public MoreLikeThisQueryDocumentsDescriptor<T> Get(long id, Func<MultiGetOperationDescriptor<T>, MultiGetOperationDescriptor<T>> getSelector = null)
		{
			return this.Get(id.ToString(CultureInfo.InvariantCulture), getSelector);
		}

		/// <summary>
		/// Describe a get operation for the mlt_query docs property
		/// </summary>
		public MoreLikeThisQueryDocumentsDescriptor<T> Get(string id, Func<MultiGetOperationDescriptor<T>, MultiGetOperationDescriptor<T>> getSelector = null) 
		{
			getSelector = getSelector ?? (s => s);
			var descriptor = getSelector(new MultiGetOperationDescriptor<T>(_allowExplicitIndex).Id(id));
			this.GetOperations.Add(descriptor);
			return this;
		}

		/// <summary>
		/// Describe a get operation for the mlt_query docs property
		/// </summary>
		/// <typeparam name="TDocument">Use a different type to lookup</typeparam>
		public MoreLikeThisQueryDocumentsDescriptor<T> Get<TDocument>(long id, Func<MultiGetOperationDescriptor<TDocument>, MultiGetOperationDescriptor<TDocument>> getSelector = null)
			where TDocument : class
		{
			return this.Get<TDocument>(id.ToString(CultureInfo.InvariantCulture), getSelector);
		}
		
		/// <summary>
		/// Describe a get operation for the mlt_query docs property
		/// </summary>
		/// <typeparam name="TDocument">Use a different type to lookup</typeparam>
		public MoreLikeThisQueryDocumentsDescriptor<T> Get<TDocument>(string id, Func<MultiGetOperationDescriptor<TDocument>, MultiGetOperationDescriptor<TDocument>> getSelector = null) 
			where TDocument : class
		{
			getSelector = getSelector ?? (s => s);
			var descriptor = getSelector(new MultiGetOperationDescriptor<TDocument>(_allowExplicitIndex).Id(id));
			this.GetOperations.Add(descriptor);
			return this;

		}

		public MoreLikeThisQueryDocumentsDescriptor<T> Get<TDocument>(Func<MultiGetOperationDescriptor<TDocument>, MultiGetOperationDescriptor<TDocument>> getSelector)
			where TDocument : class
		{
			getSelector = getSelector ?? (s => s);
			var descriptor = getSelector(new MultiGetOperationDescriptor<TDocument>(_allowExplicitIndex));
			this.GetOperations.Add(descriptor);
			return this;
		}

		public MoreLikeThisQueryDocumentsDescriptor<T> Document<TDocument>(TDocument document, string index = null, string type = null)
			where TDocument : class
		{
			var descriptor = new MultiGetOperationDescriptor<TDocument>(_allowExplicitIndex)
				.Document(document);
			if (!string.IsNullOrEmpty(index))
				descriptor.Index(index);
			if (!string.IsNullOrEmpty(type))
				descriptor.Type(type);
			this.GetOperations.Add(descriptor);
			return this;
		}
	}
}
