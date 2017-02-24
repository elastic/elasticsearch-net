using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public interface ISmoothingModel {}

	public abstract class SmoothingModelBase
	{
		public static implicit operator SmoothingModelContainer(SmoothingModelBase model) => model == null
			? null
			: new SmoothingModelContainer(model);

		internal abstract void WrapInContainer(ISmoothingModelContainer container);
	}
}
