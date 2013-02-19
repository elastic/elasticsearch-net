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
		internal string _FixedIndex { get; set; }
		internal string _FixedType { get; set; }

		internal IList<BaseBulkOperation> _Operations = new List<BaseBulkOperation>();

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

		/// <summary>
		/// Allows you to perform the multiget on a fixed path. 
		/// Each operation that doesn't specify an index or type will use this fixed index/type
		/// over the default infered index and type.
		/// </summary>
		public BulkDescriptor FixedPath(string index, string type = null)
		{
			index.ThrowIfNullOrEmpty("index");
			this._FixedIndex = index;
			this._FixedType = type;
			return this;
		}
	}
}
