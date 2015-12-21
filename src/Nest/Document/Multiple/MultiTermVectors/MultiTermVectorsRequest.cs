using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IMultiTermVectorsRequest 
	{
		[JsonProperty("docs")]
		IEnumerable<IMultiTermVectorOperation> Documents { get; set; }
	}

	public partial class MultiTermVectorsRequest 
	{
		public IEnumerable<IMultiTermVectorOperation> Documents { get; set; }
	}

	[DescriptorFor("Mtermvectors")]
	public partial class MultiTermVectorsDescriptor
	{
		private List<IMultiTermVectorOperation> _operations = new List<IMultiTermVectorOperation>();
		IEnumerable<IMultiTermVectorOperation> IMultiTermVectorsRequest.Documents { get { return this._operations; }
			set { this._operations = value?.ToList(); }
		}	

		public MultiTermVectorsDescriptor Get<T>(Func<MultiTermVectorOperationDescriptor<T>, IMultiTermVectorOperation> getSelector)
			where T : class => 
			Assign(a => this._operations.AddIfNotNull(getSelector?.Invoke(new MultiTermVectorOperationDescriptor<T>())));

		public MultiTermVectorsDescriptor GetMany<T>(IEnumerable<long> ids,
			Func<MultiTermVectorOperationDescriptor<T>, long, IMultiTermVectorOperation> getSelector = null)
			where T : class => 
			Assign(a => this._operations.AddRange(ids.Select(id => getSelector.InvokeOrDefault(new MultiTermVectorOperationDescriptor<T>().Id(id), id))));

		public MultiTermVectorsDescriptor GetMany<T>(IEnumerable<string> ids, Func<MultiTermVectorOperationDescriptor<T>, string, IMultiTermVectorOperation> getSelector = null)
			where T : class =>
			Assign(a => this._operations.AddRange(ids.Select(id => getSelector.InvokeOrDefault(new MultiTermVectorOperationDescriptor<T>().Id(id), id))));

		public MultiTermVectorsDescriptor GetMany<T>(IEnumerable<Id> ids, Func<MultiTermVectorOperationDescriptor<T>, Id, IMultiTermVectorOperation> getSelector = null)
			where T : class =>
			Assign(a => this._operations.AddRange(ids.Select(id => getSelector.InvokeOrDefault(new MultiTermVectorOperationDescriptor<T>().Id(id), id))));

	}
}
