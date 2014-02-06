namespace Nest
{
	interface IPathInfo<K> where K : FluentQueryString<K>, new()
	{
		ElasticSearchPathInfo<K> ToPathInfo(IConnectionSettings settings);
	}
}