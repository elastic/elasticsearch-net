using System;
using System.Linq;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	
	[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<PrefixFilter>,CustomJsonConverter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPrefixFilter : IFilterBase, ICustomJson
	{
		PropertyPathMarker Field { get; set; }
		string Prefix { get; set; }
	}
	public class PrefixFilter : FilterBase, IPrefixFilter, ICustomJsonReader<PrefixFilter>
	{
		bool IFilterBase.IsConditionless { get { return ((IPrefixFilter)this).Field.IsConditionless() || ((IPrefixFilter)this).Prefix.IsNullOrEmpty(); } }

		PropertyPathMarker IPrefixFilter.Field { get; set; }
		string IPrefixFilter.Prefix { get; set; }

		object ICustomJson.GetCustomJson()
		{
			var tf = ((IPrefixFilter)this);
			return this.FieldNameAsKeyFormat(tf.Field, tf.Prefix);
		}
		
		public PrefixFilter FromJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var filter = new PrefixFilter();
			var dict = base.ReadToDict(reader, serializer, filter);
			if (dict.Count == 0) return filter;

			var firstProp = dict.First();
			((IPrefixFilter)filter).Field = firstProp.Key;
			((IPrefixFilter)filter).Prefix = firstProp.Value as string;
			return filter;
		}
	}
}