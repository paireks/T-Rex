using System;
using System.Collections.Generic;
using System.Linq;
using T_RexEngine.Interfaces;
using Rhino.Geometry;
using Mesh = Rhino.Geometry.Mesh;

namespace T_RexEngine
{
    public class BimElementSet : IElementSetConvertable
    {
        public BimElementSet(Mesh mesh, List<Plane> insertPlanes, List<string> guids,
            List<string> types, List<System.Drawing.Color> colors, List<Dictionary<string, string>> infos)
        {
            Mesh = mesh;
            InsertPlanes = insertPlanes;
            Guids = guids;
            Types = types;
            Colors = colors;
            Infos = infos;
            PreviewMeshes = CreatePreviewMeshes();
        }

        public BimElementSet(Mesh mesh, List<Plane> insertPlanes, string type, System.Drawing.Color color,
            Dictionary<string, string> info)
        {
            Mesh = mesh;
            InsertPlanes = insertPlanes;
            Guids = insertPlanes.Select(unused => Guid.NewGuid().ToString()).ToList();
            Types = insertPlanes.Select(unused => type).ToList();
            Colors = insertPlanes.Select(unused => color).ToList();
            Infos = insertPlanes.Select(unused => info).ToList();
            PreviewMeshes = CreatePreviewMeshes();
        }
        
        public BimElementSet ToElementSet()
        {
            return this;
        }
        
        private List<Mesh> CreatePreviewMeshes()
        {
            List<Mesh> previewMeshes = new List<Mesh>();
            
            for (int i = 0; i < InsertPlanes.Count; i++)
            {
                Mesh previewMesh = Mesh.DuplicateMesh();
                previewMesh.Transform(Transform.PlaneToPlane(Plane.WorldXY, InsertPlanes[i]));
                previewMesh.VertexColors.CreateMonotoneMesh(Colors[i]);
                previewMeshes.Add(previewMesh);
            }

            return previewMeshes;
        }
        
        public Mesh Mesh { get; }
        public List<Plane> InsertPlanes { get; }
        public List<string> Guids { get; }
        public List<string> Types { get; }
        public List<System.Drawing.Color> Colors { get; }
        public List<Dictionary<string, string>> Infos { get; }
        public List<Mesh> PreviewMeshes { get; }
    }
}