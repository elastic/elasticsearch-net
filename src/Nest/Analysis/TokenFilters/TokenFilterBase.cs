// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(TokenFilterFormatter))]
	public interface ITokenFilter
	{
		[DataMember(Name = "type")]
		string Type { get; }

		[DataMember(Name = "version")]
		string Version { get; set; }
	}

	public abstract class TokenFilterBase : ITokenFilter
	{
		protected TokenFilterBase(string type) => Type = type;

		[DataMember(Name = "type")]
		public string Type { get; protected set; }

		[DataMember(Name = "version")]
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

		public TTokenFilter Version(string version) => Assign(version, (a, v) => a.Version = v);
	}
}
