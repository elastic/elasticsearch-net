using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IWarmers : IWrapDictionary { }
	public class Warmers : ProxyDictionary<TypeName, IWarmer>, IWarmers
	{
		public Warmers() : base() { }
		public Warmers(IDictionary<TypeName, IWarmer> container) : base(container) { }
		public Warmers(Dictionary<TypeName, IWarmer> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		/// <summary>
		/// Add any setting to the index
		/// </summary>
		public void Add(TypeName type, IWarmer mapping) => _backingDictionary.Add(type, mapping);
	}
	
	public class WarmersDescriptor : WrapDictionaryDescriptor<WarmersDescriptor, TypeName, IWarmer>, IWarmers
	{
		public WarmersDescriptor Warm<T>(string warmerName, Func<WarmerDescriptor<T>, IWarmer> selector) where T : class
		{
			_backingDictionary.Add(warmerName, selector?.Invoke(new WarmerDescriptor<T>()));
			return this;
		}
	}

}