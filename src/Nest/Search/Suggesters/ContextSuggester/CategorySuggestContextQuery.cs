using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ICategorySuggestContextQuery : ISuggestContextQuery
	{
		[JsonProperty("prefix")]
		bool? Prefix { get; set; }
	}

	public class CategorySuggestContextQuery : SuggestContextQueryBase, ICategorySuggestContextQuery
	{
		public bool? Prefix { get; set; }
	}

	public class CategorySuggestContextQueryDescriptor<T>
		: SuggestContextQueryDescriptorBase<CategorySuggestContextQueryDescriptor<T>, ICategorySuggestContextQuery, T>, ICategorySuggestContextQuery
	{
		bool? ICategorySuggestContextQuery.Prefix { get; set; }

		public CategorySuggestContextQueryDescriptor<T> Prefix(bool prefix = true) => Assign(a => a.Prefix = prefix);
	}
}
