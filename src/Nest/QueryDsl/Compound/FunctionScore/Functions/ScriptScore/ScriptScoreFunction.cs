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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The script score function allows you to wrap another query and customize the
	/// scoring of it optionally with a computation derived from other numeric
	/// field values in the doc using a script expression.
	/// </summary>
	[InterfaceDataContract]
	public interface IScriptScoreFunction : IScoreFunction
	{
		/// <summary>
		/// The script to execute to calculate score
		/// </summary>
		[DataMember(Name = "script")]
		IScript Script { get; set; }
	}

	/// <inheritdoc cref="IScriptScoreFunction"/>
	public class ScriptScoreFunction : FunctionScoreFunctionBase, IScriptScoreFunction
	{
		/// <inheritdoc />
		public IScript Script { get; set; }
	}

	/// <inheritdoc cref="IScriptScoreFunction"/>
	public class ScriptScoreFunctionDescriptor<T>
		: FunctionScoreFunctionDescriptorBase<ScriptScoreFunctionDescriptor<T>, IScriptScoreFunction, T>, IScriptScoreFunction
		where T : class
	{
		IScript IScriptScoreFunction.Script { get; set; }

    /// <inheritdoc cref="IScriptScoreFunction.Script"/>
	public ScriptScoreFunctionDescriptor<T> Script(Func<ScriptDescriptor, IScript> selector) =>
		Assign(selector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));
	}
}
