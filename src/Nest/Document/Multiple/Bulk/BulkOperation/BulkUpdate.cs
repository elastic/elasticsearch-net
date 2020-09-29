// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IBulkUpdateOperation<TDocument, TPartialDocument> : IBulkOperation
		where TDocument : class
		where TPartialDocument : class
	{
		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		TPartialDocument Doc { get; set; }

		/// <summary>
		/// Instead of sending a partial doc with <see cref="Doc" /> plus an upsert doc
		/// with <see cref="Upsert" />, setting <see cref="DocAsUpsert" /> to <c>true</c> will
		/// use the contents of doc as the upsert value.
		/// </summary>
		bool? DocAsUpsert { get; set; }

		/// <summary>
		/// Infers the id of the object to update from the provided object.
		/// See <see cref="Doc" /> to apply a partial object merge.
		/// </summary>
		TDocument IdFrom { get; set; }

		/// <summary>
		/// A script to specify the update.
		/// </summary>
		IScript Script { get; set; }

		/// <summary>
		/// If you would like your script to run regardless of whether the document exists or not — i.e. the script handles
		/// initializing the document instead of the upsert element — then set scripted_upsert to true
		/// </summary>
		bool? ScriptedUpsert { get; set; }

		/// <summary>
		/// A document to upsert when the specified document to be updated is not found
		/// </summary>
		TDocument Upsert { get; set; }

		long? IfSequenceNumber { get; set; }

		long? IfPrimaryTerm { get; set; }

		/// <summary>
		/// True or false to return the _source field or not, or a list of fields to return.
		/// </summary>
		[DataMember(Name = "_source")]
		Union<bool, ISourceFilter> Source { get; set; }
	}

	[DataContract]
	public class BulkUpdateOperation<TDocument, TPartialDocument> : BulkOperationBase, IBulkUpdateOperation<TDocument, TPartialDocument>
		where TDocument : class
		where TPartialDocument : class
	{
		public BulkUpdateOperation(Id id) => Id = id;

		/// <summary>
		/// Create a new bulk operation
		/// </summary>
		/// <param name="idFrom">Use this document to infer the id from</param>
		/// <param name="useIdFromAsUpsert">Use the document to infer on as the upsert document in this update operation</param>
		public BulkUpdateOperation(TDocument idFrom, bool useIdFromAsUpsert = false)
		{
			IdFrom = idFrom;
			if (useIdFromAsUpsert)
				Upsert = idFrom;
		}

		/// <summary>
		/// Create a new Bulk Operation
		/// </summary>
		/// <param name="idFrom">Use this document to infer the id from</param>
		/// <param name="update">The partial update document (doc) to send as update</param>
		/// <param name="useIdFromAsUpsert">Use the document to infer on as the upsert document in this update operation</param>
		public BulkUpdateOperation(TDocument idFrom, TPartialDocument update, bool useIdFromAsUpsert = false)
		{
			IdFrom = idFrom;
			if (useIdFromAsUpsert)
				Upsert = idFrom;
			Doc = update;
		}

		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		public TPartialDocument Doc { get; set; }

		/// <summary>
		/// Instead of sending a partial doc with <see cref="Doc" /> plus an upsert doc
		/// with <see cref="Upsert" />, setting <see cref="DocAsUpsert" /> to <c>true</c> will
		/// use the contents of doc as the upsert value.
		/// </summary>
		public bool? DocAsUpsert { get; set; }

		/// <summary>
		/// Infers the id of the object to update from the provided object.
		/// See <see cref="Doc" /> to apply a partial object merge.
		/// </summary>
		public TDocument IdFrom { get; set; }

		/// <summary>
		/// A script to specify the update.
		/// </summary>
		public IScript Script { get; set; }

		/// <summary>
		/// If you would like your script to run regardless of whether the document exists or not — i.e. the script handles
		/// initializing the document instead of the upsert element — then set scripted_upsert to true
		/// </summary>
		public bool? ScriptedUpsert { get; set; }

		/// <summary>
		/// A document to upsert when the specified document to be updated is not found
		/// </summary>
		public TDocument Upsert { get; set; }

		public long? IfSequenceNumber { get; set; }

		public long? IfPrimaryTerm { get; set; }

		/// <summary>
		/// True or false to return the _source field or not, or a list of fields to return.
		/// </summary>
		public Union<bool, ISourceFilter> Source { get; set; }

		protected override Type ClrType => typeof(TDocument);

		protected override string Operation => "update";

		protected override Id GetIdForOperation(Inferrer inferrer) =>
			Id ?? new Id(new[] { IdFrom, Upsert }.FirstOrDefault(o => o != null));

		protected override Routing GetRoutingForOperation(Inferrer inferrer)
		{
			if (Routing != null)
				return Routing;

			if (IdFrom != null)
				return new Routing(IdFrom);

			if (Upsert != null)
				return new Routing(Upsert);

			return null;
		}

		protected override object GetBody() =>
			new BulkUpdateBody<TDocument, TPartialDocument>
			{
				PartialUpdate = Doc,
				Script = Script,
				Upsert = Upsert,
				DocAsUpsert = DocAsUpsert,
				ScriptedUpsert = ScriptedUpsert,
				IfPrimaryTerm = IfPrimaryTerm,
				IfSequenceNumber = IfSequenceNumber,
				Source = Source
			};
	}

	[DataContract]
	public class BulkUpdateDescriptor<TDocument, TPartialDocument>
		: BulkOperationDescriptorBase<BulkUpdateDescriptor<TDocument, TPartialDocument>, IBulkUpdateOperation<TDocument, TPartialDocument>>
			, IBulkUpdateOperation<TDocument, TPartialDocument>
		where TDocument : class
		where TPartialDocument : class
	{
		protected override Type BulkOperationClrType => typeof(TDocument);
		protected override string BulkOperationType => "update";
		TPartialDocument IBulkUpdateOperation<TDocument, TPartialDocument>.Doc { get; set; }
		bool? IBulkUpdateOperation<TDocument, TPartialDocument>.DocAsUpsert { get; set; }

		TDocument IBulkUpdateOperation<TDocument, TPartialDocument>.IdFrom { get; set; }
		IScript IBulkUpdateOperation<TDocument, TPartialDocument>.Script { get; set; }
		bool? IBulkUpdateOperation<TDocument, TPartialDocument>.ScriptedUpsert { get; set; }
		TDocument IBulkUpdateOperation<TDocument, TPartialDocument>.Upsert { get; set; }

		long? IBulkUpdateOperation<TDocument, TPartialDocument>.IfSequenceNumber { get; set; }

		long? IBulkUpdateOperation<TDocument, TPartialDocument>.IfPrimaryTerm { get; set; }

		Union<bool, ISourceFilter> IBulkUpdateOperation<TDocument, TPartialDocument>.Source { get; set; }

		protected override object GetBulkOperationBody() =>
			new BulkUpdateBody<TDocument, TPartialDocument>
			{
				PartialUpdate = Self.Doc,
				Script = Self.Script,
				Upsert = Self.Upsert,
				DocAsUpsert = Self.DocAsUpsert,
				ScriptedUpsert = Self.ScriptedUpsert,
				IfPrimaryTerm = Self.IfPrimaryTerm,
				IfSequenceNumber = Self.IfSequenceNumber,
				Source = Self.Source
			};

		protected override Id GetIdForOperation(Inferrer inferrer) =>
			Self.Id ?? new Id(new[] { Self.IdFrom, Self.Upsert }.FirstOrDefault(o => o != null));

		protected override Routing GetRoutingForOperation(Inferrer inferrer)
		{
			if (Self.Routing != null)
				return Self.Routing;

			if (Self.IdFrom != null)
				return new Routing(Self.IdFrom);

			if (Self.Upsert != null)
				return new Routing(Self.Upsert);

			return null;
		}

		/// <summary>
		/// Infers the id of the object to update from the provided
		/// <param name="object">object</param>
		/// .
		/// See <see cref="Doc(TPartialDocument)" /> to apply a partial object merge.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> IdFrom(TDocument @object, bool useAsUpsert = false)
		{
			Self.IdFrom = @object;
			return useAsUpsert ? Upsert(@object) : this;
		}

		/// <summary>
		/// A document to upsert when the specified document to be updated is not found
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Upsert(TDocument @object) => Assign(@object, (a, v) => a.Upsert = v);

		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Doc(TPartialDocument @object) => Assign(@object, (a, v) => a.Doc = v);

		/// <summary>
		/// Instead of sending a partial doc with <see cref="Doc(TPartialDocument)" /> plus an upsert doc
		/// with <see cref="Upsert(TDocument)" />, setting <see cref="DocAsUpsert" /> to <c>true</c> will
		/// use the contents of doc as the upsert value.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> DocAsUpsert(bool? partialDocumentAsUpsert = true) =>
			Assign(partialDocumentAsUpsert, (a, v) => a.DocAsUpsert = v);

		/// <summary>
		/// If you would like your script to run regardless of whether the document exists or not — i.e. the script handles
		/// initializing the document instead of the upsert element — then set scripted_upsert to true
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> ScriptedUpsert(bool? scriptedUpsert = true) =>
			Assign(scriptedUpsert, (a, v) => a.ScriptedUpsert = v);

		/// <summary>
		/// A script to specify the update.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		/// <summary>
		/// How many times an update should be retried in the case of a version conflict.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> RetriesOnConflict(int? retriesOnConflict) =>
			Assign(retriesOnConflict, (a, v) => a.RetriesOnConflict = v);

		/// <summary>
		/// Operations can be made conditional and only be performed if the last modification to the document was assigned the sequence number.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> IfSequenceNumber(long? seqNo) =>
			Assign(seqNo, (a, v) => a.IfSequenceNumber = v);

		/// <summary>
		/// Operations can be made conditional and only be performed if the last modification to the document was assigned the primary term.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> IfPrimaryTerm(long? primaryTerm) =>
			Assign(primaryTerm, (a, v) => a.IfPrimaryTerm = v);

		/// <summary>
		/// True or false to return the _source field or not, or a list of fields to return.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Source(Union<bool, ISourceFilter> source) =>
			Assign(source, (a, v) => a.Source = v);
	}
}
