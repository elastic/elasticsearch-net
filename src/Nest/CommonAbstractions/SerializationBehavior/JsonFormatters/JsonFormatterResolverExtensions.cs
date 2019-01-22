using System.Runtime.CompilerServices;
using Utf8Json;

namespace Nest
{
	internal static class JsonFormatterResolverExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IConnectionSettingsValues GetConnectionSettings(this IJsonFormatterResolver formatterResolver) =>
			((NestFormatterResolver)formatterResolver).Settings;
	}
}
