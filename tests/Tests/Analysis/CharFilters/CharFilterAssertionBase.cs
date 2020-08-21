// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Tests.Analysis.CharFilters
{
	public interface ICharFilterAssertion : IAnalysisAssertion<ICharFilter, ICharFilters, CharFiltersDescriptor> { }

	public abstract class CharFilterAssertionBase<TAssertion>
		: AnalysisComponentTestBase<TAssertion, ICharFilter, ICharFilters, CharFiltersDescriptor>
			, ICharFilterAssertion
		where TAssertion : CharFilterAssertionBase<TAssertion>, new()
	{
		protected override object AnalysisJson => new
		{
			char_filter = new Dictionary<string, object> { { AssertionSetup.Name, AssertionSetup.Json } }
		};

		protected override IAnalysis FluentAnalysis(AnalysisDescriptor an) =>
			an.CharFilters(d => AssertionSetup.Fluent(AssertionSetup.Name, d));

		protected override Nest.Analysis InitializerAnalysis() =>
			new Nest.Analysis { CharFilters = new Nest.CharFilters { { AssertionSetup.Name, AssertionSetup.Initializer } } };

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[U] public override Task TestPutSettingsRequest() => base.TestPutSettingsRequest();

		[I] public override Task TestPutSettingsResponse() => base.TestPutSettingsResponse();
	}
}
