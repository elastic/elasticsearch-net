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
		QueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "params")]
		IDictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "prune")]
		bool? Prune { get; set; }
	}

	public class PhraseSuggestCollate : IPhraseSuggestCollate
	{
		public QueryContainer Query { get; set; }

		public IDictionary<string, object> Params { get; set; }

		public bool? Prune { get; set; }
	}

	public class PhraseSuggestCollateDescriptor<T> : DescriptorBase<PhraseSuggestCollateDescriptor<T>, IPhraseSuggestCollate>, IPhraseSuggestCollate
		where T : class
	{
		QueryContainer IPhraseSuggestCollate.Query { get; set; }

		IDictionary<string, object> IPhraseSuggestCollate.Params { get; set; }

		bool? IPhraseSuggestCollate.Prune { get; set; }

		public PhraseSuggestCollateDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> query) => 
			Assign(a => a.Query = query?.InvokeQuery(new QueryContainerDescriptor<T>()));

		public PhraseSuggestCollateDescriptor<T> Params(IDictionary<string, object> paramsDictionary) => Assign(a => a.Params = paramsDictionary);

		public PhraseSuggestCollateDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary) =>
			Assign(a => a.Params = paramsDictionary(new FluentDictionary<string, object>()));

		public PhraseSuggestCollateDescriptor<T> Prune(bool? prune = true) => Assign(a => a.Prune = prune);
	}

}
