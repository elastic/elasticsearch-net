using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

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
			.Fields(fs => fs
				.Field(p => p.Name)
				.Field(p => p.StartedOn)
			);

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project>
			{
				Fields = Fields<Project>(p => p.Name, p => p.StartedOn)
			};

		[I] protected Task FieldsAreReturned() => this.AssertOnAllResponses(r =>
		{
			r.Fields.Should().NotBeNull();
			r.Fields.Count().Should().BeGreaterThan(0);
			foreach (var fieldValues in r.Fields)
			{
				fieldValues.Count().Should().Be(2);
				var name = fieldValues.Value<string>(Field<Project>(p => p.Name));
				name.Should().NotBeNullOrWhiteSpace();

				var dateTime = fieldValues.ValueOf<Project, DateTime>(p => p.StartedOn);
				dateTime.Should().BeAfter(default(DateTime));
			}
		});
	}
}
