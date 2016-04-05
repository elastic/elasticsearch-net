using System.Linq;
using System.Reflection;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.Aggregations
{
    public class AggregationVisitorTests
    {
		[U]
		public void VisitMethodForEachTypeOfAggregation()
		{
			var aggregationTypes =
				from t in typeof(IAggregation).Assembly().Types()
				where typeof(IAggregation).IsAssignableFrom(t)
				where t.IsInterface()
				select t;

			var visitorMethodParameters =
				from m in typeof(IAggregationVisitor).GetTypeInfo().DeclaredMethods
				where m.Name == "Visit"
				let aggregationInterface = m.GetParameters().First().ParameterType
				where aggregationInterface != typeof(IAggregationContainer)
				select aggregationInterface;

			visitorMethodParameters.Except(aggregationTypes).Should().BeEmpty();
		}
	}
}
