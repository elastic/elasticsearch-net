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

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(LinearInterpolationSmoothingModel))]
	public interface ILinearInterpolationSmoothingModel : ISmoothingModel
	{
		[DataMember(Name = "bigram_lambda")]
		double? BigramLambda { get; set; }

		[DataMember(Name = "trigram_lambda")]
		double? TrigramLambda { get; set; }

		[DataMember(Name = "unigram_lambda")]
		double? UnigramLambda { get; set; }
	}

	public class LinearInterpolationSmoothingModel : SmoothingModelBase, ILinearInterpolationSmoothingModel
	{
		public double? BigramLambda { get; set; }
		public double? TrigramLambda { get; set; }
		public double? UnigramLambda { get; set; }

		internal override void WrapInContainer(ISmoothingModelContainer container) => container.LinearInterpolation = this;
	}

	public class LinearInterpolationSmoothingModelDescriptor
		: DescriptorBase<LinearInterpolationSmoothingModelDescriptor, ILinearInterpolationSmoothingModel>, ILinearInterpolationSmoothingModel
	{
		double? ILinearInterpolationSmoothingModel.BigramLambda { get; set; }
		double? ILinearInterpolationSmoothingModel.TrigramLambda { get; set; }
		double? ILinearInterpolationSmoothingModel.UnigramLambda { get; set; }

		public LinearInterpolationSmoothingModelDescriptor TrigramLambda(double? lambda) => Assign(lambda, (a, v) => a.TrigramLambda = v);

		public LinearInterpolationSmoothingModelDescriptor UnigramLambda(double? lambda) => Assign(lambda, (a, v) => a.UnigramLambda = v);

		public LinearInterpolationSmoothingModelDescriptor BigramLambda(double? lambda) => Assign(lambda, (a, v) => a.BigramLambda = v);
	}
}
