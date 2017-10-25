using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal static class ConnectionSettingsValuesExtensions
	{
		public static JsonNetSerializer CreateStateful(this IConnectionSettingsValues settings, JsonConverter converter)
		{
			var s = (settings as ConnectionSettings)
				?? throw new NullReferenceException($"Stateful serializer expected {nameof(IConnectionSettingsValues)} to be {nameof(ConnectionSettings)}");

			return s.SerializerFactory.CreateStateful(s, converter);
		}
	}
}
