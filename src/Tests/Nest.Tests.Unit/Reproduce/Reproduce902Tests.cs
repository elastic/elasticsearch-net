using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace Nest.Tests.Unit.Reproduce
{
	/// <summary>
	/// tests to reproduce reported errors
	/// </summary>
	[TestFixture]
	public class Reproduce902 : BaseJsonTests
	{
		[ElasticType(Name = "testclass")]
		public class TestClass
		{
			public int Id { get; set; }

			public NestedClass NestMe { get; set; }
		}

		public class NestedClass
		{
			public string NestedMultiField { get; set; }
		}

		public class ChildRecord
		{

		}

		/// <summary>
		/// https://github.com/Mpdreamz/NEST/issues/902
		/// </summary>
		[Test]
		public void Issue902()
		{
			var mapResult = this._client.Map<TestClass>(m => m
                .MapFromAttributes()
                .Properties(props => props
					.NestedObject<NestedClass>(nested => nested
						.Name(p=>p.NestMe)
						.Properties(nestedProps=>nestedProps
							.MultiField(s => s
								.Name(p => p.NestedMultiField)
								.Fields(fields => fields
									.String(ps => ps.Name(p => p.NestedMultiField.Suffix("raw")).Index(FieldIndexOption.NotAnalyzed))
									.String(ps => ps.Name(p => p.NestedMultiField).Index(FieldIndexOption.Analyzed))
								)
							)
						)
					)
                )
            );
			var json = mapResult.ConnectionStatus.Request;
			this.JsonEquals(json, MethodBase.GetCurrentMethod());
		}

	}
}
