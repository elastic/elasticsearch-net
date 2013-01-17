using System;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Indices.Analysis.Tokenizers
{
	[TestFixture]
	public class BaseAnalysisTests : BaseJsonTests
	{
		public IIndicesResponse Analysis(Func<AnalysisDescriptor, AnalysisDescriptor> analysisSelector)
		{
			var result = this._client.CreateIndex(Test.Default.DefaultIndex, c => c
				.Analysis(analysisSelector)
			);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.ConnectionStatus.Should().NotBeNull();
			return result;
		}
	}
}
