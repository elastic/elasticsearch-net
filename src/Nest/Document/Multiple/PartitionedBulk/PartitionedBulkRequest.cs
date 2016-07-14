using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	public interface IPartitionedBulkRequest<T>
	{
		/// <summary> In case of a 429 (too busy) how long should we wait before retrying</summary>
		Time BackOff { get; set; }

		/// <summary> In case of a 429 (too busy) how many times should we automatically back off before failing</summary>
		int? NumberOfBackOffRetries { get; set; }

		/// <summary> The number of documents to send per bulk</summary>
		int? Size { get; set; }

		///<summary>
		/// The documents to send to elasticsearch, try to keep the IEnumerable lazy.
		/// The bulk observable will ToList() each partitioned page in a lazy fashion, to keep memory consumption to a minimum.
		/// It makes no sense to set this to an list of 1 million records because all of those million records need to be in memory
		/// Make use of c#'s lazy generator!
		///</summary>
		IEnumerable<T> Documents { get; }

		///<summary>The maximum number of bulk operations we want to have in flight at a time</summary>
		int? MaxDegreeOfParallelism { get; set; }

		///<summary>Default index for items which don't provide one</summary>
		IndexName Index { get; set; }

		///<summary>Default document type for items which don't provide one</summary>
		TypeName Type { get; set; }

		///<summary>Explicit write consistency setting for the operation</summary>
		Consistency? Consistency { get; set; }

		///<summary>Refresh the index after performing each operation (elasticsearch will refresh locally)</summary>
		bool Refresh { get; set; }

		///<summary>Refresh the index after performing ALL the bulk operations (NOTE this is an additional request)</summary>
		bool RefreshOnCompleted { get; set; }

		///<summary>Specific per bulk operation routing value</summary>
		string Routing { get; set; }

		///<summary>Explicit per operation timeout</summary>
		Time Timeout { get; set; }

		///<summary>The pipeline id to preprocess all the incoming documents with</summary>
		string Pipeline { get; set; }

	}

	public class PartitionedBulkRequest<T> : IPartitionedBulkRequest<T>
	{
		public Time BackOff { get; set; }
		public int? Size { get; set; }
		public int? MaxDegreeOfParallelism { get; set; }
		public int? NumberOfBackOffRetries { get; set; }
		public IEnumerable<T> Documents { get; private set; }

		public IndexName Index { get; set; }
		public TypeName Type { get; set; }
		public Consistency? Consistency { get; set; }
		public bool Refresh { get; set; }
		public bool RefreshOnCompleted { get; set; }
		public string Routing { get; set; }
		public Time Timeout { get; set; }
		public string Pipeline { get; set; }

		public PartitionedBulkRequest(IEnumerable<T> documents)
		{
			this.Documents = documents;
			this.Index = typeof(T);
			this.Type = typeof(T);
		}
	}

	public class PartitionedBulkDescriptor<T> : DescriptorBase<PartitionedBulkDescriptor<T>, IPartitionedBulkRequest<T>>, IPartitionedBulkRequest<T>
		where T : class
	{
		private readonly IEnumerable<T> _documents;

		Time IPartitionedBulkRequest<T>.BackOff { get; set; }
		int? IPartitionedBulkRequest<T>.Size { get; set; }
		int? IPartitionedBulkRequest<T>.NumberOfBackOffRetries { get; set; }
		int? IPartitionedBulkRequest<T>.MaxDegreeOfParallelism { get; set; }
		IEnumerable<T> IPartitionedBulkRequest<T>.Documents => this._documents;

		IndexName IPartitionedBulkRequest<T>.Index { get; set; }
		TypeName IPartitionedBulkRequest<T>.Type { get; set; }
		Consistency? IPartitionedBulkRequest<T>.Consistency { get; set; }
		bool IPartitionedBulkRequest<T>.Refresh { get; set; }
		bool IPartitionedBulkRequest<T>.RefreshOnCompleted { get; set; }
		string IPartitionedBulkRequest<T>.Routing { get; set; }
		Time IPartitionedBulkRequest<T>.Timeout { get; set; }
		string IPartitionedBulkRequest<T>.Pipeline { get; set; }

		public PartitionedBulkDescriptor(IEnumerable<T> documents)
		{
			this._documents = documents;
			((IPartitionedBulkRequest<T>)this).Index = typeof(T);
			((IPartitionedBulkRequest<T>)this).Type = typeof(T);
		}

		/// </inheritdoc>
		public PartitionedBulkDescriptor<T> MaxDegreeOfParallelism(int? parallism) =>
			Assign(a => a.MaxDegreeOfParallelism = parallism);

		/// </inheritdoc>
		public PartitionedBulkDescriptor<T> Size(int? size) => Assign(a => a.Size = size);

		/// </inheritdoc>
		public PartitionedBulkDescriptor<T> NumberOfBackOffRetries(int? backoffs) =>
			Assign(a => a.NumberOfBackOffRetries = backoffs);

		/// </inheritdoc>
		public PartitionedBulkDescriptor<T> BackOff(Time time) => Assign(a => a.BackOff = time);

		/// </inheritdoc>
		public PartitionedBulkDescriptor<T> Index(IndexName index) => Assign(a => a.Index = index);

		/// </inheritdoc>
		public PartitionedBulkDescriptor<T> Index<TOther>() where TOther : class => Assign(a => a.Index = typeof(TOther));

		/// </inheritdoc>
		public PartitionedBulkDescriptor<T> Type(TypeName type) => Assign(a => a.Type = type);

		/// </inheritdoc>
		public PartitionedBulkDescriptor<T> Type<TOther>() where TOther : class => Assign(a => a.Type = typeof(TOther));

		/// </inheritdoc>
		public PartitionedBulkDescriptor<T> Consistency(Consistency consistency) => Assign(p => p.Consistency = consistency);

		/// </inheritdoc>
		public PartitionedBulkDescriptor<T> RefreshOnCompleted(bool refresh = true) => Assign(p => p.RefreshOnCompleted = refresh);

		/// </inheritdoc>
		public PartitionedBulkDescriptor<T> Refresh(bool refresh = true) => Assign(p => p.Refresh = refresh);

		/// </inheritdoc>
		public PartitionedBulkDescriptor<T> Routing(string routing) => Assign(p => p.Routing = routing);

		/// </inheritdoc>
		public PartitionedBulkDescriptor<T> Timeout(Time timeout) => Assign(p => p.Timeout = timeout);

		/// </inheritdoc>
		public PartitionedBulkDescriptor<T> Pipeline(string pipeline) => Assign(p => p.Pipeline = pipeline);


	}
}
