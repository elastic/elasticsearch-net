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

		public Fields StoredFields
		{
			get => Q<Fields>("stored_fields");
			set => Q("stored_fields", value);
		}
	}

	[DescriptorFor("Mget")]
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
			where T : class
		{
			_operations.AddIfNotNull(getSelector?.Invoke(new MultiGetOperationDescriptor<T>()));
			return this;
		}

		public MultiGetDescriptor GetMany<T>(IEnumerable<long> ids,
			Func<MultiGetOperationDescriptor<T>, long, IMultiGetOperation> getSelector = null
		)
			where T : class
		{
			foreach (var id in ids)
				_operations.Add(getSelector.InvokeOrDefault(new MultiGetOperationDescriptor<T>().Id(id), id));

			return this;
		}

		public MultiGetDescriptor GetMany<T>(IEnumerable<string> ids,
			Func<MultiGetOperationDescriptor<T>, string, IMultiGetOperation> getSelector = null
		)
			where T : class
		{
			foreach (var id in ids)
				_operations.Add(getSelector.InvokeOrDefault(new MultiGetOperationDescriptor<T>().Id(id), id));

			return this;
		}

		public MultiGetDescriptor GetMany<T>(IEnumerable<Id> ids, Func<MultiGetOperationDescriptor<T>, Id, IMultiGetOperation> getSelector = null)
			where T : class
		{
			foreach (var id in ids)
				_operations.Add(getSelector.InvokeOrDefault(new MultiGetOperationDescriptor<T>().Id(id), id));

			return this;
		}

		/// <summary> Default stored fields to load for each document. </summary>
		public MultiGetDescriptor StoredFields<T>(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) where T : class =>
			Assign(fields, (a, v) => a.StoredFields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <summary> Default stored fields to load for each document. </summary>
		public MultiGetDescriptor StoredFields(Fields fields) => Assign(fields, (a, v) => a.StoredFields = v);
	}
}
