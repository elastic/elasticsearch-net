using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	internal static class StatefulSerializerFactory
	{
		public static InternalSerializer CreateStateful(IConnectionSettingsValues settings, IJsonFormatterResolver formatterResolver) =>
			new InternalSerializer(settings, formatterResolver);
	}
}
