using System;
using System.ComponentModel;

namespace Nest
{
	public interface IPromise<out TValue> where TValue : class
	{
		TValue Value { get; }
	}

	public abstract class DescriptorPromiseBase<TDescriptor, TValue> : IDescriptor, IPromise<TValue>
		where TDescriptor : DescriptorPromiseBase<TDescriptor, TValue>
		where TValue : class
	{
		internal readonly TValue PromisedValue;

		protected DescriptorPromiseBase(TValue instance) => PromisedValue = instance;

		TValue IPromise<TValue>.Value => PromisedValue;

		/// <summary>
		///     Hides the <see cref="Equals" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj) => base.Equals(obj);

		/// <summary>
		///     Hides the <see cref="GetHashCode" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode() => base.GetHashCode();

		/// <summary>
		///     Hides the <see cref="ToString" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString() => base.ToString();

		protected TDescriptor Assign(Action<TValue> assigner)
		{
			assigner(PromisedValue);
			return (TDescriptor)this;
		}
	}
}
