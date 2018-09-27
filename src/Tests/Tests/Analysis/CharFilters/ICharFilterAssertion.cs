using System;
using Nest;

namespace Tests.Analysis.CharFilters
{

	public interface ICharFilterAssertion : IAnalysisAssertion
	{
		ICharFilter Initializer { get; }
		Func<string, CharFiltersDescriptor, IPromise<ICharFilters>> Fluent { get; }
	}
}
