using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Multi termvectors API allows to get multiple termvectors based on an index, type and id.
		/// <para> </para>
		/// <a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-termvectors.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-termvectors.html</a>
		/// </summary>
		/// <param name="selector">The descriptor describing the multi termvectors operation</param>
		public static MultiTermVectorsResponse MultiTermVectors(this IElasticClient client,Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null);

		/// <inheritdoc cref="MultiTermVectors(System.Func{Nest.MultiTermVectorsDescriptor,Nest.IMultiTermVectorsRequest})" />
		public static MultiTermVectorsResponse MultiTermVectors(this IElasticClient client,IMultiTermVectorsRequest request);

		/// <inheritdoc cref="MultiTermVectors(System.Func{Nest.MultiTermVectorsDescriptor,Nest.IMultiTermVectorsRequest})" />
		public static Task<MultiTermVectorsResponse> MultiTermVectorsAsync(this IElasticClient client,Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="MultiTermVectors(System.Func{Nest.MultiTermVectorsDescriptor,Nest.IMultiTermVectorsRequest})" />
		public static Task<MultiTermVectorsResponse> MultiTermVectorsAsync(this IElasticClient client,IMultiTermVectorsRequest request,
			CancellationToken ct = default
		);
	}

}
