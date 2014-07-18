using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Map.TimestampField
{
	[TestFixture]
	public class TimestampFieldTests : BaseMappingTests
	{
		[Test]
		public void TimestampFieldUsingExpression()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.TimestampField(a => a
					.Path(p => p.Name)
					.Enabled()
				)
			);
			this.DefaultResponseAssertations(result);
		}
		[Test]
		public void TimestampFieldUsingString()
		{
			var result = this.Client.Map<ElasticsearchProject>(m => m
				.TimestampField(a => a
					.Path("my_difficult_field_name")
					.Enabled()
				)
			);
			this.DefaultResponseAssertations(result);
		}
	}
}
