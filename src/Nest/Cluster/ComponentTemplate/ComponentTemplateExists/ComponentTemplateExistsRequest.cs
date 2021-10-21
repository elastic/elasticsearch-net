// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 namespace Nest
{
	/// <summary>
	/// Checks if a component template exists
	/// <para />
	/// Available in Elasticsearch 7.8.0+
	/// </summary>
	[MapsApi("cluster.exists_component_template.json")]
	public partial interface IComponentTemplateExistsRequest { }

	/// <inheritdoc cref="IComponentTemplateExistsRequest"/>
	public partial class ComponentTemplateExistsRequest { }

	/// <inheritdoc cref="IComponentTemplateExistsRequest"/>
	public partial class ComponentTemplateExistsDescriptor { }
}
