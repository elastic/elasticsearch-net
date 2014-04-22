using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FunctionScoreFilteredFunction<T> : FunctionScoreFunction<T> where T : class
	{
		[JsonProperty(PropertyName = "filter")]
		internal BaseFilterDescriptor FilterDescriptor { get; set; }

		public FunctionScoreFunction<T> Filter(Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptorDescriptor<T>();
			var f = filterSelector(filter);

			this.FilterDescriptor = f;
			return this;
		}
	}
}