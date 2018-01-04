using Elasticsearch.Net;

namespace Nest
{
	internal static class RequestParametersExtensions
	{
		internal static string GetResolvedQueryStringValue<T>(this RequestParameters<T> p, string n, IConnectionSettingsValues s)
			where T : RequestParameters<T>
		{
			var value = p.GetQueryStringValue<object>(n);
			return UrlFormatProvider.GetUnescapedStringRepresentation(value, s);
		}
	}
}
