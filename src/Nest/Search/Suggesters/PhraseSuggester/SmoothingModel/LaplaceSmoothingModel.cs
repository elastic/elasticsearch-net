// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(LaplaceSmoothingModel))]
	public interface ILaplaceSmoothingModel : ISmoothingModel
	{
		[DataMember(Name ="alpha")]
		double? Alpha { get; set; }
	}

	public class LaplaceSmoothingModel : SmoothingModelBase, ILaplaceSmoothingModel
	{
		public double? Alpha { get; set; }

		internal override void WrapInContainer(ISmoothingModelContainer container) => container.Laplace = this;
	}

	public class LaplaceSmoothingModelDescriptor : DescriptorBase<LaplaceSmoothingModelDescriptor, ILaplaceSmoothingModel>, ILaplaceSmoothingModel
	{
		double? ILaplaceSmoothingModel.Alpha { get; set; }

		public LaplaceSmoothingModelDescriptor Alpha(double? alpha) => Assign(alpha, (a, v) => a.Alpha = v);
	}
}
