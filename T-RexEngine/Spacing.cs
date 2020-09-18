using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class Spacing
    {
        public Spacing(CurveToRebar rebarShape, int count, Vector3d spaceVector)
        {
            RebarShape = rebarShape;
            Count = count;
            SpaceVector = spaceVector;

            Transform moveMesh = Transform.Translation(spaceVector);
            RebarMesh = new List<Mesh>();
            Mesh rebarShapeMesh = RebarShape.RebarMesh.DuplicateMesh();
            RebarMesh.Add(rebarShapeMesh);
            for (int i = 0; i < count - 1; i++)
            {
                rebarShapeMesh.Transform(moveMesh);
                Mesh duplicateMesh = rebarShapeMesh.DuplicateMesh();
                RebarMesh.Add(duplicateMesh);
            }
        }
        public override string ToString()
        {
            return "Rebar Group";
        }

        public CurveToRebar RebarShape { get; set; }
        public int Count { get; set; }
        public Vector3d SpaceVector { get; set; }
        public List<Mesh> RebarMesh { get; set; }
    }
}
