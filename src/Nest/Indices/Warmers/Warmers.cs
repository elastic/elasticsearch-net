using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<Warmers, TypeName, IWarmer>))]
	public interface IWarmers : IIsADictionary<TypeName, IWarmer> { }

	public class Warmers : IsADictionaryBase<TypeName, IWarmer>, IWarmers
	{
		public Warmers() : base() { }
		public Warmers(IDictionary<TypeName, IWarmer> container) : base(container) { }
		public Warmers(Dictionary<TypeName, IWarmer> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		public void Add(TypeName type, IWarmer mapping) => BackingDictionary.Add(type, mapping);
	}
	
	public class WarmersDescriptor : IsADictionaryDescriptorBase<WarmersDescriptor, IWarmers, TypeName, IWarmer>
	{
		public WarmersDescriptor() : base(new Warmers()) { }

		public WarmersDescriptor Warm<T>(string warmerName, Func<WarmerDescriptor<T>, IWarmer> selector) where T : class =>
			Assign(warmerName, selector?.Invoke(new WarmerDescriptor<T>()));
	}
}