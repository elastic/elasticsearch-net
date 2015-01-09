using Newtonsoft.Json;
using Nest.Resolvers.Converters;

namespace Nest
{
	[JsonConverter(typeof(CustomJsonConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class RawFilter : IQuery, ICustomJson
	{
		internal object Json { get; set; }
		bool IQuery.IsConditionless { get { return this.Json == null || this.Json.ToString().IsNullOrEmpty(); } }

		object ICustomJson.GetCustomJson()
		{
			return this.Json;
		}
	}
}
