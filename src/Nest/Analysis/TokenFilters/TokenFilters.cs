using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface ITokenFilters : IWrapDictionary { }

	public class TokenFilters : WrapDictionary<string, ICharFilter>, ITokenFilters
	{
		public TokenFilters() : base() { }
		public TokenFilters(IDictionary<string, ICharFilter> container) : base(container) { }
		public TokenFilters(Dictionary<string, ICharFilter> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(string name, ICharFilter analyzer) => _backingDictionary.Add(name, analyzer);
	}

	public class TokenFiltersDescriptor : WrapDictionary<string, ICharFilter>, ITokenFilters
	{
		protected TokenFiltersDescriptor Assign(string name, ICharFilter analyzer) =>
			Fluent.Assign<TokenFiltersDescriptor, TokenFiltersDescriptor>(this, (a) => _backingDictionary.Add(name, analyzer));

		public TokenFiltersDescriptor Add(string name, ICharFilter analyzer) => Assign(name, analyzer);

		public TokenFiltersDescriptor PatternReplace(string name, Func<PatternReplaceCharFilterDescriptor, IPatternReplaceCharFilter> selector) =>
			Assign(name, selector?.Invoke(new PatternReplaceCharFilterDescriptor()));

		public TokenFiltersDescriptor HtmlStrip(string name, Func<HtmlStripCharFilterDescriptor, IHtmlStripCharFilter> selector = null) =>
			Assign(name, selector?.InvokeOrDefault(new HtmlStripCharFilterDescriptor()));

		public TokenFiltersDescriptor Mapping(string name, Func<MappingCharFilterDescriptor, IMappingCharFilter> selector) =>
			Assign(name, selector?.Invoke(new MappingCharFilterDescriptor()));

	}
}
