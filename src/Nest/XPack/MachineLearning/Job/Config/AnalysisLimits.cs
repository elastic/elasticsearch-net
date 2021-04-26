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

namespace Nest
{
	/// <summary>
	/// Limits applied for the resources required to hold the mathematical models in memory.
	/// These limits are approximate and can be set per job. They do not control the memory used by other processes,
	/// for example the Elasticsearch Java processes.
	/// If necessary, you can increase the limits after the job is created.
	/// </summary>
	[ReadAs(typeof(AnalysisLimits))]
	public interface IAnalysisLimits
	{
		/// <summary>
		/// The maximum number of examples stored per category in memory and in the results data store.
		/// The default value is 4. If you increase this value, more examples are available,
		/// however it requires that you have more storage available.
		/// If you set this value to 0, no examples are stored.
		/// </summary>
		[DataMember(Name ="categorization_examples_limit")]
		long? CategorizationExamplesLimit { get; set; }

		/// <summary>
		/// The approximate maximum amount of memory resources that are required for analytical processing,
		/// in MiB. Once this limit is approached, data pruning becomes more aggressive.
		/// Upon exceeding this limit, new entities are not modeled. The default value is 4096.
		/// </summary>
		[DataMember(Name ="model_memory_limit")]
		string ModelMemoryLimit { get; set; }
	}

	/// <inheritdoc />
	public class AnalysisLimits : IAnalysisLimits
	{
		/// <inheritdoc />
		public long? CategorizationExamplesLimit { get; set; }

		/// <inheritdoc />
		public string ModelMemoryLimit { get; set; }
	}

	/// <inheritdoc />
	public class AnalysisLimitsDescriptor : DescriptorBase<AnalysisLimitsDescriptor, IAnalysisLimits>, IAnalysisLimits
	{
		long? IAnalysisLimits.CategorizationExamplesLimit { get; set; }
		string IAnalysisLimits.ModelMemoryLimit { get; set; }

		/// <inheritdoc />
		public AnalysisLimitsDescriptor CategorizationExamplesLimit(long? categorizationExamplesLimit) =>
			Assign(categorizationExamplesLimit, (a, v) => a.CategorizationExamplesLimit = v);

		/// <inheritdoc />
		public AnalysisLimitsDescriptor ModelMemoryLimit(string modelMemoryLimit) =>
			Assign(modelMemoryLimit, (a, v) => a.ModelMemoryLimit = v);
	}

	/// <summary>
	/// Limits applied for the resources required to hold the mathematical models in memory.
	/// </summary>
	[ReadAs(typeof(AnalysisMemoryLimit))]
	public interface IAnalysisMemoryLimit
	{
		/// <summary>
		/// The approximate maximum amount of memory resources that are required for analytical processing,
		/// in MiB. Once this limit is approached, data pruning becomes more aggressive.
		/// Upon exceeding this limit, new entities are not modeled. The default value is 4096.
		/// </summary>
		[DataMember(Name ="model_memory_limit")]
		string ModelMemoryLimit { get; set; }
	}

	/// <inheritdoc />
	public class AnalysisMemoryLimit : IAnalysisMemoryLimit
	{
		/// <inheritdoc />
		public string ModelMemoryLimit { get; set; }
	}

	/// <inheritdoc />
	public class AnalysisMemoryLimitDescriptor : DescriptorBase<AnalysisMemoryLimitDescriptor, IAnalysisMemoryLimit>, IAnalysisMemoryLimit
	{
		string IAnalysisMemoryLimit.ModelMemoryLimit { get; set; }

		/// <inheritdoc />
		public AnalysisMemoryLimitDescriptor ModelMemoryLimit(string modelMemoryLimit) =>
			Assign(modelMemoryLimit, (a, v) => a.ModelMemoryLimit = v);
	}
}
