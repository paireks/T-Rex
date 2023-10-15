using System;
using System.Collections.Generic;
using dotbim;
using T_RexEngine.Interfaces;
using Rhino.Geometry;
using Mesh = Rhino.Geometry.Mesh;

namespace T_RexEngine
{
    public static class Tools
    {
        private static List<double> GetBimVerticesCoordinatesFromRhinoMesh(Mesh rhinoMesh)
        {
            List<double> verticesCoordinates = new List<double>();

            foreach (var rhinoMeshVertex in rhinoMesh.Vertices)
            {
                verticesCoordinates.AddRange(new List<double>
                {
                    rhinoMeshVertex.X,
                    rhinoMeshVertex.Y,
                    rhinoMeshVertex.Z
                });
            }

            return verticesCoordinates;
        }

        private static Color GetBimColorFromDrawingColor(System.Drawing.Color drawingColor)
        {
            Color color = new Color
            {
                R = drawingColor.R,
                G = drawingColor.G,
                B = drawingColor.B,
                A = drawingColor.A
            };

            return color;
        }

        private static List<int> GetBimFacesIdsFromRhinoMesh(Mesh rhinoMesh)
        {
            List<int> facesIds = new List<int>();

            for (int i = 0; i < rhinoMesh.Faces.Count; i++)
            {
                var rhinoMeshFace = rhinoMesh.Faces[i];
                if (rhinoMeshFace.C != rhinoMeshFace.D)
                {
                    throw new ArgumentException("Face index: " + i + " is not triangular. Triangulate mesh.");
                }
                facesIds.AddRange(new List<int>
                {
                    rhinoMeshFace.A, rhinoMeshFace.B, rhinoMeshFace.C
                });
            }

            return facesIds;
        }

        private static dotbim.Mesh CreateBimMeshFromRhinoMesh(Mesh mesh, int meshId)
        {
            dotbim.Mesh bimMesh = new dotbim.Mesh
            {
                MeshId = meshId,
                Coordinates = GetBimVerticesCoordinatesFromRhinoMesh(mesh),
                Indices = GetBimFacesIdsFromRhinoMesh(mesh)
            };

            return bimMesh;
        }

        public static File CreateFile(List<IElementSetConvertable> elementSetConvertables, Dictionary<string, string> info, double scaleFactor = 1.0)
        {
            var scale = Transform.Scale(Point3d.Origin, scaleFactor);
            List<dotbim.Mesh> meshes = new List<dotbim.Mesh>();
            List<Element> elements = new List<Element>();

            int currentMeshId = 0;
            foreach (var elementSetConvertable in elementSetConvertables)
            {
                BimElementSet bimElementSet = elementSetConvertable.ToElementSet();
                var scaledMesh = bimElementSet.Mesh.DuplicateMesh();
                scaledMesh.Transform(scale);
                meshes.Add(CreateBimMeshFromRhinoMesh(scaledMesh, currentMeshId));
                for (int i = 0; i < bimElementSet.InsertPlanes.Count; i++)
                {
                    var scaledPlane = bimElementSet.InsertPlanes[i].Clone();
                    scaledPlane.Transform(scale);
                    Element element = new Element
                    {
                        MeshId = currentMeshId,
                        Vector = ConvertInsertPlaneToVector(scaledPlane),
                        Rotation = ConvertInsertPlaneToRotation(scaledPlane),
                        Color = GetBimColorFromDrawingColor(bimElementSet.Colors[i]),
                        Guid = bimElementSet.Guids[i],
                        Info = bimElementSet.Infos[i],
                        Type = bimElementSet.Types[i]
                    };
                    
                    elements.Add(element);
                }

                currentMeshId += 1;
            }
            
            File file = new File
            {
                SchemaVersion = "1.0.0",
                Meshes = meshes,
                Elements = elements,
                Info = info
            };

            return file;
        }

        private static Vector ConvertInsertPlaneToVector(Plane plane)
        {
            var p1 = Plane.WorldXY.Origin;
            var p2 = plane.Origin;
            
            return new Vector{X = p2.X - p1.X, Y = p2.Y - p1.Y, Z = p2.Z - p1.Z};
        }

        private static Rotation ConvertInsertPlaneToRotation(Plane plane)
        {
            Quaternion quaternion = new Quaternion();
            quaternion.SetRotation(Plane.WorldXY, plane);

            return new Rotation{Qx = quaternion.B, Qy = quaternion.C, Qz = quaternion.D, Qw = quaternion.A};
        }
    }
}