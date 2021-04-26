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
	/// A similarity that allows a script to be used in order to specify how scores should be computed.
	/// </summary>
	/// <remarks>
	/// Valid in Elasticsearch 6.1.0+
	/// </remarks>
	public interface IScriptedSimilarity : ISimilarity
	{
		/// <summary>
		/// Script to calculate similarity
		/// </summary>
		[DataMember(Name ="script")]
		IScript Script { get; set; }
	}

	/// <inheritdoc />
	public class ScriptedSimilarity : IScriptedSimilarity
	{
		/// <inheritdoc />
		public IScript Script { get; set; }

		public string Type => "scripted";
	}

	/// <inheritdoc cref="IScriptedSimilarity" />
	public class ScriptedSimilarityDescriptor
		: DescriptorBase<ScriptedSimilarityDescriptor, IScriptedSimilarity>, IScriptedSimilarity
	{
		IScript IScriptedSimilarity.Script { get; set; }
		string ISimilarity.Type => "scripted";

		/// <inheritdoc cref="IScriptedSimilarity.Script" />
		public ScriptedSimilarityDescriptor Script(Func<ScriptDescriptor, IScript> selector) =>
			Assign(selector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));
	}
}
