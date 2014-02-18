using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace Nest
{
	public interface IQueryResponse<T> : IResponse where T : class
	{
		ShardsMetaData Shards { get; }
		HitsMetaData<T> HitsMetaData { get; }
		IDictionary<string, Facet> Facets { get; }
		IDictionary<string, Suggest[]> Suggest { get; }
		int ElapsedMilliseconds { get; }
		string ScrollId { get; }
		long Total { get; }
		double MaxScore { get; }
		IEnumerable<T> Documents { get; }
		IEnumerable<IHit<T>> Hits { get; }
		HighlightDocumentDictionary Highlights { get; }
		F Facet<F>(Expression<Func<T, object>> expression) where F : class, IFacet;
		F Facet<F>(string fieldName) where F : class, IFacet;
		IEnumerable<F> FacetItems<F>(Expression<Func<T, object>> expression) where F : FacetItem;
		IEnumerable<F> FacetItems<F>(string fieldName) where F : FacetItem;
	}

	[JsonObject]
	public class QueryResponse<T> : BaseResponse, IQueryResponse<T> where T : class
	{
		public QueryResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty(PropertyName = "hits")]
		public HitsMetaData<T> HitsMetaData { get; internal set; }

		[JsonProperty(PropertyName = "facets")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public IDictionary<string, Facet> Facets { get; internal set; }

		[JsonProperty(PropertyName = "suggest")]
		public IDictionary<string, Suggest[]> Suggest { get; internal set; }

		[JsonProperty(PropertyName = "took")]
		public int ElapsedMilliseconds { get; internal set; }

		/// <summary>
		/// Only set when search type = scan and scroll specified
		/// </summary>
		[JsonProperty(PropertyName = "_scroll_id")]
		public string ScrollId { get; internal set; }

		public long Total
		{
			get
			{
				if (this.HitsMetaData == null)
				{
					return 0;
				}
				return this.HitsMetaData.Total;
			}
		}

		public double MaxScore
		{
			get
			{
				if (this.HitsMetaData == null)
				{
					return 0;
				}
				return this.HitsMetaData.MaxScore;
			}
		}

		public IEnumerable<T> Documents
		{
			get
			{
				if (this.HitsMetaData != null)
				{
					foreach (var hit in this.HitsMetaData.Hits)
					{
						yield return hit.Source;
					}
				}
			}
		}
		
		[JsonIgnore]
		public IEnumerable<IHit<T>> Hits
		{
			get
			{
				if (this.HitsMetaData != null)
				{
					return this.HitsMetaData.Hits;
				}

				return new List<Hit<T>>();
			}
		}

		public F Facet<F>(Expression<Func<T, object>> expression) where F : class, IFacet
		{
			var fieldName = this.ConnectionStatus.Infer.PropertyPath(expression);
			return this.Facet<F>(fieldName);
		}
		public F Facet<F>(string fieldName) where F : class, IFacet
		{
			if (this.Facets == null
				|| !this.Facets.Any()
				|| !this.Facets.ContainsKey(fieldName))
				return default(F);

			var facet = this.Facets[fieldName];

			return Convert.ChangeType(facet, typeof(F)) as F;
		}

		public IEnumerable<F> FacetItems<F>(Expression<Func<T, object>> expression) where F : FacetItem
		{
			var fieldName = this.ConnectionStatus.Infer.PropertyPath(expression);
			return this.FacetItems<F>(fieldName);
		}

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
		/// IDictionary of id <=> Highlight Collection for the document
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