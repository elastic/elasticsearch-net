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

using System;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The predicate_token_filter token filter takes a predicate script, and removes tokens that do
	/// not match the predicate.
	/// </summary>
	public interface IPredicateTokenFilter : ITokenFilter
	{
		/// <summary>
		/// a predicate script that determines whether or not the current token will
		/// be emitted.  Note that only inline scripts are supported.
		/// </summary>
		[DataMember(Name = "script")]
		IScript Script { get; set; }
	}

	public class PredicateTokenFilter : TokenFilterBase, IPredicateTokenFilter
	{
		public PredicateTokenFilter() : base("predicate_token_filter") { }

		public IScript Script { get; set; }
	}

	/// <inheritdoc cref="IPredicateTokenFilter" />
	public class PredicateTokenFilterDescriptor
		: TokenFilterDescriptorBase<PredicateTokenFilterDescriptor, IPredicateTokenFilter>, IPredicateTokenFilter
	{
		protected override string Type => "predicate_token_filter";

		IScript IPredicateTokenFilter.Script { get; set; }

		/// <inheritdoc cref="IPredicateTokenFilter.Script" />
		public PredicateTokenFilterDescriptor Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		/// <inheritdoc cref="IPredicateTokenFilter.Script" />
		public PredicateTokenFilterDescriptor Script(string predicate) =>
			Assign(new InlineScript(predicate), (a, v) => a.Script = v);
	}
}
