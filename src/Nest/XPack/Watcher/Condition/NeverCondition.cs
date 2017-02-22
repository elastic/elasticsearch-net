using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<NeverCondition>))]
	public interface INeverCondition : ICondition {}

	public class NeverCondition : ConditionBase, INeverCondition
	{
		internal override void WrapInContainer(IConditionContainer container) => container.Never = this;
	}
}
