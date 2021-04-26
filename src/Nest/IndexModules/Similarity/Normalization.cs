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
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum Normalization
	{
		/// <summary>
		/// Implementation used when there is no normalization.
		/// </summary>
		[EnumMember(Value = "no")]
		No,

		/// <summary>
		/// Normalization model that assumes a uniform distribution of the term frequency.
		/// </summary>
		[EnumMember(Value = "h1")]
		H1,

		/// <summary>
		///  Normalization model in which the term frequency is inversely related to the length.
		/// </summary>
		[EnumMember(Value = "h2")]
		H2,

		/// <summary>
		/// Dirichlet Priors normalization
		/// </summary>
		[EnumMember(Value = "h3")]
		H3,

		/// <summary>
		/// Pareto-Zipf Normalization
		/// </summary>
		[EnumMember(Value = "z")]
		Z,
	}
}
