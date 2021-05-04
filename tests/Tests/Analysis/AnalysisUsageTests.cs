// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Analysis.Analyzers;
using Tests.Analysis.CharFilters;
using Tests.Analysis.Normalizers;
using Tests.Analysis.TokenFilters;
using Tests.Analysis.Tokenizers;
using Tests.Core.Client;

namespace Tests.Analysis
{
	public class AnalysisUsageTestsTests
	{
		[U] public static void CollectionsShouldNotBeEmpty()
		{
			var analyzers = AnalysisUsageTests.AnalyzersInitializer.Analysis.Analyzers;
			var charFilters = AnalysisUsageTests.CharFiltersInitializer.Analysis.CharFilters;
			var tokenizers = AnalysisUsageTests.TokenizersInitializer.Analysis.Tokenizers;
			var tokenFilters = AnalysisUsageTests.TokenFiltersInitializer.Analysis.TokenFilters;

			analyzers.Should().NotBeNull().And.NotBeEmpty();
			charFilters.Should().NotBeNull().And.NotBeEmpty();
			tokenizers.Should().NotBeNull().And.NotBeEmpty();
			tokenFilters.Should().NotBeNull().And.NotBeEmpty();
		}
	}

	public static class AnalysisUsageTests
	{
		public static IndexSettings AnalyzersFluent =>
			Fluent<AnalyzersDescriptor, IAnalyzerAssertion, IAnalyzers>(i => i.Fluent, (a, v) => a.Analyzers = v.Value);

		public static IndexSettings AnalyzersInitializer =>
			Init<Nest.Analyzers, IAnalyzerAssertion, IAnalyzer>(i => i.Initializer, (a, v) => a.Analyzers = v);

		public static IndexSettings CharFiltersFluent =>
			Fluent<CharFiltersDescriptor, ICharFilterAssertion, ICharFilters>(i => i.Fluent, (a, v) => a.CharFilters = v.Value);

		public static IndexSettings CharFiltersInitializer =>
			Init<Nest.CharFilters, ICharFilterAssertion, ICharFilter>(i => i.Initializer, (a, v) => a.CharFilters = v);

		public static IndexSettings NormalizersFluent =>
			Fluent<NormalizersDescriptor, INormalizerAssertion, INormalizers>(i => i.Fluent, (a, v) => a.Normalizers = v.Value);

		public static IndexSettings NormalizersInitializer =>
			Init<Nest.Normalizers, INormalizerAssertion, INormalizer>(i => i.Initializer, (a, v) => a.Normalizers = v);

		public static IndexSettings TokenFiltersFluent =>
			Fluent<TokenFiltersDescriptor, ITokenFilterAssertion, ITokenFilters>(i => i.Fluent, (a, v) => a.TokenFilters = v.Value);

		public static IndexSettings TokenFiltersInitializer =>
			Init<Nest.TokenFilters, ITokenFilterAssertion, ITokenFilter>(i => i.Initializer, (a, v) => a.TokenFilters = v);

		public static IndexSettings TokenizersFluent =>
			Fluent<TokenizersDescriptor, ITokenizerAssertion, ITokenizers>(i => i.Fluent, (a, v) => a.Tokenizers = v.Value);

		public static IndexSettings TokenizersInitializer =>
			Init<Nest.Tokenizers, ITokenizerAssertion, ITokenizer>(i => i.Initializer, (a, v) => a.Tokenizers = v);

		private static IndexSettings Fluent<TContainer, TAssertion, TValue>(Func<TAssertion, Func<string, TContainer, IPromise<TValue>>> fluent,
			Action<Nest.Analysis, IPromise<TValue>> set
		)
			where TAssertion : IAnalysisAssertion
			where TContainer : IPromise<TValue>, new()
			where TValue : class => Wrap(an => set(an, Apply<TContainer, TAssertion>((t, a) => fluent(a)(a.Name, t))));

		private static IndexSettings Init<TContainer, TAssertion, TInitializer>(Func<TAssertion, TInitializer> value,
			Action<Nest.Analysis, TContainer> set
		)
			where TAssertion : IAnalysisAssertion
			where TContainer : IDictionary<string, TInitializer>, new() =>
			Wrap(an => set(an, Apply<TContainer, TAssertion>((t, a) => t[a.Name] = value(a))));

		private static TContainer Apply<TContainer, TAssertion>(Action<TContainer, TAssertion> act)
			where TAssertion : IAnalysisAssertion
			where TContainer : new() => All<TAssertion>()
			.Aggregate(new TContainer(), (t, a) =>
			{
				act(t, a);
				return t;
			}, t => t);

		private static IndexSettings Wrap(Action<Nest.Analysis> set)
		{
			var a = new Nest.Analysis();
			var s = new IndexSettings { Analysis = a };
			set(a);
			return s;
		}

		private static List<TAssertion> All<TAssertion>()
			where TAssertion : IAnalysisAssertion
		{
			var assertions = typeof(TokenizerTests).GetNestedTypes()
				.Union(typeof(TokenFilterTests).GetNestedTypes())
				.Union(typeof(NormalizerTests).GetNestedTypes())
				.Union(typeof(AnalyzerTests).GetNestedTypes())
				.Union(typeof(CharFilterTests).GetNestedTypes())
				.ToList();

			var nestedTypes = assertions
				.Where(t => typeof(TAssertion).IsAssignableFrom(t) && t.IsClass)
				.ToList();

			var types = nestedTypes
				.Select(t => new
				{
					t,
					a = t.GetCustomAttributes(typeof(SkipVersionAttribute)).FirstOrDefault() as SkipVersionAttribute
				})
				.Where(@t1 => @t1.a == null || !@t1.a.Ranges.Any(r => r.IsSatisfied(TestClient.Configuration.ElasticsearchVersion)))
				.Select(@t1 => (TAssertion)Activator.CreateInstance(@t1.t));
			return types.ToList();
		}
	}
}
