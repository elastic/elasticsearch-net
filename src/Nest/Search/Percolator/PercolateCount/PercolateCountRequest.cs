using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IPercolateCountRequest<TDocument> : IPercolateOperation
		where TDocument : class
	{
		[JsonProperty(PropertyName = "doc")]
		TDocument Document { get; set; }
	}

	public partial class PercolateCountRequest<TDocument> 
		where TDocument : class
	{
		public int? Size { get; set; }
		public bool? TrackScores { get; set; }
		public IDictionary<FieldName, ISort> Sort { get; set; }
		public IHighlightRequest Highlight { get; set; }
		public QueryContainer Query { get; set; }
		public QueryContainer Filter { get; set; }
		public IDictionary<string, IAggregationContainer> Aggregations { get; set; }

		public TDocument Document { get; set; }

		public PercolateCountRequest() : this(typeof(TDocument), typeof(TDocument)) { }
		public PercolateCountRequest(Id id) : this(typeof(TDocument), typeof(TDocument), id) { }

		IRequestParameters IPercolateOperation.GetRequestParameters()
		{
			return this.RequestState.RequestParameters;
		}
	}
	
	[DescriptorFor("CountPercolate")]
	public partial class PercolateCountDescriptor<TDocument> : IPercolateCountRequest<TDocument>
		where TDocument : class
	{
		IHighlightRequest IPercolateOperation.Highlight { get; set; }
		QueryContainer IPercolateOperation.Query { get; set; }
		QueryContainer IPercolateOperation.Filter { get; set; }

		int? IPercolateOperation.Size { get; set; }
		bool? IPercolateOperation.TrackScores { get; set; }
		
		TDocument IPercolateCountRequest<TDocument>.Document { get; set; }
		
		//TODO these dictionaries seem badly typed
		IDictionary<FieldName, ISort> IPercolateOperation.Sort { get; set; }
		IDictionary<string, IAggregationContainer> IPercolateOperation.Aggregations { get; set; }
		
		IRequestParameters IPercolateOperation.GetRequestParameters()
		{
			return this.Self.RequestParameters;
		}
		/// <summary>
		/// The object to perculate
		/// </summary>
		public PercolateCountDescriptor<TDocument> Document(TDocument @object)
		{
			Self.Document = @object;
			return this;
		}

		/// <summary>
		/// Make sure we keep calculating score even if we are sorting on a field.
		/// </summary>
		public PercolateCountDescriptor<TDocument> TrackScores(bool trackscores = true)
		{
			Self.TrackScores = trackscores;
			return this;
		}

		public PercolateCountDescriptor<TDocument> Aggregations(Func<AggregationContainerDescriptor<TDocument>, AggregationContainerDescriptor<TDocument>> aggregationsSelector)
		{
			var aggs = aggregationsSelector(new AggregationContainerDescriptor<TDocument>());
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
		public PercolateCountDescriptor<TDocument> SortAscending(Expression<Func<TDocument, object>> objectPath)
		{
			if (Self.Sort == null) Self.Sort = new Dictionary<FieldName, ISort>();

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
		public PercolateCountDescriptor<TDocument> SortDescending(Expression<Func<TDocument, object>> objectPath)
		{
			if (Self.Sort == null) Self.Sort = new Dictionary<FieldName, ISort>();

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
		public PercolateCountDescriptor<TDocument> SortAscending(string field)
		{
			if (Self.Sort == null) Self.Sort = new Dictionary<FieldName, ISort>();
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
		public PercolateCountDescriptor<TDocument> SortDescending(string field)
		{
			if (Self.Sort == null)
				Self.Sort = new Dictionary<FieldName, ISort>();

			Self.Sort.Add(field, new Sort() { Order = SortOrder.Descending });
			return this;
		}

		/// <summary>
		/// <para>Sort() allows you to fully describe your sort unlike the SortAscending and SortDescending aliases.
		/// </para>
		/// </summary>
		public PercolateCountDescriptor<TDocument> Sort(Func<SortFieldDescriptor<TDocument>, IFieldSort> sortSelector)
		{
			if (Self.Sort == null)
				Self.Sort = new Dictionary<FieldName, ISort>();

			sortSelector.ThrowIfNull("sortSelector");
			var descriptor = sortSelector(new SortFieldDescriptor<TDocument>());
			Self.Sort.Add(descriptor.Field, descriptor);
			return this;
		}

		/// <summary>
		/// <para>SortGeoDistance() allows you to sort by a distance from a geo point.
		/// </para>
		/// </summary>
		public PercolateCountDescriptor<TDocument> SortGeoDistance(Func<SortGeoDistanceDescriptor<TDocument>, IGeoDistanceSort> sortSelector)
		{
			if (Self.Sort == null)
				Self.Sort = new Dictionary<FieldName, ISort>();

			sortSelector.ThrowIfNull("sortSelector");
			var descriptor = sortSelector(new SortGeoDistanceDescriptor<TDocument>());
			Self.Sort.Add("_geo_distance", descriptor);
			return this;
		}

		/// <summary>
		/// <para>SortScript() allows you to sort by a distance from a geo point.
		/// </para>
		/// </summary>
		public PercolateCountDescriptor<TDocument> SortScript(Func<SortScriptDescriptor<TDocument>, IScriptSort> sortSelector)
		{
			if (Self.Sort == null)
				Self.Sort = new Dictionary<FieldName, ISort>();

			sortSelector.ThrowIfNull("sortSelector");
			var descriptor = sortSelector(new SortScriptDescriptor<TDocument>());
			Self.Sort.Add("_script", descriptor);
			return this;
		}

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		public PercolateCountDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> query)
		{
			query.ThrowIfNull("query");
			var q = new QueryContainerDescriptor<TDocument>();
			var bq = query(q);
			return this.Query(bq);
		}

		public PercolateCountDescriptor<TDocument> Query(QueryContainer query)
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
		public PercolateCountDescriptor<TDocument> QueryString(string userInput)
		{
			var q = new QueryContainerDescriptor<TDocument>();
			var bq = userInput.IsNullOrEmpty() ? q.MatchAll() : q.QueryString(qs => qs.Query(userInput));
			Self.Query = bq;
			return this;
		}

		/// <summary>
		/// Filter search using a filter descriptor lambda
		/// </summary>
		public PercolateCountDescriptor<TDocument> Filter(Func<QueryContainerDescriptor<TDocument>, QueryContainer> filter)
		{
			filter.ThrowIfNull("filter");
			var f = new QueryContainerDescriptor<TDocument>();

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
		public PercolateCountDescriptor<TDocument> Filter(QueryContainer QueryDescriptor)
		{
			QueryDescriptor.ThrowIfNull("filter");
			Self.Filter = QueryDescriptor;
			return this;
		}
	}
}
