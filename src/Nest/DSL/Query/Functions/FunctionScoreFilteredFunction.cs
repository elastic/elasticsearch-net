using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FunctionScoreFilteredFunction<T> : FunctionScoreFunction<T> where T : class
	{
		[JsonProperty(PropertyName = "filter")]
		internal BaseFilter _Filter { get; set; }

		public FunctionScoreFunction<T> Filter(Func<FilterDescriptor<T>, BaseFilter> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);

			this._Filter = f;
			return this;
		}
	}
}