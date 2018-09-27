using System;
using Nest;

namespace Tests.Analysis.Tokenizers
{

	public interface INormalizerAssertion : IAnalysisAssertion
	{
		INormalizer Initializer { get; }
		Func<string, NormalizersDescriptor, IPromise<INormalizers>> Fluent { get; }
	}
}
