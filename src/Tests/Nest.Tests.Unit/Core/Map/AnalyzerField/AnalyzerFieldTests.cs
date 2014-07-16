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
			var result = this._client.Map<ElasticsearchProject>(m => m
				.AnalyzerField(a => a
					.Path(p => p.Name)
					.Index()
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
		[Test]
		public void AnalyzerFieldUsingString()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.AnalyzerField(a => a
					.Path("my_difficult_field_name")
					.Index(false)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
