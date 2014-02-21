using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class RegexpFilterDescriptor<T> : FilterBase where T : class
	{
		[JsonProperty("value")]
		internal string _Value { get; set; }

		[JsonProperty("flags")]
		internal string _Flags { get; set; }

		internal PropertyPathMarker _Field { get; set; }

		internal override bool IsConditionless
		{
			get
			{
				return this._Field.IsConditionless() || this._Value.IsNullOrEmpty();
			}
		}

		public RegexpFilterDescriptor<T> Value(string regex)
		{
			this._Value = regex;
			return this;
		}
		public RegexpFilterDescriptor<T> Flags(string flags)
		{
			this._Flags = flags;
			return this;
		}
		public RegexpFilterDescriptor<T> OnField(string path)
		{
			this._Field = path;
			return this;
		}
		public RegexpFilterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			this._Field = objectPath;
			return this;
		}

	}
}
