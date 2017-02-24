using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<AlwaysCondition>))]
	public interface IAlwaysCondition : ICondition {}

	public class AlwaysCondition : ConditionBase, IAlwaysCondition
	{
		internal override void WrapInContainer(IConditionContainer container) => container.Always = this;
	}
}
