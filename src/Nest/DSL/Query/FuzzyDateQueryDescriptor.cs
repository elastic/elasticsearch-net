using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;
namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FuzzyDateQueryDescriptor<T> : IQuery where T : class
	{
		internal string _Field { get; set; }
		[JsonProperty(PropertyName = "boost")]
		internal double? _Boost { get; set; }
		[JsonProperty(PropertyName = "min_similarity")]
		internal string _MinSimilarity { get; set; }
		[JsonProperty(PropertyName = "value")]
		internal DateTime? _Value { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return this._Field.IsNullOrEmpty() || this._Value == null;
			}
		}

		public FuzzyDateQueryDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			var fieldName = new PropertyNameResolver().Resolve(objectPath);
			return this.OnField(fieldName);
		}
		public FuzzyDateQueryDescriptor<T> Boost(double boost)
		{
			this._Boost = boost;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> MinSimilarity(string minSimilarity)
		{
			this._MinSimilarity = minSimilarity;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> Value(DateTime value)
		{
			this._Value = value;
			return this;
		}
		public FuzzyDateQueryDescriptor<T> Value(DateTime? value)
		{
			this._Value = value;
			return this;
		}
	}
}
