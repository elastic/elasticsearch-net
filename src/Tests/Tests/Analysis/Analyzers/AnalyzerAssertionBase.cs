using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Tests.Analysis.Analyzers
{
	public interface IAnalyzerAssertion : IAnalysisAssertion<IAnalyzer, IAnalyzers, AnalyzersDescriptor> { }

	public abstract class AnalyzerAssertionBase<TAssertion>
		: AnalysisComponentTestBase<TAssertion, IAnalyzer, IAnalyzers, AnalyzersDescriptor>
			, IAnalyzerAssertion
		where TAssertion : AnalyzerAssertionBase<TAssertion>, new()
	{
		protected override IAnalysis FluentAnalysis(AnalysisDescriptor an) =>
			an.Analyzers(d => AssertionSetup.Fluent(AssertionSetup.Name, d));

		protected override Nest.Analysis InitializerAnalysis() =>
			new Nest.Analysis { Analyzers = new Nest.Analyzers { { AssertionSetup.Name, AssertionSetup.Initializer } } };

		protected override object AnalysisJson => new
		{
			analyzer = new Dictionary<string, object> { { AssertionSetup.Name, AssertionSetup.Json } }
		};

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[U] public override Task TestPutSettingsRequest() => base.TestPutSettingsRequest();

		[I] public override Task TestPutSettingsResponse() => base.TestPutSettingsResponse();
	}
}
