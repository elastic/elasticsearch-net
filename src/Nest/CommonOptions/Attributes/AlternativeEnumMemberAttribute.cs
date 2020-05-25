// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;

namespace Nest
{
	/// <summary>
	/// Similar to <see cref="System.Runtime.Serialization.EnumMemberAttribute" />, but allows an alternative string
	/// value to be specified for an enum field value.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class AlternativeEnumMemberAttribute : Attribute
	{
		public AlternativeEnumMemberAttribute(string value) => Value = value;

		public string Value { get; }
	}
}
