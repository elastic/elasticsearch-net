using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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
		private readonly TValue _value;
		TValue IPromise<TValue>.Value => _value;

		protected DescriptorPromiseBase(TValue instance)
		{
			this._value = instance;
		}

		protected TDescriptor Assign(Action<TValue> assigner)
		{
			assigner(this._value);
			return (TDescriptor) this;
		}

		/// <summary>
		/// Hides the <see cref="Equals"/> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj) => base.Equals(obj);

		/// <summary>
		/// Hides the <see cref="GetHashCode"/> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode() => base.GetHashCode();

		/// <summary>
		/// Hides the <see cref="ToString"/> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString() => base.ToString();
	}
}