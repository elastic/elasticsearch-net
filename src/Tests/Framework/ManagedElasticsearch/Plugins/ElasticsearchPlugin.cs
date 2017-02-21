using System.Reflection;

namespace Tests.Framework.ManagedElasticsearch.Plugins
{
	public enum ElasticsearchPlugin
	{
		[Moniker("delete-by-query")] DeleteByQuery,
		[Moniker("cloud-azure")] CloudAzure,
		[Moniker("mapper-attachments")] MapperAttachments,
		[Moniker("mapper-murmur3")] MapperMurmer3,
		[Moniker("x-pack")] XPack,
		[Moniker("ingest-geoip")] IngestGeoIp,
		[Moniker("ingest-attachment")] IngestAttachment,
		[Moniker("analysis-kuromoji")] AnalysisKuromoji,
		[Moniker("analysis-icu")] AnalysisIcu
	}
}

