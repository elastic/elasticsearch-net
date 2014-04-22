using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	public interface ITermsBaseFilter : IFilterBase
	{
		PropertyPathMarker Field { get; set; }
	}

	public interface ITermsFilter : ITermsBaseFilter
	{
		IEnumerable<object> Terms { get; set; }
	}
	
	public class TermsFilter : FilterBase, ITermsFilter
	{
		bool IFilterBase.IsConditionless { get { return ((ITermsBaseFilter)this).Field.IsConditionless() || !((ITermsFilter)this).Terms.HasAny(); } }

		PropertyPathMarker ITermsBaseFilter.Field { get; set; }
		IEnumerable<object> ITermsFilter.Terms { get; set; }
	} 
	
	public interface ITermsLookupFilterDescriptor : ITermsBaseFilter
	{
		[JsonProperty("id")]
		string _Id { get; set; }

		[JsonProperty("type")]
		TypeNameMarker _Type { get; set; }

		[JsonProperty("index")]
		IndexNameMarker _Index { get; set; }

		[JsonProperty("path")]
		PropertyPathMarker _Path { get; set; }

		[JsonProperty("routing")]
		string _Routing { get; set; }
	}

	/// <summary>
	/// http://www.elasticsearch.org/blog/terms-filter-lookup/
	/// </summary>
	public class TermsLookupFilterDescriptor : FilterBase, ITermsLookupFilterDescriptor
	{
		PropertyPathMarker ITermsBaseFilter.Field { get; set; }
		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((ITermsLookupFilterDescriptor)this)._Type == null || ((ITermsLookupFilterDescriptor)this)._Index == null || ((ITermsLookupFilterDescriptor)this)._Id.IsNullOrEmpty() 
					|| ((ITermsLookupFilterDescriptor)this)._Path.IsConditionless();
			}
		}

		[JsonProperty("id")]
		string ITermsLookupFilterDescriptor._Id { get; set; }

		[JsonProperty("type")]
		TypeNameMarker ITermsLookupFilterDescriptor._Type { get; set; }

		[JsonProperty("index")]
		IndexNameMarker ITermsLookupFilterDescriptor._Index { get; set; }

		[JsonProperty("path")]
		PropertyPathMarker ITermsLookupFilterDescriptor._Path { get; set; }

		[JsonProperty("routing")]
		string ITermsLookupFilterDescriptor._Routing { get; set; }


		public TermsLookupFilterDescriptor Lookup<T>(string field, string id, string index = null, string type = null)
		{
			return _Lookup<T>(field, id, index, type);
		}

		private TermsLookupFilterDescriptor _Lookup<T>(PropertyPathMarker field, string id, string index, string type)
		{
			((ITermsLookupFilterDescriptor)this)._Path = field;
			((ITermsLookupFilterDescriptor)this)._Id = id;
			((ITermsLookupFilterDescriptor)this)._Type = type ?? new TypeNameMarker {Type = typeof (T)};
			((ITermsLookupFilterDescriptor)this)._Index = index ?? new IndexNameMarker {Type = typeof (T)};
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
			((ITermsLookupFilterDescriptor)this)._Type = type;
			return this;
		}

		/// <summary>
		/// If not specified will use the default index for the type specified on Lookup&lt;T&gt;
		/// </summary>
		public TermsLookupFilterDescriptor Index(string index)
		{
			((ITermsLookupFilterDescriptor)this)._Index = index;
			return this;
		}

		/// <summary>
		/// A custom routing value to be used when retrieving the external terms doc.
		/// </summary>
		public TermsLookupFilterDescriptor Routing(string routing)
		{
			((ITermsLookupFilterDescriptor)this)._Routing = routing;
			return this;
		}
	}

}
