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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public enum DFRBasicModel
	{
		/// <summary>
		/// Limiting form of the Bose-Einstein model. The formula used in Lucene differs slightly from the one in the original paper: F is increased by
		/// tfn+1 and N is increased by F
		/// </summary>
		[EnumMember(Value = "be")]
		BE,

		/// <summary>
		/// Implements the approximation of the binomial model with the divergence for DFR.
		/// The formula used in Lucene differs slightly from the one in the original paper: to avoid underflow for small values of N and F, N is
		/// increased by 1 and F is always increased by tfn+1.
		/// </summary>
		[EnumMember(Value = "d")]
		D,

		/// <summary>
		/// Geometric as limiting form of the Bose-Einstein model. The formula used in Lucene differs slightly from the one in the original paper: F is
		/// increased by 1 and N is increased by F.
		/// </summary>
		[EnumMember(Value = "g")]
		G,

		/// <summary>
		/// An approximation of the I(ne) model.
		/// </summary>
		[EnumMember(Value = "if")]
		IF,

		/// <summary>
		/// The basic tf-idf model of randomness.
		/// </summary>
		[EnumMember(Value = "in")]
		IN,

		/// <summary>
		/// Tf-idf model of randomness, based on a mixture of Poisson and inverse document frequency.
		/// </summary>
		[EnumMember(Value = "ine")]
		INE,

		/// <summary>
		/// Implements the Poisson approximation for the binomial model for DFR.
		/// </summary>
		[EnumMember(Value = "p")]
		P
	}
}
