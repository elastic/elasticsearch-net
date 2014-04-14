using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class GaussFunction<T> : FunctionScoreDecayFunction<T>
		where T : class
	{
		[JsonProperty(PropertyName = "gauss")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal IDictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor> _GaussDescriptor { get; set; }

		public GaussFunction(Expression<Func<T, object>> objectPath, Action<FunctionScoreDecayFieldDescriptor> descriptorBuilder)
		{
			_GaussDescriptor = new Dictionary<PropertyPathMarker, FunctionScoreDecayFieldDescriptor>();

			var descriptor = new FunctionScoreDecayFieldDescriptor();
			descriptorBuilder(descriptor);
			_GaussDescriptor[objectPath] = descriptor;
		}
	}
}