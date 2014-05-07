using System;

namespace Nest
{
	/// <summary>
	/// Provides convenience extension to open an index by string or type.
	/// </summary>
	public static class CreateIndexExtensions
	{
		/// <summary>
		/// The create index API allows to instantiate an index. Elasticsearch provides support for multiple indices, 
		/// including executing operations across several indices.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-create-index.html
		/// </summary>
		/// <param name="index">The name of the index to be created</param>
		/// <param name="createIndexSelector">A descriptor that further describes the parameters for the create index operation</param>
		public static IIndicesOperationResponse CreateIndex(this IElasticClient client, string index, 
			Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector = null )
		{
			index.ThrowIfNullOrEmpty("index");
			createIndexSelector = createIndexSelector ?? (c => c);
			return client.CreateIndex(c=> createIndexSelector(c).Index(index));
		}

		/// <summary>
		/// The create index API allows to instantiate an index. Elasticsearch provides support for multiple indices, 
		/// including executing operations across several indices.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-create-index.html
		/// </summary>
		/// <param name="index">The name of the index to be created</param>
		/// <param name="createIndexSelector">A descriptor that further describes the parameters for the create index operation</param>
		public static IIndicesOperationResponse CreatIndex(this IElasticClient client, string index,
			Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector = null)
		{
			index.ThrowIfNullOrEmpty("index");
			createIndexSelector = createIndexSelector ?? (c => c);
			return client.CreateIndex(c => createIndexSelector(c).Index(index));
		}
		
	}
}