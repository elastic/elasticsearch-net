using Nest;
using Tests.Domain.Helpers;

namespace Tests.Domain.Extensions
{
	public static class ConnectionSettingsExtensions
	{
		public static ConnectionSettings ApplyDomainSettings(this ConnectionSettings settings) => settings
			.DefaultIndex("default-index")
			.InferMappingFor<Project>(map => map
				.IndexName(TestValueHelper.ProjectsIndex)
				.IdProperty(p => p.Name)
				.TypeName("doc")
			)
			.InferMappingFor<CommitActivity>(map => map
				.IndexName(TestValueHelper.ProjectsIndex)
				.TypeName("doc")
			)
			.InferMappingFor<Developer>(map => map
				.IndexName("devs")
				.Ignore(p => p.PrivateValue)
				.Rename(p => p.OnlineHandle, "nickname")
			)
			.InferMappingFor<ProjectPercolation>(map => map
				.IndexName("queries")
				.TypeName(TestValueHelper.PercolatorType)
			)
			.InferMappingFor<Metric>(map => map
				.IndexName("server-metrics")
				.TypeName("metric")
			)
			.InferMappingFor<Shape>(map => map
				.IndexName("shapes")
				.TypeName("doc")
			);
	}
}
