using System;
using System.Collections.Generic;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using T_RexEngine.Interfaces;
using Xbim.Ifc;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.StructuralElementsDomain;
using Color = System.Drawing.Color;

namespace T_RexEngine.ElementLibrary
{
    public class MeshToElements: ElementGroup, IElementSetConvertable
    {
        private Mesh _mesh;
        public MeshToElements(string name, Mesh mesh, Material material, int type, List<Plane> insertPlanes)
        {
            Name = name;
            Mesh = mesh;
            Material = material;
            Amount = insertPlanes.Count;
            Volume = VolumeMassProperties.Compute(mesh).Volume * Amount;
            Mass = Volume * material.Density;
            InsertPlanes = insertPlanes;
            ElementType = IfcTools.IntToType(type);
            ResultMesh = new List<Mesh>();

            foreach (var plane in InsertPlanes)
            {
                Mesh duplicateMesh = Mesh.DuplicateMesh();
                Transform planeToPlane = Transform.PlaneToPlane(Plane.WorldXY, plane);
                duplicateMesh.Transform(planeToPlane);
                ResultMesh.Add(duplicateMesh);
            }
        }

        public override string ToString()
        {
            return $"Element Group{Environment.NewLine}" + $"Count: {Amount}";
        }

        public override BimElementSet ToElementSet()
        {
            Mesh triangulatedMesh = Mesh.DuplicateMesh();
            triangulatedMesh.Faces.ConvertQuadsToTriangles();
            return new BimElementSet(triangulatedMesh, InsertPlanes, Name, Color.White, new Dictionary<string, string>
            {
                {"Material Name", Material.Name},
                {"Material Grade", Material.Grade},
                {"Element Type", ElementType.ToString()},
            });
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

        public Mesh Mesh
        {
            get { return _mesh; }
            private set
            {
                if (!value.IsClosed)
                {
                    throw new ArgumentException("Mesh should be closed");
                }
                _mesh = value;
            }
        }

        public List<Mesh> ResultMesh { get; }
        public List<Plane> InsertPlanes { get; }
        
    }
}