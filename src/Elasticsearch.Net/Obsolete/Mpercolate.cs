using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
#pragma warning disable 0618
	using MpercolateSelector = Func<MpercolateRequestParameters, MpercolateRequestParameters>;
#pragma warning restore 0618

	[Obsolete("Scheduled to be removed in 2.0, renamed to TypeExistsRequestParameters")]
	public class MpercolateRequestParameters : MultiPercolateRequestParameters { }

	public static class MpercolateClientExtensions
	{
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static ElasticsearchResponse<T> MpercolateGet<T>(this IElasticsearchClient client, MpercolateSelector requestParameters)
		{
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateGet<T>(selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static Task<ElasticsearchResponse<T>> MpercolateGetAsync<T>(this IElasticsearchClient client, MpercolateSelector requestParameters)
		{

			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateGetAsync<T>(selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> MpercolateGet(this IElasticsearchClient client, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateGet(selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> MpercolateGetAsync(this IElasticsearchClient client, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateGetAsync(selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static ElasticsearchResponse<T> MpercolateGet<T>(this IElasticsearchClient client, string index, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateGet<T>(index, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static Task<ElasticsearchResponse<T>> MpercolateGetAsync<T>(this IElasticsearchClient client, string index, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateGetAsync<T>(index, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> MpercolateGet(this IElasticsearchClient client, string index, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateGet(index, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> MpercolateGetAsync(this IElasticsearchClient client, string index, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateGetAsync(index, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static ElasticsearchResponse<T> MpercolateGet<T>(this IElasticsearchClient client, string index, string type, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateGet<T>(index, type, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static Task<ElasticsearchResponse<T>> MpercolateGetAsync<T>(this IElasticsearchClient client, string index, string type, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateGetAsync<T>(index, type, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> MpercolateGet(this IElasticsearchClient client, string index, string type, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateGet(index, type, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> MpercolateGetAsync(this IElasticsearchClient client, string index, string type, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateGetAsync(index, type, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static ElasticsearchResponse<T> Mpercolate<T>(this IElasticsearchClient client, object body, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.Mpercolate<T>(body, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static Task<ElasticsearchResponse<T>> MpercolateAsync<T>(this IElasticsearchClient client, object body, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateAsync<T>(body, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> Mpercolate(this IElasticsearchClient client, object body, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.Mpercolate(body, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> MpercolateAsync(this IElasticsearchClient client, object body, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateAsync(body, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static ElasticsearchResponse<T> Mpercolate<T>(this IElasticsearchClient client, string index, object body, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.Mpercolate<T>(index, body, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static Task<ElasticsearchResponse<T>> MpercolateAsync<T>(this IElasticsearchClient client, string index, object body, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateAsync<T>(index, body, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> Mpercolate(this IElasticsearchClient client, string index, object body, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.Mpercolate(index, body, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> MpercolateAsync(this IElasticsearchClient client, string index, object body, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateAsync(index, body, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static ElasticsearchResponse<T> Mpercolate<T>(this IElasticsearchClient client, string index, string type, object body, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.Mpercolate<T>(index, type, body, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static Task<ElasticsearchResponse<T>> MpercolateAsync<T>(this IElasticsearchClient client, string index, string type, object body, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateAsync<T>(index, type, body, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> Mpercolate(this IElasticsearchClient client, string index, string type, object body, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.Mpercolate(index, type, body, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of MultiPercolateRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> MpercolateAsync(this IElasticsearchClient client, string index, string type, object body, MpercolateSelector requestParameters)
		{
			
			var selector = Obsolete.UpCastSelector<MpercolateRequestParameters, MultiPercolateRequestParameters>(requestParameters);
			return client.MpercolateAsync(index, type, body, selector);
		}
	}
}
