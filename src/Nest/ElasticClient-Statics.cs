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

		private T Deserialize<T>(string value, IEnumerable<JsonConverter> extraConverters = null)
		{
			return this.Serializer.Deserialize<T>(value, extraConverters);
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

		protected virtual R ToParsedResponse<R>(ConnectionStatus status, bool allow404 = false, IEnumerable<JsonConverter> extraConverters = null) where R : BaseResponse
		{
			return this.Serializer.ToParsedResponse<R>(status, allow404, extraConverters);
		}

		private string ResolveTypeName(TypeNameMarker typeNameMarker, string defaultIndexName = null)
		{
			return typeNameMarker != null ? typeNameMarker.Resolve(this._connectionSettings) : defaultIndexName;
		}

	}
}
