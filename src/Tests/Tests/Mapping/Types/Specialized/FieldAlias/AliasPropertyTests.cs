using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;

namespace Tests.Mapping.Types.Specialized.FieldAlias
{
	public class FieldAliasPropertyTests : PropertyTestsBase
	{
		public FieldAliasPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				leadDevFirstName = new
				{
					type = "alias",
					path = "leadDeveloper.firstName",
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
				.FieldAlias(s => s
					.Name("leadDevFirstName")
					.Path(p=>p.LeadDeveloper.FirstName)
				);

		protected override IProperties InitializerProperties => new Properties
		{
			{ "leadDevFirstName", new FieldAliasProperty
				{
					Path = Infer.Field<Project>(p=>p.LeadDeveloper.FirstName)
				}
			}
		};
	}
}
