using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PhraseSuggestCollate>))]
	public interface IPhraseSuggestCollate
	{
		[JsonProperty(PropertyName = "query")]
		ITemplateQuery Query { get; set; }

		[JsonProperty(PropertyName = "prune")]
		bool? Prune { get; set; }
	}

	public class PhraseSuggestCollate : IPhraseSuggestCollate
	{
		public ITemplateQuery Query { get; set; }

		public bool? Prune { get; set; }
	}

	public class PhraseSuggestCollateDescriptor<T> : DescriptorBase<PhraseSuggestCollateDescriptor<T>, IPhraseSuggestCollate>, IPhraseSuggestCollate
		where T : class
	{
		ITemplateQuery IPhraseSuggestCollate.Query { get; set; }

		bool? IPhraseSuggestCollate.Prune { get; set; }

		public PhraseSuggestCollateDescriptor<T> Query(Func<TemplateQueryDescriptor<T>, ITemplateQuery> selector) =>
			Assign(a => a.Query = selector?.Invoke(new TemplateQueryDescriptor<T>()));

		public PhraseSuggestCollateDescriptor<T> Prune(bool? prune = true) => Assign(a => a.Prune = prune);
	}
}
