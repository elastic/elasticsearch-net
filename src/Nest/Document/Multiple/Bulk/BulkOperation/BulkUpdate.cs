using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IBulkUpdateOperation<TDocument, TPartialDocument> : IBulkOperation
		where TDocument : class
		where TPartialDocument : class
	{
		/// <summary>
		/// Infers the id of the object to update from the provided object.
		/// See <see cref="Doc"/> to apply a partial object merge.
		/// </summary>
		TDocument IdFrom { get; set; }

		/// <summary>
		/// A document to upsert when the specified document to be updated is not found
		/// </summary>
		TDocument Upsert { get; set; }

		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		TPartialDocument Doc { get; set; }

		/// <summary>
		/// Instead of sending a partial doc with <see cref="Doc"/> plus an upsert doc
		/// with <see cref="Upsert"/>, setting <see cref="DocAsUpsert"/> to <c>true</c> will
		/// use the contents of doc as the upsert value.
		/// </summary>
		bool? DocAsUpsert { get; set; }

		/// <summary>
		/// If you would like your script to run regardless of whether the document exists or not — i.e. the script handles
		/// initializing the document instead of the upsert element — then set scripted_upsert to true
		/// </summary>
		bool? ScriptedUpsert { get; set; }

		/// <summary>
		/// A script to specify the update.
		/// </summary>
		IScript Script { get; set; }
	}

	public class BulkUpdateOperation<TDocument, TPartialDocument> : BulkOperationBase, IBulkUpdateOperation<TDocument, TPartialDocument>
		where TDocument : class
		where TPartialDocument : class
	{

		public BulkUpdateOperation(Id id) { this.Id = id; }

		/// <summary>
		/// Create a new bulk operation
		/// </summary>
		/// <param name="idFrom">Use this document to infer the id from</param>
		/// <param name="useIdFromAsUpsert">Use the document to infer on as the upsert document in this update operation</param>
		public BulkUpdateOperation(TDocument idFrom, bool useIdFromAsUpsert = false)
		{
			this.IdFrom = idFrom;
			if (useIdFromAsUpsert)
				this.Upsert = idFrom;
		}

		/// <summary>
		/// Create a new Bulk Operation
		/// </summary>
		/// <param name="idFrom">Use this document to infer the id from</param>
		/// <param name="update">The partial update document (doc) to send as update</param>
		/// <param name="useIdFromAsUpsert">Use the document to infer on as the upsert document in this update operation</param>
		public BulkUpdateOperation(TDocument idFrom, TPartialDocument update, bool useIdFromAsUpsert = false)
		{
			this.IdFrom = idFrom;
			if (useIdFromAsUpsert)
				this.Upsert = idFrom;
			this.Doc = update;
		}

		protected override string Operation => "update";

		protected override Type ClrType => typeof(TDocument);

		protected override Id GetIdForOperation(Inferrer inferrer) =>
			this.Id ?? new Id(new[] { this.IdFrom, this.Upsert }.FirstOrDefault(o=>o != null));

		protected override Routing GetRoutingForOperation(Inferrer inferrer) =>
			this.Routing ?? new Routing(new[] { this.IdFrom, this.Upsert }.FirstOrDefault(o=>o != null));

		protected override object GetBody() =>
			new BulkUpdateBody<TDocument, TPartialDocument>
		{
			_PartialUpdate = this.Doc,
			_Script = this.Script,
			_Upsert = this.Upsert,
			_DocAsUpsert = this.DocAsUpsert,
			_ScriptedUpsert = this.ScriptedUpsert
		};

		/// <summary>
		/// Infers the id of the object to update from the provided object.
		/// See <see cref="Doc"/> to apply a partial object merge.
		/// </summary>
		public TDocument IdFrom { get; set; }

		/// <summary>
		/// A document to upsert when the specified document to be updated is not found
		/// </summary>
		public TDocument Upsert { get; set; }

		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		public TPartialDocument Doc { get; set; }

		/// <summary>
		/// Instead of sending a partial doc with <see cref="Doc"/> plus an upsert doc
		/// with <see cref="Upsert"/>, setting <see cref="DocAsUpsert"/> to <c>true</c> will
		/// use the contents of doc as the upsert value.
		/// </summary>
		public bool? DocAsUpsert { get; set; }

		/// <summary>
		/// If you would like your script to run regardless of whether the document exists or not — i.e. the script handles
		/// initializing the document instead of the upsert element — then set scripted_upsert to true
		/// </summary>
		public bool? ScriptedUpsert { get; set; }

		/// <summary>
		/// A script to specify the update.
		/// </summary>
		public IScript Script { get; set; }
	}

	public class BulkUpdateDescriptor<TDocument, TPartialDocument>
		: BulkOperationDescriptorBase<BulkUpdateDescriptor<TDocument, TPartialDocument>, IBulkUpdateOperation<TDocument, TPartialDocument>>
		, IBulkUpdateOperation<TDocument, TPartialDocument>
		where TDocument : class
		where TPartialDocument : class
	{
		protected override string BulkOperationType => "update";
		protected override Type BulkOperationClrType => typeof(TDocument);

		TDocument IBulkUpdateOperation<TDocument, TPartialDocument>.IdFrom { get; set; }
		TDocument IBulkUpdateOperation<TDocument, TPartialDocument>.Upsert { get; set; }
		TPartialDocument IBulkUpdateOperation<TDocument, TPartialDocument>.Doc { get; set; }
		bool? IBulkUpdateOperation<TDocument, TPartialDocument>.DocAsUpsert { get; set; }
		bool? IBulkUpdateOperation<TDocument, TPartialDocument>.ScriptedUpsert { get; set; }
		IScript IBulkUpdateOperation<TDocument, TPartialDocument>.Script { get; set; }

		protected override object GetBulkOperationBody() =>
			new BulkUpdateBody<TDocument, TPartialDocument>
			{
				_PartialUpdate = Self.Doc,
				_Script = Self.Script,
				_Upsert = Self.Upsert,
				_DocAsUpsert = Self.DocAsUpsert,
				_ScriptedUpsert = Self.ScriptedUpsert
			};

		protected override Id GetIdForOperation(Inferrer inferrer) =>
			Self.Id ?? new Id(new[] { Self.IdFrom, Self.Upsert }.FirstOrDefault(o=>o != null));

		protected override Routing GetRoutingForOperation(Inferrer inferrer) =>
			Self.Routing ?? new Routing(new[] { Self.IdFrom, Self.Upsert }.FirstOrDefault(o=>o != null));

		/// <summary>
		/// Infers the id of the object to update from the provided <param name="object">object</param>.
		/// See <see cref="Doc(TPartialDocument)"/> to apply a partial object merge.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> IdFrom(TDocument @object, bool useAsUpsert = false)
		{
			Self.IdFrom = @object;
			return useAsUpsert ? this.Upsert(@object) : this;
		}

		/// <summary>
		/// A document to upsert when the specified document to be updated is not found
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Upsert(TDocument @object) => Assign(a => a.Upsert = @object);

		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Doc(TPartialDocument @object) => Assign(a => a.Doc = @object);

		/// <summary>
		/// Instead of sending a partial doc with <see cref="Doc(TPartialDocument)"/> plus an upsert doc
		/// with <see cref="Upsert(TDocument)"/>, setting <see cref="DocAsUpsert"/> to <c>true</c> will
		/// use the contents of doc as the upsert value.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> DocAsUpsert(bool? partialDocumentAsUpsert = true) =>
			Assign(a => a.DocAsUpsert = partialDocumentAsUpsert);

		/// <summary>
		/// If you would like your script to run regardless of whether the document exists or not — i.e. the script handles
		/// initializing the document instead of the upsert element — then set scripted_upsert to true
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> ScriptedUpsert(bool? scriptedUpsert = true) =>
			Assign(a => a.ScriptedUpsert = scriptedUpsert);

		/// <summary>
		/// A script to specify the update.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		/// <summary>
		/// How many times an update should be retried in the case of a version conflict.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> RetriesOnConflict(int? retriesOnConflict) =>
			Assign(a => a.RetriesOnConflict = retriesOnConflict);
	}
}
