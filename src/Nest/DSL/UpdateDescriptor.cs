using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public class UpdateDescriptor<T, K> 
		where T : class 
		where K : class
	{
		private readonly TypeNameResolver typeNameResolver;

		public UpdateDescriptor()
		{
			this.typeNameResolver = new TypeNameResolver();
		}

		[JsonProperty(PropertyName = "script")]
		internal string _Script { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> _Params { get; set; }

		[JsonProperty(PropertyName = "upsert")]
		internal object _Upsert { get; set; }

		[JsonProperty(PropertyName = "doc_as_upsert")]
		internal bool? _DocAsUpsert { get; set; }

		[JsonProperty(PropertyName = "doc")]
		internal object _Document { get; set; }

		internal int? _RetriesOnConflict { get; set; }
		internal bool? _Refresh { get; set; }
		internal string _Percolate { get; set; }
		internal Consistency? _Consistency { get; set; }
		internal Nest.Replication? _Replication { get; set; }
		internal int? _Timeout { get; set; }
		internal string _Parent { get; set; }
		internal string _Routing { get; set; }
		internal string _Index { get; set; }
		internal TypeNameMarker _Type { get; set; }
		internal string _Id { get; set; }
		internal T _Object { get; set; }

		public UpdateDescriptor<T, K> Script(string script)
		{
			script.ThrowIfNull("script");
			this._Script = script;
			return this;
		}

		public UpdateDescriptor<T, K> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			this._Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		public UpdateDescriptor<T, K> Upsert(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> upsertValues)
		{
			upsertValues.ThrowIfNull("upsertValues");
			this._Upsert = upsertValues(new FluentDictionary<string, object>());
			return this;
		}

		public UpdateDescriptor<T, K> Upsert(K upsertObject)
		{
			upsertObject.ThrowIfNull("upsertObject");
			this._Upsert = upsertObject;
			return this;
		}

		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		public UpdateDescriptor<T, K> Document(object @object)
		{
			this._Document = @object;
			return this;
		}

		public UpdateDescriptor<T, K> DocAsUpsert(bool docAsUpsert = true)
		{
			this._DocAsUpsert = docAsUpsert;
			return this;
		}
		

		public UpdateDescriptor<T, K> Index(string index)
		{
			index.ThrowIfNullOrEmpty("indices");
			this._Index = index;
			return this;
		}
		public UpdateDescriptor<T, K> Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			this._Type = type;
			return this;
		}
		public UpdateDescriptor<T, K> Type(Type type)
		{
			type.ThrowIfNull("type");
			this._Type = type;
			return this;
		}
		public UpdateDescriptor<T, K> Routing(string routing)
		{
			routing.ThrowIfNullOrEmpty("routing");
			this._Routing = routing;
			return this;
		}
		public UpdateDescriptor<T, K> Parent(string parent)
		{
			parent.ThrowIfNullOrEmpty("parent");
			this._Parent = parent;
			return this;
		}
		public UpdateDescriptor<T, K> Timeout(int timeout)
		{
			this._Timeout = timeout;
			return this;
		}
		public UpdateDescriptor<T, K> Replication(Replication replication)
		{
			this._Replication = replication;
			return this;
		}
		public UpdateDescriptor<T, K> Concistency(Consistency consistency)
		{
			this._Consistency = consistency;
			return this;
		}
		public UpdateDescriptor<T, K> Percolate(string percolation)
		{
			this._Percolate = percolation;
			return this;
		}
		public UpdateDescriptor<T, K> Refresh(bool refresh = true)
		{
			this._Refresh = refresh;
			return this;
		}
		public UpdateDescriptor<T, K> RetriesOnConflict(int retriesOnConflict)
		{
			this._RetriesOnConflict = retriesOnConflict;
			return this;
		}
		public UpdateDescriptor<T, K> Id(int id)
		{
			return this.Id(id.ToString());
		}
		public UpdateDescriptor<T, K> Id(string id)
		{
			this._Id = id;
			return this;
		}
		public UpdateDescriptor<T, K> Object(T @object)
		{
			this._Object = @object;
			return this;
		}

	}
}
