// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Tests.Analysis.Analyzers
{
	public interface IAnalyzerAssertion : IAnalysisAssertion<IAnalyzer, IAnalyzers, AnalyzersDescriptor> { }

	public abstract class AnalyzerAssertionBase<TAssertion>
		: AnalysisComponentTestBase<TAssertion, IAnalyzer, IAnalyzers, AnalyzersDescriptor>
			, IAnalyzerAssertion
		where TAssertion : AnalyzerAssertionBase<TAssertion>, new()
	{
		protected override object AnalysisJson => new
		{
			analyzer = new Dictionary<string, object> { { AssertionSetup.Name, AssertionSetup.Json } }
		};

		protected override IAnalysis FluentAnalysis(AnalysisDescriptor an) =>
			an.Analyzers(d => AssertionSetup.Fluent(AssertionSetup.Name, d));

		protected override Nest.Analysis InitializerAnalysis() =>
			new Nest.Analysis { Analyzers = new Nest.Analyzers { { AssertionSetup.Name, AssertionSetup.Initializer } } };

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[U] public override Task TestPutSettingsRequest() => base.TestPutSettingsRequest();

		[I] public override Task TestPutSettingsResponse() => base.TestPutSettingsResponse();
	}
}
