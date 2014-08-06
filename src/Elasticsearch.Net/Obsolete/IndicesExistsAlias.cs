using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net.Connection.Configuration;

namespace Elasticsearch.Net.Obsolete
{
	using IndicesExistsAliasSelector = Func<IndicesExistsAliasRequestParameters, IndicesExistsAliasRequestParameters>;
	
	[Obsolete("Scheduled to be removed in 2.0, renamed to AliasExistsRequestParameters")]
	public class IndicesExistsAliasRequestParameters : AliasExistsRequestParameters { }

	public static class ClientExtensions
	{
		public static Func<TUp, TUp> UpCastSelector<TDown, TUp>(Func<TDown, TDown> oldSelector)
			where TDown : IRequestParameters, TUp, new()
			where TUp : IRequestParameters, new()

		{
			if (oldSelector == null) return null;
			return (s) => oldSelector(DownCastDescriptor<TDown, TUp>(s));
		}

		public static TDown DownCastDescriptor<TDown, TUp>(TUp instance)
			where TDown : IRequestParameters, new()
			where TUp : IRequestParameters, new()
		{
			return new TDown()
			{
				RequestConfiguration = instance.RequestConfiguration,
				DeserializationState = instance.DeserializationState,
				QueryString = instance.QueryString
			};
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static ElasticsearchResponse<T> IndicesExistsAlias<T>(
			this IElasticsearchClient client, string index, string name, IndicesExistsAliasSelector requestParameters)
		{
			var selector = UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAlias<T>(index, name, selector);
		}

		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesExistsAliasAsync<T>(
			this IElasticsearchClient client, string index, string name, IndicesExistsAliasSelector requestParameters)
		{
			var selector = UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAliasAsync<T>(index, name, selector);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesExistsAlias(
			this IElasticsearchClient client, string index, string name, IndicesExistsAliasSelector requestParameters)
		{
			var selector = UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAlias(index, name, selector);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesExistsAliasAsync(
			this IElasticsearchClient client, string index, string name, IndicesExistsAliasSelector requestParameters)
		{
			var selector = UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAliasAsync(index, name, selector);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static ElasticsearchResponse<T> IndicesExistsAlias<T>(
			this IElasticsearchClient client, string index, IndicesExistsAliasSelector requestParameters)
		{
			var selector = UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAlias<T>(index, selector);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static Task<ElasticsearchResponse<T>> IndicesExistsAliasAsync<T>(
			this IElasticsearchClient client, string index, IndicesExistsAliasSelector requestParameters)
		{
			var selector = UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAliasAsync<T>(index, selector);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static ElasticsearchResponse<DynamicDictionary> IndicesExistsAlias(
			this IElasticsearchClient client, string index, IndicesExistsAliasSelector requestParameters)
		{
			var selector = UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAlias(index, selector);
		}
		
		[Obsolete("Scheduled to be removed in 2.0, use the method that takes a Func of AliasExistsRequestParameters")]
		public static Task<ElasticsearchResponse<DynamicDictionary>> IndicesExistsAliasAsync(
			this IElasticsearchClient client, string index, IndicesExistsAliasSelector requestParameters)
		{
			var selector = UpCastSelector<IndicesExistsAliasRequestParameters, AliasExistsRequestParameters>(requestParameters);
			return client.IndicesExistsAliasAsync(index, selector);
		}
	
	}
}
