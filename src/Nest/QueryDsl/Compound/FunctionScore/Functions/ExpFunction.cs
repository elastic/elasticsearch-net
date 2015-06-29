using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class ExpFunction<T> : FunctionScoreDecayFunction<T>
		where T : class
	{
		[JsonProperty(PropertyName = "exp")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal IDictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor> _ExpDescriptor { get; set; }

		public ExpFunction(
			Expression<Func<T, object>> objectPath,
			Func<FunctionScoreDecayFieldDescriptor, FunctionScoreDecayFieldDescriptor> functionScoreDecaySelector)
		{
			this._ExpDescriptor = new Dictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor>
			{
				[objectPath] = functionScoreDecaySelector?.Invoke(new FunctionScoreDecayFieldDescriptor())
			};
		}

		public ExpFunction(
			string field,
			Func<FunctionScoreDecayFieldDescriptor, FunctionScoreDecayFieldDescriptor> functionScoreDecaySelector)
		{
			this._ExpDescriptor = new Dictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor>
			{
				[field] = functionScoreDecaySelector?.Invoke(new FunctionScoreDecayFieldDescriptor())
			};
		}
	}
}