using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Map.DynamicTemplates
{
	[TestFixture]
	public class DynamicTemplatesTests : BaseMappingTests
	{
		[Test]
		public void MultiFieldWithGenericTypes()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.DynamicTemplates(d => d
					.Add(t => t
						.Name("template_1")
						.Match("multi*")
						.Mapping(tm => tm
							.MultiField(mf => mf
								.Fields(mff => mff
									.Generic(g => g
										.Name("{name}")
										.Type("{dynamic_type}")
										.Index("analyzed")
										.Store(false)
									)
									.Generic(g => g
										.Name("org")
										.Type("{dynamic_type}")
										.Index("not_analyzed")
										.Store()
									)
								)
							)
						)
					)
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void CompletelyGenericTemplate()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.DynamicTemplates(d => d
					.Add(t => t
						.Name("store_generic")
						.Match("*")
						.Mapping(tm => tm
							.Generic(g=>g.Store())
						)
					)
				)
			);
			this.DefaultResponseAssertations(result);
		}
	}
}
