// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
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

		/// <summary>
		/// A document for which to generate term vectors
		/// </summary>
		public MultiTermVectorsDescriptor Documents<T>(Func<MultiTermVectorOperationDescriptor<T>, IMultiTermVectorOperation> selector)
			where T : class
		{
			Operations.AddIfNotNull(selector?.Invoke(new MultiTermVectorOperationDescriptor<T>()));
			return this;
		}

		/// <inheritdoc cref="IMultiTermVectorsRequest.Documents" />
		public MultiTermVectorsDescriptor Documents<T>(IEnumerable<long> ids,
			Func<MultiTermVectorOperationDescriptor<T>, long, IMultiTermVectorOperation> selector = null
		)
			where T : class
		{
			foreach (var id in ids)
				Operations.Add(selector.InvokeOrDefault(new MultiTermVectorOperationDescriptor<T>().Id(id), id));

			return this;
		}

		/// <inheritdoc cref="IMultiTermVectorsRequest.Documents" />
		public MultiTermVectorsDescriptor Documents<T>(IEnumerable<string> ids,
			Func<MultiTermVectorOperationDescriptor<T>, string, IMultiTermVectorOperation> getSelector = null
		)
			where T : class
		{
			foreach (var id in ids)
				Operations.Add(getSelector.InvokeOrDefault(new MultiTermVectorOperationDescriptor<T>().Id(id), id));

			return this;
		}

		/// <inheritdoc cref="IMultiTermVectorsRequest.Documents" />
		public MultiTermVectorsDescriptor Documents<T>(IEnumerable<Id> ids,
			Func<MultiTermVectorOperationDescriptor<T>, Id, IMultiTermVectorOperation> getSelector = null
		)
			where T : class
		{
			foreach (var id in ids)
				Operations.Add(getSelector.InvokeOrDefault(new MultiTermVectorOperationDescriptor<T>().Id(id), id));

			return this;
		}

		/// <inheritdoc cref="IMultiTermVectorsRequest.Ids" />
		public MultiTermVectorsDescriptor Ids(IEnumerable<Id> ids) => Assign(ids, (a, v) => a.Ids = v);

		/// <inheritdoc cref="IMultiTermVectorsRequest.Ids" />
		public MultiTermVectorsDescriptor Ids(params Id[] ids) => Assign(ids, (a, v) => a.Ids = v);
	}
}
