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
	public interface IBulkDeleteOperation<T> : IBulkOperation
		where T : class
	{
		T Document { get; set; }

		[DataMember(Name = "if_seq_no")]
		long? IfSequenceNumber { get; set; }

		[DataMember(Name = "if_primary_term")]
		long? IfPrimaryTerm { get; set; }
	}

	[DataContract]
	public class BulkDeleteOperation<T> : BulkOperationBase, IBulkDeleteOperation<T>
		where T : class
	{
		public BulkDeleteOperation(T document) => Document = document;

		public BulkDeleteOperation(Id id) => Id = id;

		public T Document { get; set; }

		public long? IfSequenceNumber { get; set; }

		public long? IfPrimaryTerm { get; set; }

		protected override Type ClrType => typeof(T);

		protected override string Operation => "delete";

		protected override object GetBody() => null;

		protected override Id GetIdForOperation(Inferrer inferrer) => Id ?? new Id(Document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => Routing ?? new Routing(Document);
	}

	[DataContract]
	public class BulkDeleteDescriptor<T> : BulkOperationDescriptorBase<BulkDeleteDescriptor<T>, IBulkDeleteOperation<T>>, IBulkDeleteOperation<T>
		where T : class
	{
		protected override Type BulkOperationClrType => typeof(T);
		protected override string BulkOperationType => "delete";
		long? IBulkDeleteOperation<T>.IfSequenceNumber { get; set; }
		long? IBulkDeleteOperation<T>.IfPrimaryTerm { get; set; }

		T IBulkDeleteOperation<T>.Document { get; set; }

		protected override object GetBulkOperationBody() => null;

		protected override Id GetIdForOperation(Inferrer inferrer) => Self.Id ?? new Id(Self.Document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => Self.Routing ?? new Routing(Self.Document);

		/// <summary>
		/// The object to infer the id off, (if id is not passed using Id())
		/// </summary>
		public BulkDeleteDescriptor<T> Document(T @object) => Assign(@object, (a, v) => a.Document = v);

		public BulkDeleteDescriptor<T> IfSequenceNumber(long? sequenceNumber) => Assign(sequenceNumber, (a, v) => a.IfSequenceNumber = v);

		public BulkDeleteDescriptor<T> IfPrimaryTerm(long? primaryTerm) => Assign(primaryTerm, (a, v) => a.IfPrimaryTerm = v);
	}
}
