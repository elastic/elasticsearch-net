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
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// The conditional token filter takes a predicate script and a list of subfilters, and
	/// only applies the subfilters to the current token if it matches the predicate.
	/// </summary>
	public interface IConditionTokenFilter : ITokenFilter
	{
		/// <summary>
		/// a predicate script that determines whether or not the filters will be applied
		/// to the current token.  Note that only inline scripts are supported
		/// </summary>
		[DataMember(Name = "script")]
		IScript Script { get; set; }

		/// <summary>
		///a chain of token filters to apply to the current token if the predicate
		/// matches. These can be any token filters defined elsewhere in the index mappings.
		/// </summary>
		[DataMember(Name = "filter")]
		IEnumerable<string> Filters { get; set; }
	}

	/// <inheritdoc cref="IConditionTokenFilter" />
	public class ConditionTokenFilter : TokenFilterBase, IConditionTokenFilter
	{
		public ConditionTokenFilter() : base("condition") { }

		/// <inheritdoc cref="IConditionTokenFilter.Script" />
		public IScript Script { get; set; }

		/// <inheritdoc cref="IConditionTokenFilter.Filters" />
		public IEnumerable<string> Filters { get; set; }
	}

	/// <inheritdoc cref="IConditionTokenFilter" />
	public class ConditionTokenFilterDescriptor
		: TokenFilterDescriptorBase<ConditionTokenFilterDescriptor, IConditionTokenFilter>, IConditionTokenFilter
	{
		protected override string Type => "condition";

		IScript IConditionTokenFilter.Script { get; set; }
		IEnumerable<string> IConditionTokenFilter.Filters { get; set; }

		/// <inheritdoc cref="IConditionTokenFilter.Script" />
		public ConditionTokenFilterDescriptor Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		/// <inheritdoc cref="IConditionTokenFilter.Script" />
		public ConditionTokenFilterDescriptor Script(string predicate) =>
			Assign(new InlineScript(predicate), (a, v) => a.Script = v);

		/// <inheritdoc cref="IConditionTokenFilter.Filters" />
		public ConditionTokenFilterDescriptor Filters(params string[] filters) =>
			Assign(filters, (a, v) => a.Filters = v);

		/// <inheritdoc cref="IConditionTokenFilter.Filters" />
		public ConditionTokenFilterDescriptor Filters(IEnumerable<string> filters) =>
			Assign(filters, (a, v) => a.Filters = v);

}
}
