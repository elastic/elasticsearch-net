using System;
using Nest;

namespace Tests.Analysis.Tokenizers
{

	public interface ICharFilterAssertion : IAnalysisAssertion
	{
		ICharFilter Initializer { get; }
		Func<string, CharFiltersDescriptor, IPromise<ICharFilters>> Fluent { get; }
	}
}
