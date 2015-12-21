using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{

	[JsonConverter(typeof(MultiGetRequestJsonConverter))]
	public partial interface IMultiGetRequest 
	{
		[JsonProperty("docs")]
		IEnumerable<IMultiGetOperation> Documents { get; set; }
	}

	public partial class MultiGetRequest 
	{
		public IEnumerable<IMultiGetOperation> Documents { get; set; }
	}

	[DescriptorFor("Mget")]
	public partial class MultiGetDescriptor
	{
		private List<IMultiGetOperation> _operations = new List<IMultiGetOperation>();

		IEnumerable<IMultiGetOperation> IMultiGetRequest.Documents { get { return this._operations; } set { this._operations = value?.ToList(); } }

		public MultiGetDescriptor Get<T>(Func<MultiGetOperationDescriptor<T>, IMultiGetOperation> getSelector)
			where T : class => 
			Assign(a => this._operations.AddIfNotNull(getSelector?.Invoke(new MultiGetOperationDescriptor<T>())));

		public MultiGetDescriptor GetMany<T>(IEnumerable<long> ids,
			Func<MultiGetOperationDescriptor<T>, long, IMultiGetOperation> getSelector = null)
			where T : class => 
			Assign(a => this._operations.AddRange(ids.Select(id => getSelector.InvokeOrDefault(new MultiGetOperationDescriptor<T>().Id(id), id))));

		public MultiGetDescriptor GetMany<T>(IEnumerable<string> ids, Func<MultiGetOperationDescriptor<T>, string, IMultiGetOperation> getSelector = null)
			where T : class =>
			Assign(a => this._operations.AddRange(ids.Select(id => getSelector.InvokeOrDefault(new MultiGetOperationDescriptor<T>().Id(id), id))));

		public MultiGetDescriptor GetMany<T>(IEnumerable<Id> ids, Func<MultiGetOperationDescriptor<T>, Id, IMultiGetOperation> getSelector = null)
			where T : class =>
			Assign(a => this._operations.AddRange(ids.Select(id => getSelector.InvokeOrDefault(new MultiGetOperationDescriptor<T>().Id(id), id))));
	}
}
