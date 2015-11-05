using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IHoltLinearModel : IMovingAverageModel
	{
		[JsonProperty("alpha")]
		float? Alpha { get; set; }

		[JsonProperty("beta")]
		float? Beta { get; set; }
	}

	public class HoltLinearModel : IHoltLinearModel
	{
		string IMovingAverageModel.Name { get; } = "holt";

		public float? Alpha { get; set; }
		public float? Beta { get; set; }
	}

	public class HoltLinearModelDescriptor
		: DescriptorBase<HoltLinearModelDescriptor, IHoltLinearModel>, IHoltLinearModel
	{
		string IMovingAverageModel.Name { get; } = "holt";
		float? IHoltLinearModel.Alpha { get; set; }
		float? IHoltLinearModel.Beta { get; set; }

		public HoltLinearModelDescriptor Alpha(float alpha) => Assign(a => a.Alpha = alpha);

		public HoltLinearModelDescriptor Beta(float beta) => Assign(a => a.Beta = beta);
	}
}
