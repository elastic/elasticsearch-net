// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;

namespace Nest
{
	/// <inheritdoc cref="IAction"/>
	public abstract class ActionsDescriptorBase<TDescriptor, TInterface>
		: DescriptorBase<TDescriptor, TInterface>, IAction
		where TDescriptor : DescriptorBase<TDescriptor, TInterface>, TInterface
		where TInterface : class, IAction
	{
		private string _name;

		protected ActionsDescriptorBase(string name)
		{
			if (name == null) throw new ArgumentNullException(nameof(name));
			if (name.Length == 0) throw new ArgumentException("cannot be empty");

			_name = name;
		}

		protected abstract ActionType ActionType { get; }

		ActionType IAction.ActionType => ActionType;

		string IAction.Name
		{
			get => _name;
			set => _name = value;
		}

		Time IAction.ThrottlePeriod { get; set; }
		TransformContainer IAction.Transform { get; set; }
		ConditionContainer IAction.Condition { get; set; }
		string IAction.Foreach { get; set; }
		int? IAction.MaxIterations { get; set; }

		/// <inheritdoc cref="IAction.Transform"/>
		public TDescriptor Transform(Func<TransformDescriptor, TransformContainer> selector) =>
			Assign(selector.InvokeOrDefault(new TransformDescriptor()), (a, v) => a.Transform = v);

		/// <inheritdoc cref="IAction.Condition"/>
		public TDescriptor Condition(Func<ConditionDescriptor, ConditionContainer> selector) =>
			Assign(selector.InvokeOrDefault(new ConditionDescriptor()), (a, v) => a.Condition = v);

		/// <inheritdoc cref="IAction.ThrottlePeriod"/>
		public TDescriptor ThrottlePeriod(Time throttlePeriod) => Assign(throttlePeriod, (a, v) => a.ThrottlePeriod = v);

		/// <inheritdoc cref="IAction.Foreach"/>
		public TDescriptor Foreach(string @foreach) => Assign(@foreach, (a, v) => a.Foreach = v);

		/// <inheritdoc cref="IAction.MaxIterations"/>
		public TDescriptor MaxIterations(int? maxIterations) => Assign(maxIterations, (a,v) => a.MaxIterations = v);
	}
}
