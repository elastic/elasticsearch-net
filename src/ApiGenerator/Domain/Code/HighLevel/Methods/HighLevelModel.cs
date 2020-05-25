// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace ApiGenerator.Domain.Code.HighLevel.Methods 
{
	public class HighLevelModel
	{
		public CsharpNames CsharpNames { get; set; }
		public FluentMethod Fluent { get; set; }
		public BoundFluentMethod FluentBound { get; set; }
		public InitializerMethod Initializer { get; set; }

	}
}