using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IPercolateRequest<TDocument> : IPercolateOperation
		where TDocument : class
	{
		[JsonProperty(PropertyName = "doc")]
		TDocument Document { get; set; }

	}

	public partial class PercolateRequest<TDocument>
		where TDocument : class
	{
		public PercolateRequest() : this(typeof(TDocument), typeof(TDocument)) { }
		public PercolateRequest(Id id) : this(typeof(TDocument), typeof(TDocument), id) { }

		public string MultiPercolateName => "percolate";
		public IHighlightRequest Highlight { get; set; }
		public QueryContainer Query { get; set; }
		public QueryContainer Filter { get; set; }
		public IDictionary<string, IAggregationContainer> Aggregations { get; set; }
		
		public int? Size { get; set; }
		public bool? TrackScores { get; set; }
		public TDocument Document { get; set; }
		public IDictionary<FieldName, ISort> Sort { get; set; }

		IRequestParameters IPercolateOperation.GetRequestParameters() => this.RequestState.RequestParameters;

		partial void DocumentFromPath(TDocument document)
		{
			Self.Document = document;
			if (Self.Document != null)
				Self.RouteValues.Remove("id");
		}
	}

	public partial class PercolateDescriptor<TDocument> : IPercolateRequest<TDocument>
		where TDocument : class
	{
		IRequestParameters IPercolateOperation.GetRequestParameters() => Self.RequestParameters;

		IHighlightRequest IPercolateOperation.Highlight { get; set; }
		QueryContainer IPercolateOperation.Query { get; set; }
		QueryContainer IPercolateOperation.Filter { get; set; }

		int? IPercolateOperation.Size { get; set; }
		bool? IPercolateOperation.TrackScores { get; set; }
		
		TDocument IPercolateRequest<TDocument>.Document { get; set; }
		IDictionary<FieldName, ISort> IPercolateOperation.Sort { get; set; }
		IDictionary<string, IAggregationContainer> IPercolateOperation.Aggregations { get; set; }

		string IPercolateOperation.MultiPercolateName => "percolate";

		/// <summary>
		/// The object to perculate
		/// </summary>
		public PercolateDescriptor<TDocument> Document(TDocument @object)
		{
			Self.Document = @object;
			return this;
		}
		/// Make sure we keep calculating score even if we are sorting on a field.
		/// </summary>
		public PercolateDescriptor<TDocument> TrackScores(bool trackscores = true)
		{
			Self.TrackScores = trackscores;
			return this;
		}

		public PercolateDescriptor<TDocument> Aggregations(Func<AggregationContainerDescriptor<TDocument>, AggregationContainerDescriptor<TDocument>> aggregationsSelector)
		{
			var aggs = aggregationsSelector(new AggregationContainerDescriptor<TDocument>());
			if (aggs == null) return this;
			Self.Aggregations = ((IAggregationContainer)aggs).Aggregations;
			return this;
		}

		/// <summary>
		/// Allow to highlight search results on one or more fields. 
		/// </summary>
		public PercolateDescriptor<TDocument> Highlight(int size, Func<HighlightDescriptor<TDocument>,HighlightDescriptor<TDocument>> highlightDescriptor)
		{
			highlightDescriptor.ThrowIfNull("highlightDescriptor");
			var d  = highlightDescriptor(new HighlightDescriptor<TDocument>());
			Self.Size = size;
			Self.Highlight = d;
			return this;
		}

		public PercolateDescriptor<TDocument> Size(int size)
		{
			Self.Size = size;
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
		public PercolateDescriptor<TDocument> SortAscending(Expression<Func<TDocument, object>> objectPath)
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
		public PercolateDescriptor<TDocument> SortDescending(Expression<Func<TDocument, object>> objectPath)
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
		public PercolateDescriptor<TDocument> SortAscending(string field)
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
		public PercolateDescriptor<TDocument> SortDescending(string field)
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
		public PercolateDescriptor<TDocument> Sort(Func<SortFieldDescriptor<TDocument>, IFieldSort> sortSelector)
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
		public PercolateDescriptor<TDocument> SortGeoDistance(Func<SortGeoDistanceDescriptor<TDocument>, IGeoDistanceSort> sortSelector)
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
		public PercolateDescriptor<TDocument> SortScript(Func<SortScriptDescriptor<TDocument>, IScriptSort> sortSelector)
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
		public PercolateDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> query)
		{
			query.ThrowIfNull("query");
			var q = new QueryContainerDescriptor<TDocument>();
			var bq = query(q);
			return this.Query(bq);
		}

		public PercolateDescriptor<TDocument> Query(QueryContainer query)
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
		public PercolateDescriptor<TDocument> QueryString(string userInput)
		{
			var q = new QueryContainerDescriptor<TDocument>();
			var bq = userInput.IsNullOrEmpty() ? q.MatchAll() : q.QueryString(qs => qs.Query(userInput));
			Self.Query = bq;
			return this;
		}

		/// <summary>
		/// Filter search using a filter descriptor lambda
		/// </summary>
		public PercolateDescriptor<TDocument> Filter(Func<QueryContainerDescriptor<TDocument>, QueryContainer> filter)
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
		public PercolateDescriptor<TDocument> Filter(QueryContainer QueryDescriptor)
		{
			QueryDescriptor.ThrowIfNull("filter");
			Self.Filter = QueryDescriptor;
			return this;
		}
	}
}
