using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ICategorySuggestContext : ISuggestContext
	{
		[JsonProperty("default")]
		IEnumerable<string> Default { get; set; }
	}

	[JsonObject]
	public class CategorySuggestContext : SuggestContextBase, ICategorySuggestContext
	{
		public override string Type => "category";
		public IEnumerable<string> Default { get; set; }
	}

	public class CategorySuggestContextDescriptor<T> : SuggestContextDescriptorBase<CategorySuggestContextDescriptor<T>, ICategorySuggestContext, T>, ICategorySuggestContext
		where T : class
	{
		protected override string Type => "category";
		IEnumerable<string> ICategorySuggestContext.Default { get; set; }

		public CategorySuggestContextDescriptor<T> Default(params string[] defaults) => Assign(a => a.Default = defaults);

		public CategorySuggestContextDescriptor<T> Default(IEnumerable<string> defaults) => Assign(a => a.Default = defaults);
	}
}
