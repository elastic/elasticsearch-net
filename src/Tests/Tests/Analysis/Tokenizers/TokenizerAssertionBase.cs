using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Tests.Analysis.Tokenizers
{
	public interface ITokenizerAssertion : IAnalysisAssertion<ITokenizer, ITokenizers, TokenizersDescriptor> { }

	public abstract class TokenizerAssertionBase<TAssertion>
		: AnalysisComponentTestBase<TAssertion, ITokenizer, ITokenizers, TokenizersDescriptor>
			, ITokenizerAssertion
		where TAssertion : TokenizerAssertionBase<TAssertion>, new()
	{
		protected override IAnalysis FluentAnalysis(AnalysisDescriptor an) =>
			an.Tokenizers(d => AssertionSetup.Fluent(AssertionSetup.Name, d));

		protected override Nest.Analysis InitializerAnalysis() =>
			new Nest.Analysis {Tokenizers = new Nest.Tokenizers {{AssertionSetup.Name, AssertionSetup.Initializer}}};

		protected override object AnalysisJson => new
		{
			tokenizer = new Dictionary<string, object> { {AssertionSetup.Name, AssertionSetup.Json} }
		};

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[U] public override Task TestPutSettingsRequest() => base.TestPutSettingsRequest();
		[I] public override Task TestPutSettingsResponse() => base.TestPutSettingsResponse();
	}

}
