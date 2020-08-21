// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Tests.Analysis.Normalizers
{
	public interface INormalizerAssertion : IAnalysisAssertion<INormalizer, INormalizers, NormalizersDescriptor> { }

	public abstract class NormalizerAssertionBase<TAssertion>
		: AnalysisComponentTestBase<TAssertion, INormalizer, INormalizers, NormalizersDescriptor>
			, INormalizerAssertion
		where TAssertion : NormalizerAssertionBase<TAssertion>, new()
	{
		protected override object AnalysisJson => new
		{
			normalizer = new Dictionary<string, object> { { AssertionSetup.Name, AssertionSetup.Json } }
		};

		protected override IAnalysis FluentAnalysis(AnalysisDescriptor an) =>
			an.Normalizers(d => AssertionSetup.Fluent(AssertionSetup.Name, d));

		protected override Nest.Analysis InitializerAnalysis() =>
			new Nest.Analysis { Normalizers = new Nest.Normalizers { { AssertionSetup.Name, AssertionSetup.Initializer } } };

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[U] public override Task TestPutSettingsRequest() => base.TestPutSettingsRequest();

		[I] public override Task TestPutSettingsResponse() => base.TestPutSettingsResponse();
	}
}
