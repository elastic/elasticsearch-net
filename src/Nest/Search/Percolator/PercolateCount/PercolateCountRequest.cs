using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
	public partial interface IPercolateCountRequest<TDocument> : IPercolateOperation
		where TDocument : class
	{
		[JsonProperty(PropertyName = "doc")]
		TDocument Document { get; set; }
	}

	[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
	public partial class PercolateCountRequest<TDocument> where TDocument : class
	{
		public PercolateCountRequest() : this(typeof(TDocument), typeof(TDocument)) { }

		public PercolateCountRequest(Id id) : this(typeof(TDocument), typeof(TDocument), id) { }

		public AggregationDictionary Aggregations { get; set; }

		public TDocument Document { get; set; }
		public QueryContainer Filter { get; set; }
		public IHighlight Highlight { get; set; }
		public string MultiPercolateName => "count";
		public QueryContainer Query { get; set; }
		public int? Size { get; set; }
		public IList<ISort> Sort { get; set; }
		public bool? TrackScores { get; set; }

		IRequestParameters IPercolateOperation.GetRequestParameters() => RequestState.RequestParameters;

		partial void DocumentFromPath(TDocument document)
		{
			Self.Document = document;
			if (Self.Document != null)
				Self.RouteValues.Remove("id");
		}
	}

	[DescriptorFor("CountPercolate")]
	[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with search api")]
	public partial class PercolateCountDescriptor<TDocument> : IPercolateCountRequest<TDocument>
		where TDocument : class
	{
		AggregationDictionary IPercolateOperation.Aggregations { get; set; }

		TDocument IPercolateCountRequest<TDocument>.Document { get; set; }
		QueryContainer IPercolateOperation.Filter { get; set; }
		IHighlight IPercolateOperation.Highlight { get; set; }

		string IPercolateOperation.MultiPercolateName => "count";
		QueryContainer IPercolateOperation.Query { get; set; }

		int? IPercolateOperation.Size { get; set; }

		IList<ISort> IPercolateOperation.Sort { get; set; }
		bool? IPercolateOperation.TrackScores { get; set; }

		IRequestParameters IPercolateOperation.GetRequestParameters() =>
			Self.RequestParameters;

		/// <summary>
		/// The object to perculate
		/// </summary>
		public PercolateCountDescriptor<TDocument> Document(TDocument @object) => Assign(a => a.Document = @object);

		/// <summary>
		/// Make sure we keep calculating score even if we are sorting on a field.
		/// </summary>
		public PercolateCountDescriptor<TDocument> TrackScores(bool trackscores = true) => Assign(a => a.TrackScores = trackscores);

		public PercolateCountDescriptor<TDocument> Aggregations(
			Func<AggregationContainerDescriptor<TDocument>, IAggregationContainer> aggregationsSelector
		) =>
			Assign(a => a.Aggregations = aggregationsSelector(new AggregationContainerDescriptor<TDocument>())?.Aggregations);

		public PercolateCountDescriptor<TDocument> Aggregations(AggregationDictionary aggregations) =>
			Assign(a => a.Aggregations = aggregations);

		public PercolateCountDescriptor<TDocument> Sort(Func<SortDescriptor<TDocument>, IPromise<IList<ISort>>> selector) =>
			Assign(a => a.Sort = selector?.Invoke(new SortDescriptor<TDocument>())?.Value);

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		public PercolateCountDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> query) =>
			Assign(a => a.Query = query?.Invoke(new QueryContainerDescriptor<TDocument>()));

		/// <summary>
		/// Filter search using a filter descriptor lambda
		/// </summary>
		public PercolateCountDescriptor<TDocument> Filter(Func<QueryContainerDescriptor<TDocument>, QueryContainer> filter) =>
			Assign(a => a.Filter = filter?.Invoke(new QueryContainerDescriptor<TDocument>()));
	}
}
