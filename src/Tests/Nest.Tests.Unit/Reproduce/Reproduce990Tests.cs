using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce990Tests : BaseJsonTests
	{
		[Test]
		public void DateMappingShouldNotTrow()
		{
			Assert.DoesNotThrow(() =>
			{
				var result = _client.Map<ElasticsearchProject>(p => p
					.DynamicTemplates(d => d
						.Add(dt => dt
							.Name("date_template")
							.Match("*_date")
							.MatchMappingType("date")
							.Mapping(mp => mp
								.Date(date => date
									.Format("basic_date || date|| yyyy/MM/dd || dd/MM/yyyy || dd-MM-yyyy || MM/dd/yyyy || MM-dd-yyyy")
									.Fields(f => f
										.String(fs => fs
											.Name("raw")
											.Analyzer("number_delimiter")
										)
									)
								)
							)
						)
					)
				);
			});
		}
	}
}
