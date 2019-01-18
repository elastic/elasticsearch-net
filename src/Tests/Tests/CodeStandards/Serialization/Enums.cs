using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.CodeStandards.Serialization
{
	public class Enums
	{
		[U]
		public void EnumsWithEnumMembersShouldBeMarkedWithStringEnumAttribute()
		{
			var exceptions = new HashSet<Type>
			{
				typeof(SimpleQueryStringFlags)
			};

			var enums = typeof(IElasticClient).Assembly()
				.GetTypes()
				.Where(t => t.IsEnum)
				.Except(exceptions)
				.ToList();
			var notMarkedStringEnum = new List<string>();
			foreach (var e in enums)
			{
				if (e.GetFields().Any(fi => fi.FieldType == e && fi.GetCustomAttribute<EnumMemberAttribute>() != null)
					&& e.GetCustomAttribute<StringEnumAttribute>() == null)
					notMarkedStringEnum.Add(e.Name);
			}
			notMarkedStringEnum.Should().BeEmpty();
		}
	}
}
