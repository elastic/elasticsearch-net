using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Analysis.TokenFilters;
using Tests.Core.Client;
using Tests.Search;

namespace Tests.Analysis.Tokenizers
{
	public static class AnalysisUsageTests
	{
		public static IndexSettings TokenizersFluent => Fluent<TokenizersDescriptor, ITokenizerAssertion, ITokenizers>(i => i.Fluent, (a, v) => a.Tokenizers = v.Value);

		public static IndexSettings TokenFiltersFluent => Fluent<TokenFiltersDescriptor, ITokenFilterAssertion, ITokenFilters>(i => i.Fluent, (a, v) => a.TokenFilters = v.Value);

		public static IndexSettings TokenizersInitializer => Init<Nest.Tokenizers, ITokenizerAssertion, ITokenizer>(i => i.Initializer, (a, v) => a.Tokenizers = v);

		public static IndexSettings TokenFiltersInitializer => Init<Nest.TokenFilters, ITokenFilterAssertion, ITokenFilter>(i => i.Initializer, (a, v) => a.TokenFilters = v);

		private static IndexSettings Fluent<TContainer, TAssertion, TValue>(Func<TAssertion, Func<string, TContainer, IPromise<TValue>>> fluent, Action<Nest.Analysis, IPromise<TValue>> set)
			where TAssertion : IAnalysisAssertion
			where TContainer : IPromise<TValue>, new()
			where TValue : class => Wrap(an => set(an, Apply<TContainer, TAssertion>((t, a) => fluent(a)(a.Name, t))));

		private static IndexSettings Init<TContainer, TAssertion, TInitializer>(Func<TAssertion, TInitializer> value, Action<Nest.Analysis, TContainer> set)
			where TAssertion : IAnalysisAssertion
			where TContainer : IDictionary<string, TInitializer>, new() => Wrap(an => set(an, Apply<TContainer, TAssertion>((t, a) => t[a.Name] = value(a))));

		private static TContainer Apply<TContainer, TAssertion>(Action<TContainer, TAssertion> act)
			where TAssertion : IAnalysisAssertion
			where TContainer : new() => All<TAssertion>().Aggregate(new TContainer() , (t,a) => { act(t,a); return t; }, t=>t);

		private static IndexSettings Wrap(Action<Nest.Analysis> set)
		{
			var a = new Nest.Analysis();
			var s =new IndexSettings { Analysis = a };
			set(a);
			return s;
		}

		private static List<TAssertion> All<TAssertion>()
			where TAssertion : IAnalysisAssertion
		{
			var types =
				from t in typeof(TokenizerTests).GetNestedTypes()
				where typeof(TAssertion).IsAssignableFrom(t) && t.IsClass
				let a = t.GetCustomAttributes(typeof(SkipVersionAttribute)).FirstOrDefault() as SkipVersionAttribute
				where a != null && !a.Ranges.Any(r=>r.IsSatisfied(TestClient.Configuration.ElasticsearchVersion))
				select (TAssertion) Activator.CreateInstance(t);
			return types.ToList();
		}


	}
}
