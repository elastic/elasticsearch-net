/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

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
		protected sealed override void RequestDefaults(MultiGetRequestParameters parameters) =>
			parameters.CustomResponseBuilder = new MultiGetResponseBuilder(this);

		public Fields StoredFields { get; set; }
		public IEnumerable<IMultiGetOperation> Documents { get; set; }

	}

	public partial class MultiGetDescriptor
	{
		protected sealed override void RequestDefaults(MultiGetRequestParameters parameters) =>
			parameters.CustomResponseBuilder = new MultiGetResponseBuilder(this);

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
