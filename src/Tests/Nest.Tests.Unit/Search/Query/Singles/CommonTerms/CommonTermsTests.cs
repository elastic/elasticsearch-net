using System.Reflection;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.Query.Singles.CommonTerms
{
	[TestFixture]
	public class CommonTermsTests : BaseJsonTests
	{
		[Test]
		public void CommonTermsFull()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(q=>q
					.CommonTerms(qs=>qs
						.OnField(p=>p.Content)
						.Analyzer("myAnalyzer")
						.Boost(1.2)
						.CutOffFrequency(0.01)
						.DisableCoord(false)
						.HighFrequencyOperator(Operator.and)
						.LowFrequencyOperator(Operator.or)
						.MinimumShouldMatch(1)
						.Query("This is the most awful stopwords query ever")
					)
			);
				
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}
