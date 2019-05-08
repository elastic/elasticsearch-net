namespace Nest
{
	/// <summary>
	/// This action will set the index to be read-only.
	/// </summary>
	/// <remarks>
	/// Phases allowed: warm.
	/// </remarks>
	public interface IReadOnlyLifecycleAction : ILifecycleAction { }

	public class ReadOnlyLifecycleAction : IReadOnlyLifecycleAction { }

	public class ReadOnlyLifecycleActionDescriptor
		: DescriptorBase<ReadOnlyLifecycleActionDescriptor, IReadOnlyLifecycleAction>, IReadOnlyLifecycleAction { }
}
