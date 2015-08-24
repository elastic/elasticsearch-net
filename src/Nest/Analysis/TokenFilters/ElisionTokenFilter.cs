using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter which removes elisions. For example, “l’avion” (the plane) will tokenized as “avion” (plane).
	/// </summary>
	public interface IElisionTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Accepts articles setting which is a set of stop words articles
		/// </summary>
		[JsonProperty("articles")]
		IEnumerable<string> Articles { get; set; }
	}

	/// <inheritdoc/>
	public class ElisionTokenFilter : TokenFilterBase, IElisionTokenFilter
	{
		public ElisionTokenFilter() : base("elision") { }

		/// <inheritdoc/>
		public IEnumerable<string> Articles { get; set; }
	}
	///<inheritdoc/>
	public class ElisionTokenFilterDescriptor 
		: TokenFilterDescriptorBase<ElisionTokenFilterDescriptor, IElisionTokenFilter>, IElisionTokenFilter
	{
		protected override string Type => "elision";

		IEnumerable<string> IElisionTokenFilter.Articles { get; set; }

		///<inheritdoc/>
		public ElisionTokenFilterDescriptor Articles(IEnumerable<string> articles) => Assign(a => a.Articles = articles);

		///<inheritdoc/>
		public ElisionTokenFilterDescriptor Articles(params string[] articles) => Assign(a => a.Articles = articles);

	}

}