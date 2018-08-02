using Nest;
using Tests.Framework.ManagedElasticsearch;

namespace Tests.Framework.MockData
{
	public static class ConnectionSettingsExtensions
	{
		public static ConnectionSettings ApplyDomainSettings(this ConnectionSettings settings) => settings
			.DefaultIndex("default-index")
			.DefaultMappingFor<Project>(map => map
				.IndexName(VersionDependentValues.ProjectsIndex)
				.IdProperty(p => p.Name)
				.RelationName("project")
				.TypeName("doc")
			)
			.DefaultMappingFor<CommitActivity>(map => map
				.IndexName(VersionDependentValues.ProjectsIndex)
				.RelationName("commits")
				.TypeName("doc")
			)
			.DefaultMappingFor<Developer>(map => map
				.IndexName("devs")
				.Ignore(p => p.PrivateValue)
				.PropertyName(p => p.OnlineHandle, "nickname")
			)
			.DefaultMappingFor<ProjectPercolation>(map => map
				.IndexName("queries")
				.TypeName(VersionDependentValues.PercolatorType)
			)
			.DefaultMappingFor<Metric>(map => map
				.IndexName("server-metrics")
				.TypeName("metric")
			)
			.DefaultMappingFor<Shape>(map => map
				.IndexName("shapes")
				.TypeName("doc")
			);
	}
}
