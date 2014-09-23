using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Nest.Tests.Integration.Core.Analyze
{
	[TestFixture]
	public class AnalyzeTests : IntegrationTests
	{
		[Test]
		public void AnalyzeTest()
		{
			var request = new AnalyzeRequest("text to analyze");
			var result = this.Client.Analyze(request);
			result.IsValid.Should().BeTrue();
		}

		[Test]
		public void AnalyzeEmptyStringDoesNotThrow()
		{
			var request = new AnalyzeRequest(string.Empty);
			var result = this.Client.Analyze(request);
			result.IsValid.Should().BeFalse();
		}
	}
}
