using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	internal static class StatefulSerializerFactory
	{
		public static InternalSerializer CreateStateful(IConnectionSettingsValues settings, IJsonFormatterResolver converter)
		{
			// TODO: get the current resolver from settings
			return new InternalSerializer(settings, converter);
		}
	}
}
