using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(NormalizerJsonConverter))]
	public interface INormalizer
	{
		[JsonProperty("version")]
		string Version { get; set; }

		[JsonProperty("type")]
		string Type { get; }
	}

	public abstract class NormalizerBase : INormalizer
	{
		internal NormalizerBase() { }

		protected NormalizerBase(string type) => Type = type;

		public string Version { get; set; }

		public virtual string Type { get; protected set; }
	}

	public abstract class NormalizerDescriptorBase<TNormalizer, TNormalizerInterface>
		: DescriptorBase<TNormalizer, TNormalizerInterface>, INormalizer
		where TNormalizer : NormalizerDescriptorBase<TNormalizer, TNormalizerInterface>, TNormalizerInterface
		where TNormalizerInterface : class, INormalizer
	{
		string INormalizer.Version { get; set; }
		string INormalizer.Type => this.Type;
		protected abstract string Type { get; }

		public TNormalizer Version(string version) => Assign(a => a.Version = version);
	}

}
