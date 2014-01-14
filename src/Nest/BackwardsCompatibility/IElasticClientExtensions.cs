namespace Nest.BackwardsCompatibility
{
	public static class IELasticClientExtensions
	{
		public static IIndicesOperationResponse OpenIndex(this IElasticClient client, string index)
		{
			return client.OpenIndex(f => f.Index(index));
		}

		public static IIndicesOperationResponse OpenIndex<T>(this IElasticClient client)
			where T : class 
		{
			return client.OpenIndex(f => f.Index<T>());
		}
		
		public static IIndicesOperationResponse CloseIndex(this IElasticClient client, string index)
		{
			return client.CloseIndex(f => f.Index(index));
		}

		public static IIndicesOperationResponse CloseIndex<T>(this IElasticClient client)
			where T : class 
		{
			return client.CloseIndex(f => f.Index<T>());
		}

	}
}