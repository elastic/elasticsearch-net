// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 namespace Nest
{
	/// <summary>
	/// Gets component templates
	/// <para />
	/// Available in Elasticsearch 7.8.0+
	/// </summary>
	[MapsApi("cluster.get_component_template.json")]
	public partial interface IGetComponentTemplateRequest { }

	/// <inheritdoc cref="IGetComponentTemplateRequest"/>
	public partial class GetComponentTemplateRequest { }

	/// <inheritdoc cref="IGetComponentTemplateRequest"/>
	public partial class GetComponentTemplateDescriptor { }
}
