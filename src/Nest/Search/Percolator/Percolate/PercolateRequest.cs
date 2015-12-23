using System;
using System.Collections.Generic;
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
		public IHighlight Highlight { get; set; }
		public QueryContainer Query { get; set; }
		public QueryContainer Filter { get; set; }
		public AggregationDictionary Aggregations { get; set; }
		
		public int? Size { get; set; }
		public bool? TrackScores { get; set; }
		public TDocument Document { get; set; }
		public IList<ISort> Sort { get; set; }

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

		IHighlight IPercolateOperation.Highlight { get; set; }
		QueryContainer IPercolateOperation.Query { get; set; }
		QueryContainer IPercolateOperation.Filter { get; set; }

		int? IPercolateOperation.Size { get; set; }
		bool? IPercolateOperation.TrackScores { get; set; }
		
		TDocument IPercolateRequest<TDocument>.Document { get; set; }
		IList<ISort> IPercolateOperation.Sort { get; set; }
		AggregationDictionary IPercolateOperation.Aggregations { get; set; }

		string IPercolateOperation.MultiPercolateName => "percolate";

		/// <summary>
		/// The object to perculate
		/// </summary>
		public PercolateDescriptor<TDocument> Document(TDocument @object) => Assign(a => a.Document = @object);

		/// Make sure we keep calculating score even if we are sorting on a field.
		/// </summary>
		public PercolateDescriptor<TDocument> TrackScores(bool? trackScores = true) => Assign(a => a.TrackScores = trackScores);

		public PercolateDescriptor<TDocument> Aggregations(Func<AggregationContainerDescriptor<TDocument>, IAggregationContainer> aggregationsSelector) =>
			Assign(a => a.Aggregations = aggregationsSelector(new AggregationContainerDescriptor<TDocument>())?.Aggregations);

		/// <summary>
		/// Allow to highlight search results on one or more fields. 
		/// </summary>
		public PercolateDescriptor<TDocument> Highlight(Func<HighlightDescriptor<TDocument>, IHighlight> highlightDescriptor) =>
			Assign(a => a.Highlight = highlightDescriptor?.Invoke(new HighlightDescriptor<TDocument>()));

		public PercolateDescriptor<TDocument> Size(int? size) => Assign(a => a.Size = size);

		public PercolateDescriptor<TDocument> Sort(Func<SortDescriptor<TDocument>, IPromise<IList<ISort>>> selector) => Assign(a => a.Sort = selector?.Invoke(new SortDescriptor<TDocument>())?.Value);

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		public PercolateDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> query) =>
			Assign(a => a.Query = query?.InvokeQuery(new QueryContainerDescriptor<TDocument>()));

		/// <summary>
		/// Filter search using a filter descriptor lambda
		/// </summary>
		public PercolateDescriptor<TDocument> Filter(Func<QueryContainerDescriptor<TDocument>, QueryContainer> filter) =>
			Assign(a => a.Filter = filter?.InvokeQuery(new QueryContainerDescriptor<TDocument>()));

	}
}
