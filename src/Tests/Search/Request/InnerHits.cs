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
	public class InnerHits
	{
		public class Usage : SearchUsageBase
		{
			public Usage(ReadOnlyIntegration i) : base(i)
			{
			}

			protected override object ExpectJson => new
			{
				query = new
				{
					nested = new
					{
						query = new
						{
							match = new
							{
								name = new {
									query = "1.0"
								}
							}
						},
						path = "project.tags",
						inner_hits = new { }
					}
				}
			};

			protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
				.Query(q => q
					.Nested(n => n
						.Path("project.tags")
						.Query(nq => nq
							.Match(m => m
								.OnField(p => p.Name)
								.Query("1.0")
							)
						)
						.InnerHits()
					)
				);

			protected override SearchRequest<Project> Initializer =>
				new SearchRequest<Project>
				{
					Query = new NestedQuery
					{
						Path = "project.tags",
						Query = new QueryContainer(new MatchQuery
						{
							Field = "name",
							Query = "1.0"
						}),
						InnerHits = new Nest.InnerHits()
					}
				};
		}
	}
}
