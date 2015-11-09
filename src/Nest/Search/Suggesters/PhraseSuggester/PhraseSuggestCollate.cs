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

	public class PhraseSuggestCollateDescriptor<T> : IPhraseSuggestCollate
		where T : class
	{
		internal IPhraseSuggestCollate Collate = new PhraseSuggestCollate();

		QueryContainer IPhraseSuggestCollate.Query { get; set; }

		QueryContainer IPhraseSuggestCollate.Filter { get; set; }

		IDictionary<string, object> IPhraseSuggestCollate.Params { get; set; }

		string IPhraseSuggestCollate.Preference { get; set; }

		public PhraseSuggestCollateDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> query)
		{
			this.Collate.Query = query(new QueryContainerDescriptor<T>());
			return this;
		}

		public PhraseSuggestCollateDescriptor<T> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> filter)
		{
			this.Collate.Filter = filter(new QueryContainerDescriptor<T>());
			return this;
		}

		public PhraseSuggestCollateDescriptor<T> Params(IDictionary<string, object> paramsDictionary)
		{
			this.Collate.Params = paramsDictionary;
			return this;
		}

		public PhraseSuggestCollateDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary)
		{
			this.Collate.Params = paramsDictionary(new FluentDictionary<string, object>());
			return this;
		}

		public PhraseSuggestCollateDescriptor<T> Preference(string preference)
		{
			this.Collate.Preference = preference;
			return this;
		}
	}

}
