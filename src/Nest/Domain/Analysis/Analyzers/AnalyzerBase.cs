using Newtonsoft.Json;

namespace Nest
{
	public abstract class AnalyzerBase : IAnalysisSetting
	{
		[JsonProperty(PropertyName = "version")]
		public string Version { get; set; }
		
		[JsonProperty(PropertyName = "type")]
		public string Type { get; protected set; }
	}
}
