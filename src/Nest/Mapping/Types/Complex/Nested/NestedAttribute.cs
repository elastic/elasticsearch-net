// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	public class NestedAttribute : ObjectAttribute, INestedProperty
	{
		public NestedAttribute() : base(FieldType.Nested) { }

		public bool IncludeInParent
		{
			get => Self.IncludeInParent.GetValueOrDefault();
			set => Self.IncludeInParent = value;
		}

		public bool IncludeInRoot
		{
			get => Self.IncludeInRoot.GetValueOrDefault();
			set => Self.IncludeInRoot = value;
		}

		bool? INestedProperty.IncludeInParent { get; set; }
		bool? INestedProperty.IncludeInRoot { get; set; }
		private INestedProperty Self => this;
	}
}
