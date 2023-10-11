using System.Collections.Generic;
using System.Drawing;
using T_RexEngine.Interfaces;
using Rhino.Geometry;
using Mesh = Rhino.Geometry.Mesh;

namespace T_RexEngine
{
    public class BimElement : IElementSetConvertable
    {
        public BimElement(Mesh mesh, string type, Color color, Dictionary<string, string> info)
        {
            Mesh = mesh;
            Guid = System.Guid.NewGuid().ToString();
            Type = type;
            Color = color;
            Info = info;
            PreviewMesh = CreatePreviewMesh();
        }
        
        public BimElement(Mesh mesh, string guid, string type, Color color, Dictionary<string, string> info)
        {
            Mesh = mesh;
            Guid = guid;
            Type = type;
            Color = color;
            Info = info;
            PreviewMesh = CreatePreviewMesh();
        }

        public BimElementSet ToElementSet()
        {
            return new BimElementSet(Mesh, new List<Plane> {Plane.WorldXY}, new List<string> {Guid},
                new List<string> {Type}, new List<Color> {Color}, new List<Dictionary<string, string>> {Info});
        }
        
        private Mesh CreatePreviewMesh()
        {
            Mesh previewMesh = Mesh.DuplicateMesh();
            previewMesh.VertexColors.CreateMonotoneMesh(Color);
            return previewMesh;
        }
        
        public Mesh Mesh { get; }
        public string Guid { get; }
        public string Type { get; }
        public Color Color { get; }
        public Dictionary<string, string> Info { get; }
        public Mesh PreviewMesh { get; }
    }
}