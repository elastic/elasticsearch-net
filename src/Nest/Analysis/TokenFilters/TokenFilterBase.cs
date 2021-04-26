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
