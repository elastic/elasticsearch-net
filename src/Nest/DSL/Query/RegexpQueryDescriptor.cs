using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class RegexpQueryDescriptor<T> : IQuery where T : class
	{
		[JsonProperty("value")]
		internal string _Value { get; set; }

		[JsonProperty("flags")]
		internal string _Flags { get; set; }

		internal string _Field { get; set; }

		[JsonProperty("boost")]
		public double? _Boost { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return this._Field.IsNullOrEmpty() || this._Value.IsNullOrEmpty();
			}
		}

		public RegexpQueryDescriptor<T> Value(string regex)
		{
			this._Value = regex;
			return this;
		}
		public RegexpQueryDescriptor<T> Flags(string flags)
		{
			this._Flags = flags;
			return this;
		}
		public RegexpQueryDescriptor<T> OnField(string path)
		{
			this._Field = path;
			return this;
		}
		public RegexpQueryDescriptor<T> Boost(double boost)
		{
			this._Boost = boost;
			return this;
		}

		public RegexpQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			var fieldName = new PropertyNameResolver().Resolve(objectPath);
			return this.OnField(fieldName);
		}


	}
}
