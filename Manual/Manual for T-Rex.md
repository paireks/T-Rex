# Manual for T-Rex 1.0.0

[TOC]

## Introduction

### Info

<img src="Img\TRexLogoOk.png" alt="TRexLogoOk" style="zoom:67%;" />

Hello! Thank you for using T-Rex! ;)

T-Rex is an open-source plug-in for Grasshopper.

**Purpose: **T-Rex can help you to create parametric models of reinforced concrete structures with Grasshopper.

**Requirements:** Rhino 6

**Contact:** If you have any specific questions, email me: w.radaczynski@gmail.com

**Website:** www.code-structures.com

**Tutorials:** You can find many tutorials on my YT channel here: https://www.youtube.com/channel/UCfXkMo1rOMhKGBoNwd7JPsw

### License (MIT License)

Copyright © 2020 Wojciech Radaczyński

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
associated documentation files (the "Software"), to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to
do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or
substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN
AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

### How does T-Rex work

T-Rex will help you to model thousands of the rebars with Grasshopper. The secret of it's speed is in the discrete mesh representation of the rebars.

Basically if you want to model a pipe, that will represent a single bar, using only built-in components: you'll probably choose "Pipe" component. Pipe component is really precise (as all of the NURBS-based components), the baked model of the pipe would be lightweight, but the problem is: it's also will be pretty slow. It will be visible if you decide to create, let's say, thousands of them. The more complex the shape of the bar is, the more time it will take to create a preview.

T-Rex will create a discrete mesh model of the pipe, which is **less precise**, but it will render **much quicker**, which is a huge advantage when we talk about parametric modeling, that should be executed just-in-time.

<img src="Img\MeshNurbs.png" alt="MeshNurbs" style="zoom:80%;" />

You can see the difference in precision on the image above. Let's look at the speed:![MeshSpeed](Img\MeshSpeed.png)

Meshes even for 10000 rebars works pretty fast. Making the same with the pipes makes slider freeze for some time, depending on the shape of the pipe.

**Important note:** Discrete models are problematic when you will try to measure things on your own. For example, if you decide to measure the volume by taking a mesh, then you'll get wrong results:

<img src="Img\MeshNurbsVolume.png" alt="MeshNurbsVolume" style="zoom:67%;" />

Volume of rebar with diameter: 8 and length: 100 should be around 5026.5482. As you can see Pipe component will give you a precise model and the result is close enough, but the discrete mesh result is far from the truth. That's why you shouldn't measure the mesh results. You should use proper components that calculate it by themselves:

![ComponentVolume](Img\ComponentVolume.png)

## Properties

## Rebar Shape

## Rebar Spacing

## Tools

