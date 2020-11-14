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

### Units

Grasshopper is unit-less, you can read more about it there: https://www.grasshopper3d.com/forum/topics/units-m-ft

So is the T-Rex. It means that you will have to choose your unit system by yourself. If you choose to use meters then make sure that every dimension is in meters.

## Properties

### About

Properties components will help you to put the informations about different properties, that will be used later to create different objects.

### Components

#### Cover Dimensions

This component will create an offset for Rebar Shapes components that require Rectangle as an input.

![CoverDimensions](Img\CoverDimensions.png)

To see how it works we have to look on a Rebar Shape result. If you plug 0 for all of the inputs, you will get this:

![CoverDimensions0](Img\CoverDimensions0.png)

So if you want to offset this stirrup / bar in the boundary rectangle, you will have to change a values:

![CoverDimension1](Img\CoverDimension1.png)

It works similar for all of the Rebar Shape components that require Rectangle input.

#### Material

Material will help you to add some informations about material of the objects.

![Material](Img\Material.png)

The important thing is density, as it will affect the result of calculating the weight of the objects.

**Check the Units chapter to find more informations.**

Basically, if you want to have a density 7850 kg/m^3 by plugging slider with the value 7850, then you have to make sure that all of the other dimensions are in meters. Then you will get the result of weight in kilograms.

If your whole model is in millimeters, and you still want to get the weight in kilograms, then you will have to plug the density in kg/mm^3.

#### Rebar Properties

This component will take Diameter and Material as an input.

![RebarProperties](Img\RebarProperties.png)

Then you can plug those properties to create Rebar Shapes objects.

## Rebar Shape

### About

Rebar Shape components will help you to create different shapes of the rebars. Those objects can be later used to create objects called Rebar Group.

Every component from that section will output Rebar Shape and Mesh. Mesh is a discrete model of the rebar (you can read more about it in the Introduction chapter). Rebar Shape will be useful to create Rebar Group.

#### Bending Roller Diameter

Not all of the components, but most of them, require Bending Roller Diameter as input. This value is required to make fillets of the polyline correctly. Look at the image below to see how it works:

![BendingRollerDiameter](Img\BendingRollerDiameter.png)

60 is just an example: the proper value is for you to decide, it is often described in different standards / documents, and it can depend on things like for example diameter of the rebar or other values.

### Components

#### Curve To Rebar Shape

This component converts curve to a Rebar Shape. You can basically draw any curve you'd like and convert it to the rebar.

![CurveToRebarShape](Img\CurveToRebarShape.png)

<img src="Img\CurveToRebarExample.png" alt="CurveToRebarExample" style="zoom:50%;" />

#### Polyline To Rebar Shape

This component converts polyline to Rebar Shape. It requires one more output then Curve To Rebar Shape: Bending Roller Diameter. So at first this polyline will be filleted, and then it will be translated to Rebar Shape.

![PolylineToRebarShape](Img\PolylineToRebarShape.png)

There are few requirements: polyline can't be straight and there need to be at least 3 points of polyline - those requirements are to make sure that it is possible to create fillets. If you want to draw a straight line, and turn it to Rebar Shape - use Curve To Rebar Shape component.

#### L-Bar Shape

![L-BarShape](Img\L-BarShape.png)

#### Spacer Shape

![SpacerShape](Img\SpacerShape.png)

<img src="Img\SpacerShape3d.png" alt="SpacerShape3d" style="zoom:67%;" />

#### Stirrup Shape

![StirrupShape](Img\StirrupShape.png)

Stirrup Shape requires additional info about the type of the hooks. Right now there are 2 types:

- Type = 0:

  ![StirrupShapeHook0](Img\StirrupShapeHook0.png)

- Type = 1:

  ![StirrupShapeHook1](Img\StirrupShapeHook1.png)

#### Rectangle To Line Bar Shape

#### Rectangle To Stirrup Shape

#### Rectangle To U-Bar Shape

## Rebar Spacing

## Tools

