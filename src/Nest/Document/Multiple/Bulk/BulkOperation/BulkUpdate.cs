using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IBulkUpdateOperation<TDocument, TPartialDocument> : IBulkOperation
		where TDocument : class
		where TPartialDocument : class
	{
		TDocument InferFrom { get; set; }

		TDocument Upsert { get; set; }
		
		TPartialDocument Doc { get; set; }
		
		bool? DocAsUpsert { get; set; }
		
		string Lang { get; set; }
		
		string Script { get; set; }

		string ScriptId { get; set; }

		string ScriptFile { get; set; }
		
		Dictionary<string, object> Params { get; set; }
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
			this.InferFrom = idFrom;
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
			this.InferFrom = idFrom;
			if (useIdFromAsUpsert)
				this.Upsert = idFrom;
			this.Doc = update;
		}

		protected override string Operation => "update";

		protected override Type ClrType => typeof(TDocument);

		protected override Id GetIdForOperation(Inferrer inferrer) => this.Id ?? new Id(new[] { this.InferFrom, this.Upsert }.FirstOrDefault(o=>o != null));

		protected override object GetBody() => 
			new BulkUpdateBody<TDocument, TPartialDocument>
		{
			_PartialUpdate = this.Doc,
			_Script = this.Script,
			_ScriptId = this.ScriptId,
			_ScriptFile = this.ScriptFile,
			_Lang = this.Lang,
			_Params = this.Params,
			_Upsert = this.Upsert,
			_DocAsUpsert = this.DocAsUpsert
		};

		public TDocument InferFrom { get; set; }
		public TDocument Upsert { get; set; }
		public TPartialDocument Doc { get; set; }
		public bool? DocAsUpsert { get; set; }
		public string Lang { get; set; }
		public string Script { get; set; }
		public string ScriptId { get; set; }
		public string ScriptFile { get; set; }
		public Dictionary<string, object> Params { get; set; }
	}

	public class BulkUpdateDescriptor<TDocument, TPartialDocument>
		: BulkOperationDescriptorBase<BulkUpdateDescriptor<TDocument, TPartialDocument>, IBulkUpdateOperation<TDocument, TPartialDocument>>
		, IBulkUpdateOperation<TDocument, TPartialDocument>
		where TDocument : class
		where TPartialDocument : class
	{
		protected override string BulkOperationType => "update";
		protected override Type BulkOperationClrType => typeof(TDocument);

		TDocument IBulkUpdateOperation<TDocument, TPartialDocument>.InferFrom { get; set; }

		TDocument IBulkUpdateOperation<TDocument, TPartialDocument>.Upsert { get; set; }

		TPartialDocument IBulkUpdateOperation<TDocument, TPartialDocument>.Doc { get; set; }

		bool? IBulkUpdateOperation<TDocument, TPartialDocument>.DocAsUpsert { get; set; }

		string IBulkUpdateOperation<TDocument, TPartialDocument>.Lang { get; set; }

		string IBulkUpdateOperation<TDocument, TPartialDocument>.Script { get; set; }

		string IBulkUpdateOperation<TDocument, TPartialDocument>.ScriptId { get; set; }

		string IBulkUpdateOperation<TDocument, TPartialDocument>.ScriptFile { get; set; }

		Dictionary<string, object> IBulkUpdateOperation<TDocument, TPartialDocument>.Params { get; set; }

		protected override object GetBulkOperationBody() =>
			new BulkUpdateBody<TDocument, TPartialDocument>
			{
				_PartialUpdate = Self.Doc,
				_Script = Self.Script,
				_ScriptId = Self.ScriptId,
				_ScriptFile = Self.ScriptFile,
				_Lang = Self.Lang,
				_Params = Self.Params,
				_Upsert = Self.Upsert,
				_DocAsUpsert = Self.DocAsUpsert
			};

		protected override Id GetIdForOperation(Inferrer inferrer) => Self.Id ?? new Id(new[] { Self.InferFrom, Self.Upsert }.FirstOrDefault(o=>o != null));

		/// <summary>
		/// The object to update, if id is not manually set it will be inferred from the object.
		/// Used ONLY to infer the ID see Document() to apply a partial object merge.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> IdFrom(TDocument @object, bool useAsUpsert = false)
		{
			Self.InferFrom = @object;
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

		public BulkUpdateDescriptor<TDocument, TPartialDocument> DocAsUpsert(bool partialDocumentAsUpsert = true) => Assign(a => a.DocAsUpsert = partialDocumentAsUpsert);

		public BulkUpdateDescriptor<TDocument, TPartialDocument> Lang(string lang) => Assign(a => a.Lang = lang);

		public BulkUpdateDescriptor<TDocument, TPartialDocument> Script(string script) => Assign(a => a.Script = script);

		public BulkUpdateDescriptor<TDocument, TPartialDocument> ScriptId(string scriptId) => Assign(a => a.ScriptId = scriptId);

		public BulkUpdateDescriptor<TDocument, TPartialDocument> ScriptFile(string scriptFile) => Assign(a => a.ScriptFile = scriptFile);

		public BulkUpdateDescriptor<TDocument, TPartialDocument> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary) =>
			Assign(a => a.Params = paramDictionary(new FluentDictionary<string, object>()));

		public BulkUpdateDescriptor<TDocument, TPartialDocument> RetriesOnConflict(int? retriesOnConflict) => Assign(a => a.RetriesOnConflict = retriesOnConflict);
	}
}