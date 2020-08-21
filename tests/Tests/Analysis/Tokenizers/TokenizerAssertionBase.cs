// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Tests.Analysis.Tokenizers
{
	public interface ITokenizerAssertion : IAnalysisAssertion<ITokenizer, ITokenizers, TokenizersDescriptor> { }

	public abstract class TokenizerAssertionBase<TAssertion>
		: AnalysisComponentTestBase<TAssertion, ITokenizer, ITokenizers, TokenizersDescriptor>
			, ITokenizerAssertion
		where TAssertion : TokenizerAssertionBase<TAssertion>, new()
	{
		protected override object AnalysisJson => new
		{
			tokenizer = new Dictionary<string, object> { { AssertionSetup.Name, AssertionSetup.Json } }
		};

		protected override IAnalysis FluentAnalysis(AnalysisDescriptor an) =>
			an.Tokenizers(d => AssertionSetup.Fluent(AssertionSetup.Name, d));

		protected override Nest.Analysis InitializerAnalysis() =>
			new Nest.Analysis { Tokenizers = new Nest.Tokenizers { { AssertionSetup.Name, AssertionSetup.Initializer } } };

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[U] public override Task TestPutSettingsRequest() => base.TestPutSettingsRequest();

		[I] public override Task TestPutSettingsResponse() => base.TestPutSettingsResponse();
	}
}
