using System;
using System.Collections.Generic;
using System.Globalization;
using Elasticsearch.Net;

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
		

		public BulkUpdateOperation(string id) { this.Id = id; }
		public BulkUpdateOperation(long id) : this(id.ToString(CultureInfo.InvariantCulture)) {}

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


		public override string Operation { get { return "update"; } } 

		public override Type ClrType { get { return typeof(TDocument); } }
		
		public override string GetIdForOperation(ElasticInferrer inferrer)
		{
			return this.Id ?? inferrer.Id(this.InferFrom);
		}
		
		public override object GetBody()
		{
			return new BulkUpdateBody<TDocument, TPartialDocument>
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
		}

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

	public class BulkUpdateDescriptor<TDocument, TPartialDocument> : BulkOperationDescriptorBase, IBulkUpdateOperation<TDocument, TPartialDocument>
		where TDocument : class
		where TPartialDocument : class
	{
		private IBulkUpdateOperation<TDocument, TPartialDocument> Self => this;

		protected override string BulkOperationType { get { return "update"; } }
		protected override Type BulkOperationClrType { get { return typeof(TDocument); } }

		TDocument IBulkUpdateOperation<TDocument, TPartialDocument>.InferFrom { get; set; }

		TDocument IBulkUpdateOperation<TDocument, TPartialDocument>.Upsert { get; set; }

		TPartialDocument IBulkUpdateOperation<TDocument, TPartialDocument>.Doc { get; set; }

		bool? IBulkUpdateOperation<TDocument, TPartialDocument>.DocAsUpsert { get; set; }

		string IBulkUpdateOperation<TDocument, TPartialDocument>.Lang { get; set; }

		string IBulkUpdateOperation<TDocument, TPartialDocument>.Script { get; set; }

		string IBulkUpdateOperation<TDocument, TPartialDocument>.ScriptId { get; set; }

		string IBulkUpdateOperation<TDocument, TPartialDocument>.ScriptFile { get; set; }

		Dictionary<string, object> IBulkUpdateOperation<TDocument, TPartialDocument>.Params { get; set; }
	
		protected override object GetBulkOperationBody()
		{
			return new BulkUpdateBody<TDocument, TPartialDocument>
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
		}

		protected override string GetIdForOperation(ElasticInferrer inferrer)
		{
			return Self.Id ?? inferrer.Id(Self.InferFrom) ?? inferrer.Id(Self.Upsert);
		}

		/// <summary>
		/// Manually set the index, default to the default index or the fixed index set on the bulk operation
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Index(string index)
		{
			index.ThrowIfNullOrEmpty(nameof(index));
			Self.Index = index;
			return this;
		}
		/// <summary>
		/// Manualy set the type to get the object from, default to whatever
		/// T will be inferred to if not passed or the fixed type set on the parent bulk operation
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Type(string type)
		{
			type.ThrowIfNullOrEmpty(nameof(type));
			Self.Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the type of which a typename will be inferred
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Type(Type type)
		{
			type.ThrowIfNull(nameof(type));
			Self.Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Id(long id)
		{
			return this.Id(id.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Id(string id)
		{
			Self.Id = id;
			return this;
		}

		/// <summary>
		/// The object to update, if id is not manually set it will be inferred from the object.
		/// Used ONLY to infer the ID see Document() to apply a partial object merge.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> IdFrom(TDocument @object, bool useAsUpsert = false)
		{
			Self.InferFrom = @object;
			if (useAsUpsert) return this.Upsert(@object);
			return this;
		}
		/// <summary>
		/// A document to upsert when the specified document to be updated is not found
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Upsert(TDocument @object)
		{
			Self.Upsert = @object;
			return this;
		}
		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Doc(TPartialDocument @object)
		{
			Self.Doc = @object;
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialDocument> DocAsUpsert(bool partialDocumentAsUpsert = true)
		{
			Self.DocAsUpsert = partialDocumentAsUpsert;
			return this;
		}
		
		public BulkUpdateDescriptor<TDocument, TPartialDocument> Lang(string lang)
		{
			Self.Lang = lang;
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialDocument> Script(string script)
		{
			script.ThrowIfNull(nameof(script));
			Self.Script = script;
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialDocument> ScriptId(string scriptId)
		{
			scriptId.ThrowIfNull(nameof(scriptId));
			Self.ScriptId = scriptId;
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialDocument> ScriptFile(string scriptFile)
		{
			scriptFile.ThrowIfNull(nameof(scriptFile));
			Self.ScriptFile = scriptFile;
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialDocument> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull(nameof(paramDictionary));
			Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

        public BulkUpdateDescriptor<TDocument, TPartialDocument> Version(long version)
		{
			Self.Version = version; 
			return this;
		}


		public BulkUpdateDescriptor<TDocument, TPartialDocument> VersionType(VersionType versionType)
		{
			Self.VersionType = versionType;
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialDocument> Routing(string routing)
		{
			Self.Routing = routing; 
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialDocument> Parent(string parent)		{
			Self.Parent = parent; 
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialDocument> Timestamp(long timestamp)
		{
			Self.Timestamp = timestamp; 
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialDocument> Ttl(string ttl)
		{
			Self.Ttl = ttl; 
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialDocument> RetriesOnConflict(int retriesOnConflict)
		{
			Self.RetriesOnConflict = retriesOnConflict;
			return this;
		}

		}
}