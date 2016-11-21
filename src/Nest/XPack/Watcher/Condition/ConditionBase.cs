using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface ICondition {}

	public abstract class ConditionBase
	{


		internal abstract void WrapInContainer(IConditionContainer container);
	}
}
