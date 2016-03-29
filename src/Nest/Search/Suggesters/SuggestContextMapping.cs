using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{

	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<SuggestContextMapping, string, ISuggestContext>))]
	public interface ISuggestContextMapping : IIsADictionary<string, ISuggestContext>
	{
	}

	public class SuggestContextMapping: IsADictionaryBase<string, ISuggestContext>, ISuggestContextMapping
	{
		public SuggestContextMapping() : base() { }
		public SuggestContextMapping(IDictionary<string, ISuggestContext> container) : base(container) { }

		public void Add(string name, ISuggestContext filter) => BackingDictionary.Add(name, filter);
	}


	public class SuggestContextMappingDescriptor<T> : IsADictionaryDescriptorBase<SuggestContextMappingDescriptor<T>, ISuggestContextMapping, string, ISuggestContext>
		where T : class
	{
		public SuggestContextMappingDescriptor() : base(new SuggestContextMapping()) { }

		public SuggestContextMappingDescriptor<T> Category(string name, Func<CategorySuggestContextDescriptor<T>, ICategorySuggestContext> categoryDescriptor) =>
			Assign(name ,categoryDescriptor(new CategorySuggestContextDescriptor<T>()));

		public SuggestContextMappingDescriptor<T> GeoLocation(string name, Func<GeoLocationSuggestContextDescriptor<T>, IGeoLocationSuggestContext> geoLocationDescriptor) =>
			Assign(name, geoLocationDescriptor(new GeoLocationSuggestContextDescriptor<T>()));

	}
}
