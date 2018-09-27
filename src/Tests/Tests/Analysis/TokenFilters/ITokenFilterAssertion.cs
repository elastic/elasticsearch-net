using System;
using Nest;

namespace Tests.Analysis.TokenFilters
{

	public interface ITokenFilterAssertion : IAnalysisAssertion
	{
		ITokenFilter Initializer { get; }
		Func<string, TokenFiltersDescriptor, IPromise<ITokenFilters>> Fluent { get; }
	}
}
