Utilisation



Si besoin d'initialiser la base :

- add-migration
- update-database

Pour lancer chrome qui autorise le port pour les requetes lancer le .bat




Bien penser à regenerer le fichier swagger pour le front pour qu'il pointe vers l'url https://api.coco-health.com



















dans sparoot ClientApp/


build

npm install 


publish
 <Exec WorkingDirectory="$(SpaRoot)" Command="npm install" />
    <Exec WorkingDirectory="$(SpaRoot)" Command="npm run build -- --prod" />
    <Exec WorkingDirectory="$(SpaRoot)" Command="npm run build:ssr -- --prod" Condition=" '$(BuildServerSideRenderer)' == 'true' " />







<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
    <TypeScriptCompileBlocked>true</TypeScriptCompileBlocked>
    <TypeScriptToolsVersion>Latest</TypeScriptToolsVersion>
    <IsPackable>false</IsPackable>
    <SpaRoot>ClientApp\</SpaRoot>
    <DefaultItemExcludes>$(DefaultItemExcludes);$(SpaRoot)node_modules\**</DefaultItemExcludes>

    <!-- Set this to true if you enable server-side prerendering -->
    <PublishWithAspNetCoreTargetManifest>false</PublishWithAspNetCoreTargetManifest>
    <BuildServerSideRenderer>false</BuildServerSideRenderer>
  </PropertyGroup>

  <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
    <NoWarn>1701;1702;1705</NoWarn>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.All" Version="2.0.8" />
    <PackageReference Include="Microsoft.AspNetCore.SpaServices.Extensions" Version="2.0.0" />
    <PackageReference Include="Microsoft.NETCore.DotNetHostPolicy" Version="2.0.0" />
  </ItemGroup>

  <ItemGroup>
    <DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="2.0.1" />
  </ItemGroup>

  <ItemGroup>
    <!-- Don't publish the SPA source files, but do show them in the project files list -->
    <Content Remove="$(SpaRoot)**" />
    <None Include="$(SpaRoot)**" Exclude="$(SpaRoot)node_modules\**" />
  </ItemGroup>

  <ItemGroup>
    <None Remove="ClientApp\src\app\components\panel\admin\usermanager.component.ts" />
    <None Remove="ClientApp\src\app\components\panel\admin\usermanager\edit.usermanager.component.ts" />
    <None Remove="ClientApp\src\app\components\panel\admin\usermanager\new.usermanager.component.ts" />
    <None Remove="ClientApp\src\app\models\homepage.faq.model.ts" />
    <None Remove="ClientApp\src\app\models\homepage.memberinteam.model.ts" />
    <None Remove="ClientApp\src\app\models\linkwithname.model.ts" />
    <None Remove="ClientApp\src\app\services\context.service.ts" />
    <None Remove="ClientApp\src\app\services\cuturl.service.ts" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\weblib\weblib.csproj" />
  </ItemGroup>

  <ItemGroup>
    <None Update="ClientApp\favicon.ico">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
    <None Update="Data\cocohealth.db">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
    <None Update="SwaggerProject.xml">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
  </ItemGroup>

  <ItemGroup>
    <TypeScriptCompile Include="ClientApp\src\app\components\panel\admin\usermanager\edit.usermanager.component.ts" />
    <TypeScriptCompile Include="ClientApp\src\app\components\panel\admin\usermanager\new.usermanager.component.ts" />
    <TypeScriptCompile Include="ClientApp\src\app\components\panel\admin\usermanager\usermanager.component.ts" />
    <TypeScriptCompile Include="ClientApp\src\app\models\homepage.faq.model.ts" />
    <TypeScriptCompile Include="ClientApp\src\app\models\homepage.memberinteam.model.ts" />
    <TypeScriptCompile Include="ClientApp\src\app\models\linkwithname.model.ts" />
    <TypeScriptCompile Include="ClientApp\src\app\services\context.service.ts" />
    <TypeScriptCompile Include="ClientApp\src\app\services\cuturl.service.ts" />
  </ItemGroup>

  <ItemGroup>
    <Content Update="appsettings.Development.json">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="appsettings.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
    <Content Update="appsettings.Production.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
    <Content Update="hosting.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
  </ItemGroup>

  <Target Name="DebugEnsureNodeEnv" BeforeTargets="Build" Condition=" '$(Configuration)' == 'Debug' And !Exists('$(SpaRoot)node_modules') ">
    <!-- Ensure Node.js is installed -->
    <Exec Command="node --version" ContinueOnError="true">
      <Output TaskParameter="ExitCode" PropertyName="ErrorCode" />
    </Exec>
    <Error Condition="'$(ErrorCode)' != '0'" Text="Node.js is required to build and run this project. To continue, please install Node.js from https://nodejs.org/, and then restart your command prompt or IDE." />
    <Message Importance="high" Text="Restoring dependencies using 'npm'. This may take several minutes..." />
    <Exec WorkingDirectory="$(SpaRoot)" Command="npm install" />
  </Target>

  <Target Name="PublishRunWebpack" AfterTargets="ComputeFilesToPublish">
    <!-- As part of publishing, ensure the JS resources are freshly built in production mode -->
    <Exec WorkingDirectory="$(SpaRoot)" Command="npm install" />
    <Exec WorkingDirectory="$(SpaRoot)" Command="npm run build -- --prod" />
    <Exec WorkingDirectory="$(SpaRoot)" Command="npm run build:ssr -- --prod" Condition=" '$(BuildServerSideRenderer)' == 'true' " />

    <!-- Include the newly-built files in the publish output -->
    <ItemGroup>
      <DistFiles Include="$(SpaRoot)dist\**; $(SpaRoot)dist-server\**" />
      <DistFiles Include="$(SpaRoot)node_modules\**" Condition="'$(BuildServerSideRenderer)' == 'true'" />
      <ResolvedFileToPublish Include="@(DistFiles->'%(FullPath)')" Exclude="@(ResolvedFileToPublish)">
        <RelativePath>%(DistFiles.Identity)</RelativePath>
        <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
      </ResolvedFileToPublish>
    </ItemGroup>
  </Target>

</Project>
