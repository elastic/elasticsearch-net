using Elasticsearch.Net;

namespace Nest
{
	internal static class FluentRequestParametersExtensions
	{
		internal static string GetResolvedQueryStringValue<T>(this FluentRequestParameters<T> p, string n, IConnectionSettingsValues s)
			where T : FluentRequestParameters<T>
		{
			var value = p.GetQueryStringValue<object>(n);
			return UrlFormatProvider.GetUnescapedStringRepresentation(value, s);
		}
	}
}