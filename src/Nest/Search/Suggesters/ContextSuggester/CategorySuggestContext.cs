using System.Runtime.Serialization;

namespace Nest
{
	public interface ICategorySuggestContext : ISuggestContext { }

	[DataContract]
	public class CategorySuggestContext : SuggestContextBase, ICategorySuggestContext
	{
		public override string Type => "category";
	}

	[DataContract]
	public class CategorySuggestContextDescriptor<T>
		: SuggestContextDescriptorBase<CategorySuggestContextDescriptor<T>, ICategorySuggestContext, T>, ICategorySuggestContext
		where T : class
	{
		protected override string Type => "category";
	}
}
