using System;
using System.Collections.Generic;
using System.Globalization;
using Elasticsearch.Net;

namespace Nest
{
	public interface IBulkUpdateOperation<TDocument, TPartialUpdate> : IBulkOperation
		where TDocument : class
		where TPartialUpdate : class
	{
		TDocument Document { get; set; }
		TDocument Upsert { get; set; }
		
		TPartialUpdate PartialUpdate { get; set; }
		
		bool? DocAsUpsert { get; set; }
		
		string Lang { get; set; }
		
		string Script { get; set; }
		
		Dictionary<string, object> Params { get; set; }
	}

	public class BulkUpdateOperation<TDocument, TPartialUpdate> 
		: BulkOperationBase, IBulkUpdateOperation<TDocument, TPartialUpdate>
		where TDocument : class
		where TPartialUpdate : class
	{
		public override string Operation { get { return "update"; } } 
		public override Type ClrType { get { return typeof(TDocument); } }

		public override object GetBody()
		{
			return new BulkUpdateBody<TDocument, TPartialUpdate>
			{
				_PartialUpdate = this.PartialUpdate,
				_Script = this.Script,
				_Lang = this.Lang,
				_Params = this.Params,
				_Upsert = this.Upsert,
				_DocAsUpsert = this.DocAsUpsert
			};
		}

		public TDocument Document { get; set; }
		public TDocument Upsert { get; set; }
		public TPartialUpdate PartialUpdate { get; set; }
		public bool? DocAsUpsert { get; set; }
		public string Lang { get; set; }
		public string Script { get; set; }
		public Dictionary<string, object> Params { get; set; }
	}

	public class BulkUpdateDescriptor<TDocument, TPartialUpdate>
		: BulkOperationDescriptorBase, IBulkUpdateOperation<TDocument, TPartialUpdate>
		where TDocument : class
		where TPartialUpdate : class
	{
		private IBulkUpdateOperation<TDocument, TPartialUpdate> Self { get { return this; } }

		protected override string _Operation { get { return "update"; } }
		protected override Type _ClrType { get { return typeof(TDocument); } }

		TDocument IBulkUpdateOperation<TDocument, TPartialUpdate>.Document { get; set; }
		TDocument IBulkUpdateOperation<TDocument, TPartialUpdate>.Upsert { get; set; }
		TPartialUpdate IBulkUpdateOperation<TDocument, TPartialUpdate>.PartialUpdate { get; set; }
		bool? IBulkUpdateOperation<TDocument, TPartialUpdate>.DocAsUpsert { get; set; }
		string IBulkUpdateOperation<TDocument, TPartialUpdate>.Lang { get; set; }
		string IBulkUpdateOperation<TDocument, TPartialUpdate>.Script { get; set; }
		Dictionary<string, object> IBulkUpdateOperation<TDocument, TPartialUpdate>.Params { get; set; }
	
		protected override object _GetBody()
		{
			return new BulkUpdateBody<TDocument, TPartialUpdate>
			{
				_PartialUpdate = Self.PartialUpdate,
				_Script = Self.Script,
				_Lang = Self.Lang,
				_Params = Self.Params,
				_Upsert = Self.Upsert,
				_DocAsUpsert = Self.DocAsUpsert
			};
		}

		protected override string GetIdForOperation(ElasticInferrer inferrer)
		{
			return Self.Id ?? inferrer.Id(Self.Document);
		}

		/// <summary>
		/// Manually set the index, default to the default index or the fixed index set on the bulk operation
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialUpdate> Index(string index)
		{
			index.ThrowIfNullOrEmpty("indices");
			Self.Index = index;
			return this;
		}
		/// <summary>
		/// Manualy set the type to get the object from, default to whatever
		/// T will be inferred to if not passed or the fixed type set on the parent bulk operation
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialUpdate> Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			Self.Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the type of which a typename will be inferred
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialUpdate> Type(Type type)
		{
			type.ThrowIfNull("type");
			Self.Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialUpdate> Id(long id)
		{
			return this.Id(id.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialUpdate> Id(string id)
		{
			Self.Id = id;
			return this;
		}

		/// <summary>
		/// The object to update, if id is not manually set it will be inferred from the object.
		/// Used ONLY to infer the ID see Document() to apply a partial object merge.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialUpdate> Document(TDocument @object)
		{
			Self.Document = @object;
			return this;
		}
		/// <summary>
		/// A document to upsert when the specified document to be updated is not found
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialUpdate> Upsert(TDocument @object)
		{
			Self.Upsert = @object;
			return this;
		}
		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		public BulkUpdateDescriptor<TDocument, TPartialUpdate> PartialUpdate(TPartialUpdate @object)
		{
			Self.PartialUpdate = @object;
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialUpdate> DocAsUpsert(bool docAsUpsert = true)
		{
			Self.DocAsUpsert = docAsUpsert;
			return this;
		}
		
		public BulkUpdateDescriptor<TDocument, TPartialUpdate> Lang(string lang)
		{
			Self.Lang = lang;
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialUpdate> Script(string script)
		{
			script.ThrowIfNull("script");
			Self.Script = script;
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialUpdate> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialUpdate> Version(string version)
		{
			Self.Version = version; 
			return this;
		}


		public BulkUpdateDescriptor<TDocument, TPartialUpdate> VersionType(VersionTypeOptions versionType)
		{
			Self.VersionType = versionType;
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialUpdate> Routing(string routing)
		{
			Self.Routing = routing; 
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialUpdate> Parent(string parent)		{
			Self.Parent = parent; 
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialUpdate> Timestamp(long timestamp)
		{
			Self.Timestamp = timestamp; 
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialUpdate> Ttl(string ttl)
		{
			Self.Ttl = ttl; 
			return this;
		}

		public BulkUpdateDescriptor<TDocument, TPartialUpdate> RetriesOnConflict(int retriesOnConflict)
		{
			Self.RetriesOnConflict = retriesOnConflict;
			return this;
		}

		}
}