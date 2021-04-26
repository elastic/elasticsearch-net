/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
