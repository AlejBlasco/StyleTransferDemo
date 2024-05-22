# StyleTransferDemo

## Descripción

StyleTransferDemo es una solución que contiene dos proyectos diseñados para trabajar juntos en la implementación de estilos y componentes reutilizables en aplicaciones Blazor. El primer proyecto es una librería de clases Razor que encapsula estilos LESS y componentes Blazor, mientras que el segundo es una aplicación Blazor WASM que se encarga de renderizar los estilos y componentes del primero.

## Contenido de la Solución

### 1. StyleTransferDemo.Blazor

Este proyecto se encarga de:
- Contener los estilos LESS.
- Generar los estilos LESS a CSS en tiempo de compilación
- Definir componentes Blazor.
- Generar un paquete NuGet para facilitar su reutilización en otras aplicaciones.

#### Estructura del Proyecto

    StyleTransferDemo.Blazor
    - themes
      |- bootstrap
      |- components
         |- blazor (aquí estan los *.scss)
      |- fonts
      |- _bootstrap.scss (fichero principal bootstrap)
      |- default-dummy.scss (fichero de estilos)
    - controls
      |- clock.razor (componente Blazor)
      
#### Generar los estilos en tiempo de compilación
Para que el proyecto genere los estilos *.css en tiempo de compilación deberemos añadir el siguiente fragmento de código al fichero *.csproj.

    <!-- Compile SCSS files into CSS -->
    <Target Name="CompileGlobalSass" BeforeTargets="Compile">
    	<Message Text="Compiling global SCSS files" Importance="high" />
    	<Exec Command="sass themes/default-dummy.scss wwwroot/css/default-dummy.css --style=compressed --no-source-map" />
    </Target>

> **Nota**: Añadimos ***--style=compressed --no-source-map*** para que el estilo final sea minificado.

> **Nota 2**: El *.css final lo alojamos en wwwroot/css por facilidad.

#### Generar un Nuget

> **NOTA:** En este demo partimos de la base de que el desarrollador ya cuenta con una cuenta en [Nuget](https://www.nuget.org/) y ha
> configurado dicha cuenta con sus tokens para subir paquetes.

Revisamos en el *.csproj los datos con los que se generará el paquete.

    <!--Package Info-->
    <IsPackable>true</IsPackable>
    <PackageId>StyleTransferDemo.Blazor</PackageId>
    <Product>StyleTransferDemo.Blazor</Product>
    <Version>2.0.0</Version>
    <Copyright>ABlasco</Copyright>
    <Authors>ABlasco</Authors>
    <Description>Torrezno ipsum dolor amet sebo york cerveza pimentón. Solomillo sal pimentón secreto picadillo manteca soria.</Description>
    <PackageTags>blazor design components css</PackageTags>
    <PackageReadmeFile>README.md</PackageReadmeFile>
    <PackageProjectUrl>https://github.com/AlejBlasco</PackageProjectUrl>
    <PackageIcon>icon.png</PackageIcon>
    <PackageLicenseFile>LICENSE.txt</PackageLicenseFile>
    <Title>Style Transfer Demo </Title>
    <RepositoryUrl>https://github.com/AlejBlasco/StyleTransferDemo</RepositoryUrl>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>

Con el proyecto compilado, desplegamos un terminal para desarrolladores, nos posicionamos en la raiz del proyecto StyleTrasnferDemo.Blazor y lanzamos el siguiente comando.

    cd src/StyleTransferDemo.Blazor
    dotnet pack

Si nuestro proyecto no contiene errores, se habrá generado el *.nupkg pertinente en la carpeta de releases.

Ahora, con el terminal nos posicionamos en la carpeta de releases y lanzamos el el siguiente comando para pushear el paquete

Nota. Esto también se puede hacer a mano desde la propia web de Nuget

    cd src\StyleTransferDemo.Blazor\bin\Release
    dotnet nuget push StyleTransferDemo.Blazor.2.0.0.nupkg --api-key qz2jga8pl3dvn2akksyquwcs9ygggg4exypy3bhxy6w6x6 --source https://api.nuget.org/v3/index.json

Una vez nuestro paquete haya sido validado lo tendremos disponible para su instalación en otros proyectos.

VER IMAGEN

### 2. StyleTransferDemo.Demo

Este proyecto se encarga de:
- Referenciar el paquete NuGet generado en el anterior proyecto.
- Usar los estilos *.css generados en el anterior proyecto.
- Utilizar los controles *.razor generados en el anterior proyecto.

#### Estructura del Proyecto

    StyleTransferDemo.Blazor
    - Dependencias
      |- Paquetes
         |- StyleTransferDemo.Razor (2.x.x)
    - controls
      |- login.razor (componente Blazor)
    - Layout 
      |- ... (los default de la plantilla WASM)
    - Pages 
      |- Home.razor (default modificada para nuestros ejemplos)
      |- ... (las defeult de la plantilla WASM)

#### Añadir el paquete NuGet a nuestro proyecto

Una vez el paquete generado en el anterior proyecto pase a estar disponible, nos posicionaremos en el raiz del proyecto StyleTransferDemo.Demo y lanzaremos el siguiente comando para instalarlo.

    cd src\StyleTransferDemo.Demo
    dotnet add package StyleTransferDemo.Blazor --version 2.0.0

Al terminar la operación podremos ver el paquete correctamente añadido en las dependencias del proyecto.

VER IMAGEN

#### Utilizar hoja de estilos

Tan solo es necesario añadirla como una stylesheet mas a la página donde queramos usarla.

    <link rel="stylesheet" href="_content/StyleTransferDemo.Blazor/css/default-dummy.css">

> **NOTA**: siempre usar la nomenclatura _*content/[Id Paquete]/[path del fichero css dentro del paquete]*

### Utilizar componente Blazor
 
 Para utilizar un componente Blazor desde un NuGet tendremos que utilizarlo igual que si estuviera contenido dentro del mismo proyecto.

    <p>Esto es un control de este proyecto usando css del NuGet</p>
    <StyleTransferDemo.Demo.Controls.Login></StyleTransferDemo.Demo.Controls.Login>
    
    <br />
    
    <p>Esto es un control del NuGet</p>
    <StyleTransferDemo.Blazor.controls.Clock></StyleTransferDemo.Blazor.controls.Clock>

> **NOTA**: Se puede acortar el nombre haciendo uso del fichero _Imports.razor

### Resultado final
Si se han seguido todos los pasos, al ejecutar el proyecto StyleTransferDemo.Demo en la página de Home veremos el siguiente resultado

VER IMAGEN