using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Integration.Core.Map.AnalyzerField
{
	[TestFixture]
	public class AnalyzerFieldTests : BaseMappingTests
	{
		[Test]
		public void AnalyzerFieldUsingExpression()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.AnalyzerField(a => a
					.SetPath(p => p.Name)
					.SetIndexed()
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void AnalyzerFieldUsingString()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.AnalyzerField(a => a
					.SetPath("my_difficult_field_name")
					.SetIndexed(false)
				)
			);
			this.DefaultResponseAssertations(result);
		}
	}
}
