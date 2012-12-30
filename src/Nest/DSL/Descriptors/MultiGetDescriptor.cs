using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	public class MultiGetDescriptor
	{
		internal readonly IList<BaseSimpleGetDescriptor> _GetOperations = new List<BaseSimpleGetDescriptor>();

		internal string _FixedIndex { get; set; }
		internal string _FixedType { get; set; }

		public MultiGetDescriptor Get<K>(Action<SimpleGetDescriptor<K>> getSelector) where K : class
		{
			getSelector.ThrowIfNull("getSelector");

			var descriptor = new SimpleGetDescriptor<K>();
			getSelector(descriptor);

			this._GetOperations.Add(descriptor);
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
