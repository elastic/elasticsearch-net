using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<TypeFilterDescriptor>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITypeFilter : IFilter
	{
		[JsonProperty(PropertyName = "value")]
		TypeNameMarker Value { get; set; }
	}

	public class TypeFilter : PlainFilter, ITypeFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Type = this;
		}

		public TypeNameMarker Value { get; set; }
	}

	public class TypeFilterDescriptor : FilterBase, ITypeFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				return ((ITypeFilter)this).Value.IsConditionless();
			}

		}

		[JsonProperty(PropertyName = "value")]
		TypeNameMarker ITypeFilter.Value { get; set; }
	}
}
