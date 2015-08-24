using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface ICharFilters : IWrapDictionary { }

	public class CharFilters : WrapDictionary<string, ICharFilter>, ICharFilters
	{
		public CharFilters() : base() { }
		public CharFilters(IDictionary<string, ICharFilter> container) : base(container) { }
		public CharFilters(Dictionary<string, ICharFilter> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(string name, ICharFilter analyzer) => _backingDictionary.Add(name, analyzer);
	}

	public class CharFiltersDescriptor : WrapDictionary<string, ICharFilter>, ICharFilters
	{
		protected CharFiltersDescriptor Assign(string name, ICharFilter analyzer) =>
			Fluent.Assign<CharFiltersDescriptor, CharFiltersDescriptor>(this, (a) => _backingDictionary.Add(name, analyzer));

		public CharFiltersDescriptor Add(string name, ICharFilter analyzer) => Assign(name, analyzer);

		public CharFiltersDescriptor PatternReplace(string name, Func<PatternReplaceCharFilterDescriptor, IPatternReplaceCharFilter> selector) =>
			Assign(name, selector?.Invoke(new PatternReplaceCharFilterDescriptor()));

		public CharFiltersDescriptor HtmlStrip(string name, Func<HtmlStripCharFilterDescriptor, IHtmlStripCharFilter> selector = null) =>
			Assign(name, selector?.InvokeOrDefault(new HtmlStripCharFilterDescriptor()));

		public CharFiltersDescriptor Mapping(string name, Func<MappingCharFilterDescriptor, IMappingCharFilter> selector) =>
			Assign(name, selector?.Invoke(new MappingCharFilterDescriptor()));

	}
}
