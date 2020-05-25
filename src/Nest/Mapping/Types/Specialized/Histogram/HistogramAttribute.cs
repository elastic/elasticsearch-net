// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	public class HistogramAttribute : ElasticsearchPropertyAttributeBase, IHistogramProperty
	{
		public HistogramAttribute() : base(FieldType.Histogram) { }
		
		/// <inheritdoc cref="IHistogramProperty.IgnoreMalformed"/>
		public bool IgnoreMalformed
		{
			get => Self.IgnoreMalformed.GetValueOrDefault();
			set => Self.IgnoreMalformed = value;
		}
		
		bool? IHistogramProperty.IgnoreMalformed { get; set; }

		private IHistogramProperty Self => this;
	}
}
