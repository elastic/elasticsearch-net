using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests._Internals.MockData;
using Tests._Internals.Integration;
using FluentAssertions;

namespace Tests.SearchAPIs.RequestBodySearch
{
	public class Sort
	{
		/**
		 * Allows to add one or more sort on specific fields. Each sort can be reversed as well. 
		 * The sort is defined on a per field level, with special field name for _score to sort by score.
		 */

		public class Usage : SearchUsageBase
		{
			public Usage(ReadOnlyIntegration i) : base(i) { }

			public override bool ExpectIsValid => true;

			public override int ExpectStatusCode => 200;

			protected override object ExpectedJson => 
				new {
					  sort = new object [] 
					    {
						  new { startedOn = new { order = "asc" } },
						  new { name = new { order = "desc" } }
						}
					};

			protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
				.Sort(ss => ss
					.OnField(p => p.StartedOn)
					.Order(SortOrder.Ascending)
				)
				.Sort(ss => ss
					.OnField(p => p.Name)
					.Order(SortOrder.Descending)
				);
				
			protected override SearchRequest<Project> Initializer =>
				new SearchRequest<Project>
				{
					Sort = new List<ISort>
					{
						new Nest.Sort
						{
							Field = "startedOn",
							Order = SortOrder.Ascending
						},
						new Nest.Sort
						{
							Field = "name",
							Order = SortOrder.Descending
						}
					}
				};

			public override void AssertUrl(Uri requestUri) => requestUri.AbsolutePath.Should().Be("/project/project/_search");
		}
	}
}
