using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<NeverCondition>))]
	public interface INeverCondition : ICondition { }

	public class NeverCondition : ConditionBase, INeverCondition
	{
		internal override void WrapInContainer(IConditionContainer container) => container.Never = this;
	}
}
