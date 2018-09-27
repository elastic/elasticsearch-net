using System;
using Nest;

namespace Tests.Analysis.Analyzers
{

	public interface IAnalyzerAssertion : IAnalysisAssertion
	{
		IAnalyzer Initializer { get; }
		Func<string, AnalyzersDescriptor, IPromise<IAnalyzers>> Fluent { get; }
	}
}
