namespace Nest
{
	public static class IndexNameExtensions
	{
		public static string Resolve(this IndexName marker, IElasticsearchClientSettings elasticsearchClientSettings)
		{
			if (marker == null)
				return null;

			elasticsearchClientSettings.ThrowIfNull(nameof(elasticsearchClientSettings));

			if (marker.Type == null)
				return marker.Name;

			return new IndexNameResolver(elasticsearchClientSettings).Resolve(marker.Type);
		}
	}
}
