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

	public partial class PercolateCountRequest<TDocument> where TDocument : class
	{
		public string MultiPercolateName => "count";
		public int? Size { get; set; }
		public bool? TrackScores { get; set; }
		public IList<ISort> Sort { get; set; }
		public IHighlight Highlight { get; set; }
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
		IHighlight IPercolateOperation.Highlight { get; set; }
		QueryContainer IPercolateOperation.Query { get; set; }
		QueryContainer IPercolateOperation.Filter { get; set; }

		int? IPercolateOperation.Size { get; set; }
		bool? IPercolateOperation.TrackScores { get; set; }

		TDocument IPercolateCountRequest<TDocument>.Document { get; set; }

		//TODO these dictionaries seem badly typed
		IList<ISort> IPercolateOperation.Sort { get; set; }
		IDictionary<string, IAggregationContainer> IPercolateOperation.Aggregations { get; set; }

		string IPercolateOperation.MultiPercolateName => "count";

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

		public PercolateCountDescriptor<TDocument> Sort(Func<SortDescriptor<TDocument>, IPromise<IList<ISort>>> selector) => Assign(a => a.Sort = selector?.Invoke(new SortDescriptor<TDocument>())?.Value);

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		public PercolateCountDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> query)
		{
			query.ThrowIfNull(nameof(query));
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
			filter.ThrowIfNull(nameof(filter));
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
		public PercolateCountDescriptor<TDocument> Filter(QueryContainer filter)
		{
			filter.ThrowIfNull(nameof(filter));
			Self.Filter = filter;
			return this;
		}
	}
}
