using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	public partial interface IMultiGetRequest 
	{
		[JsonProperty("docs")]
		IList<IMultiGetOperation> GetOperations { get; set; }
	}

	public partial class MultiGetRequest 
	{
		public IList<IMultiGetOperation> GetOperations { get; set; }

		public MultiGetRequest() { }

		public MultiGetRequest(Indices indices, Types types)
			: base(p => p.Required(indices).Required(types))
		{ }

		public MultiGetRequest(Indices indices)
			: base(p => p.Required(indices))
		{ }

		public MultiGetRequest(Types types)
			: base(p => p.Required(types))
		{ }
	}

	[DescriptorFor("Mget")]
	public partial class MultiGetDescriptor 
	{
		private IMultiGetRequest Self => this;

		IList<IMultiGetOperation> IMultiGetRequest.GetOperations { get; set; } = new List<IMultiGetOperation>();

		public MultiGetDescriptor() { }

		public MultiGetDescriptor(Indices indices, Types types)
			: base(p => p.Required(indices).Required(types))
		{ }

		public MultiGetDescriptor(Indices indices)
			: base(p => p.Required(indices))
		{ }

		public MultiGetDescriptor(Types types)
			: base(p => p.Required(types))
		{ }

		public MultiGetDescriptor Get<T>(Func<MultiGetOperationDescriptor<T>, MultiGetOperationDescriptor<T>> getSelector)
			where T : class
		{
			getSelector.ThrowIfNull("getSelector");
			var descriptor = getSelector(new MultiGetOperationDescriptor<T>());
			Self.GetOperations.Add(descriptor);
			return this;

		}

		public MultiGetDescriptor GetMany<T>(IEnumerable<long> ids, Func<MultiGetOperationDescriptor<T>, long, MultiGetOperationDescriptor<T>> getSelector = null)
			where T : class
		{
			getSelector = getSelector ?? ((sg, s) => sg);
			foreach (var sg in ids.Select(id => getSelector(new MultiGetOperationDescriptor<T>().Id(id), id)))
				this.Self.GetOperations.Add(sg);
			return this;

		}
		public MultiGetDescriptor GetMany<T>(IEnumerable<string> ids, Func<MultiGetOperationDescriptor<T>, string, MultiGetOperationDescriptor<T>> getSelector = null)
			where T : class
		{
			getSelector = getSelector ?? ((sg, s) => sg);
			foreach (var sg in ids.Select(id => getSelector(new MultiGetOperationDescriptor<T>().Id(id), id)))
				this.Self.GetOperations.Add(sg);
			return this;

		}
	}
}
