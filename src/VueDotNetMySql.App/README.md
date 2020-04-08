# ASP.NET Core & Vue.js Starter

Starter Template for ASP.NET Core and Vue.JS (Vue) - with Webpack (with HMR), Web API, Vuex state manangement and other best-practices baked in!

> Written in ES6, TypeScript version coming soon!

[![Nuget](https://img.shields.io/nuget/v/aspnetcore-vuejs.svg?style=for-the-badge&color=5b1096)](https://www.nuget.org/packages/aspnetcore-vuejs/)
[![Nuget Downloads](https://img.shields.io/nuget/dt/aspnetcore-vuejs.svg?label=Nuget%20Downloads&style=for-the-badge&color=b31ae7)](https://www.nuget.org/packages/aspnetcore-vuejs/)
[![MIT License](https://img.shields.io/badge/license-MIT-blue.svg?style=for-the-badge&color=e51384)](/LICENSE)

---

[![Trilon Consulting](https://trilon.io/trilon-logo-clear.png)](https://trilon.io)


### Made with love by [Trilon.io](https://trilon.io)

---

# Features

- **ASP.NET Core 2.2**
  - Web API
- **VueJS 2**
  - Vuex (State Store)
- **Webpack**
  - HMR (Hot Module Replacement/Reloading)
- **Bootstrap 4**

# Prerequisites:
 * [.Net Core 2.2](https://www.microsoft.com/net/download/windows)
 * [NodeJS](https://nodejs.org/) >= 10.x
 * [VSCode](https://code.visualstudio.com/) (ideally), or VS2017

# Installation:

### Nuget | Dotnet Templates

Find the template through NuGet package manager inside Visual Studio or [here](https://www.nuget.org/packages/aspnetcore-vuejs)

> Or download it via dotnet templates

```ts
// Make a directory where you want the project
mkdir my-vue-starter && cd my-vue-starter

// Download the dotnet template
dotnet new -i aspnetcore-vuejs

// Run and install the template
dotnet new vuejs

// Make sure you install the dependencies
npm install
```

Now you can open the project via Visual Studio or VSCode, press F5 to run the application!

Note:

* This will automatically run `dotnet restore` unless you install with `dotnet new vuejs --skipRestore`
* ([Official documentation](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new?tabs=netcore2x))
   * This will automatically run `dotnet restore` unless you install with `dotnet new vuejs --skipRestore`

### -OR- Clone this Repo itself

 * Clone this repository : `$ git clone https://github.com/harendra1993/aspnetcore-Vue-starter.git VueWeb`
 * `$ cd VueWeb/content`
 * `$ dotnet restore && npm install`
 * (If using VSCode) `$ code .`
 * (If using Visual Studio) Open the `*.sln` file with "Open project" from Visual Studio IDE


## Start the application:
You have two choices when it come at how your preffer to run it. You can either use the command line or the build-in run command.

### 1. Using the command line
Run the application using `dotnet run` or `npm run dev`
- note `dotnet run` should be run in `Development` environment for hot reloading. This setting can be set either within the command line or via the `launchSettings.json` available in the `Properties` folder.

### 2. Using the built-in run command
Run the application in VSCode or Visual Studio 2017 by hitting `F5`.

## View your application running
When running the app using debug menu or `F5` VS open auto the app in the browser;



