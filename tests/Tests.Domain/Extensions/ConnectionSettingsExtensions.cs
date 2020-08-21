// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
