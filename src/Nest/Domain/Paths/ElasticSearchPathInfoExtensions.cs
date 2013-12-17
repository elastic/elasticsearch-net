using System.Linq;

namespace Nest
{
	public static class ElasticSearchPathInfoExtensions
	{
		private static ElasticSearchPathInfo<K> Inferred<T, K, TQueryPathDescriptor>(
			QueryPathDescriptorBase<TQueryPathDescriptor, T> query,
			IConnectionSettings settings, 
			T obj = null
			)
			where T : class
			where TQueryPathDescriptor : QueryPathDescriptorBase<TQueryPathDescriptor, T>, new()
			where K : FluentQueryString<K>, new()
		{
			//start out with defaults
			var inferrer = new ElasticInferrer(settings);
			var index = inferrer.IndexName<T>();
			var type = inferrer.TypeName<T>();
			var id = inferrer.Id(obj);
			var pathInfo = new ElasticSearchPathInfo<K>()
			{
				Id = id,
				Index = index,
				Type = type
			};
			if (query == null)
				return pathInfo;

			//modify pathInfo according to the PathDescriptor values
			if (query._AllTypes)
				pathInfo.Type = null;
			else if (query._Types.HasAny())
				pathInfo.Type = string.Join(",", query._Types.Select(s=>s.Resolve(settings)));

			if (query._AllIndices && pathInfo.Type == inferrer.TypeName<T>())
				pathInfo.Type = null;

			if (query._AllIndices && !pathInfo.Type.IsNullOrEmpty())
				pathInfo.Index = "_all";
			else if (query._AllIndices)
				pathInfo.Index = null;
			else if (query._Indices.HasAny())
				pathInfo.Index = string.Join(",", query._Indices);
			return pathInfo;
		}

		internal static ElasticSearchPathInfo<IndicesValidateQueryQueryString> ToPathInfo<T>(
			this ValidateQueryDescriptor<T> query,
			IConnectionSettings settings
			)
			where T : class
		{
			var pathInfo = Inferred<T, IndicesValidateQueryQueryString, ValidateQueryDescriptor<T>>(query, settings);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			if (query == null)
				return pathInfo;
			var qs = new IndicesValidateQueryQueryString();
			if (query._Explain.HasValue)
				qs.Explain(query._Explain.Value);
			if (query._IgnoreIndices.HasValue)
				qs.IgnoreIndices(query._IgnoreIndices.Value);
			if (!query._OperationThreading.IsNullOrEmpty())
				qs.OperationThreading(query._OperationThreading);
			if (!query._Source.IsNullOrEmpty())
				qs.Source(query._Source);
			if (!query._QueryStringQuery.IsNullOrEmpty())
				qs.Q(query._QueryStringQuery);
			pathInfo.QueryString = qs;	
			if (!query._Source.IsNullOrEmpty() || !query._QueryStringQuery.IsNullOrEmpty())
				pathInfo.HttpMethod = PathInfoHttpMethod.GET;

			return pathInfo;
		}
	}
}