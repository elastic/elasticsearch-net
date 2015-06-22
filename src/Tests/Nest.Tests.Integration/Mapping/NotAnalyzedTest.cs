using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Mapping
{
	[TestFixture]
	public class NotAnalyzedTests : IntegrationTests
	{
		//TODO Here we had two integration tests that tested our not_analyzed enum with facets.
		//Worthwhile to revive this using aggs, or is this just noise?
	}
}
