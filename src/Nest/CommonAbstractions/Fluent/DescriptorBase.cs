using System;
using System.ComponentModel;

namespace Nest
{
	public interface IDescriptor { }

	public abstract class DescriptorBase<TDescriptor, TInterface> : IDescriptor
		where TDescriptor : DescriptorBase<TDescriptor, TInterface>, TInterface
		where TInterface : class
	{
		private readonly TDescriptor _self;

		protected DescriptorBase() => _self = (TDescriptor)this;

		protected TInterface Self => _self;

		/// <summary>
		///     Hides the <see cref="Equals" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		// ReSharper disable once BaseObjectEqualsIsObjectEquals
		//only used to hide by default
		public override bool Equals(object obj) => base.Equals(obj);

		/// <summary>
		///     Hides the <see cref="GetHashCode" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		// ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
		//only used to hide by default
		public override int GetHashCode() => base.GetHashCode();

		/// <summary>
		///     Hides the <see cref="ToString" /> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString() => base.ToString();

		protected TDescriptor Assign(Action<TInterface> assigner) => Fluent.Assign(_self, assigner);
	}
}
