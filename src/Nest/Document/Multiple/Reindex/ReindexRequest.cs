using System;

namespace Nest
{
	public interface IReindexRequest 
	{
		IndexName To { get; set; }
		IndexName From { get; set; }
		Types Type { get; set; }
		Time Scroll { get; set; }
		int? Size { get; set; }
		
		QueryContainer Query { get; set; }

		ICreateIndexRequest CreateIndexRequest { get; set; }

		IPutMappingRequest PutMappingRequest { get; set; } 
	}

	public class ReindexRequest : IReindexRequest
	{
		public IndexName To { get; set; }
		public IndexName From { get; set; }
		public Types Type { get; set; }
		public Time Scroll { get; set; }
		public int? Size { get; set; }
		public QueryContainer Query { get; set; }
		public ICreateIndexRequest CreateIndexRequest { get; set; }
		public IPutMappingRequest PutMappingRequest { get; set; } 
		public ReindexRequest(IndexName from, IndexName to, Types type)
		{
			this.To = to;
			this.From = from;
			this.Type = type;
		}
	}

	public class ReindexDescriptor<T> : DescriptorBase<ReindexDescriptor<T>, IReindexRequest>, IReindexRequest where T : class
	{
		IndexName IReindexRequest.To { get; set; }
		IndexName IReindexRequest.From { get; set; }
		Types IReindexRequest.Type { get; set; }
		Time IReindexRequest.Scroll { get; set; }
		int? IReindexRequest.Size { get; set; }
		QueryContainer IReindexRequest.Query { get; set; }
		ICreateIndexRequest IReindexRequest.CreateIndexRequest { get; set; }
		IPutMappingRequest IReindexRequest.PutMappingRequest { get; set; } 
		
		public ReindexDescriptor(IndexName from, IndexName to)
		{
			Assign(a => a.From = from)
			.Assign(a => a.To = to)
			.Assign(a => a.Type = typeof(T));
		}

		/// <summary>
		/// A search request can be scrolled by specifying the scroll parameter. The scroll parameter is a time value parameter (for example: scroll=5m), indicating for how long the nodes that participate in the search will maintain relevant resources in order to continue and support it. This is very similar in its idea to opening a cursor against a database.
		/// </summary>
		/// <param name="scrollTime">The scroll parameter is a time value parameter (for example: scroll=5m)</param>
		/// <returns></returns>
		public ReindexDescriptor<T> Scroll(Time scrollTime) => Assign(a => a.Scroll = scrollTime);

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
		/// Specify the document types to reindex. By default, will be <typeparamref name="T"/>  
		/// </summary>
		public ReindexDescriptor<T> Type(Types type) => Assign(a => a.Type = type);

		/// <summary>
		/// Reindex all document types.
		/// </summary>
		public ReindexDescriptor<T> AllTypes() => this.Type(Types.AllTypes); 

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