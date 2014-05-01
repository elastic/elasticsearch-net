using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{

	[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<TermsLookupFilterDescriptor>,CustomJsonConverter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITermsLookupFilterDescriptor : ITermsBaseFilter, ICustomJson
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
	}

	public class TermsLookupFilterDescriptor : FilterBase, ITermsLookupFilterDescriptor
	{
		PropertyPathMarker ITermsBaseFilter.Field { get; set; }
		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((ITermsLookupFilterDescriptor)this).Type == null || ((ITermsLookupFilterDescriptor)this).Index == null || ((ITermsLookupFilterDescriptor)this).Id.IsNullOrEmpty() 
					|| ((ITermsLookupFilterDescriptor)this).Path.IsConditionless();
			}
		}

		[JsonProperty("id")]
		string ITermsLookupFilterDescriptor.Id { get; set; }

		[JsonProperty("type")]
		TypeNameMarker ITermsLookupFilterDescriptor.Type { get; set; }

		[JsonProperty("index")]
		IndexNameMarker ITermsLookupFilterDescriptor.Index { get; set; }

		[JsonProperty("path")]
		PropertyPathMarker ITermsLookupFilterDescriptor.Path { get; set; }

		[JsonProperty("routing")]
		string ITermsLookupFilterDescriptor.Routing { get; set; }
		
		object ICustomJson.GetCustomJson()
		{
			var tf = ((ITermsLookupFilterDescriptor)this);
			var f = new
			{
				id = tf.Id,
				type= tf.Type, 
				index = tf.Index,
				path = tf.Path, 
				routing = tf.Routing
			};
			return this.FieldNameAsKeyFormat(tf.Field, f);
		}

		public TermsLookupFilterDescriptor Lookup<T>(string field, string id, string index = null, string type = null)
		{
			return _Lookup<T>(field, id, index, type);
		}

		private TermsLookupFilterDescriptor _Lookup<T>(PropertyPathMarker field, string id, string index, string type)
		{
			((ITermsLookupFilterDescriptor)this).Path = field;
			((ITermsLookupFilterDescriptor)this).Id = id;
			((ITermsLookupFilterDescriptor)this).Type = type ?? new TypeNameMarker {Type = typeof (T)};
			((ITermsLookupFilterDescriptor)this).Index = index ?? new IndexNameMarker {Type = typeof (T)};
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
			((ITermsLookupFilterDescriptor)this).Type = type;
			return this;
		}

		/// <summary>
		/// If not specified will use the default index for the type specified on Lookup&lt;T&gt;
		/// </summary>
		public TermsLookupFilterDescriptor Index(string index)
		{
			((ITermsLookupFilterDescriptor)this).Index = index;
			return this;
		}

		/// <summary>
		/// A custom routing value to be used when retrieving the external terms doc.
		/// </summary>
		public TermsLookupFilterDescriptor Routing(string routing)
		{
			((ITermsLookupFilterDescriptor)this).Routing = routing;
			return this;
		}
	}

}
