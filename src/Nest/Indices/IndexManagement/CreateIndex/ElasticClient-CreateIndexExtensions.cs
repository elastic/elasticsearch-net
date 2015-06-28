using System;
using System.Threading.Tasks;

namespace Nest
{
	public static class CreateIndexExtensions
	{
		/// <summary>
		/// The create index API allows to instantiate an index. Elasticsearch provides support for multiple indices, 
		/// including executing operations across several indices.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-create-index.html
		/// </summary>
		/// <param name="client"></param>
		/// <param name="index">The name of the index to be created</param>
		/// <param name="createIndexSelector">A descriptor that further describes the parameters for the create index operation</param>
		public static IIndicesOperationResponse CreateIndex(this IElasticClient client, string index,
			Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector = null)
		{
			return CreateIndexDispatch(index, createIndexSelector, client.CreateIndex);
		}

		/// <summary>
		/// The create index API allows to instantiate an index. Elasticsearch provides support for multiple indices, 
		/// including executing operations across several indices.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-create-index.html
		/// </summary>
		/// <param name="client"></param>
		/// <param name="index">The name of the index to be created</param>
		/// <param name="createIndexSelector">A descriptor that further describes the parameters for the create index operation</param>
		public static Task<IIndicesOperationResponse> CreateIndexAsync(this IElasticClient client, string index,
			Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector = null)
		{
			return CreateIndexDispatch(index, createIndexSelector, client.CreateIndexAsync);
		}

		private static TResponse CreateIndexDispatch<TResponse>(string index,
			Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector,
			Func<Func<CreateIndexDescriptor, CreateIndexDescriptor>, TResponse> createIndexMethod)
		{
			index.ThrowIfNullOrEmpty("index");
			createIndexSelector = createIndexSelector ?? (c => c);
			return createIndexMethod(c => createIndexSelector(c).Index(index));
		}
	}
}
