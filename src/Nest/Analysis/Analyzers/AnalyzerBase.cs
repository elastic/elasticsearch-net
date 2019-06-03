using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(AnalyzerFormatter))]
	public interface IAnalyzer
	{
		[DataMember(Name = "type")]
		string Type { get; }

		[DataMember(Name = "version")]
		string Version { get; set; }
	}

	public abstract class AnalyzerBase : IAnalyzer
	{
		internal AnalyzerBase() { }

		// ReSharper disable once VirtualMemberCallInConstructor
		protected AnalyzerBase(string type) => Type = type;

		public virtual string Type { get; protected set; }

		public string Version { get; set; }
	}

	public abstract class AnalyzerDescriptorBase<TAnalyzer, TAnalyzerInterface>
		: DescriptorBase<TAnalyzer, TAnalyzerInterface>, IAnalyzer
		where TAnalyzer : AnalyzerDescriptorBase<TAnalyzer, TAnalyzerInterface>, TAnalyzerInterface
		where TAnalyzerInterface : class, IAnalyzer
	{
		protected abstract string Type { get; }
		string IAnalyzer.Type => Type;
		string IAnalyzer.Version { get; set; }

		public TAnalyzer Version(string version) => Assign(version, (a, v) => a.Version = v);
	}
}
