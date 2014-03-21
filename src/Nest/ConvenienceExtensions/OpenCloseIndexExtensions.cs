namespace Nest
{
	/// <summary>
	/// Provides convenience extension to open an index by string or type.
	/// </summary>
	public static class OpenCloseIndexExtensions
	{
		/// <summary>
		/// The open and close index APIs allow to close an index, and later on opening it. 
		/// A closed index has almost no overhead on the cluster (except for maintaining its metadata), and is blocked 
		/// for read/write operations. 
		/// A closed index can be opened which will then go through the normal recovery process.
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html
		/// </summary>
		/// <param name="client"></param>
		/// <param name="index">The name of the index to be opened</param>
		public static IIndicesOperationResponse OpenIndex(this IElasticClient client, string index)
		{
			return client.OpenIndex(f => f.Index(index));
		}

		/// <summary>
		/// The open and close index APIs allow to close an index, and later on opening it. 
		/// A closed index has almost no overhead on the cluster (except for maintaining its metadata), and is blocked 
		/// for read/write operations. 
		/// A closed index can be opened which will then go through the normal recovery process.
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html
		/// </summary>
		/// <typeparam name="T"> The type used to infer the index name to be opened</typeparam>
		/// <param name="client"></param>
		public static IIndicesOperationResponse OpenIndex<T>(this IElasticClient client)
			where T : class
		{
			return client.OpenIndex(f => f.Index<T>());
		}

		/// <summary>
		/// The open and close index APIs allow to close an index, and later on opening it. 
		/// A closed index has almost no overhead on the cluster (except for maintaining its metadata), and is blocked 
		/// for read/write operations. 
		/// A closed index can be opened which will then go through the normal recovery process.
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html
		/// </summary>
		/// <param name="client"></param>
		/// <param name="index">The name of the index to be closed</param>
		public static IIndicesOperationResponse CloseIndex(this IElasticClient client, string index)
		{
			return client.CloseIndex(f => f.Index(index));
		}

		/// <summary>
		/// The open and close index APIs allow to close an index, and later on opening it. 
		/// A closed index has almost no overhead on the cluster (except for maintaining its metadata), and is blocked 
		/// for read/write operations. 
		/// A closed index can be opened which will then go through the normal recovery process.
		/// <para> </para>>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the index name to be closed</typeparam>
		/// <param name="client"></param>
		public static IIndicesOperationResponse CloseIndex<T>(this IElasticClient client)
			where T : class
		{
			return client.CloseIndex(f => f.Index<T>());
		}
	}
}