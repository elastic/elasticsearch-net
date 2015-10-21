using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Search.Request
{
	public class FieldsUsageTests : SearchUsageTestBase
	{
		public FieldsUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			fields = new[] { "name", "startedOn" }
		};

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Fields(
				f => f.Name,
				f => f.StartedOn
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Fields = new FieldName[] { "name", "startedOn" }
			};
	}
}
