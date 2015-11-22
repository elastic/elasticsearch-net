using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeJsonConverter<NamedFiltersContainer>, VerbatimDictionaryKeysJsonConverter>))]
	public interface INamedFiltersContainer
	{
	}

	[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeJsonConverter<NamedFiltersContainer>, VerbatimDictionaryKeysJsonConverter>))]
	public abstract class NamedFiltersContainerBase : IsADictionary<string, IQueryContainer>, INamedFiltersContainer
	{
		protected NamedFiltersContainerBase () : base() { }
		protected NamedFiltersContainerBase(IDictionary<string, IQueryContainer> container) : base(container) { }

		public static implicit operator NamedFiltersContainerBase(Dictionary<string, IQueryContainer> container) =>
			new NamedFiltersContainer(container);

		public static implicit operator NamedFiltersContainerBase(Dictionary<string, QueryContainer> container) =>
			new NamedFiltersContainer(container);
	}

	public class NamedFiltersContainer: NamedFiltersContainerBase
	{
		public NamedFiltersContainer() : base() { }
		public NamedFiltersContainer(IDictionary<string, IQueryContainer> container) : base(container) { }
		public NamedFiltersContainer(Dictionary<string, QueryContainer> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => (IQueryContainer)kv.Value))
		{ }

		public void Add(string name, IQueryContainer filter) => BackingDictionary.Add(name, filter);
		public void Add(string name, QueryContainer filter) => BackingDictionary.Add(name, filter);
	}

	public class NamedFiltersContainerDescriptor<T>: NamedFiltersContainerBase
		where T : class
	{
		public NamedFiltersContainerDescriptor() : base() { }
		protected NamedFiltersContainerDescriptor(IDictionary<string, IQueryContainer> container) : base(container) { }

		public NamedFiltersContainerDescriptor<T> Filter(string name, IQueryContainer filter)
		{
			 BackingDictionary.Add(name, filter);
			return this;
		}

		public NamedFiltersContainerDescriptor<T> Filter(string name, Func<QueryContainerDescriptor<T>, IQueryContainer> selector)
		{
			var filter = selector?.Invoke(new QueryContainerDescriptor<T>());
			if (filter != null) BackingDictionary.Add(name, filter);
			return this;
		}

		public NamedFiltersContainerDescriptor<T> Filter<TOther>(string name, Func<QueryContainerDescriptor<TOther>, IQueryContainer> selector)
			where TOther : class
		{
			var filter = selector?.Invoke(new QueryContainerDescriptor<TOther>());
			if (filter != null) BackingDictionary.Add(name, filter);
			return this;
		}
	}

}
