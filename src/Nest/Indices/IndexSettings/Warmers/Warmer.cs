using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IWarmer
	{
		[JsonProperty("types")]
		IEnumerable<TypeName> Types { get; set; }

		[JsonProperty("source")]
		ISearchRequest Source { get; set; }
	}

	[JsonObject]
	public class Warmer
	{
		public IEnumerable<TypeName> Types { get; set; }

		public ISearchRequest Source { get; set; }
	}
	
	//TODO discuss with @gmarz wether this should be typed or not
	public class WarmerDescriptor<T> : DescriptorBase<WarmerDescriptor<T>, IWarmer>, IWarmer
		where T : class
	{
		IEnumerable<TypeName> IWarmer.Types { get; set; }
		ISearchRequest IWarmer.Source { get; set; }

		public WarmerDescriptor() { Assign(a => a.Types = new TypeName[] { typeof(T) }); }

		public WarmerDescriptor<T> AllTypes() => Assign(a => a.Types = null);

		public WarmerDescriptor<T> Types(IEnumerable<TypeName> types) => Assign(a => a.Types = types);

		public WarmerDescriptor<T> Types(params TypeName[] types) => Assign(a => a.Types = types);

		public WarmerDescriptor<T> Source(Func<SearchDescriptor<T>, ISearchRequest> selector) =>
			Assign(a => a.Source = selector?.Invoke(new SearchDescriptor<T>()));

	}
}