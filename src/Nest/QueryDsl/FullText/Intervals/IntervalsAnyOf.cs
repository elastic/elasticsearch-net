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
	/// A rule that emits intervals produced by any of its sub-rules.
	/// </summary>
	[ReadAs(typeof(IntervalsAnyOf))]
	public interface IIntervalsAnyOf : IIntervals
	{
		/// <summary>
		/// An array of rules to match.
		/// </summary>
		[DataMember(Name = "intervals")]
		IEnumerable<IntervalsContainer> Intervals { get; set; }
	}

	/// <inheritdoc cref="IIntervalsAnyOf" />
	public class IntervalsAnyOf : IntervalsBase, IIntervalsAnyOf
	{
		/// <inheritdoc />
		public IEnumerable<IntervalsContainer> Intervals { get; set; }

		internal override void WrapInContainer(IIntervalsContainer container) => container.AnyOf = this;
	}

	/// <inheritdoc cref="IIntervalsAnyOf" />
	public class IntervalsAnyOfDescriptor : IntervalsDescriptorBase<IntervalsAnyOfDescriptor, IIntervalsAnyOf>, IIntervalsAnyOf
	{
		IEnumerable<IntervalsContainer> IIntervalsAnyOf.Intervals { get; set; }

		/// <inheritdoc cref="IIntervalsAnyOf.Intervals" />
		public IntervalsAnyOfDescriptor Intervals(Func<IntervalsListDescriptor, IPromise<List<IntervalsContainer>>> selector) =>
			Assign(selector, (a, v) => a.Intervals = v.InvokeOrDefault(new IntervalsListDescriptor())?.Value);
	}
}
