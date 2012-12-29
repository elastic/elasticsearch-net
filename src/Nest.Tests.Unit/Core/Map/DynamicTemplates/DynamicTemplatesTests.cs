using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Map.DynamicTemplates
{
	[TestFixture]
	public class DynamicTemplatesTests : BaseJsonTests
	{
		[Test]
		public void MultiFieldWithGenericTypes()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
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
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
		[Test]
		public void CompletelyGenericTemplate()
		{
			var result = this._client.MapFluent<ElasticSearchProject>(m => m
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
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
