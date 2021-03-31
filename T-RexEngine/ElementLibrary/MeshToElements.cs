using System;
using System.Collections.Generic;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using T_RexEngine.Enums;
using Xbim.Ifc;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.StructuralElementsDomain;

namespace T_RexEngine.ElementLibrary
{
    public class MeshToElements: ElementGroup
    {
        public MeshToElements(string name, Mesh mesh, Material material, int type, List<Plane> insertPlanes)
        {
            Name = name;
            Mesh = mesh;
            Material = material;
            Amount = insertPlanes.Count;
            Volume = VolumeMassProperties.Compute(mesh).Volume;
            Mass = Volume * material.Density;
            InsertPlanes = insertPlanes;
            switch (type)
            {
                case 0:
                    ElementType = ElementType.PadFooting;
                    break;
                case 1:
                    ElementType = ElementType.StripFootings;
                    break;
                case 2:
                    ElementType = ElementType.Beams;
                    break;
                case 3:
                    ElementType = ElementType.Columns;
                    break;
                default:
                    throw new ArgumentException("Element type not recognized");
            }
            
            ResultMesh = new List<Mesh>();

            foreach (var plane in InsertPlanes)
            {
                Mesh duplicateMesh = Mesh.DuplicateMesh();
                Transform planeToPlane = Transform.PlaneToPlane(Plane.WorldXY, plane);
                duplicateMesh.Transform(planeToPlane);
                ResultMesh.Add(duplicateMesh);
            }
        }
        
        public override List<IfcReinforcingElement> ToReinforcingElementIfc(IfcStore model)
        {
            throw new ArgumentException("Mesh elements should be converted to IfcBuildingElement");
        }

        public override List<IfcBuildingElement> ToBuildingElementIfc(IfcStore model)
        {
            using (var transaction = model.BeginTransaction("Create Mesh Element"))
            {
                MeshFaceList faces = Mesh.Faces;
                MeshVertexList vertices = Mesh.Vertices;
                List<IfcCartesianPoint> ifcVertices = IfcTools.VerticesToIfcCartesianPoints(model, vertices);
                IfcFaceBasedSurfaceModel faceBasedSurfaceModel = IfcTools.CreateIfcFaceBasedSurfaceModel(model, faces, ifcVertices);
                var shape = IfcTools.CreateIfcShapeRepresentation(model, "Mesh");
                shape.Items.Add(faceBasedSurfaceModel);
                var ifcRelAssociatesMaterial = IfcTools.CreateIfcRelAssociatesMaterial(model, Material.Name, Material.Grade);
                var buildingElements = IfcTools.CreateBuildingElements(model, ElementType, Name, shape, InsertPlanes,
                    ifcRelAssociatesMaterial);

                transaction.Commit();
                
                return buildingElements;
            }
        }
        public string Name { get; }
        public Mesh Mesh { get; }
        public List<Mesh> ResultMesh { get; }
        public List<Plane> InsertPlanes { get; }
        
    }
}