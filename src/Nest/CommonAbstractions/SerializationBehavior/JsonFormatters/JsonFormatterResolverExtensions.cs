using System.Runtime.CompilerServices;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal static class JsonFormatterResolverExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IConnectionSettingsValues GetConnectionSettings(this IJsonFormatterResolver formatterResolver) =>
			((IJsonFormatterResolverWithSettings)formatterResolver).Settings;
	}
}
