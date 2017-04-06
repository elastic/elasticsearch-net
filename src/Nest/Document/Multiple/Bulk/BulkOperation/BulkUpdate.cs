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
		/// Infers the id of the object to update from the provided <param name="object">object</param>.
		/// See <see cref="Doc"/> to apply a partial object merge.
		/// </summary>
		TDocument InferFrom { get; set; }

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
		/// The script language to use
		/// </summary>
		string Lang { get; set; }

		/// <summary>
		/// An inline script to specify the update
		/// </summary>
		string Script { get; set; }

		/// <summary>
		/// The id of an indexed script to specify the update
		/// </summary>
		string ScriptId { get; set; }

		/// <summary>
		/// The file of a script to specify the update
		/// </summary>
		string ScriptFile { get; set; }

		/// <summary>
		/// The parameters for the script
		/// </summary>
		Dictionary<string, object> Params { get; set; }
	}

	public class BulkUpdateOperation<TDocument, TPartialDocument> : BulkOperationBase, IBulkUpdateOperation<TDocument, TPartialDocument>
		where TDocument : class
		where TPartialDocument : class
	{
		/// <summary>
		/// Create a new bulk update operation
		/// </summary>
		public BulkUpdateOperation(Id id) { this.Id = id; }

		/// <summary>
		/// Create a new bulk update operation
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
		/// Create a new bulk update operation
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
				_Script = CreateScriptFromProperties(),
				_Upsert = this.Upsert,
				_DocAsUpsert = this.DocAsUpsert
			};

		/// <summary>
		/// Infers the id of the object to update from the provided <param name="object">object</param>.
		/// See <see cref="Doc"/> to apply a partial object merge.
		/// </summary>
		public TDocument InferFrom { get; set; }

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

		/// <inheritdoc/>
		public bool? ScriptedUpsert { get; set; }

		/// <summary>
		/// The script language to use
		/// </summary>
		public string Lang { get; set; }

		/// <summary>
		/// An inline script to specify the update
		/// </summary>
		public string Script { get; set; }

		/// <summary>
		/// The id of an indexed script to specify the update
		/// </summary>
		public string ScriptId { get; set; }

		/// <summary>
		/// The file of a script to specify the update
		/// </summary>
		public string ScriptFile { get; set; }

		/// <summary>
		/// The parameters for the script
		/// </summary>
		public Dictionary<string, object> Params { get; set; }

		private IScript CreateScriptFromProperties()
		{
			IScript script = null;

			if (!this.Script.IsNullOrEmpty())
			{
				script = new InlineScript(this.Script)
				{
					Lang = this.Lang,
					Params = this.Params
				};
			}
			else if (!this.ScriptFile.IsNullOrEmpty())
			{
				script = new FileScript(this.ScriptFile)
				{
					Lang = this.Lang,
					Params = this.Params
				};
			}
			else if (!this.ScriptId.IsNullOrEmpty())
			{
				script = new IndexedScript(this.ScriptId)
				{
					Lang = this.Lang,
					Params = this.Params
				};
			}

			return script;
		}
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
		bool? IBulkUpdateOperation<TDocument, TPartialDocument>.ScriptedUpsert { get; set; }
		string IBulkUpdateOperation<TDocument, TPartialDocument>.Lang { get; set; }
		string IBulkUpdateOperation<TDocument, TPartialDocument>.Script { get; set; }
		string IBulkUpdateOperation<TDocument, TPartialDocument>.ScriptId { get; set; }
		string IBulkUpdateOperation<TDocument, TPartialDocument>.ScriptFile { get; set; }

		Dictionary<string, object> IBulkUpdateOperation<TDocument, TPartialDocument>.Params { get; set; }

		protected override object GetBulkOperationBody() =>
			new BulkUpdateBody<TDocument, TPartialDocument>
			{
				_PartialUpdate = Self.Doc,
				_Script = CreateScriptFromProperties(),
				_Upsert = Self.Upsert,
				_DocAsUpsert = Self.DocAsUpsert,
				_ScriptedUpsert = Self.ScriptedUpsert
			};

		protected override Id GetIdForOperation(Inferrer inferrer) => Self.Id ?? new Id(new[] { Self.InferFrom, Self.Upsert }.FirstOrDefault(o=>o != null));

		/// <summary>
		/// Infers the id of the object to update from the provided <param name="object">object</param>.
		/// See <see cref="Doc(TPartialDocument)"/> to apply a partial object merge.
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

		/// <summary>
		/// Instead of sending a partial doc with <see cref="Doc(TPartialDocument)"/> plus an upsert doc
		/// with <see cref="Upsert(TDocument)"/>, setting <see cref="DocAsUpsert"/> to <c>true</c> will
		/// use the contents of doc as the upsert value.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> DocAsUpsert(bool partialDocumentAsUpsert = true) => Assign(a => a.DocAsUpsert = partialDocumentAsUpsert);

		/// <inheritdoc/>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> ScriptedUpsert(bool scriptedUpdate = true) => Assign(a => a.ScriptedUpsert = scriptedUpdate);

		/// <summary>
		/// The script language to use
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Lang(string lang) => Assign(a => a.Lang = lang);

		/// <summary>
		/// An inline script to specify the update
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Script(string script) => Assign(a => a.Script = script);

		/// <summary>
		/// The id of an indexed script to specify the update
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> ScriptId(string scriptId) => Assign(a => a.ScriptId = scriptId);

		/// <summary>
		/// The file of a script to specify the update
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> ScriptFile(string scriptFile) => Assign(a => a.ScriptFile = scriptFile);

		/// <summary>
		/// The parameters for the script
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary) =>
			Assign(a => a.Params = paramDictionary(new FluentDictionary<string, object>()));

		/// <summary>
		/// How many times an update should be retried in the case of a version conflict.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> RetriesOnConflict(int? retriesOnConflict) => Assign(a => a.RetriesOnConflict = retriesOnConflict);

		private IScript CreateScriptFromProperties()
		{
			IScript script = null;

			if (!Self.Script.IsNullOrEmpty())
			{
				script = new InlineScript(Self.Script)
				{
					Lang = Self.Lang,
					Params = Self.Params
				};
			}
			else if (!Self.ScriptFile.IsNullOrEmpty())
			{
				script = new FileScript(Self.ScriptFile)
				{
					Lang = Self.Lang,
					Params = Self.Params
				};
			}
			else if (!Self.ScriptId.IsNullOrEmpty())
			{
				script = new IndexedScript(Self.ScriptId)
				{
					Lang = Self.Lang,
					Params = Self.Params
				};
			}

			return script;
		}
	}
}
