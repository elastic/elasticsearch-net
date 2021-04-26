/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Runtime.Serialization;
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
