using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IFunctionScoreFunction
	{
	}
	
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class FunctionScoreFunction<T> : IFunctionScoreFunction 
		where T : class
	{
		
	}
}