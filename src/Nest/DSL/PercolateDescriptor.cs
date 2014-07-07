using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Elasticsearch.Net;
using Nest.DSL.Descriptors;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPercolateRequest<TDocument> : IIndexTypePath<PercolateRequestParameters>
		where TDocument : class
	{
		[JsonProperty(PropertyName = "size")]
		int? Size { get; set; }

		[JsonProperty(PropertyName = "track_scores")]
		bool? TrackScores { get; set; }

		[JsonProperty(PropertyName = "doc")]
		TDocument Document { get; set; }

		[JsonProperty(PropertyName = "score")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<PropertyPathMarker, ISort> Sort { get; set; }

		[JsonProperty(PropertyName = "facets")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<PropertyPathMarker, IFacetContainer> Facets { get; set; }

		[JsonProperty(PropertyName = "highlight")]
		IHighlightRequest Highlight { get; set; }

		[JsonProperty(PropertyName = "query")]
		QueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		FilterContainer Filter { get; set; }

		[JsonProperty(PropertyName = "aggs")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<string, IAggregationContainer> Aggregations { get; set; }
	}

	internal static class PercolatePathInfo
	{
		public static void Update<T>(ElasticsearchPathInfo<PercolateRequestParameters> pathInfo, IPercolateRequest<T> request)
			where T : class
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}

	public partial class PercolateRequest<TDocument> : IndexTypePathBase<PercolateRequestParameters, TDocument>, IPercolateRequest<TDocument>
		where TDocument : class
	{
		public IHighlightRequest Highlight { get; set; }
		public QueryContainer Query { get; set; }
		public FilterContainer Filter { get; set; }
		public IDictionary<string, IAggregationContainer> Aggregations { get; set; }

		public int? Size { get; set; }
		public bool? TrackScores { get; set; }
		public TDocument Document { get; set; }
		public IDictionary<PropertyPathMarker, ISort> Sort { get; set; }
		public IDictionary<PropertyPathMarker, IFacetContainer> Facets { get; set; }

		public PercolateRequest(TDocument document)
		{
			this.Document = document;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PercolateRequestParameters> pathInfo)
		{
			PercolatePathInfo.Update(pathInfo, this);
		}

	}
	public partial class PercolateDescriptor<T> : IndexTypePathDescriptor<PercolateDescriptor<T>, PercolateRequestParameters, T>, IPercolateRequest<T>
		where T : class
	{
		private IPercolateRequest<T> Self { get { return this; } }

		IHighlightRequest IPercolateRequest<T>.Highlight { get; set; }
		QueryContainer IPercolateRequest<T>.Query { get; set; }
		FilterContainer IPercolateRequest<T>.Filter { get; set; }

		int? IPercolateRequest<T>.Size { get; set; }
		bool? IPercolateRequest<T>.TrackScores { get; set; }
		T IPercolateRequest<T>.Document { get; set; }
		IDictionary<PropertyPathMarker, ISort> IPercolateRequest<T>.Sort { get; set; }
		IDictionary<PropertyPathMarker, IFacetContainer> IPercolateRequest<T>.Facets { get; set; }
		IDictionary<string, IAggregationContainer> IPercolateRequest<T>.Aggregations { get; set; }

		/// <summary>
		/// The object to perculate
		/// </summary>
		public PercolateDescriptor<T> Object(T @object)
		{
			Self.Document = @object;
			return this;
		}


		/// <summary>
		/// Make sure we keep calculating score even if we are sorting on a field.
		/// </summary>
		public PercolateDescriptor<T> TrackScores(bool trackscores = true)
		{
			Self.TrackScores = trackscores;
			return this;
		}

		public PercolateDescriptor<T> Aggregations(Func<AggregationDescriptor<T>, AggregationDescriptor<T>> aggregationsSelector)
		{
			var aggs = aggregationsSelector(new AggregationDescriptor<T>());
			if (aggs == null) return this;
			Self.Aggregations = ((IAggregationContainer)aggs).Aggregations;
			return this;
		}


		/// <summary>
		/// <para>Allows to add one or more sort on specific fields. Each sort can be reversed as well.
		/// The sort is defined on a per field level, with special field name for _score to sort by score.
		/// </para>
		/// <para>
		/// Sort ascending.
		/// </para>
		/// </summary>
		public PercolateDescriptor<T> SortAscending(Expression<Func<T, object>> objectPath)
		{
			if (Self.Sort == null) Self.Sort = new Dictionary<PropertyPathMarker, ISort>();

			Self.Sort.Add(objectPath, new Sort() { Order = SortOrder.Ascending });
			return this;
		}

		/// <summary>
		/// <para>Allows to add one or more sort on specific fields. Each sort can be reversed as well.
		/// The sort is defined on a per field level, with special field name for _score to sort by score.
		/// </para>
		/// <para>
		/// Sort descending.
		/// </para>
		/// </summary>
		public PercolateDescriptor<T> SortDescending(Expression<Func<T, object>> objectPath)
		{
			if (Self.Sort == null) Self.Sort = new Dictionary<PropertyPathMarker, ISort>();

			Self.Sort.Add(objectPath, new Sort() { Order = SortOrder.Descending });
			return this;
		}

		/// <summary>
		/// <para>Allows to add one or more sort on specific fields. Each sort can be reversed as well.
		/// The sort is defined on a per field level, with special field name for _score to sort by score.
		/// </para>
		/// <para>
		/// Sort ascending.
		/// </para>
		/// </summary>
		public PercolateDescriptor<T> SortAscending(string field)
		{
			if (Self.Sort == null) Self.Sort = new Dictionary<PropertyPathMarker, ISort>();
			Self.Sort.Add(field, new Sort() { Order = SortOrder.Ascending });
			return this;
		}

		/// <summary>
		/// <para>Allows to add one or more sort on specific fields. Each sort can be reversed as well.
		/// The sort is defined on a per field level, with special field name for _score to sort by score.
		/// </para>
		/// <para>
		/// Sort descending.
		/// </para>
		/// </summary>
		public PercolateDescriptor<T> SortDescending(string field)
		{
			if (Self.Sort == null)
				Self.Sort = new Dictionary<PropertyPathMarker, ISort>();

			Self.Sort.Add(field, new Sort() { Order = SortOrder.Descending });
			return this;
		}

		/// <summary>
		/// <para>Sort() allows you to fully describe your sort unlike the SortAscending and SortDescending aliases.
		/// </para>
		/// </summary>
		public PercolateDescriptor<T> Sort(Func<SortFieldDescriptor<T>, SortFieldDescriptor<T>> sortSelector)
		{
			if (Self.Sort == null)
				Self.Sort = new Dictionary<PropertyPathMarker, ISort>();

			sortSelector.ThrowIfNull("sortSelector");
			var descriptor = sortSelector(new SortFieldDescriptor<T>());
			Self.Sort.Add(descriptor.Field, descriptor);
			return this;
		}

		/// <summary>
		/// <para>SortGeoDistance() allows you to sort by a distance from a geo point.
		/// </para>
		/// </summary>
		public PercolateDescriptor<T> SortGeoDistance(Func<SortGeoDistanceDescriptor<T>, SortGeoDistanceDescriptor<T>> sortSelector)
		{
			if (Self.Sort == null)
				Self.Sort = new Dictionary<PropertyPathMarker, ISort>();

			sortSelector.ThrowIfNull("sortSelector");
			var descriptor = sortSelector(new SortGeoDistanceDescriptor<T>());
			Self.Sort.Add("_geo_distance", descriptor);
			return this;
		}

		/// <summary>
		/// <para>SortScript() allows you to sort by a distance from a geo point.
		/// </para>
		/// </summary>
		public PercolateDescriptor<T> SortScript(Func<SortScriptDescriptor<T>, SortScriptDescriptor<T>> sortSelector)
		{
			if (Self.Sort == null)
				Self.Sort = new Dictionary<PropertyPathMarker, ISort>();

			sortSelector.ThrowIfNull("sortSelector");
			var descriptor = sortSelector(new SortScriptDescriptor<T>());
			Self.Sort.Add("_script", descriptor);
			return this;
		}

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		public PercolateDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> query)
		{
			query.ThrowIfNull("query");
			var q = new QueryDescriptor<T>();
			var bq = query(q);
			return this.Query(bq);
		}

		public PercolateDescriptor<T> Query(QueryContainer query)
		{
			if (query == null)
				return this;

			if (query.IsConditionless)
				return this;
			Self.Query = query;
			return this;

		}

		/// <summary>
		/// Shortcut to .Query(q=>q.QueryString(qs=>qs.Query("string"))
		/// Does a match_all if the userInput string is null or empty;
		/// </summary>
		public PercolateDescriptor<T> QueryString(string userInput)
		{
			var q = new QueryDescriptor<T>();
			QueryContainer bq;
			if (userInput.IsNullOrEmpty())
				bq = q.MatchAll();
			else
				bq = q.QueryString(qs => qs.Query(userInput));
			Self.Query = bq;
			return this;
		}

		/// <summary>
		/// Filter search using a filter descriptor lambda
		/// </summary>
		public PercolateDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filter)
		{
			filter.ThrowIfNull("filter");
			var f = new FilterDescriptor<T>();

			var bf = filter(f);
			if (bf == null)
				return this;
			if (bf.IsConditionless)
				return this;


			Self.Filter = bf;
			return this;
		}
		/// <summary>
		/// Filter search
		/// </summary>
		public PercolateDescriptor<T> Filter(FilterContainer filterDescriptor)
		{
			filterDescriptor.ThrowIfNull("filter");
			Self.Filter = filterDescriptor;
			return this;
		}


		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PercolateRequestParameters> pathInfo)
		{
			PercolatePathInfo.Update(pathInfo, this);
		}
	}
}
