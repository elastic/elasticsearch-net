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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IBulkIndexOperation<T> : IBulkOperation
	{
		[JsonFormatter(typeof(SourceWriteFormatter<>))]
		T Document { get; set; }

		[DataMember(Name ="_percolate")]
		string Percolate { get; set; }

		[DataMember(Name ="pipeline")]
		string Pipeline { get; set; }

		[DataMember(Name = "if_seq_no")]
		long? IfSequenceNumber { get; set; }

		[DataMember(Name = "if_primary_term")]
		long? IfPrimaryTerm { get; set; }
	}

	[DataContract]
	public class BulkIndexOperation<T> : BulkOperationBase, IBulkIndexOperation<T>
		where T : class
	{
		public BulkIndexOperation(T document) => Document = document;

		public T Document { get; set; }

		public string Percolate { get; set; }

		public string Pipeline { get; set; }

		public long? IfSequenceNumber { get; set; }

		public long? IfPrimaryTerm { get; set; }

		protected override Type ClrType => typeof(T);

		protected override string Operation => "index";

		protected override object GetBody() => Document;

		protected override Id GetIdForOperation(Inferrer inferrer) => Id ?? new Id(Document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => Routing ?? new Routing(Document);
	}

	[DataContract]
	public class BulkIndexDescriptor<T> : BulkOperationDescriptorBase<BulkIndexDescriptor<T>, IBulkIndexOperation<T>>, IBulkIndexOperation<T>
		where T : class
	{
		protected override Type BulkOperationClrType => typeof(T);
		protected override string BulkOperationType => "index";
		T IBulkIndexOperation<T>.Document { get; set; }
		string IBulkIndexOperation<T>.Percolate { get; set; }
		string IBulkIndexOperation<T>.Pipeline { get; set; }
		long? IBulkIndexOperation<T>.IfSequenceNumber { get; set; }

		long? IBulkIndexOperation<T>.IfPrimaryTerm { get; set; }

		protected override object GetBulkOperationBody() => Self.Document;

		protected override Id GetIdForOperation(Inferrer inferrer) => Self.Id ?? new Id(Self.Document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => Self.Routing ?? new Routing(Self.Document);

		/// <summary>
		/// The object to index, if id is not manually set it will be inferred from the object
		/// </summary>
		public BulkIndexDescriptor<T> Document(T @object) => Assign(@object, (a, v) => a.Document = v);

		/// <summary>
		/// The pipeline id to preprocess documents with
		/// </summary>
		public BulkIndexDescriptor<T> Pipeline(string pipeline) => Assign(pipeline, (a, v) => a.Pipeline = v);

		public BulkIndexDescriptor<T> Percolate(string percolate) => Assign(percolate, (a, v) => a.Percolate = v);

		public BulkIndexDescriptor<T> IfSequenceNumber(long? seqNo) => Assign(seqNo, (a, v) => a.IfSequenceNumber = v);

		public BulkIndexDescriptor<T> IfPrimaryTerm(long? primaryTerm) => Assign(primaryTerm, (a, v) => a.IfPrimaryTerm = v);
	}
}
