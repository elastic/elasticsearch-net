using Nest;
using Tests.Configuration;
using static Tests.Domain.Helpers.TestValueHelper;

namespace Tests.Domain.Extensions
{
	public static class ConnectionSettingsExtensions
	{
		public static ConnectionSettings ApplyDomainSettings(this ConnectionSettings settings) => settings
			.DefaultIndex("default-index")
			.InferMappingFor<Project>(ProjectMapping)
			.InferMappingFor<Ranges>(RangesMapping)
			.InferMappingFor<CommitActivity>(map => map
				.IndexName("project")
				.TypeName("commits")
			)
			.InferMappingFor<Developer>(map => map
				.IndexName("devs")
				.Ignore(p => p.PrivateValue)
				.Rename(p => p.OnlineHandle, "nickname")
			)
			.InferMappingFor<PercolatedQuery>(map => map
				.IndexName("queries")
				.TypeName(PercolatorType)
			)
			.InferMappingFor<Metric>(map => map
				.IndexName("server-metrics")
				.TypeName("metric")
			)
			.InferMappingFor<Shape>(map => map
				.IndexName("shapes")
				.TypeName("doc")
			);
		private static IClrTypeMapping<Project> ProjectMapping(ClrTypeMappingDescriptor<Project> m)
		{
			m.IndexName("project").IdProperty(p => p.Name);
			//*_range type only available since 5.2.0 so we ignore them when running integration tests
			if (InRange("<5.2.0") && TestConfiguration.Instance.RunIntegrationTests)
				m.Ignore(p => p.Ranges);
			return m;
		}

		private static IClrTypeMapping<Ranges> RangesMapping(ClrTypeMappingDescriptor<Ranges> m)
		{
			//ip_range type only available since 5.5.0 so we ignore them when running integration tests
			if (InRange("<5.5.0") && TestConfiguration.Instance.RunIntegrationTests)
				m.Ignore(p => p.Ips);
			return m;
		}
	}
}
