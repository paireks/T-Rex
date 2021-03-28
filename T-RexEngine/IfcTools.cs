using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using Xbim.Ifc;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.MaterialResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.RepresentationResource;
using Xbim.Ifc4.TopologyResource;

namespace T_RexEngine
{
    public static class IfcTools
    {
        public static IfcLocalPlacement CreateLocalPlacement(IfcStore model, Plane insertPlane)
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
    }
}