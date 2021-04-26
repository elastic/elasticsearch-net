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
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(BucketScriptAggregation))]
	public interface IBucketScriptAggregation : IPipelineAggregation
	{
		[DataMember(Name ="script")]
		IScript Script { get; set; }
	}

	public class BucketScriptAggregation
		: PipelineAggregationBase, IBucketScriptAggregation
	{
		internal BucketScriptAggregation() { }

		public BucketScriptAggregation(string name, MultiBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		public IScript Script { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.BucketScript = this;
	}

	public class BucketScriptAggregationDescriptor
		: PipelineAggregationDescriptorBase<BucketScriptAggregationDescriptor, IBucketScriptAggregation, MultiBucketsPath>
			, IBucketScriptAggregation
	{
		IScript IBucketScriptAggregation.Script { get; set; }

		public BucketScriptAggregationDescriptor Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		public BucketScriptAggregationDescriptor Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		public BucketScriptAggregationDescriptor BucketsPath(Func<MultiBucketsPathDescriptor, IPromise<IBucketsPath>> selector) =>
			Assign(selector, (a, v) => a.BucketsPath = v?.Invoke(new MultiBucketsPathDescriptor())?.Value);
	}
}
