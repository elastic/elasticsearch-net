using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Delete an index alias
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#deleting
		/// </summary>
		/// <param name="request">A descriptor that describes the delete alias request</param>
		public static DeleteAliasResponse DeleteAlias(this IElasticClient client,IDeleteAliasRequest request);

		/// <inheritdoc />
		public static Task<DeleteAliasResponse> DeleteAliasAsync(this IElasticClient client,IDeleteAliasRequest request, CancellationToken ct = default);

		/// <inheritdoc />
		public static DeleteAliasResponse DeleteAlias(this IElasticClient client,Indices indices, Names names, Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null);

		/// <inheritdoc />
		public static Task<DeleteAliasResponse> DeleteAliasAsync(this IElasticClient client,
			Indices indices,
			Names names,
			Func<DeleteAliasDescriptor, IDeleteAliasRequest> selector = null,
			CancellationToken ct = default
		);
	}


}
