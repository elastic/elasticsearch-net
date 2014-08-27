using System.Collections.Generic;
using Nest.Domain;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace Nest
{
	public interface ISearchResponse<T> : IResponse where T : class
	{
		ShardsMetaData Shards { get; }
		HitsMetaData<T> HitsMetaData { get; }
		[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
		IDictionary<string, Facet> Facets { get; }
		IDictionary<string, IAggregation> Aggregations { get; }
		AggregationsHelper Aggs { get; }
		IDictionary<string, Suggest[]> Suggest { get; }
		int ElapsedMilliseconds { get; }
		string ScrollId { get; }
		long Total { get; }
		double MaxScore { get; }
		/// <summary>
		/// Returns a view on the documents inside the hits that are returned. 
		/// <para>NOTE: if you use Fields() on the search descriptor .Documents will be empty use 
		/// .Fields instead or try the 'source filtering' feature introduced in Elasticsearch 1.0 
		/// using .Source() on the search descriptor to get Documents of type T with only certain parts selected
		/// </para>
		/// </summary>
		IEnumerable<T> Documents { get; }
		IEnumerable<IHit<T>> Hits { get; }

		/// <summary>
		/// Will return the field selections inside the hits when the search descriptor specified .Fields.
		/// Otherwise this will always be an empty collection.
		/// </summary>
		IEnumerable<FieldSelection<T>> FieldSelections { get; }
		HighlightDocumentDictionary Highlights { get; }
		[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
		F Facet<F>(Expression<Func<T, object>> expression) where F : class, IFacet;
		[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
		F Facet<F>(string fieldName) where F : class, IFacet;
		[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
		IEnumerable<F> FacetItems<F>(Expression<Func<T, object>> expression) where F : FacetItem;
		[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
		IEnumerable<F> FacetItems<F>(string fieldName) where F : FacetItem;
	}

	[JsonObject]
	public class SearchResponse<T> : BaseResponse, ISearchResponse<T> where T : class
	{
		public SearchResponse()
		{
			this.IsValid = true;
			this.Aggregations = new Dictionary<string, IAggregation>();
			this.Facets = new Dictionary<string, Facet>();
		}

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty(PropertyName = "hits")]
		public HitsMetaData<T> HitsMetaData { get; internal set; }

		[JsonProperty(PropertyName = "facets")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
		public IDictionary<string, Facet> Facets { get; internal set; }
		
		[JsonProperty(PropertyName = "aggregations")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public IDictionary<string, IAggregation> Aggregations { get; internal set; }
		
		private AggregationsHelper _agg = null;
		[JsonIgnore]
		public AggregationsHelper Aggs
		{
			get { return _agg ?? (_agg = new AggregationsHelper(this.Aggregations)); }
		}
		
		[JsonProperty(PropertyName = "suggest")]
		public IDictionary<string, Suggest[]> Suggest { get; internal set; }

		[JsonProperty(PropertyName = "took")]
		public int ElapsedMilliseconds { get; internal set; }

		/// <summary>
		/// Only set when search type = scan and scroll specified
		/// </summary>
		[JsonProperty(PropertyName = "_scroll_id")]
		public string ScrollId { get; internal set; }

		[JsonIgnore]
		public long Total { get { return this.HitsMetaData == null ? 0 : this.HitsMetaData.Total; } }

		[JsonIgnore]
		public double MaxScore { get { return this.HitsMetaData == null ? 0 : this.HitsMetaData.MaxScore; } }

		private IList<T> _documents; 
		/// <inheritdoc />
		[JsonIgnore]
		public IEnumerable<T> Documents
		{
			get
			{
				return this._documents ?? (this._documents = this.Hits
					.Select(h => h.Source)
					.Where(d => d != null)
					.ToList());
			}
		}
		
		[JsonIgnore]
		public IEnumerable<IHit<T>> Hits
		{
			get { return this.HitsMetaData != null ? (IEnumerable<IHit<T>>) this.HitsMetaData.Hits : new List<Hit<T>>(); }
		}

		/// <inheritdoc />
		[JsonIgnore]
		public IEnumerable<FieldSelection<T>> FieldSelections
		{
			get 
			{ 
				return this.Hits
					.Select(h => h.Fields)
					.Where(f=>f != null)
					.Select(f => new FieldSelection<T>(this.Settings, f.FieldValuesDictionary)); 
			}
		}


		[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
		public F Facet<F>(Expression<Func<T, object>> expression) where F : class, IFacet
		{
			var fieldName = this.Infer.PropertyPath(expression);
			return this.Facet<F>(fieldName);
		}
		[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
		public F Facet<F>(string fieldName) where F : class, IFacet
		{
			if (this.Facets == null
				|| !this.Facets.Any()
				|| !this.Facets.ContainsKey(fieldName))
				return default(F);

			var facet = this.Facets[fieldName];

			return Convert.ChangeType(facet, typeof(F)) as F;
		}

		[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
		public IEnumerable<F> FacetItems<F>(Expression<Func<T, object>> expression) where F : FacetItem
		{
			var fieldName = this.Infer.PropertyPath(expression);
			return this.FacetItems<F>(fieldName);
		}

		[Obsolete("Facets are deprecated and will be removed in a future release. You are encouraged to migrate to aggregations instead.")]
		public IEnumerable<F> FacetItems<F>(string fieldName) where F : FacetItem
		{
			if (this.Facets == null
				|| !this.Facets.Any()
				|| !this.Facets.ContainsKey(fieldName))
				return Enumerable.Empty<F>();

			var facet = this.Facets[fieldName];
			if (facet is DateHistogramFacet)
				return ((DateHistogramFacet)facet).Items.Cast<F>();

			if (facet is DateRangeFacet)
				return ((DateRangeFacet)facet).Items.Cast<F>();

			if (facet is GeoDistanceFacet)
				return ((GeoDistanceFacet)facet).Items.Cast<F>();

			if (facet is HistogramFacet)
				return ((HistogramFacet)facet).Items.Cast<F>();

			if (facet is RangeFacet)
				return ((RangeFacet)facet).Items.Cast<F>();

			if (facet is TermFacet)
				return ((TermFacet)facet).Items.Cast<F>();

			if (facet is TermStatsFacet)
				return ((TermStatsFacet)facet).Items.Cast<F>();

			return Enumerable.Empty<F>();
		}

		/// <summary>
		/// IDictionary of id -Highlight Collection for the document
		/// </summary>
		public HighlightDocumentDictionary Highlights
		{
			get
			{
				var dict = new HighlightDocumentDictionary();
				if (this.HitsMetaData == null || !this.HitsMetaData.Hits.HasAny())
					return dict;
				

				foreach (var hit in this.HitsMetaData.Hits)
				{
					if (!hit.Highlights.Any())
						continue;

					dict.Add(hit.Id, hit.Highlights);

				}
				return dict;
			}
		}
	}
}