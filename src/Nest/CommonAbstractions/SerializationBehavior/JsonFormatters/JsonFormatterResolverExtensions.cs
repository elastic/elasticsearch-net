using System.Runtime.CompilerServices;
using Elasticsearch.Net;

namespace Nest
{
	internal static class JsonFormatterResolverExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IConnectionSettingsValues GetConnectionSettings(this IJsonFormatterResolver formatterResolver) =>
			((IJsonFormatterResolverWithSettings)formatterResolver).Settings;
	}
}
