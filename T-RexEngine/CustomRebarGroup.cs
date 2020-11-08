using System;
using System.Collections.Generic;
using System.Globalization;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class CustomRebarGroup
    {
        private int _id;
        public CustomRebarGroup(int id, List<RebarShape> rebarShapes)
        {
            RebarShapes = rebarShapes;
            Id = id;
            
            RebarGroupMesh = new List<Mesh>();
            Volume = 0.0;
            Weight = 0.0;

            Material firstBarMaterial = rebarShapes[0].Props.Material;
            double firstBarDiameter = rebarShapes[0].Props.Diameter;

            foreach (var rebarShape in rebarShapes)
            {
                if (firstBarMaterial.ToString() != rebarShape.Props.Material.ToString())
                {
                    throw new ArgumentException("You can't add bars with different materials to one group");
                }
                if (firstBarDiameter.ToString(CultureInfo.InvariantCulture) != rebarShape.Props.Diameter.ToString(CultureInfo.InvariantCulture))
                {
                    throw new ArgumentException("You can't add bars with different diameters to one group");
                }
                
                RebarGroupMesh.Add(rebarShape.RebarMesh);

                double currentRebarVolume = rebarShape.RebarCurve.GetLength() * Math.PI * Math.Pow(rebarShape.Props.Radius, 2.0);
                
                Volume += currentRebarVolume;
                Weight += currentRebarVolume * rebarShape.Props.Material.Density;
            }
        }
        public override string ToString()
        {
            return "Custom Rebar Group Id: " + Id;
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
                else
                {
                    _id = value;
                }
            }
        }
        public List<RebarShape> RebarShapes { get; }
        public int Count => RebarShapes.Count;
        public List<Mesh> RebarGroupMesh { get; }
        public double Volume { get; }
        public double Weight { get; }
    }
}
