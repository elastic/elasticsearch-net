using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ICategorySuggestContext : ISuggestContext { }

	[JsonObject]
	public class CategorySuggestContext : SuggestContextBase, ICategorySuggestContext
	{
		public override string Type => "category";
	}

	public class CategorySuggestContextDescriptor<T> : SuggestContextDescriptorBase<CategorySuggestContextDescriptor<T>, ICategorySuggestContext, T>, ICategorySuggestContext
		where T : class
	{
		protected override string Type => "category";
	}
}
