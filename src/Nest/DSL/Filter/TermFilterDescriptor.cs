using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITermFilter : IFieldNameFilter
	{
		[JsonProperty("value")]
		object Value { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }
	}
	
	public class TermFilter : PlainFilter, ITermFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Term = this;
		}

		public PropertyPathMarker Field { get; set; }
		public object Value { get; set; }
		public double? Boost { get; set; }
	}

	public class TermFilterDescriptor : FilterBase, ITermFilter
	{
		PropertyPathMarker IFieldNameFilter.Field { get; set; }
		object ITermFilter.Value { get; set; }
		double? ITermFilter.Boost { get; set; }

		bool IFilter.IsConditionless
		{
			get
			{
				var tf = ((ITermFilter)this);
				return tf.Value == null || tf.Value.ToString().IsNullOrEmpty() || tf.Field.IsConditionless();
			}
		}
		
	}
}
