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
        public Spacing(RebarShape rebarShape, int count, Vector3d spaceVector)
        {
            RebarShape = rebarShape;
            Count = count;
            SpaceVector = spaceVector;

            Transform moveMesh = Transform.Translation(spaceVector);
            Mesh rebarShapeMesh = RebarShape.RebarMesh.DuplicateMesh();
            RebarMesh = new List<Mesh>();
            RebarMesh.Add(rebarShapeMesh.DuplicateMesh());
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

        public RebarShape RebarShape { get; set; }
        public int Count { get; set; }
        public Vector3d SpaceVector { get; set; }
        public List<Mesh> RebarMesh { get; set; }
    }
}
