using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(TokenizerJsonConverter))]
	public interface ITokenizer
	{
		[JsonProperty("version")]
		string Version { get; set; }

		[JsonProperty("type")]
		string Type { get; }
	}

	public abstract class TokenizerBase : ITokenizer
	{
		public string Version { get; set; }

		public string Type { get; protected set; }
	}

	public abstract class TokenizerDescriptorBase<TTokenizer, TTokenizerInterface>
		: DescriptorBase<TTokenizer, TTokenizerInterface>, ITokenizer
		where TTokenizer : TokenizerDescriptorBase<TTokenizer, TTokenizerInterface>, TTokenizerInterface
		where TTokenizerInterface : class, ITokenizer
	{
		string ITokenizer.Version { get; set; }
		string ITokenizer.Type => this.Type;
		protected abstract string Type { get; }

		public TTokenizer Version(string version) => Assign(a => a.Version = version);
	}

}
