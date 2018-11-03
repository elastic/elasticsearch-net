using System;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public partial interface IElasticLowLevelClient
	{
		/// <summary>GET on /_xpack/migration/deprecations
		/// <para>http://www.elastic.co/guide/en/migration/current/migration-api-deprecation.html</para>
		/// </summary>
		/// <param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		[Obsolete("Use XpackMigrationDeprecations")]
		TResponse XpackDeprecationInfo<TResponse>(DeprecationInfoRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new();

		/// <summary>GET on /_xpack/migration/deprecations
		/// <para>http://www.elastic.co/guide/en/migration/current/migration-api-deprecation.html</para>
		/// </summary>
		/// <param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		[Obsolete("Use XpackMigrationDeprecationsAsync")]
		Task<TResponse> XpackDeprecationInfoAsync<TResponse>(DeprecationInfoRequestParameters requestParameters = null,
			CancellationToken ctx = default(CancellationToken)
		) where TResponse : class, IElasticsearchResponse, new();

		/// <summary>GET on /{index}/_xpack/migration/deprecations
		/// <para>http://www.elastic.co/guide/en/migration/current/migration-api-deprecation.html</para>
		/// </summary>
		/// <param name="index">Index pattern</param>
		/// <param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		[Obsolete("Use XpackMigrationDeprecations")]
		TResponse XpackDeprecationInfo<TResponse>(string index, DeprecationInfoRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new();

		/// <summary>GET on /{index}/_xpack/migration/deprecations
		/// <para>http://www.elastic.co/guide/en/migration/current/migration-api-deprecation.html</para>
		/// </summary>
		/// <param name="index">Index pattern</param>
		/// <param name="requestParameters">A func that allows you to describe the querystring parameters &amp; request specific connection settings.</param>
		[Obsolete("Use XpackMigrationDeprecationsAsync")]
		Task<TResponse> XpackDeprecationInfoAsync<TResponse>(string index, DeprecationInfoRequestParameters requestParameters = null,
			CancellationToken ctx = default(CancellationToken)
		) where TResponse : class, IElasticsearchResponse, new();
	}
}
