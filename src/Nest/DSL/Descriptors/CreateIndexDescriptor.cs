using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Domain;

namespace Nest
{
	
	public class CreateIndexDescriptor
	{
		internal IndexSettings _IndexSettings = new IndexSettings();

		/// <summary>
		/// Initialize the descriptor using the values from for instance a previous Get Index Settings call.
		/// </summary>
		public CreateIndexDescriptor InitializeUsing(IndexSettings indexSettings)
		{
			this._IndexSettings = indexSettings;
			return this;
		}
		
		/// <summary>
		/// Set the number of shards (if possible) for the new index.
		/// </summary>
		/// <param name="shards"></param>
		/// <returns></returns>
		public CreateIndexDescriptor NumberOfShards(int shards)
		{
			this._IndexSettings.NumberOfShards = shards;
			return this;
		}

		/// <summary>
		/// Set the number of replicas (if possible) for the new index.
		/// </summary>
		/// <param name="replicas"></param>
		/// <returns></returns>
		public CreateIndexDescriptor NumberOfReplicas(int replicas)
		{
			this._IndexSettings.NumberOfReplicas = replicas;
			return this;
		}

		/// <summary>
		/// Set/Update settings, the index.* prefix is not needed for the keys.
		/// </summary>
		public CreateIndexDescriptor Settings(Action<FluentDictionary<string, object>> settingsSelector)
		{
			settingsSelector.ThrowIfNull("settingsSelector");
			var dict = new FluentDictionary<string, object>();
			settingsSelector(dict);
			foreach (var kv in dict)
			{
				this._IndexSettings.TryAdd(kv.Key, kv.Value);
			}
			return this;
		}
		public CreateIndexDescriptor RemoveMapping(string typeName)
		{
			this._IndexSettings.Mappings = this._IndexSettings.Mappings.Where(m => m.Type != typeName).ToList();

			return this;
		}
		public CreateIndexDescriptor RemoveMapping<T>() where T : class
		{
			var typeName = new TypeNameResolver().GetTypeNameFor<T>();
			return this.RemoveMapping(typeName);
		}

		public CreateIndexDescriptor AddMapping<T>(Func<RootObjectMappingDescriptor<T>, RootObjectMappingDescriptor<T>> typeMappingDescriptor) where T : class
		{
			typeMappingDescriptor.ThrowIfNull("typeMappingDescriptor");
			var d = typeMappingDescriptor(new RootObjectMappingDescriptor<T>());
			var typeMapping = d._Mapping;
			this._IndexSettings.Mappings.Add(typeMapping);

			return this;
		}
		public CreateIndexDescriptor AddMapping<T>(RootObjectMapping rootObjectMapping, Func<RootObjectMappingDescriptor<T>, RootObjectMappingDescriptor<T>> typeMappingDescriptor) where T : class
		{
			typeMappingDescriptor.ThrowIfNull("typeMappingDescriptor");
			var d = typeMappingDescriptor(new RootObjectMappingDescriptor<T>() { _Mapping = rootObjectMapping });
			var typeMapping = d._Mapping;
			this._IndexSettings.Mappings.Add(typeMapping);

			return this;
		}

	}
}
