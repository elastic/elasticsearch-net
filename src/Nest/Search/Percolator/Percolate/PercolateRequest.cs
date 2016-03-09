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

		/// <summary>
		/// Whether the _score is included for each match. The _score is based on the query and represents 
		/// how the query matched the percolate query’s metadata, not how the document (that is being percolated) 
		/// matched the query. The <see cref="Query"/> option is required for this option.
		/// </summary>
		public bool? TrackScores { get; set; }

		/// <summary>
		/// The object to percolate
		/// </summary>
		public TDocument Document { get; set; }

		/// <summary>
		/// Define a sort specification like in the search API. Currently only sorting _score reverse 
		/// (default relevancy) is supported. Other sort fields will throw an exception. 
		/// The <see cref="Size"/> and <see cref="Query"/> option are required for this setting. Like <see cref="TrackScores"/> 
		/// the score is based on the query and represents how the query matched to the percolate query’s metadata 
		/// and not how the document being percolated matched to the query.
		/// </summary>
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

		/// <summary>
		/// Whether the _score is included for each match. The _score is based on the query and represents 
		/// how the query matched the percolate query’s metadata, not how the document (that is being percolated) 
		/// matched the query. The <see cref="Query"/> option is required for this option.
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

		/// <summary>
		/// Define a sort specification like in the search API. Currently only sorting _score reverse 
		/// (default relevancy) is supported. Other sort fields will throw an exception. 
		/// The <see cref="Size"/> and <see cref="Query"/> option are required for this setting. Like <see cref="TrackScores"/> 
		/// the score is based on the query and represents how the query matched to the percolate query’s metadata 
		/// and not how the document being percolated matched to the query.
		/// </summary>
		public PercolateDescriptor<TDocument> Sort(Func<SortDescriptor<TDocument>, IPromise<IList<ISort>>> selector) => Assign(a => a.Sort = selector?.Invoke(new SortDescriptor<TDocument>())?.Value);

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		public PercolateDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> query) =>
			Assign(a => a.Query = query?.Invoke(new QueryContainerDescriptor<TDocument>()));

		/// <summary>
		/// Filter search using a filter descriptor lambda
		/// </summary>
		public PercolateDescriptor<TDocument> Filter(Func<QueryContainerDescriptor<TDocument>, QueryContainer> filter) =>
			Assign(a => a.Filter = filter?.Invoke(new QueryContainerDescriptor<TDocument>()));

	}
}
