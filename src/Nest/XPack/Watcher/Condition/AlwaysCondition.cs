using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(AlwaysCondition))]
	public interface IAlwaysCondition : ICondition { }

	public class AlwaysCondition : ConditionBase, IAlwaysCondition
	{
		internal override void WrapInContainer(IConditionContainer container) => container.Always = this;
	}
}
