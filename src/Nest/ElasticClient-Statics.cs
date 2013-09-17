using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Resolvers;

namespace Nest
{
	public partial class ElasticClient
	{
		internal readonly PropertyNameResolver PropertyNameResolver;
		
		/// <summary>
		/// serialize an object using the internal registered converters without camelcasing properties as is done 
		/// while indexing objects
		/// </summary>
		public string Serialize(object @object)
		{
			return this._elasticSerializer.Serialize(@object);
		}

		/// <summary>
		/// Deserialize an object 
		/// </summary>
		public T Deserialize<T>(string value, IEnumerable<JsonConverter> extraConverters = null)
		{
			return this._elasticSerializer.Deserialize<T>(value, extraConverters);
		}

		public string GetTypeNameFor<T>()
		{
			return GetTypeNameFor(typeof (T));
		}
		public string GetTypeNameFor(Type type)
		{
			return this.TypeNameResolver.GetTypeNameFor(type).Resolve(this.Settings);
		}

		public string GetIndexNameFor<T>()
		{
			return GetIndexName(typeof(T));
		}
		public string GetIndexName(Type type)
		{
			return this.IndexNameResolver.GetIndexForType(type);
		}

		private string ResolveTypeName(TypeNameMarker typeNameMarker, string defaultIndexName = null)
		{
			return typeNameMarker != null ? typeNameMarker.Resolve(this.Settings) : defaultIndexName;
		}

	}
}
