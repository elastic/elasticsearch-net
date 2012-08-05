using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace Nest
{
	public interface IQueryResponse<T> : IResponse where T : class
	{
		ShardsMetaData Shards { get; }
		HitsMetaData<T> Hits { get; }
		IDictionary<string, Facet> Facets { get; }
		int ElapsedMilliseconds { get; }
		string ScrollId { get; }
		int Total { get; }
		double MaxScore { get; }
		IEnumerable<T> Documents { get; }
		IEnumerable<Hit<T>> DocumentsWithMetaData { get; }
		IEnumerable<Highlight> Highlights { get; }
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
		public HitsMetaData<T> Hits { get; internal set; }

		[JsonProperty(PropertyName = "facets")]
		public IDictionary<string, Facet> Facets { get; internal set; }

		[JsonProperty(PropertyName = "took")]
		public int ElapsedMilliseconds { get; internal set; }

		/// <summary>
		/// Only set when search type = scan and scroll specified
		/// </summary>
		[JsonProperty(PropertyName = "_scroll_id")]
		public string ScrollId { get; internal set; }


		public int Total
		{
			get
			{
				if (this.Hits == null)
				{
					return 0;
				}
				return this.Hits.Total;
			}
		}

		public double MaxScore
		{
			get
			{
				if (this.Hits == null)
				{
					return 0;
				}
				return this.Hits.MaxScore;
			}
		}

		public IEnumerable<T> Documents
		{
			get
			{
				if (this.Hits != null)
				{
					foreach (var hit in this.Hits.Hits)
					{
						yield return hit.Source ?? hit.Fields;
					}
				}
			}
		}

		public IEnumerable<Hit<T>> DocumentsWithMetaData
		{
			get
			{
				if (this.Hits != null)
				{
					return this.Hits.Hits;
				}

				return new List<Hit<T>>();
			}
		}

		
		public F Facet<F>(Expression<Func<T, object>> expression) where F : class, IFacet
		{
		  var fieldName = this.PropertyNameResolver.Resolve(expression);
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
			var fieldName = this.PropertyNameResolver.Resolve(expression);
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
		
		public IEnumerable<Highlight> Highlights
		{
			get
			{
				if (this.Hits != null)
				{
					foreach (var hit in this.Hits.Hits)
					{
			if (hit == null || hit.Highlight == null)
			  continue;

						foreach (var h in hit.Highlight)
						{
							yield return new Highlight {Field = h.Key, Highlights = h.Value };
						}
					}
				}
			}
		}
	}
}