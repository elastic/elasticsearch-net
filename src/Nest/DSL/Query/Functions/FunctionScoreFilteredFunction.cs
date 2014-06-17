using Newtonsoft.Json;
using Shared.Extensions;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FunctionScoreFilteredFunction<T> : FunctionScoreFunction<T> where T : class
	{
		[JsonProperty(PropertyName = "filter")]
		internal FilterContainer FilterDescriptor { get; set; }

		public FunctionScoreFunction<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);

			this.FilterDescriptor = f;
			return this;
		}
	}
}