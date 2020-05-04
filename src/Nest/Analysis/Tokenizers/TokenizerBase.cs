// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(TokenizerFormatter))]
	public interface ITokenizer
	{
		[DataMember(Name = "type")]
		string Type { get; }

		[DataMember(Name = "version")]
		string Version { get; set; }
	}

	public abstract class TokenizerBase : ITokenizer
	{
		public string Type { get; protected set; }
		public string Version { get; set; }
	}

	public abstract class TokenizerDescriptorBase<TTokenizer, TTokenizerInterface>
		: DescriptorBase<TTokenizer, TTokenizerInterface>, ITokenizer
		where TTokenizer : TokenizerDescriptorBase<TTokenizer, TTokenizerInterface>, TTokenizerInterface
		where TTokenizerInterface : class, ITokenizer
	{
		protected abstract string Type { get; }
		string ITokenizer.Type => Type;
		string ITokenizer.Version { get; set; }

		public TTokenizer Version(string version) => Assign(version, (a, v) => a.Version = v);
	}
}
