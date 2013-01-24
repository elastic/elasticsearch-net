using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Nest.Domain;

namespace Nest
{
	public class BulkDescriptor
	{
		internal readonly IList<BaseSimpleGetDescriptor> _GetOperations = new List<BaseSimpleGetDescriptor>();

		internal string _FixedIndex { get; set; }
		internal string _FixedType { get; set; }

		internal IList<BaseBulkOperation> _Operations = new List<BaseBulkOperation>();

		public BulkDescriptor Create<T>(Func<BulkCreateDescriptor<T>, BulkCreateDescriptor<T>> bulkCreateSelector)
		{
			bulkCreateSelector.ThrowIfNull("bulkCreateSelector");
			var descriptor = bulkCreateSelector(new BulkCreateDescriptor<T>());
			if (descriptor == null)
				return this;
			this._Operations.Add(descriptor);
			return this;
		}
		
		public BulkDescriptor Index<T>(Func<BulkIndexDescriptor<T>, BulkIndexDescriptor<T>> bulkIndexSelector)
		{
			bulkIndexSelector.ThrowIfNull("bulkIndexSelector");
			var descriptor = bulkIndexSelector(new BulkIndexDescriptor<T>());
			if (descriptor == null)
				return this;
			this._Operations.Add(descriptor);
			return this;
		}

		public BulkDescriptor Delete<T>(Func<BulkDeleteDescriptor<T>, BulkDeleteDescriptor<T>> bulkDeleteSelector)
		{
			bulkDeleteSelector.ThrowIfNull("bulkDeleteSelector");
			var descriptor = bulkDeleteSelector(new BulkDeleteDescriptor<T>());
			if (descriptor == null)
				return this;
			this._Operations.Add(descriptor);
			return this;
		}

		/// <summary>
		/// Allows you to perform the multiget on a fixed path. 
		/// The index and optionally type specified here take precedence over the chained get operations.
		/// </summary>
		public void FixedPath(string index, string type = null)
		{
			index.ThrowIfNullOrEmpty("index");
			this._FixedIndex = index;
			this._FixedType = type;
		}
	}
}
