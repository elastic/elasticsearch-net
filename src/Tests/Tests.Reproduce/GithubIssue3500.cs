using System.Linq;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GithubIssue3500
	{
		[U] public void BulkDescriptorDeleteManyCallableWithStringWithoutTypeDeclaration()
		{
			var ids = new List<string> { "1" };

			var bulkDescriptor = new BulkDescriptor().DeleteMany(ids);

			if (bulkDescriptor is IBulkRequest bulkRequest) {
				bulkRequest.Operations.First().Id.Should().Be(ids.First());
			}
		}
	}
}
