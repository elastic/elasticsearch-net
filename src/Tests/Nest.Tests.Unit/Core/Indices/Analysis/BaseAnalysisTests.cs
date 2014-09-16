using System;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.Indices.Analysis.Tokenizers
{
	[TestFixture]
	public abstract class BaseAnalysisTests : BaseJsonTests
	{
		public IIndicesOperationResponse Analysis(Func<AnalysisDescriptor, AnalysisDescriptor> analysisSelector)
		{
			var result = this._client.CreateIndex(UnitTestDefaults.DefaultIndex, c => c
				.Analysis(analysisSelector)
			);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.ConnectionStatus.Should().NotBeNull();
			return result;
		}
	}
}
