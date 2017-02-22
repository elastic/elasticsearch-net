using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public interface ICondition {}

	public abstract class ConditionBase : ICondition
	{
		internal abstract void WrapInContainer(IConditionContainer container);
	}
}
