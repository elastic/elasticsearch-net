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
		
		private string Serialize(object @object)
		{
			return this.Serializer.Serialize(@object);
		}

		

		public string GetTypeNameFor<T>()
		{
			return GetTypeNameFor(typeof (T));
		}

		public string GetTypeNameFor(Type type)
		{
			return this.TypeNameResolver.GetTypeNameFor(type).Resolve(this._connectionSettings);
		}

		public string GetIndexNameFor<T>()
		{
			return GetIndexNameFor(typeof(T));
		}

		public string GetIndexNameFor(Type type)
		{
			return this.IndexNameResolver.GetIndexForType(type);
		}

		protected virtual T Deserialize<T>(object value,IEnumerable<JsonConverter> extraConverters = null, bool allow404 = false) where T : class
		{
			return this.Serializer.Deserialize<T>(value, extraConverters, allow404);
		}

		private string ResolveTypeName(TypeNameMarker typeNameMarker, string defaultIndexName = null)
		{
			return typeNameMarker != null ? typeNameMarker.Resolve(this._connectionSettings) : defaultIndexName;
		}

	}
}
