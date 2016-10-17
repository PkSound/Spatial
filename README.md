# Pk.Spatial
An opinionated adapter for [MathNet.Spatial](https://github.com/mathnet/mathnet-spatial) using [Units.Net](https://github.com/anjdreas/UnitsNet) .

# Why?
At PK Sound we need to be able to model physical systems of interconnected speaker cabinets in order to perform basic safety calculations. We then offer feedback to the technicians that use our speakers, so we also need to be able to localize these representations so we can talk feet and pounds in the US, while using meters and kg in Canada. Relying on comments and static documentation to keep track of units when representing the location of a speaker as {3.2, 4.8, 20.1} is just not good enough. If that last sentence felt awkward, then you understand the problem. Enter [Units.Net](https://github.com/anjdreas/UnitsNet) and [MathNet.Spatial](https://github.com/mathnet/mathnet-spatial).

[MathNet.Spatial](https://github.com/mathnet/mathnet-spatial) is a useful upcoming geometry library for .Net projects, but it lacks baked-in units. [Units.Net](https://github.com/anjdreas/UnitsNet) is a vibrant project that brings object orientation to unit conversion.

By using Units.Net and MathNet.Spatial together, new types such as Location3D can be created that offer type-safety and convenient unit conversion to projects that include geometric concerns. Now we know that our speaker is located at, say, {3.2 ft, 4.8 ft, 20.1 ft}. Better!

# Build Status
[![Build status](https://ci.appveyor.com/api/projects/status/jrhpgefql680lyb6/branch/develop?svg=true)](https://ci.appveyor.com/project/JKSnd/spatial/branch/develop)

# Deployment
There's currently a pre-release NuGet package up on Nuget. Please note that while we're in beta there may be breaking changes to the library's interfaces without notice. Once we've released 1.0.0 we promise we'll be very strict about backward-compatibility.

[![NuGet Pre Release](https://img.shields.io/nuget/vpre/pk.spatial.svg)](https://www.nuget.org/packages/Pk.Spatial/)

