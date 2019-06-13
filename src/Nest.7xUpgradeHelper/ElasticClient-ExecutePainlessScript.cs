using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Executes an arbitrary Painless script and returns a result.
		/// Useful for testing the syntactical correctness of Painless scripts
		/// </summary>
		public static ExecutePainlessScriptResponse<TResult> ExecutePainlessScript<TResult>(this IElasticClient client,
			Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector
		);

		/// <inheritdoc cref="ExecutePainlessScript{TResult}(System.Func{Nest.ExecutePainlessScriptDescriptor,Nest.IExecutePainlessScriptRequest})" />
		public static ExecutePainlessScriptResponse<TResult> ExecutePainlessScript<TResult>(this IElasticClient client,IExecutePainlessScriptRequest request);

		/// <inheritdoc cref="ExecutePainlessScript{TResult}(System.Func{Nest.ExecutePainlessScriptDescriptor,Nest.IExecutePainlessScriptRequest})" />
		public static Task<ExecutePainlessScriptResponse<TResult>> ExecutePainlessScriptAsync<TResult>(this IElasticClient client,
			Func<ExecutePainlessScriptDescriptor, IExecutePainlessScriptRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ExecutePainlessScript{TResult}(System.Func{Nest.ExecutePainlessScriptDescriptor,Nest.IExecutePainlessScriptRequest})" />
		public static Task<ExecutePainlessScriptResponse<TResult>> ExecutePainlessScriptAsync<TResult>(this IElasticClient client,IExecutePainlessScriptRequest request,
			CancellationToken ct = default
		);
	}

}
