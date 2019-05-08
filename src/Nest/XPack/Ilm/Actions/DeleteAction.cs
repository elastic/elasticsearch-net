namespace Nest
{
	/// <summary>
	/// The Delete Action deletes the index.
	/// </summary>
	/// <remarks>
	/// Phases allowed: delete.
	/// </remarks>
	public interface IDeleteLifecycleAction : ILifecycleAction { }

	public class DeleteLifecycleAction : IDeleteLifecycleAction { }

	public class DeleteLifecycleActionDescriptor : DescriptorBase<DeleteLifecycleActionDescriptor, IDeleteLifecycleAction>, IDeleteLifecycleAction { }
}
