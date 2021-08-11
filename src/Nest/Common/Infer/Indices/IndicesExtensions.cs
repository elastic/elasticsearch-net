using Nest.Types.Core;

namespace Nest
{
	public static class IndicesExtensions
	{
		public static string Resolve(this Indices marker, IElasticsearchClientSettings elasticsearchClientSettings)
		{
			elasticsearchClientSettings.ThrowIfNull(nameof(elasticsearchClientSettings));
			return elasticsearchClientSettings.Inferrer.Resolve(marker);
		}
	}
}
