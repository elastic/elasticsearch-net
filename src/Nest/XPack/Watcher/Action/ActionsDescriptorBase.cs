/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;

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
