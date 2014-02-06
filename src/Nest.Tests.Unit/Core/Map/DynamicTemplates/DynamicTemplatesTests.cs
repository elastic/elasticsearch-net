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
			var result = this._client.Map<ElasticSearchProject>(m => m
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
									.Generic(g => g
										.Name("suggest", noNameProperty: true)
										.Type("completion")
										.IndexAnalyzer("default")
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
			var result = this._client.Map<ElasticSearchProject>(m => m
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
		[Test]
		public void MultipleTemplates()
		{
			var result = this._client.Map<ElasticSearchProject>(m => m
				.DynamicTemplates(d => d
					.Add(t => t
						.Name("string")
						.Match("str_*")
						.MatchMappingType("string")
						.Mapping(tm => tm.String(sm=>sm.Index(FieldIndexOption.not_analyzed)))
					)
					.Add(t => t
						.Name("numbers")
						.Match("nu_*")
						.MatchMappingType("integer")
						.Mapping(tm => tm.Number(sm => sm.Type(NumberType.integer).NullValue(4)))
					)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
