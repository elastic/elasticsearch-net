using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface ICondition { }

	public abstract class ConditionBase : ICondition
	{
		internal abstract void WrapInContainer(IConditionContainer container);
	}
}
