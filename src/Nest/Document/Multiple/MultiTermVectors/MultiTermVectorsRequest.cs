using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A Multi termvectors API request
	/// </summary>
	[MapsApi("mtermvectors.json")]
	public partial interface IMultiTermVectorsRequest
	{
		/// <summary>
		/// The documents for which to generate term vectors
		/// </summary>
		[DataMember(Name = "docs")]
		IEnumerable<IMultiTermVectorOperation> Documents { get; set; }

		/// <summary>
		/// The ids of documents within the same index and type
		/// for which to generate term vectors. Must be used in
		/// conjunction with <see cref="Index" /> and <see cref="Type" />
		/// </summary>
		[DataMember(Name = "ids")]
		IEnumerable<Id> Ids { get; set; }
	}

	/// <inheritdoc cref="IMultiTermVectorsRequest" />
	public partial class MultiTermVectorsRequest
	{
		/// <inheritdoc />
		public IEnumerable<IMultiTermVectorOperation> Documents { get; set; }

		/// <inheritdoc />
		public IEnumerable<Id> Ids { get; set; }
	}

	/// <inheritdoc cref="IMultiTermVectorsRequest" />
	public partial class MultiTermVectorsDescriptor
	{
		private List<IMultiTermVectorOperation> _operations;

		IEnumerable<IMultiTermVectorOperation> IMultiTermVectorsRequest.Documents
		{
			get => _operations;
			set => _operations = value?.ToList();
		}

		IEnumerable<Id> IMultiTermVectorsRequest.Ids { get; set; }

		private List<IMultiTermVectorOperation> Operations =>
			_operations ?? (_operations = new List<IMultiTermVectorOperation>());

		// TODO: Rename to Documents in 7.x
		/// <summary>
		/// A document for which to generate term vectors
		/// </summary>
		public MultiTermVectorsDescriptor Get<T>(Func<MultiTermVectorOperationDescriptor<T>, IMultiTermVectorOperation> getSelector)
			where T : class =>
			Assign(a => Operations.AddIfNotNull(getSelector?.Invoke(new MultiTermVectorOperationDescriptor<T>())));

		// TODO: Rename to Documents in 7.x
		/// <inheritdoc cref="IMultiTermVectorsRequest.Documents" />
		public MultiTermVectorsDescriptor GetMany<T>(IEnumerable<long> ids,
			Func<MultiTermVectorOperationDescriptor<T>, long, IMultiTermVectorOperation> getSelector = null
		)
			where T : class =>
			Assign(a => Operations.AddRange(ids.Select(id => getSelector.InvokeOrDefault(new MultiTermVectorOperationDescriptor<T>().Id(id), id))));

		// TODO: Rename to Documents in 7.x
		/// <inheritdoc cref="IMultiTermVectorsRequest.Documents" />
		public MultiTermVectorsDescriptor GetMany<T>(IEnumerable<string> ids,
			Func<MultiTermVectorOperationDescriptor<T>, string, IMultiTermVectorOperation> getSelector = null
		)
			where T : class =>
			Assign(a => Operations.AddRange(ids.Select(id => getSelector.InvokeOrDefault(new MultiTermVectorOperationDescriptor<T>().Id(id), id))));

		// TODO: Rename to Documents in 7.x
		/// <inheritdoc cref="IMultiTermVectorsRequest.Documents" />
		public MultiTermVectorsDescriptor GetMany<T>(IEnumerable<Id> ids,
			Func<MultiTermVectorOperationDescriptor<T>, Id, IMultiTermVectorOperation> getSelector = null
		)
			where T : class =>
			Assign(a => Operations.AddRange(ids.Select(id => getSelector.InvokeOrDefault(new MultiTermVectorOperationDescriptor<T>().Id(id), id))));

		/// <inheritdoc cref="IMultiTermVectorsRequest.Ids" />
		public MultiTermVectorsDescriptor Ids(IEnumerable<Id> ids) => Assign(a => a.Ids = ids);

		/// <inheritdoc cref="IMultiTermVectorsRequest.Ids" />
		public MultiTermVectorsDescriptor Ids(params Id[] ids) => Assign(a => a.Ids = ids);
	}
}
