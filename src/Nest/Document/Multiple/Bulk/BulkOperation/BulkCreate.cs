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
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IBulkCreateOperation<T> : IBulkOperation
		where T : class
	{
		T Document { get; set; }

		[DataMember(Name ="pipeline")]
		string Pipeline { get; set; }
	}

	[DataContract]
	public class BulkCreateOperation<T> : BulkOperationBase, IBulkCreateOperation<T>
		where T : class
	{
		public BulkCreateOperation(T document) => Document = document;

		public T Document { get; set; }

		public string Pipeline { get; set; }

		protected override Type ClrType => typeof(T);

		protected override string Operation => "create";

		protected override object GetBody() => Document;

		protected override Id GetIdForOperation(Inferrer inferrer) => Id ?? new Id(Document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => Routing ?? new Routing(Document);
	}

	[DataContract]
	public class BulkCreateDescriptor<T> : BulkOperationDescriptorBase<BulkCreateDescriptor<T>, IBulkCreateOperation<T>>, IBulkCreateOperation<T>
		where T : class
	{
		protected override Type BulkOperationClrType => typeof(T);
		protected override string BulkOperationType => "create";

		T IBulkCreateOperation<T>.Document { get; set; }

		string IBulkCreateOperation<T>.Pipeline { get; set; }

		protected override object GetBulkOperationBody() => Self.Document;

		protected override Id GetIdForOperation(Inferrer inferrer) => Self.Id ?? new Id(Self.Document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => Self.Routing ?? new Routing(Self.Document);

		/// <summary>
		/// The object to update, if id is not manually set it will be inferred from the object
		/// </summary>
		public BulkCreateDescriptor<T> Document(T @object) => Assign(@object, (a, v) => a.Document = v);

		/// <summary>
		/// The pipeline id to preprocess documents with
		/// </summary>
		public BulkCreateDescriptor<T> Pipeline(string pipeline) => Assign(pipeline, (a, v) => a.Pipeline = v);
	}
}
