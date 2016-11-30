using System;

namespace Nest
{
	public interface IReindexRequest
	{
		IndexName To { get; set; }
		IndexName From { get; set; }
		Types Types { get; set; }
		/// <summary>
		/// A search request can be scrolled by specifying the scroll parameter. The scroll parameter is a time value
		/// parameter (for example: scroll=5m), indicating for how long the nodes that participate in the search
		/// will maintain relevant resources in order to continue and support it. This is very similar in its idea to opening a cursor against a database.
		/// </summary>
		Time Scroll { get; set; }

		/// <summary>
		/// The number of hits to return. Defaults to 100. When using scroll search type,
		/// size is actually multiplied by the number of shards!
		/// </summary>
		int? Size { get; set; }

		/// <summary>
		/// A query to optionally limit the documents to use for the reindex operation.
		/// </summary>
		QueryContainer Query { get; set; }

		/// <summary>
		/// Do not send a create index call on <see cref="To"/>, assume the index has been created outside of the reindex.
		/// </summary>
		bool OmitIndexCreation { get; set; }

		/// <summary>
		/// Describe how the newly created index should be created. Remember you can also register Index Templates for more dynamic usecases.
		/// </summary>
		ICreateIndexRequest CreateIndexRequest { get; set; }
	}

	public class ReindexRequest : IReindexRequest
	{
		/// <inheritdoc/>
		public bool OmitIndexCreation { get; set; }
		/// <inheritdoc/>
		public IndexName To { get; set; }
		/// <inheritdoc/>
		public IndexName From { get; set; }
		/// <inheritdoc/>
		public Types Types { get; set; }
		/// <inheritdoc/>
		public Time Scroll { get; set; }
		/// <inheritdoc/>
		public int? Size { get; set; }
		/// <inheritdoc/>
		public QueryContainer Query { get; set; }
		/// <inheritdoc/>
		public ICreateIndexRequest CreateIndexRequest { get; set; }

		public ReindexRequest(IndexName from, IndexName to, Types types)
		{
			this.To = to;
			this.From = from;
			this.Types = types;
		}
	}

	public class ReindexDescriptor<T> : DescriptorBase<ReindexDescriptor<T>, IReindexRequest>, IReindexRequest where T : class
	{
		bool IReindexRequest.OmitIndexCreation { get; set; }
		IndexName IReindexRequest.To { get; set; }
		IndexName IReindexRequest.From { get; set; }
		Types IReindexRequest.Types { get; set; }
		Time IReindexRequest.Scroll { get; set; }
		int? IReindexRequest.Size { get; set; }
		QueryContainer IReindexRequest.Query { get; set; }
		ICreateIndexRequest IReindexRequest.CreateIndexRequest { get; set; }

		public ReindexDescriptor(IndexName from, IndexName to)
		{
			Assign(a => a.From = from)
			.Assign(a => a.To = to)
			.Assign(a => a.Types = typeof(T));
		}

		/// <inheritdoc/>
		public ReindexDescriptor<T> Scroll(Time scrollTime) => Assign(a => a.Scroll = scrollTime);

		/// <inheritdoc/>
		public ReindexDescriptor<T> Size(int? size) => Assign(a => a.Size = size);

		/// <inheritdoc/>
		public ReindexDescriptor<T> Take(int? take) => Assign(a => a.Size = take);

		/// <inheritdoc/>
		public ReindexDescriptor<T> OmitIndexCreation(bool omit = true) => Assign(a => a.OmitIndexCreation = true);

		/// <inheritdoc/>
		public ReindexDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));

		/// <inheritdoc/>
		public ReindexDescriptor<T> Query(QueryContainer query) => Assign(a => a.Query = query);

		/// <summary>
		/// Specify the document types to reindex. By default, will be <typeparamref name="T"/>
		/// </summary>
		public ReindexDescriptor<T> Type(Types type) => Assign(a => a.Types = type);

		/// <summary>
		/// Reindex all document types.
		/// </summary>
		public ReindexDescriptor<T> AllTypes() => this.Type(Types.AllTypes);

		/// <summary>
		/// CreateIndex selector if not explicitly specified will reuse settings from the originating index
		/// </summary>
		public ReindexDescriptor<T> CreateIndex(Func<CreateIndexDescriptor, ICreateIndexRequest> createIndexSelector) =>
			Assign(a => a.CreateIndexRequest = createIndexSelector.InvokeOrDefault(new CreateIndexDescriptor(a.From)));
	}
}
