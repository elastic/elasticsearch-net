using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.DSL.Descriptors;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPercolateCountRequest<TDocument> : IIndexTypePath<PercolateCountRequestParameters>, IPercolateOperation
		where TDocument : class
	{
		[JsonProperty(PropertyName = "doc")]
		TDocument Document { get; set; }
	}

	internal static class PercolateCountPathInfo
	{
		public static void Update<T>(ElasticsearchPathInfo<PercolateCountRequestParameters> pathInfo, IPercolateCountRequest<T> request)
			where T : class
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class PercolateCountRequest<TDocument> : IndexTypePathBase<PercolateCountRequestParameters, TDocument>, IPercolateCountRequest<TDocument>
		where TDocument : class
	{
		public string Id { get; set; }
		public int? Size { get; set; }
		public bool? TrackScores { get; set; }
		public IDictionary<PropertyPathMarker, ISort> Sort { get; set; }
		public IDictionary<PropertyPathMarker, IFacetContainer> Facets { get; set; }
		public IHighlightRequest Highlight { get; set; }
		public QueryContainer Query { get; set; }
		public FilterContainer Filter { get; set; }
		public IDictionary<string, IAggregationContainer> Aggregations { get; set; }

		public TDocument Document { get; set; }
		
		IRequestParameters IPercolateOperation.GetRequestParameters()
		{
			return this.Request.RequestParameters;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PercolateCountRequestParameters> pathInfo)
		{
			PercolateCountPathInfo.Update(pathInfo, this);
		}

	}
	
	[DescriptorFor("CountPercolate")]
	public partial class PercolateCountDescriptor<T> : IndexTypePathDescriptor<PercolateCountDescriptor<T>, PercolateCountRequestParameters, T>
		, IPercolateCountRequest<T>
		where T : class
	{

		private IPercolateCountRequest<T> Self { get { return this; } }

		IHighlightRequest IPercolateOperation.Highlight { get; set; }
		QueryContainer IPercolateOperation.Query { get; set; }
		FilterContainer IPercolateOperation.Filter { get; set; }

		string IPercolateOperation.Id { get; set; }
		int? IPercolateOperation.Size { get; set; }
		bool? IPercolateOperation.TrackScores { get; set; }
		
		T IPercolateCountRequest<T>.Document { get; set; }


		IDictionary<PropertyPathMarker, ISort> IPercolateOperation.Sort { get; set; }
		IDictionary<PropertyPathMarker, IFacetContainer> IPercolateOperation.Facets { get; set; }
		IDictionary<string, IAggregationContainer> IPercolateOperation.Aggregations { get; set; }
		
		IRequestParameters IPercolateOperation.GetRequestParameters()
		{
			return this.Request.RequestParameters;
		}
		/// <summary>
		/// The object to perculate
		/// </summary>
		public PercolateCountDescriptor<T> Document(T @object)
		{
			Self.Document = @object;
			return this;
		}

		/// <summary>
		/// The object to perculate
		/// </summary>
		[Obsolete("Scheduled for removal in 2.0, please use Document() instead")]
		public PercolateCountDescriptor<T> Object(T @object)
		{
			Self.Document = @object;
			return this;
		}

		/// <summary>
		/// The object to perculate
		/// </summary>
		public PercolateCountDescriptor<T> Id(string id)
		{
			Self.Id = id;
			return this;
		}

		/// <summary>
		/// The object to perculate
		/// </summary>
		public PercolateCountDescriptor<T> Id(long id)
		{
			Self.Id = id.ToString(CultureInfo.InvariantCulture);
			return this;
		}
		/// <summary>
		/// Make sure we keep calculating score even if we are sorting on a field.
		/// </summary>
		public PercolateCountDescriptor<T> TrackScores(bool trackscores = true)
		{
			Self.TrackScores = trackscores;
			return this;
		}

		public PercolateCountDescriptor<T> Aggregations(Func<AggregationDescriptor<T>, AggregationDescriptor<T>> aggregationsSelector)
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
		public PercolateCountDescriptor<T> SortAscending(Expression<Func<T, object>> objectPath)
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
		public PercolateCountDescriptor<T> SortDescending(Expression<Func<T, object>> objectPath)
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
		public PercolateCountDescriptor<T> SortAscending(string field)
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
		public PercolateCountDescriptor<T> SortDescending(string field)
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
		public PercolateCountDescriptor<T> Sort(Func<SortFieldDescriptor<T>, IFieldSort> sortSelector)
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
		public PercolateCountDescriptor<T> SortGeoDistance(Func<SortGeoDistanceDescriptor<T>, IGeoDistanceSort> sortSelector)
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
		public PercolateCountDescriptor<T> SortScript(Func<SortScriptDescriptor<T>, IScriptSort> sortSelector)
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
		public PercolateCountDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> query)
		{
			query.ThrowIfNull("query");
			var q = new QueryDescriptor<T>();
			var bq = query(q);
			return this.Query(bq);
		}

		public PercolateCountDescriptor<T> Query(QueryContainer query)
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
		public PercolateCountDescriptor<T> QueryString(string userInput)
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
		public PercolateCountDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filter)
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
		public PercolateCountDescriptor<T> Filter(FilterContainer filterDescriptor)
		{
			filterDescriptor.ThrowIfNull("filter");
			Self.Filter = filterDescriptor;
			return this;
		}
		
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<PercolateCountRequestParameters> pathInfo)
		{
			PercolateCountPathInfo.Update(pathInfo, this);
		}
	}
}
