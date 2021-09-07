using Elastic.Clients.Elasticsearch;
using Tests.Domain.Helpers;

namespace Tests.Domain.Extensions
{
	public static class ConnectionSettingsExtensions
	{
		public static ElasticsearchClientSettings ApplyDomainSettings(this ElasticsearchClientSettings settings) =>
			settings
				.DefaultIndex("default-index")
				.DefaultMappingFor<Project>(map => map
					.IndexName(TestValueHelper.ProjectsIndex)
					.IdProperty(p => p.Name)
					.RelationName("project"));
	}
}
