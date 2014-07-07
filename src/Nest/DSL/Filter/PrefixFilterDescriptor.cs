using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IPrefixFilter : IFieldNameFilter
	{
		string Prefix { get; set; }
	}

	public class PrefixFilter : PlainFilter, IPrefixFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Prefix = this;
		}

		public PropertyPathMarker Field { get; set; }
		public string Prefix { get; set; }
	}

	public class PrefixFilterDescriptor : FilterBase, IPrefixFilter
	{
		bool IFilter.IsConditionless { get { return ((IPrefixFilter)this).Field.IsConditionless() || ((IPrefixFilter)this).Prefix.IsNullOrEmpty(); } }

		PropertyPathMarker IFieldNameFilter.Field { get; set; }
		string IPrefixFilter.Prefix { get; set; }
		
	}
}