using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface ITransform { }

	public abstract class TransformBase
	{
		public static implicit operator TransformContainer(TransformBase transform) => transform == null
			? null
			: new TransformContainer(transform);

		internal abstract void WrapInContainer(ITransformContainer container);
	}
}
