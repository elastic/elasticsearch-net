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
	public interface IRegexpQuery
	{
		[JsonProperty("value")]
		string _Value { get; set; }

		[JsonProperty("flags")]
		string _Flags { get; set; }

		PropertyPathMarker _Field { get; set; }

		[JsonProperty("boost")]
		double? _Boost { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class RegexpQueryDescriptor<T> : IQuery, IRegexpQuery where T : class
	{
		string IRegexpQuery._Value { get; set; }

		string IRegexpQuery._Flags { get; set; }

		PropertyPathMarker IRegexpQuery._Field { get; set; }

		double? IRegexpQuery._Boost { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IRegexpQuery)this)._Field.IsConditionless() || ((IRegexpQuery)this)._Value.IsNullOrEmpty();
			}
		}

		public RegexpQueryDescriptor<T> Value(string regex)
		{
			((IRegexpQuery)this)._Value = regex;
			return this;
		}
		public RegexpQueryDescriptor<T> Flags(string flags)
		{
			((IRegexpQuery)this)._Flags = flags;
			return this;
		}
		public RegexpQueryDescriptor<T> OnField(string path)
		{
			((IRegexpQuery)this)._Field = path;
			return this;
		}
		public RegexpQueryDescriptor<T> Boost(double boost)
		{
			((IRegexpQuery)this)._Boost = boost;
			return this;
		}

		public RegexpQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IRegexpQuery)this)._Field = objectPath;
			return this;
		}

	}
}
