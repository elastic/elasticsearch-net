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
			)
			.DefaultMappingFor<CommitActivity>(map => map
				.IndexName(TestValueHelper.ProjectsIndex)
				.RelationName("commits")
			)
			.DefaultMappingFor<Developer>(map => map
				.IndexName("devs")
				.Ignore(p => p.PrivateValue)
				.PropertyName(p => p.OnlineHandle, "nickname")
			)
			.DefaultMappingFor<ProjectPercolation>(map => map
				.IndexName("queries")
			)
			.DefaultMappingFor<Metric>(map => map
				.IndexName("server-metrics")
			)
			.DefaultMappingFor<GeoShape>(map => map
				.IndexName("geoshapes")
			)
			.DefaultMappingFor<Shape>(map => map
				.IndexName("shapes")
			);
	}
}
