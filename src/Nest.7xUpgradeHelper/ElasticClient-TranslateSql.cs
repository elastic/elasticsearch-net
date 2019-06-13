using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary> The SQL Translate API accepts SQL in a JSON document and translates it into a native Elasticsearch search request</summary>
		public static TranslateSqlResponse TranslateSql(this IElasticClient client,Func<TranslateSqlDescriptor, ITranslateSqlRequest> selector = null);

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})" />
		public static TranslateSqlResponse TranslateSql(this IElasticClient client,ITranslateSqlRequest request);

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})" />
		public static Task<TranslateSqlResponse> TranslateSqlAsync(this IElasticClient client,Func<TranslateSqlDescriptor, ITranslateSqlRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="TranslateSql(System.Func{Nest.TranslateSqlDescriptor,Nest.ITranslateSqlRequest})" />
		public static Task<TranslateSqlResponse> TranslateSqlAsync(this IElasticClient client,ITranslateSqlRequest request, CancellationToken ct = default);
	}

}
