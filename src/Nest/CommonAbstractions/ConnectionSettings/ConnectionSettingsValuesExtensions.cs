using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal static class ConnectionSettingsValuesExtensions
	{
		public static InternalSerializer CreateStateful(this IConnectionSettingsValues settings, JsonConverter converter)
		{
			var s = (settings as ConnectionSettings)
				?? throw new NullReferenceException($"Stateful serializer expected {nameof(IConnectionSettingsValues)} to be {nameof(ConnectionSettings)}");

			return StatefulSerializerFactory.CreateStateful(s, converter);
		}
	}
}
