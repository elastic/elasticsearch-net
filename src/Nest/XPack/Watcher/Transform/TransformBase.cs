using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface ITransform {}

	public abstract class TransformBase
	{
		public static implicit operator TransformContainer(TransformBase transform) => transform == null
			? null
			: new TransformContainer(transform);

		internal abstract void WrapInContainer(ITransformContainer container);
	}
}
