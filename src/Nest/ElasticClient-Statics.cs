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
			return this.Infer.TypeName(type);
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
