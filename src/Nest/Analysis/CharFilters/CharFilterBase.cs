// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(CharFilterFormatter))]
	public interface ICharFilter
	{
		[DataMember(Name = "type")]
		string Type { get; }

		[DataMember(Name = "version")]
		string Version { get; set; }
	}


	public abstract class CharFilterBase : ICharFilter
	{
		protected CharFilterBase(string type) => Type = type;

		public string Type { get; protected set; }
		public string Version { get; set; }
	}

	public abstract class CharFilterDescriptorBase<TCharFilter, TCharFilterInterface>
		: DescriptorBase<TCharFilter, TCharFilterInterface>, ICharFilter
		where TCharFilter : CharFilterDescriptorBase<TCharFilter, TCharFilterInterface>, TCharFilterInterface
		where TCharFilterInterface : class, ICharFilter
	{
		protected abstract string Type { get; }
		string ICharFilter.Type => Type;
		string ICharFilter.Version { get; set; }

		public TCharFilter Version(string version) => Assign(version, (a, v) => a.Version = v);
	}
}
