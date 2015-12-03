using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPhraseSuggestCollate
	{
		[JsonProperty(PropertyName = "query")]
		QueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeJsonConverter<QueryContainer>, CustomJsonConverter>))]
		QueryContainer Filter { get; set; }

		[JsonProperty(PropertyName = "params")]
		IDictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "preference")]
		string Preference { get; set; }
	}

	public class PhraseSuggestCollate : IPhraseSuggestCollate
	{
		public QueryContainer Query { get; set; }

		public QueryContainer Filter { get; set; }

		public IDictionary<string, object> Params { get; set; }

		public string Preference { get; set; }
	}

	public class PhraseSuggestCollateDescriptor<T> : DescriptorBase<PhraseSuggestCollateDescriptor<T>, IPhraseSuggestCollate>, IPhraseSuggestCollate
		where T : class
	{
		QueryContainer IPhraseSuggestCollate.Query { get; set; }

		QueryContainer IPhraseSuggestCollate.Filter { get; set; }

		IDictionary<string, object> IPhraseSuggestCollate.Params { get; set; }

		string IPhraseSuggestCollate.Preference { get; set; }

		public PhraseSuggestCollateDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> query) => 
			Assign(a => a.Query = query?.InvokeQuery(new QueryContainerDescriptor<T>()));

		public PhraseSuggestCollateDescriptor<T> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> filter) =>
			Assign(a => a.Filter = filter?.InvokeQuery(new QueryContainerDescriptor<T>()));

		public PhraseSuggestCollateDescriptor<T> Params(IDictionary<string, object> paramsDictionary) => Assign(a => a.Params = paramsDictionary);

		public PhraseSuggestCollateDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary) =>
			Assign(a => a.Params = paramsDictionary(new FluentDictionary<string, object>()));

		public PhraseSuggestCollateDescriptor<T> Preference(string preference) => Assign(a => a.Preference = preference);
	}

}
