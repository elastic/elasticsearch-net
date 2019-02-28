using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[MapsApi("mget.json")]
	[JsonFormatter(typeof(MultiGetRequestFormatter))]
	public partial interface IMultiGetRequest
	{
		[DataMember(Name = "docs")]
		IEnumerable<IMultiGetOperation> Documents { get; set; }
	}

	public partial class MultiGetRequest
	{
		public Fields StoredFields { get; set; }
		public IEnumerable<IMultiGetOperation> Documents { get; set; }
	}

	public partial class MultiGetDescriptor
	{
		private List<IMultiGetOperation> _operations = new List<IMultiGetOperation>();

		IEnumerable<IMultiGetOperation> IMultiGetRequest.Documents
		{
			get => _operations;
			set => _operations = value?.ToList();
		}

		Fields IMultiGetRequest.StoredFields
		{
			get => Q<Fields>("stored_fields");
			set => Q("stored_fields", value);
		}

		public MultiGetDescriptor Get<T>(Func<MultiGetOperationDescriptor<T>, IMultiGetOperation> getSelector)
			where T : class =>
			Assign(a => _operations.AddIfNotNull(getSelector?.Invoke(new MultiGetOperationDescriptor<T>())));

		public MultiGetDescriptor GetMany<T>(IEnumerable<long> ids,
			Func<MultiGetOperationDescriptor<T>, long, IMultiGetOperation> getSelector = null
		)
			where T : class =>
			Assign(a => _operations.AddRange(ids.Select(id => getSelector.InvokeOrDefault(new MultiGetOperationDescriptor<T>().Id(id), id))));

		public MultiGetDescriptor GetMany<T>(IEnumerable<string> ids,
			Func<MultiGetOperationDescriptor<T>, string, IMultiGetOperation> getSelector = null
		)
			where T : class =>
			Assign(a => _operations.AddRange(ids.Select(id => getSelector.InvokeOrDefault(new MultiGetOperationDescriptor<T>().Id(id), id))));

		public MultiGetDescriptor GetMany<T>(IEnumerable<Id> ids, Func<MultiGetOperationDescriptor<T>, Id, IMultiGetOperation> getSelector = null)
			where T : class =>
			Assign(a => _operations.AddRange(ids.Select(id => getSelector.InvokeOrDefault(new MultiGetOperationDescriptor<T>().Id(id), id))));

		/// <summary> Default stored fields to load for each document. </summary>
		public MultiGetDescriptor StoredFields<T>(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) where T : class =>
			Assign(a => a.StoredFields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <summary> Default stored fields to load for each document. </summary>
		public MultiGetDescriptor StoredFields(Fields fields) => Assign(a => a.StoredFields = fields);
	}
}
