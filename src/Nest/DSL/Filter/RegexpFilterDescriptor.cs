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
	public interface IRegexpFilter : IFilterBase
	{
		[JsonProperty("value")]
		string _Value { get; set; }

		[JsonProperty("flags")]
		string _Flags { get; set; }

		PropertyPathMarker _Field { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class RegexpFilterDescriptor<T> : FilterBase, IRegexpFilter where T : class
	{
		string IRegexpFilter._Value { get; set; }

		string IRegexpFilter._Flags { get; set; }

		PropertyPathMarker IRegexpFilter._Field { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IRegexpFilter)this)._Field.IsConditionless() || ((IRegexpFilter)this)._Value.IsNullOrEmpty();
			}
		}

		public RegexpFilterDescriptor<T> Value(string regex)
		{
			((IRegexpFilter)this)._Value = regex;
			return this;
		}
		public RegexpFilterDescriptor<T> Flags(string flags)
		{
			((IRegexpFilter)this)._Flags = flags;
			return this;
		}
		public RegexpFilterDescriptor<T> OnField(string path)
		{
			((IRegexpFilter)this)._Field = path;
			return this;
		}
		public RegexpFilterDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IRegexpFilter)this)._Field = objectPath;
			return this;
		}

	}
}
