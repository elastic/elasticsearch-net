using System.Runtime.Serialization;

namespace Nest
{
	[ContractJsonConverter(typeof(TokenFilterJsonConverter))]
	public interface ITokenFilter
	{
		[DataMember(Name ="type")]
		string Type { get; }

		[DataMember(Name ="version")]
		string Version { get; set; }
	}

	public abstract class TokenFilterBase : ITokenFilter
	{
		protected TokenFilterBase(string type) => Type = type;

		[DataMember(Name ="type")]
		public string Type { get; protected set; }

		[DataMember(Name ="version")]
		public string Version { get; set; }
	}

	public abstract class TokenFilterDescriptorBase<TTokenFilter, TTokenFilterInterface>
		: DescriptorBase<TTokenFilter, TTokenFilterInterface>, ITokenFilter
		where TTokenFilter : TokenFilterDescriptorBase<TTokenFilter, TTokenFilterInterface>, TTokenFilterInterface
		where TTokenFilterInterface : class, ITokenFilter
	{
		protected abstract string Type { get; }
		string ITokenFilter.Type => Type;
		string ITokenFilter.Version { get; set; }

		public TTokenFilter Version(string version) => Assign(a => a.Version = version);
	}
}
