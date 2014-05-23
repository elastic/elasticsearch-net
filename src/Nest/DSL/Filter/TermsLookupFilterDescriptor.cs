using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Nest.Resolvers.Converters.Filters;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITermsLookupFilter : ITermsBaseFilter
	{
		[JsonProperty("id")]
		string Id { get; set; }

		[JsonProperty("type")]
		TypeNameMarker Type { get; set; }

		[JsonProperty("index")]
		IndexNameMarker Index { get; set; }

		[JsonProperty("path")]
		PropertyPathMarker Path { get; set; }

		[JsonProperty("routing")]
		string Routing { get; set; }

		[JsonProperty("cache")]
		bool? CacheLookup { get; set; }
	}

	public class TermsLookupFilter : PlainFilter, ITermsLookupFilter
	{
		protected override void WrapInContainer(IFilterContainer container)
		{
			container.Terms = this;
		}

		public PropertyPathMarker Field { get; set; }
		public TermsExecution? Execution { get; set; }
	

		public string Id { get; set; }
		public TypeNameMarker Type { get; set; }
		public IndexNameMarker Index { get; set; }
		public PropertyPathMarker Path { get; set; }
		public string Routing { get; set; }
		public bool? CacheLookup { get; set; }
	}

	public class TermsLookupFilterDescriptor : FilterBase, ITermsLookupFilter
	{
		PropertyPathMarker ITermsBaseFilter.Field { get; set; }
		bool IFilter.IsConditionless
		{
			get
			{
				return ((ITermsLookupFilter)this).Type == null || ((ITermsLookupFilter)this).Index == null || ((ITermsLookupFilter)this).Id.IsNullOrEmpty() 
					|| ((ITermsLookupFilter)this).Path.IsConditionless();
			}
		}

		string ITermsLookupFilter.Id { get; set; }

		TypeNameMarker ITermsLookupFilter.Type { get; set; }

		IndexNameMarker ITermsLookupFilter.Index { get; set; }

		PropertyPathMarker ITermsLookupFilter.Path { get; set; }

		string ITermsLookupFilter.Routing { get; set; }
		
		TermsExecution? ITermsBaseFilter.Execution { get; set; }

		bool? ITermsLookupFilter.CacheLookup { get; set; }
		
		public TermsLookupFilterDescriptor Lookup<T>(string field, string id, string index = null, string type = null)
		{
			return _Lookup<T>(field, id, index, type);
		}

		private TermsLookupFilterDescriptor _Lookup<T>(PropertyPathMarker field, string id, string index, string type)
		{
			((ITermsLookupFilter)this).Path = field;
			((ITermsLookupFilter)this).Id = id;
			((ITermsLookupFilter)this).Type = type ?? new TypeNameMarker {Type = typeof (T)};
			((ITermsLookupFilter)this).Index = index ?? new IndexNameMarker {Type = typeof (T)};
			return this;
		}

		public TermsLookupFilterDescriptor Lookup<T>(Expression<Func<T, object>> field, string id, string index = null, string type = null)
		{
			return _Lookup<T>(field, id, index, type);
		}

		/// <summary>
		/// If not specified will use the default typename for the type specified on Lookup&lt;T&gt;
		/// </summary>
		public TermsLookupFilterDescriptor Type(string type)
		{
			((ITermsLookupFilter)this).Type = type;
			return this;
		}

		/// <summary>
		/// If not specified will use the default index for the type specified on Lookup&lt;T&gt;
		/// </summary>
		public TermsLookupFilterDescriptor Index(string index)
		{
			((ITermsLookupFilter)this).Index = index;
			return this;
		}

		/// <summary>
		/// Whether to cache the filter built from the retrieved document (true - default) or whether to fetch and 
		/// rebuild the filter on every request (false).
		/// </summary>
		public TermsLookupFilterDescriptor CacheLookup(bool cache = true)
		{
			((ITermsLookupFilter)this).CacheLookup = cache;
			return this;
		}
		/// <summary>
		/// A custom routing value to be used when retrieving the external terms doc.
		/// </summary>
		public TermsLookupFilterDescriptor Routing(string routing)
		{
			((ITermsLookupFilter)this).Routing = routing;
			return this;
		}
		
		/// <summary>
		/// The way terms filter executes is by iterating over the terms provided and 
		/// finding matches docs (loading into a bitset) and caching it. Sometimes, 
		/// we want a different execution model that can still be achieved by building more complex 
		/// queries in the DSL, but we can support them in the more compact model that terms filter provides.
		/// </summary>
		public TermsLookupFilterDescriptor Routing(TermsExecution execution)
		{
			((ITermsLookupFilter)this).Execution = execution;
			return this;
		}
	}

}
