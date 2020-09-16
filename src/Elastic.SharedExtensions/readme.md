## Elastic.SharedExtensions

This project is built in isolation and never referenced directly.

It's internal extension methods are source included in those artifacts that need it.


```xml
  <ItemGroup>
    <Compile Include="..\Elastic.Extensions\*.cs">
      <Link>Extensions\*.cs</Link>
    </Compile>
  </ItemGroup>
```

This project is **not** a catchall for all extension methods. Feel free to create
extension methods that are only used by one assembly in said assembly.


If you want to place an extension method here that requires a NuGet package reference it's 99%
certain it should not live here!. 

That was it, safe journey perusing the rest of the codebase :cat_smile: