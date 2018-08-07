using Nest;
using Tests.Domain.Helpers;

namespace Tests.Domain.Extensions
{
	public static class ConnectionSettingsExtensions
	{
		public static ConnectionSettings ApplyDomainSettings(this ConnectionSettings settings) => settings
			.DefaultIndex("default-index")
			.DefaultMappingFor<Project>(map => map
				.IndexName(TestValueHelper.ProjectsIndex)
				.IdProperty(p => p.Name)
				.RelationName("project")
				.TypeName("doc")
			)
			.DefaultMappingFor<CommitActivity>(map => map
				.IndexName(TestValueHelper.ProjectsIndex)
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
				.TypeName(TestValueHelper.PercolatorType)
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
