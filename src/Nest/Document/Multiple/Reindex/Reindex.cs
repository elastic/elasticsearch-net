using System;
using Elasticsearch.Net;

namespace Nest
{
	public interface IReindexRequest 
	{
		IndexName To { get; set; }
		IndexName From { get; set; }
		//TODO TimeUnitExpression, needs to propagate to generated querystring methods
		string Scroll { get; set; }
		int? Size { get; set; }
		
		QueryContainer Query { get; set; }

		ICreateIndexRequest CreateIndexRequest { get; set; }

		IPutMappingRequest PutMappingRequest { get; set; } 
	}
	public class ReindexRequest : IReindexRequest
	{
		public IndexName To { get; set; }
		public IndexName From { get; set; }
		public string Scroll { get; set; }
		public int? Size { get; set; }
		public QueryContainer Query { get; set; }
		public ICreateIndexRequest CreateIndexRequest { get; set; }
		public IPutMappingRequest PutMappingRequest { get; set; } 
		public ReindexRequest(IndexName from, IndexName to)
		{
			this.To = to;
			this.From = from;
		}
	}

	public class ReindexDescriptor<T> : IReindexRequest where T : class
	{
		ReindexDescriptor<T> Assign(Action<IReindexRequest> assign)  => Fluent.Assign(this, assign);

		IndexName IReindexRequest.To { get; set; }
		IndexName IReindexRequest.From { get; set; }
		string IReindexRequest.Scroll { get; set; }
		int? IReindexRequest.Size { get; set; }
		QueryContainer IReindexRequest.Query { get; set; }
		ICreateIndexRequest IReindexRequest.CreateIndexRequest { get; set; }
		IPutMappingRequest IReindexRequest.PutMappingRequest { get; set; } 
		
		public ReindexDescriptor(IndexName from, IndexName to)
		{
			Assign(a => a.From = from).Assign(a => a.To = to);
		}

		/// <summary>
		/// A search request can be scrolled by specifying the scroll parameter. The scroll parameter is a time value parameter (for example: scroll=5m), indicating for how long the nodes that participate in the search will maintain relevant resources in order to continue and support it. This is very similar in its idea to opening a cursor against a database.
		/// </summary>
		/// <param name="scrollTime">The scroll parameter is a time value parameter (for example: scroll=5m)</param>
		/// <returns></returns>
		public ReindexDescriptor<T> Scroll(string scrollTime) => Assign(a => a.Scroll = scrollTime);

		/// <summary>
		/// The number of hits to return. Defaults to 100. When using scroll search type,
		/// size is actually multiplied by the number of shards!
		/// </summary>
		public ReindexDescriptor<T> Size(int? size) => Assign(a => a.Size = size);

		/// <summary>
		/// The number of hits to return. Defaults to 100.
		/// </summary>
		public ReindexDescriptor<T> Take(int? take) => Assign(a => a.Size = take);

		/// <summary>
		/// A query to optionally limit the documents to use for the reindex operation.  
		/// </summary>
		public ReindexDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));

		/// <summary>
		/// A query to optionally limit the documents to use for the reindex operation.  
		/// </summary>
		public ReindexDescriptor<T> Query(QueryContainer query) => Assign(a => a.Query = query);

		/// <summary>
		/// CreateIndex selector, will be passed the a descriptor initialized with the settings from
		/// the index we're reindexing from
		/// </summary>
		public ReindexDescriptor<T> CreateIndex(Func<CreateIndexDescriptor, ICreateIndexRequest> createIndexSelector) =>
			Assign(a => a.CreateIndexRequest = createIndexSelector.InvokeOrDefault(new CreateIndexDescriptor(a.From)));

		/// <summary>
		/// CreateIndex selector, will be passed the a descriptor initialized with the settings from
		/// the index we're reindexing from
		/// </summary>
		public ReindexDescriptor<T> CreateIndex(ICreateIndexRequest createIndexRequest) => Assign(a => a.CreateIndexRequest = createIndexRequest);
	}
}