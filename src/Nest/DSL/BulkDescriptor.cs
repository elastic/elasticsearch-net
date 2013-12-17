using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Nest.Domain;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace Nest
{
	public partial class BulkDescriptor : QueryPathDescriptor
	{
		internal string _FixedIndex { get; set; }
		internal string _FixedType { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		internal Consistency? _Consistency { get; set; }

		internal bool? _Refresh { get; set; }

		internal IList<BaseBulkOperation> _Operations = new SynchronizedCollection<BaseBulkOperation>();

		public BulkDescriptor Create<T>(Func<BulkCreateDescriptor<T>, BulkCreateDescriptor<T>> bulkCreateSelector) where T : class
		{
			bulkCreateSelector.ThrowIfNull("bulkCreateSelector");
			var descriptor = bulkCreateSelector(new BulkCreateDescriptor<T>());
			if (descriptor == null)
				return this;
			this._Operations.Add(descriptor);
			return this;
		}

		public BulkDescriptor Index<T>(Func<BulkIndexDescriptor<T>, BulkIndexDescriptor<T>> bulkIndexSelector) where T : class
		{
			bulkIndexSelector.ThrowIfNull("bulkIndexSelector");
			var descriptor = bulkIndexSelector(new BulkIndexDescriptor<T>());
			if (descriptor == null)
				return this;
			this._Operations.Add(descriptor);
			return this;
		}

		public BulkDescriptor Delete<T>(Func<BulkDeleteDescriptor<T>, BulkDeleteDescriptor<T>> bulkDeleteSelector) where T : class
		{
			bulkDeleteSelector.ThrowIfNull("bulkDeleteSelector");
			var descriptor = bulkDeleteSelector(new BulkDeleteDescriptor<T>());
			if (descriptor == null)
				return this;
			this._Operations.Add(descriptor);
			return this;
		}
		public BulkDescriptor Update<T>(Func<BulkUpdateDescriptor<T, T>, BulkUpdateDescriptor<T, T>> bulkUpdateSelector) where T : class
		{
			return this.Update<T, T>(bulkUpdateSelector);
		}
		public BulkDescriptor Update<T, K>(Func<BulkUpdateDescriptor<T, K>, BulkUpdateDescriptor<T, K>> bulkUpdateSelector)
			where T : class
			where K : class
		{
			bulkUpdateSelector.ThrowIfNull("bulkUpdateSelector");
			var descriptor = bulkUpdateSelector(new BulkUpdateDescriptor<T, K>());
			if (descriptor == null)
				return this;
			this._Operations.Add(descriptor);
			return this;
		}

		/// <summary>
		/// When making bulk calls, you can require a minimum number of active shards in the partition 
		/// through the consistency parameter. The values allowed are one, quorum, and all. It defaults to the node level
		/// setting of action.write_consistency, which in turn defaults to quorum.
		/// <pre>
		/// For example, in a N shards with 2 replicas index, there will have to be at least 2 active shards within the relevant partition (quorum) for the 
		/// operation to succeed. In a N shards with 1 replica scenario, there will need to be a single shard active (in this case, one and quorum is the same).
		/// </pre>
		/// </summary>
		/// <param name="consistency"></param>
		/// <returns></returns>
		public BulkDescriptor Consistency(Consistency consistency)
		{
			this._Consistency = consistency;
			return this;
		}

		public BulkDescriptor FixedPath(string index, string type = null)
		{
			this.Index(index);
			this.Type(type);
			return this;
		}
	}
}
