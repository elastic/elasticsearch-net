using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Nest.Resolvers.Converters.Filters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{
	[JsonConverter(typeof(CompositeJsonConverter<RegexpFilterJsonReader,CustomJsonConverter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRegexpFilter : IFilterBase, ICustomJson
	{
		[JsonProperty("value")]
		string Value { get; set; }

		[JsonProperty("flags")]
		string Flags { get; set; }

		PropertyPathMarker Field { get; set; }
	}

	public class RegexpFilterDescriptor<T> : FilterBase, IRegexpFilter where T : class
	{
		string IRegexpFilter.Value { get; set; }

		string IRegexpFilter.Flags { get; set; }

		PropertyPathMarker IRegexpFilter.Field { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IRegexpFilter)this).Field.IsConditionless() || ((IRegexpFilter)this).Value.IsNullOrEmpty();
			}
		}

		public RegexpFilterDescriptor<T> Value(string regex)
		{
			((IRegexpFilter)this).Value = regex;
			return this;
		}
		public RegexpFilterDescriptor<T> Flags(string flags)
		{
			((IRegexpFilter)this).Flags = flags;
			return this;
		}
		public RegexpFilterDescriptor<T> OnField(string path)
		{
			((IRegexpFilter)this).Field = path;
			return this;
		}
		public RegexpFilterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IRegexpFilter)this).Field = objectPath;
			return this;
		}

		object ICustomJson.GetCustomJson()
		{
			var tf = ((IRegexpFilter)this);
			var rf = new { value = tf.Value, flags = tf.Flags };
			return this.FieldNameAsKeyFormat(tf.Field, rf);
		}
	}
}
