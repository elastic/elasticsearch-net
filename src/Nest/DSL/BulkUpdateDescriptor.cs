using Elasticsearch.Net;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Nest
{
	public class BulkUpdateDescriptor<T, K> : BaseBulkOperation
		 where T : class
		where K : class
	{
		internal override Type _ClrType { get { return typeof(T); } }
		internal override string _Operation { get { return "update"; } }
		internal override object _Object { get; set; }

		internal string _Lang { get; set; }
		internal K _Document { get; set; }
		internal string _Script { get; set; }
		internal Dictionary<string, object> _Params { get; set; }
		internal object _Upsert { get; set; }
		internal bool? _DocAsUpsert { get; set; }

		internal override object GetBody()
		{
			return new BulkUpdateBody<T, K>
			{
				_Document = this._Document,
				_Script = this._Script,
				_Lang = this._Lang,
				_Params = this._Params,
				_Upsert = this._Upsert,
				_DocAsUpsert = this._DocAsUpsert
			};
		}

		internal override string GetIdForObject(ElasticInferrer inferrer)
		{
			if (!this._Id.IsNullOrEmpty())
				return this._Id;

			return inferrer.Id((T)_Object);

		}

		/// <summary>
		/// Manually set the index, default to the default index or the fixed index set on the bulk operation
		/// </summary>
		public BulkUpdateDescriptor<T, K> Index(string index)
		{
			index.ThrowIfNullOrEmpty("indices");
			this._Index = index;
			return this;
		}
		/// <summary>
		/// Manualy set the type to get the object from, default to whatever
		/// T will be inferred to if not passed or the fixed type set on the parent bulk operation
		/// </summary>
		public BulkUpdateDescriptor<T, K> Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			this._Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the type of which a typename will be inferred
		/// </summary>
		public BulkUpdateDescriptor<T, K> Type(Type type)
		{
			type.ThrowIfNull("type");
			this._Type = type;
			return this;
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkUpdateDescriptor<T, K> Id(long id)
		{
			return this.Id(id.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public BulkUpdateDescriptor<T, K> Id(string id)
		{
			this._Id = id;
			return this;
		}

		/// <summary>
		/// The object to update, if id is not manually set it will be inferred from the object.
		/// Used ONLY to infer the ID see Document() to apply a partial object merge.
		/// </summary>
		public BulkUpdateDescriptor<T, K> Object(T @object)
		{
			this._Object = @object;
			return this;
		}
		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		public BulkUpdateDescriptor<T, K> Document(K @object)
		{
			this._Document = @object;
			return this;
		}

		public BulkUpdateDescriptor<T, K> DocAsUpsert(bool docAsUpsert = true)
		{
			this._DocAsUpsert = docAsUpsert;
			return this;
		}
		
		public BulkUpdateDescriptor<T, K> Lang(string lang)
		{
			this._Lang = lang;
			return this;
		}

		public BulkUpdateDescriptor<T, K> Script(string script)
		{
			script.ThrowIfNull("script");
			this._Script = script;
			return this;
		}

		public BulkUpdateDescriptor<T, K> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			this._Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		/// <summary>
		/// The full document to be created if an existing document does not exist for a partial merge.
		/// </summary>
		public BulkUpdateDescriptor<T, K> Upsert(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> upsertValues)
		{
			upsertValues.ThrowIfNull("upsertValues");
			this._Upsert = upsertValues(new FluentDictionary<string, object>());
			return this;
		}

		/// <summary>
		/// The full document to be created if an existing document does not exist for a partial merge.
		/// </summary>
		public BulkUpdateDescriptor<T, K> Upsert(T upsertObject)
		{
			upsertObject.ThrowIfNull("upsertObject");
			this._Upsert = upsertObject;
			return this;
		}

		public BulkUpdateDescriptor<T, K> Version(string version)
		{
			this._Version = version; 
			return this;
		}

		public BulkUpdateDescriptor<T, K> VersionType(string versionType)
		{
			this._VersionType = versionType;
			return this;
		}


		public BulkUpdateDescriptor<T, K> VersionType(VersionType versionType)
		{
			switch (versionType)
			{
				case Nest.VersionType.External:
					this._VersionType = "external";
					break;
				case Nest.VersionType.Internal:
					this._VersionType = "internal";
					break;
				case Nest.VersionType.Force:
					this._VersionType = "force";
					break;
			}
			return this;
		}

		public BulkUpdateDescriptor<T, K> Routing(string routing)
		{
			this._Routing = routing; 
			return this;
		}

		public BulkUpdateDescriptor<T, K> Parent(string parent)
		{
			this._Parent = parent; 
			return this;
		}

		public BulkUpdateDescriptor<T, K> Timestamp(long timestamp)
		{
			this._Timestamp = timestamp; 
			return this;
		}

		public BulkUpdateDescriptor<T, K> Ttl(string ttl)
		{
			this._Ttl = ttl; 
			return this;
		}

		public BulkUpdateDescriptor<T, K> RetriesOnConflict(int retriesOnConflict)
		{
			this._RetriesOnConflict = retriesOnConflict;
			return this;
		}
	


	}
}