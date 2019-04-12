namespace Nest
{
	public interface IDeleteAction : ILifecycleAction { }

	public class DeleteAction : LifecycleActionBase, IDeleteAction
	{
		public DeleteAction() : base("delete"){ }
	}

	public class DeleteActionDescriptor : LifecycleActionDescriptorBase<DeleteActionDescriptor, IDeleteAction>, IDeleteAction
	{
		public DeleteActionDescriptor() : base("delete") { }
	}
}
