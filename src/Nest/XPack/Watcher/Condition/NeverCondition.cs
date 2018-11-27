using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(NeverCondition))]
	public interface INeverCondition : ICondition { }

	public class NeverCondition : ConditionBase, INeverCondition
	{
		internal override void WrapInContainer(IConditionContainer container) => container.Never = this;
	}
}
