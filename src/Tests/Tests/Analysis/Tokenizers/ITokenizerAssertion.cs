using System;
using Nest;

namespace Tests.Analysis.Tokenizers
{

	public interface ITokenizerAssertion : IAnalysisAssertion
	{
		ITokenizer Initializer { get; }
		Func<string, TokenizersDescriptor, IPromise<ITokenizers>> Fluent { get; }
	}
}
