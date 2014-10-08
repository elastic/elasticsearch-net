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
		IQueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		IFilterContainer Filter { get; set; }

		[JsonProperty(PropertyName = "params")]
		IDictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "preference")]
		string Preference { get; set; }
	}

	public class PhraseSuggestCollate : IPhraseSuggestCollate
	{
		public IQueryContainer Query { get; set; }

		public IFilterContainer Filter { get; set; }

		public IDictionary<string, object> Params { get; set; }

		public string Preference { get; set; }
	}

	public class PhraseSuggestCollateDescriptor<T> : IPhraseSuggestCollate
		where T : class
	{
		internal IPhraseSuggestCollate Collate = new PhraseSuggestCollate();

		IQueryContainer IPhraseSuggestCollate.Query { get; set; }

		IFilterContainer IPhraseSuggestCollate.Filter { get; set; }

		IDictionary<string, object> IPhraseSuggestCollate.Params { get; set; }

		string IPhraseSuggestCollate.Preference { get; set; }

		public PhraseSuggestCollateDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> query)
		{
			this.Collate.Query = query(new QueryDescriptor<T>());
			return this;
		}

		public PhraseSuggestCollateDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filter)
		{
			this.Collate.Filter = filter(new FilterDescriptor<T>());
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
