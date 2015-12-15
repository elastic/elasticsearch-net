using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(AnalyzerJsonConverter))]
	public interface IAnalyzer
	{
		[JsonProperty(PropertyName = "version")]
		string Version { get; set; }
		
		[JsonProperty(PropertyName = "type")]
		string Type { get; }
	}

	public abstract class AnalyzerBase : IAnalyzer
	{
		public string Version { get; set; }
		
		public virtual string Type { get; protected set; }
	}

	public abstract class AnalyzerDescriptorBase<TAnalyzer, TAnalyzerInterface>
		: DescriptorBase<TAnalyzer, TAnalyzerInterface>, IAnalyzer
		where TAnalyzer : AnalyzerDescriptorBase<TAnalyzer, TAnalyzerInterface>, TAnalyzerInterface
		where TAnalyzerInterface : class, IAnalyzer
	{
		string IAnalyzer.Version { get; set; }
		string IAnalyzer.Type => this.Type;
		protected abstract string Type { get; }

		public TAnalyzer Version(string version) => Assign(a => a.Version = version);
	}
		
}
