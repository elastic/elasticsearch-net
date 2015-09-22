using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	public partial interface IMultiPercolateRequest 
	{
		IList<IPercolateOperation> Percolations { get; set; }
	}

	public partial class MultiPercolateRequest 
	{
		public IList<IPercolateOperation> Percolations { get; set; }

	}

	[DescriptorFor("Mpercolate")]
	public partial class MultiPercolateDescriptor 
	{
		private IMultiPercolateRequest Self => this;

		IList<IPercolateOperation> IMultiPercolateRequest.Percolations { get; set; }

		public MultiPercolateDescriptor Percolate<T>(Func<PercolateDescriptor<T>, PercolateDescriptor<T>> getSelector) 
			where T : class
		{
			getSelector.ThrowIfNull("getSelector");
			var descriptor = getSelector(new PercolateDescriptor<T>());
			Self.Percolations.Add(descriptor);
			return this;
		}

		public MultiPercolateDescriptor PercolateMany<T>(IEnumerable<T> sources, Func<PercolateDescriptor<T>, T, PercolateDescriptor<T>> getSelector)
			where T : class
		{
			foreach (var source in sources)
			{
				getSelector.ThrowIfNull("getSelector");
				var descriptor = getSelector(new PercolateDescriptor<T>(), source);
				Self.Percolations.Add(descriptor);
			}
			return this;
		}

		public MultiPercolateDescriptor Count<T>(Func<PercolateCountDescriptor<T>, PercolateCountDescriptor<T>> getSelector) 
			where T : class
		{
			getSelector.ThrowIfNull("getSelector");
			var descriptor = getSelector(new PercolateCountDescriptor<T>());
			Self.Percolations.Add(descriptor);
			return this;
		}
	}
}
