using NUnit.Framework;
using Nest.Tests.MockData.Domain;

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
					.Path(p => p.Name)
					.Index()
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void AnalyzerFieldUsingString()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.AnalyzerField(a => a
					.Path("my_difficult_field_name")
					.Index(false)
				)
			);
			this.DefaultResponseAssertations(result);
		}
	}
}
