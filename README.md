# T-Rex
Parametric Reinforcement Plug-in for Grasshopper.

Introduction video: https://youtu.be/r2GVTfjJHdA 


## Purpose

T-Rex can help you to create 3D models of reinforced concrete structures with Grasshopper.

## How it works?

Basically T-Rex can help you to model different rebars, which are represented by meshes, which allows real-time generation of model even for thousands of bars.

Check Manual here: https://code-structures.com/T_Rex_Manual/Manual%20for%20T-Rex.html

## Features (0.3.0) BETA


- Concrete: Profile, Profile To Elements, Mesh To Elements, Element Group Info
- IFC: Create IFC
- Rebar Shapes from curves: Curve To Rebar, Polyline To Rebar
- Rebar Shapes from rectangle: Rectangle To Line Bar Shape, Rectangle To U-Bar Shape, Rectangle To Stirrup Shape
- Rebar Shapes library: L-Bar Shape, Stirrup Shape, Spacer Shape
- Rebar Spacings: Curve Spacing, Custom Spacing, Vector Count Spacing, Vector Length Spacing, Rebar Group Info
- Properties: Bending Roller, Cover Dimensions, Material, Rebar Properties

## License (MIT License)

Copyright © 2023 Wojciech Radaczyński

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

## About the code

Code is not designed to be used outside Grasshopper environment.

xbim library was used to allow IFCs creation: https://docs.xbim.net/, license: https://docs.xbim.net/license/license.html.

xUnit library was used to unit-test the project.

Brontosaurus plug-in was used to create automatic tests inside Grasshopper.

## Contact

You can email me anytime: [w.radaczynski@gmail.com](mailto:w.radaczynski@gmail.com)

