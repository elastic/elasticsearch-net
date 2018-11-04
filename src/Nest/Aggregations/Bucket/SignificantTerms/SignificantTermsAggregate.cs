using System.Collections.Generic;

namespace Nest
{
	public class SignificantTermsAggregate : MultiBucketAggregate<SignificantTermsBucket>
	{
		public SignificantTermsAggregate() { }

		public SignificantTermsAggregate(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public long? BgCount { get; set; }

		public long DocCount { get; set; }
	}
}
