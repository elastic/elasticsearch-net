using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(TokenFilterJsonConverter))]
	public interface ITokenFilter
	{
		[JsonProperty("version")]
		string Version { get; set; }

		[JsonProperty("type")]
		string Type { get; }

	}

	public abstract class TokenFilterBase : ITokenFilter
	{
		protected TokenFilterBase(string type)
		{
			this.Type = type;
		}

		[JsonProperty("version")]
		public string Version { get; set; }

		[JsonProperty("type")]
		public string Type { get; protected set; }
	}

	public abstract class TokenFilterDescriptorBase<TTokenFilter, TTokenFilterInterface> 
		: DescriptorBase<TTokenFilter, TTokenFilterInterface>, ITokenFilter
		where TTokenFilter : TokenFilterDescriptorBase<TTokenFilter, TTokenFilterInterface>, TTokenFilterInterface
		where TTokenFilterInterface : class, ITokenFilter
	{
		string ITokenFilter.Version { get; set; }
		string ITokenFilter.Type => this.Type;
		protected abstract string Type { get; }

		public TTokenFilter Version(string version) => Assign(a => a.Version = version);
	}

}