using System;
using System.Collections.Generic;
using System.Globalization;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class RebarGroup
    {
        private int _id;

        public RebarGroup(int id, RebarSpacing rebarSpacing)
        {
            Id = id;
            OriginRebarShape = rebarSpacing.OriginRebarShape;
            Count = rebarSpacing.Count;
            Volume = rebarSpacing.Volume;
            Weight = rebarSpacing.Weight;
            RebarGroupMesh = rebarSpacing.RebarGroupMesh;
            RebarGroupCurves = rebarSpacing.RebarGroupCurves;
            RebarInsertPlanes = rebarSpacing.RebarInsertPlanes;
            Diameter = rebarSpacing.OriginRebarShape.Props.Diameter;
            Material = rebarSpacing.OriginRebarShape.Props.Material;
        }
        public RebarGroup(int id, List<RebarShape> rebarShapes)
        {
            Id = id;
            Count = rebarShapes.Count;
            RebarGroupMesh = new List<Mesh>();
            RebarGroupCurves = new List<Curve>();
            Volume = 0.0;
            Weight = 0.0;
            Diameter = rebarShapes[0].Props.Diameter;
            Material = rebarShapes[0].Props.Material;

            foreach (var rebarShape in rebarShapes)
            {
                if (Material.ToString() != rebarShape.Props.Material.ToString())
                {
                    throw new ArgumentException("You can't add bars with different materials to one group");
                }
                if (Diameter.ToString(CultureInfo.InvariantCulture) != rebarShape.Props.Diameter.ToString(CultureInfo.InvariantCulture))
                {
                    throw new ArgumentException("You can't add bars with different diameters to one group");
                }
                
                RebarGroupMesh.Add(rebarShape.RebarMesh);
                RebarGroupCurves.Add(rebarShape.RebarCurve);

                double currentRebarVolume = rebarShape.RebarCurve.GetLength() * Math.PI * Math.Pow(rebarShape.Props.Radius, 2.0);
                
                Volume += currentRebarVolume;
                Weight += currentRebarVolume * rebarShape.Props.Material.Density;
            }
        }
        public override string ToString()
        {
            return String.Format("Rebar Group{0}" +
                                 "Id: {1}{0}" +
                                 "Count: {2}",
                Environment.NewLine, Id, Count);
        }
        public int Id
        {
            get { return _id; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Id can't be < 0");
                }

                _id = value;
            }
        }
        public RebarShape OriginRebarShape { get; }
        public double Diameter { get; }
        public Material Material { get; }
        public List<Mesh> RebarGroupMesh { get; }
        public List<Curve> RebarGroupCurves { get; }
        public List<Plane> RebarInsertPlanes { get; }
        public int Count { get; }
        public double Volume { get; }
        public double Weight { get; }
    }
}
