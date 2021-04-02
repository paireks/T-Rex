using System;
using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using T_RexEngine.Enums;
using Xbim.Ifc;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.MaterialResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.RepresentationResource;
using Xbim.Ifc4.SharedBldgElements;
using Xbim.Ifc4.StructuralElementsDomain;
using Xbim.Ifc4.TopologyResource;

namespace T_RexEngine
{
    public static class IfcTools
    {
        public static ElementType IntToType(int integerType)
        {
            ElementType type;
            
            switch (integerType)
            {
                case 0:
                    type = ElementType.PadFooting;
                    break;
                case 1:
                    type = ElementType.StripFootings;
                    break;
                case 2:
                    type = ElementType.Beams;
                    break;
                case 3:
                    type = ElementType.Columns;
                    break;
                default:
                    throw new ArgumentException("Element type not recognized");
            }

            return type;
        }

        private static IfcLocalPlacement CreateLocalPlacement(IfcStore model, Plane insertPlane)
        {
            var localPlacement = model.Instances.New<IfcLocalPlacement>();
            var ax3D = model.Instances.New<IfcAxis2Placement3D>();
            
            var location = model.Instances.New<IfcCartesianPoint>();
            location.SetXYZ(insertPlane.OriginX, insertPlane.OriginY, insertPlane.OriginZ);
            ax3D.Location = location;

            ax3D.RefDirection = model.Instances.New<IfcDirection>();
            ax3D.RefDirection.SetXYZ(insertPlane.XAxis.X, insertPlane.XAxis.Y, insertPlane.XAxis.Z);
            ax3D.Axis = model.Instances.New<IfcDirection>();
            ax3D.Axis.SetXYZ(insertPlane.ZAxis.X, insertPlane.ZAxis.Y, insertPlane.ZAxis.Z);
            localPlacement.RelativePlacement = ax3D;

            return localPlacement;
        }

        public static List<IfcCartesianPoint> VerticesToIfcCartesianPoints(IfcStore model, MeshVertexList vertices)
        {
            List<IfcCartesianPoint> ifcCartesianPoints = new List<IfcCartesianPoint>();
            
            foreach (var vertex in vertices)
            {
                IfcCartesianPoint currentVertex = model.Instances.New<IfcCartesianPoint>();
                currentVertex.SetXYZ(vertex.X, vertex.Y, vertex.Z);
                ifcCartesianPoints.Add(currentVertex);
            }

            return ifcCartesianPoints;
        }
        
        public static List<IfcCartesianPoint> PointsToIfcCartesianPoints(IfcStore model, List<Point3d> points, bool closeShape)
        {
            List<IfcCartesianPoint> ifcCartesianPoints = new List<IfcCartesianPoint>();
            
            foreach (var point in points)
            {
                IfcCartesianPoint currentVertex = model.Instances.New<IfcCartesianPoint>();
                currentVertex.SetXYZ(point.X, point.Y, point.Z);
                ifcCartesianPoints.Add(currentVertex);
            }

            if (closeShape)
            {
                IfcCartesianPoint currentVertex = model.Instances.New<IfcCartesianPoint>();
                currentVertex.SetXYZ(points[0].X, points[0].Y, points[0].Z);
                ifcCartesianPoints.Add(currentVertex);
            }

            return ifcCartesianPoints;
        }

        public static IfcFaceBasedSurfaceModel CreateIfcFaceBasedSurfaceModel(IfcStore model, MeshFaceList faces,
            List<IfcCartesianPoint> ifcVertices)
        {
            var faceSet = model.Instances.New<IfcConnectedFaceSet>();

            foreach (var meshFace in faces)
            {
                List<IfcCartesianPoint> points = new List<IfcCartesianPoint>
                {
                    ifcVertices[meshFace.A], ifcVertices[meshFace.B], ifcVertices[meshFace.C]
                };
                if (meshFace.C != meshFace.D)
                {
                    points.Add(ifcVertices[meshFace.D]);
                }
                    
                var polyLoop = model.Instances.New<IfcPolyLoop>();
                polyLoop.Polygon.AddRange(points);
                var bound = model.Instances.New<IfcFaceOuterBound>();
                bound.Bound = polyLoop;
                var face = model.Instances.New<IfcFace>();
                face.Bounds.Add(bound);
                
                faceSet.CfsFaces.Add(face);
            }
            
            var faceBasedSurfaceModel = model.Instances.New<IfcFaceBasedSurfaceModel>();
            faceBasedSurfaceModel.FbsmFaces.Add(faceSet);

            return faceBasedSurfaceModel;
        }

        public static IfcShapeRepresentation CreateIfcShapeRepresentation(IfcStore model, string representationType)
        {
            var shape = model.Instances.New<IfcShapeRepresentation>();
            var modelContext = model.Instances.OfType<IfcGeometricRepresentationContext>().FirstOrDefault();
            shape.ContextOfItems = modelContext;
            shape.RepresentationType = representationType;
            shape.RepresentationIdentifier = representationType;

            return shape;
        }

        public static IfcRelAssociatesMaterial CreateIfcRelAssociatesMaterial(IfcStore model, string name, string grade)
        {
            var material = model.Instances.New<IfcMaterial>();
            material.Category = name;
            material.Name = grade;
            IfcRelAssociatesMaterial ifcRelAssociatesMaterial = model.Instances.New<IfcRelAssociatesMaterial>();
            ifcRelAssociatesMaterial.RelatingMaterial = material;

            return ifcRelAssociatesMaterial;
        }

        private static void ApplyRepresentationAndPlacement(IfcStore model, IfcBuildingElement element, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var representation = model.Instances.New<IfcProductDefinitionShape>();
            representation.Representations.Add(shape);
            element.Representation = representation;
                            
            var localPlacement = IfcTools.CreateLocalPlacement(model, insertPlane);
            element.ObjectPlacement = localPlacement;
        }

        private static IfcFooting CreateFooting(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var footing = model.Instances.New<IfcFooting>();
            footing.Name = name;

            switch (type)
            {
                case ElementType.PadFooting:
                    footing.PredefinedType = IfcFootingTypeEnum.PAD_FOOTING;
                    break;
                case ElementType.StripFootings:
                    footing.PredefinedType = IfcFootingTypeEnum.STRIP_FOOTING;
                    break;
                default:
                    throw new ArgumentException("Footing type not recognized, can be only Pad or Strip");
            }
                            
            ApplyRepresentationAndPlacement(model, footing, shape, insertPlane);

            return footing;
        }

        private static IfcBeam CreateBeam(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var beam = model.Instances.New<IfcBeam>();
            beam.Name = name;
            beam.PredefinedType = IfcBeamTypeEnum.BEAM;
                            
            ApplyRepresentationAndPlacement(model, beam, shape, insertPlane);

            return beam;
        }

        private static IfcColumn CreateColumn(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var column = model.Instances.New<IfcColumn>();
            column.Name = name;
            column.PredefinedType = IfcColumnTypeEnum.COLUMN;
                            
            ApplyRepresentationAndPlacement(model, column, shape, insertPlane);

            return column;
        }

        public static List<IfcBuildingElement> CreateBuildingElements(IfcStore model, ElementType type, string name,
            IfcShapeRepresentation shape, List<Plane> insertPlanes, IfcRelAssociatesMaterial relAssociatesMaterial)
        {
            var buildingElements = new List<IfcBuildingElement>();

            switch (type)
            {
                case ElementType.PadFooting:
                case ElementType.StripFootings:
                {
                    foreach (var insertPlane in insertPlanes)
                    {
                        var footing = CreateFooting(model, type, name, shape, insertPlane);
                        relAssociatesMaterial.RelatedObjects.Add(footing);
                        buildingElements.Add(footing);
                    }
                    break;
                }
                case ElementType.Beams:
                {
                    foreach (var insertPlane in insertPlanes)
                    {
                        var beam = CreateBeam(model, name, shape, insertPlane);
                        relAssociatesMaterial.RelatedObjects.Add(beam);
                        buildingElements.Add(beam); 
                    }
                    break;
                }
                case ElementType.Columns:
                {
                    foreach (var insertPlane in insertPlanes)
                    {
                        var column = CreateColumn(model, name, shape, insertPlane);
                        relAssociatesMaterial.RelatedObjects.Add(column);
                        buildingElements.Add(column);
                    }
                    break;
                }
                default:
                    throw new ArgumentException("Element type not recognized");
            }

            return buildingElements;
        }
        
        public static List<IfcBuildingElement> CreateBuildingElements(IfcStore model, ElementType type, string name,
            List<IfcShapeRepresentation> shapes, List<Plane> insertPlanes, IfcRelAssociatesMaterial relAssociatesMaterial)
        {
            var buildingElements = new List<IfcBuildingElement>();

            switch (type)
            {
                case ElementType.PadFooting:
                case ElementType.StripFootings:
                {
                    for (int i = 0; i < insertPlanes.Count; i++)
                    {
                        var footing = CreateFooting(model, type, name, shapes[i], insertPlanes[i]);
                        relAssociatesMaterial.RelatedObjects.Add(footing);
                        buildingElements.Add(footing);
                    }
                    break;
                }
                case ElementType.Beams:
                {
                    for (int i = 0; i < insertPlanes.Count; i++)
                    {
                        var beam = CreateBeam(model, name, shapes[i], insertPlanes[i]);
                        relAssociatesMaterial.RelatedObjects.Add(beam);
                        buildingElements.Add(beam); 
                    }
                    break;
                }
                case ElementType.Columns:
                {
                    for (int i = 0; i < insertPlanes.Count; i++)
                    {
                        var column = CreateColumn(model, name, shapes[i], insertPlanes[i]);
                        relAssociatesMaterial.RelatedObjects.Add(column);
                        buildingElements.Add(column);
                    }
                    break;
                }
                default:
                    throw new ArgumentException("Element type not recognized");
            }

            return buildingElements;
        }
    }
}