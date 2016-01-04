using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(CharFilterJsonConverter))]
	public interface ICharFilter
	{
		[JsonProperty("version")]
		string Version { get; set; }

		[JsonProperty("type")]
		string Type { get; }
	}


	public abstract class CharFilterBase : ICharFilter
	{
		protected CharFilterBase(string type)
		{
			this.Type = type;
		}
		public string Version { get; set; }

		public string Type { get; protected set; }
	}

	public abstract class CharFilterDescriptorBase<TCharFilter, TCharFilterInterface>
		: DescriptorBase<TCharFilter, TCharFilterInterface>, ICharFilter
		where TCharFilter : CharFilterDescriptorBase<TCharFilter, TCharFilterInterface>, TCharFilterInterface
		where TCharFilterInterface : class, ICharFilter
	{
		string ICharFilter.Version { get; set; }
		string ICharFilter.Type => this.Type;
		protected abstract string Type { get; }

		public TCharFilter Version(string version) => Assign(a => a.Version = version);
	}
}