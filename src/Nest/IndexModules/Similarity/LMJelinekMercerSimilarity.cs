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

// ReSharper disable InconsistentNaming

namespace Nest
{
	/// <summary>
	/// A similarity that attempts to capture important patterns in the text,
	/// while leaving out noise.
	/// </summary>
	public interface ILMJelinekMercerSimilarity : ISimilarity
	{
		/// <summary>
		/// The lambda parameter
		/// </summary>
		[DataMember(Name ="lambda")]
		[JsonFormatter(typeof(NullableStringDoubleFormatter))]
		double? Lambda { get; set; }
	}

	/// <inheritdoc />
	public class LMJelinekMercerSimilarity : ILMJelinekMercerSimilarity
	{
		/// <inheritdoc />
		public double? Lambda { get; set; }

		public string Type => "LMJelinekMercer";
	}

	/// <inheritdoc />
	public class LMJelinekMercerSimilarityDescriptor
		: DescriptorBase<LMJelinekMercerSimilarityDescriptor, ILMJelinekMercerSimilarity>, ILMJelinekMercerSimilarity
	{
		double? ILMJelinekMercerSimilarity.Lambda { get; set; }
		string ISimilarity.Type => "LMJelinekMercer";

		/// <inheritdoc />
		public LMJelinekMercerSimilarityDescriptor Lamdba(double? lamda) => Assign(lamda, (a, v) => a.Lambda = v);
	}
}
