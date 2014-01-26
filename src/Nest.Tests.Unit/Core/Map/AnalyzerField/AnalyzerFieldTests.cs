using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Map.AnalyzerField
{
	[TestFixture]
	public class AnalyzerFieldTests : BaseJsonTests
	{
		[Test]
		public void AnalyzerFieldUsingExpression()
		{
			var result = this._client.Map<ElasticSearchProject>(m => m
				.AnalyzerField(a => a
					.SetPath(p => p.Name)
					.SetIndexed()
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
		[Test]
		public void AnalyzerFieldUsingString()
		{
			var result = this._client.Map<ElasticSearchProject>(m => m
				.AnalyzerField(a => a
					.SetPath("my_difficult_field_name")
					.SetIndexed(false)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
