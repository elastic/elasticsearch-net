using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class SuggestContextMappingDescriptor<T>
		where T : class
	{
		internal IDictionary<string, ISuggestContext> _Contexts = new Dictionary<string, ISuggestContext>();

		public SuggestContextMappingDescriptor<T> Category(string name, Func<CategorySuggestDescriptor<T>, CategorySuggestDescriptor<T>> categoryDescriptor)
		{
			var selector = categoryDescriptor(new CategorySuggestDescriptor<T>());
			var context = selector._Context;
			AddContext(name, context);
			return this;
		}

		public SuggestContextMappingDescriptor<T> GeoLocation(string name, Func<GeoLocationSuggestDescriptor<T>, GeoLocationSuggestDescriptor<T>> geoLocationDescriptor)
		{
			var selector = geoLocationDescriptor(new GeoLocationSuggestDescriptor<T>());
			var context = selector._Context;
			AddContext(name, context);
			return this;
		}

		private void AddContext(string key, ISuggestContext context)
		{
			if (_Contexts.ContainsKey(key))
				_Contexts[key] = context;
			else
				_Contexts.Add(key, context);
		}
	}
}
