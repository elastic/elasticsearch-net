using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FunctionScoreDecayFunction<T> : FunctionScoreFunction<T>
		where T : class
	{
	}
}