using System;
using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using T_RexEngine.Enums;
using Xbim.Ifc;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.MaterialResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.RepresentationResource;
using Xbim.Ifc4.SharedBldgElements;
using Xbim.Ifc4.SharedComponentElements;
using Xbim.Ifc4.StructuralElementsDomain;
using Xbim.Ifc4.TopologyResource;
using Xbim.Ifc4.SharedBldgServiceElements;

namespace T_RexEngine
{
    public static class IfcTools
    {
        public static ElementType IntToType(int integerType)
        {
            ElementType type;

            switch (integerType)
            {
                case 0:
                    type = ElementType.PadFooting;
                    break;
                case 1:
                    type = ElementType.StripFooting;
                    break;
                case 2:
                    type = ElementType.Beam;
                    break;
                case 3:
                    type = ElementType.Column;
                    break;
                case 4:
                    type = ElementType.Door;
                    break;
                case 5:
                    type = ElementType.Stair;
                    break;
                case 6:
                    type = ElementType.Proxy;
                    break;
                case 7:
                    type = ElementType.Covering_Notdefined;
                    break;
                case 8:
                    type = ElementType.CurtainWall_Notdefined;
                    break;
                case 9:
                    type = ElementType.Footing_Notdefined;
                    break;
                case 10:
                    type = ElementType.Member_Notdefined;
                    break;
                case 11:
                    type = ElementType.Pile_Notdefined;
                    break;
                case 12:
                    type = ElementType.Plate_Notdefined;
                    break;
                case 13:
                    type = ElementType.Railing_Notdefined;
                    break;
                case 14:
                    type = ElementType.RampFlight_Notdefined;
                    break;
                case 15:
                    type = ElementType.RampFlight;
                    break;
                case 16:
                    type = ElementType.Roof_Notdefined;
                    break;
                case 17:
                    type = ElementType.Slab;
                    break;
                case 18:
                    type = ElementType.StairFlight_Notdefined;
                    break;
                case 19:
                    type = ElementType.Wall;
                    break;
                case 20:
                    type = ElementType.Window;
                    break;
                case 21:
                    type = ElementType.BuildingElementProxy_Notdefined;
                    break;
                case 22:
                    type = ElementType.BuildingElementPart_Notdefined;
                    break;
                case 23:
                    type = ElementType.ReinforcingBar_Stud;
                    break;
                case 24:
                    type = ElementType.Reinforcingmesh_Userdefined;
                    break;
                case 25:
                    type = ElementType.Tendon_Bar;
                    break;
                case 26:
                    type = ElementType.TendonAnchor_Coupler;
                    break;
                case 27:
                    type = ElementType.DistributionElement;
                    break;
                case 28:
                    type = ElementType.DistributionControlElement;
                    break;
                case 29:
                    type = ElementType.DistributionFlowElement;
                    break;
                case 30:
                    type = ElementType.DistributionChamberElement_FormedDuct;
                    break;
                case 31:
                    type = ElementType.EnergyConversionDevice;
                    break;
                case 32:
                    type = ElementType.FlowController;
                    break;
                case 33:
                    type = ElementType.FlowFitting;
                    break;
                case 34:
                    type = ElementType.FlowMovingDevice;
                    break;
                case 35:
                    type = ElementType.FlowSegment;
                    break;
                case 36:
                    type = ElementType.FlowStorageDevice;
                    break;
                case 37:
                    type = ElementType.FlowTerminal;
                    break;
                case 38:
                    type = ElementType.FlowTreatmentDevice;
                    break;
                case 39:
                    type = ElementType.ElementAssembly_AccessoryAssembly;
                    break;
                case 40:
                    type = ElementType.DiscreteAccessory_Anchorplate;
                    break;
                case 41:
                    type = ElementType.MechanicalFastener_Userdefined;
                    break;
                case 42:
                    type = ElementType.Fastener_Userdefined;
                    break;
                case 43:
                    type = ElementType.FurnishingElement;
                    break;
                case 44:
                    type = ElementType.TransportElement_Userdefined;
                    break;
                case 45:
                    type = ElementType.VirtualElement;
                    break;
                case 101:
                    type = ElementType.Door_Gate;
                    break;
                case 102:
                    type = ElementType.Door_Trapdoor;
                    break;
                case 103:
                    type = ElementType.Door_Userdefined;
                    break;
                case 104:
                    type = ElementType.Door_Notdefined;
                    break;
                case 105:
                    type = ElementType.Wall_Movable;
                    break;
                case 106:
                    type = ElementType.Wall_Parapet;
                    break;
                case 107:
                    type = ElementType.Wall_Partitioning;
                    break;
                case 108:
                    type = ElementType.Wall_Plumbingwall;
                    break;
                case 109:
                    type = ElementType.Wall_Shear;
                    break;
                case 110:
                    type = ElementType.Wall_Solidwall;
                    break;
                case 111:
                    type = ElementType.Wall_Standard;
                    break;
                case 112:
                    type = ElementType.Wall_Polygonal;
                    break;
                case 113:
                    type = ElementType.Wall_Elementedwall;
                    break;
                case 114:
                    type = ElementType.Wall_Userdefined;
                    break;
                case 115:
                    type = ElementType.Wall_Notdefined;
                    break;
                case 116:
                    type = ElementType.Slab_Floor;
                    break;
                case 117:
                    type = ElementType.Slab_Roof;
                    break;
                case 118:
                    type = ElementType.Slab_Landing;
                    break;
                case 119:
                    type = ElementType.Slab_Baseslab;
                    break;
                case 120:
                    type = ElementType.Slab_Userdefined;
                    break;
                case 121:
                    type = ElementType.Slab_Notdefined;
                    break;
                case 122:
                    type = ElementType.Beam_Joist;
                    break;
                case 123:
                    type = ElementType.Beam_Hollowcore;
                    break;
                case 124:
                    type = ElementType.Beam_Lintel;
                    break;
                case 125:
                    type = ElementType.Beam_Spandrel;
                    break;
                case 126:
                    type = ElementType.Beam_T_Beam;
                    break;
                case 127:
                    type = ElementType.Beam_Userdefined;
                    break;
                case 128:
                    type = ElementType.Beam_Notdefined;
                    break;
                case 129:
                    type = ElementType.Column_Pilaster;
                    break;
                case 130:
                    type = ElementType.Column_Userdefined;
                    break;
                case 131:
                    type = ElementType.Column_Notdefined;
                    break;
                case 132:
                    type = ElementType.Stair_StraightRunStair;
                    break;
                case 133:
                    type = ElementType.Stair_TwoStraightRunStair;
                    break;
                case 134:
                    type = ElementType.Stair_QuarterWindingStair;
                    break;
                case 135:
                    type = ElementType.Stair_QuarterTurnStair;
                    break;
                case 136:
                    type = ElementType.Stair_HalfWindingStair;
                    break;
                case 137:
                    type = ElementType.Stair_HalfTurnStair;
                    break;
                case 138:
                    type = ElementType.Stair_TwoQuarterWindingStair;
                    break;
                case 139:
                    type = ElementType.Stair_TwoQuarterTurnStair;
                    break;
                case 140:
                    type = ElementType.Stair_ThreeQuarterWindingStair;
                    break;
                case 141:
                    type = ElementType.Stair_ThreeQuarterTurnStair;
                    break;
                case 142:
                    type = ElementType.Stair_SpiralStair;
                    break;
                case 143:
                    type = ElementType.Stair_DoubleReturnStair;
                    break;
                case 144:
                    type = ElementType.Stair_CurvedRunStair;
                    break;
                case 145:
                    type = ElementType.Stair_TwoCurvedRunStair;
                    break;
                case 146:
                    type = ElementType.Stair_Userdefined;
                    break;
                case 147:
                    type = ElementType.Stair_Notdefined;
                    break;
                case 148:
                    type = ElementType.Window_Skylight;
                    break;
                case 149:
                    type = ElementType.Window_Lightdome;
                    break;
                case 150:
                    type = ElementType.Window_Userdefined;
                    break;
                case 151:
                    type = ElementType.Window_Notdefined;
                    break;
                case 152:
                    type = ElementType.RampFlight_Straight;
                    break;
                case 153:
                    type = ElementType.RampFlight_Spiral;
                    break;
                case 154:
                    type = ElementType.RampFlight_Userdefined;
                    break;
                case 155:
                    type = ElementType.RampFlight_Notdefined;
                    break;
                case 156:
                    type = ElementType.CurtainWall_Notdefined;
                    break;
                case 157:
                    type = ElementType.Plate_Curtain_Panel;
                    break;
                case 158:
                    type = ElementType.Plate_Sheet;
                    break;
                case 159:
                    type = ElementType.Plate_Userdefined;
                    break;
                case 160:
                    type = ElementType.Plate_Notdefined;
                    break;
                case 161:
                    type = ElementType.Railing_Handrail;
                    break;
                case 162:
                    type = ElementType.Railing_Guardrail;
                    break;
                case 163:
                    type = ElementType.Railing_Balustrade;
                    break;
                case 164:
                    type = ElementType.Railing_Userdefined;
                    break;
                case 165:
                    type = ElementType.Railing_Notdefined;
                    break;
                case 166:
                    type = ElementType.StairFlight_Straight;
                    break;
                case 167:
                    type = ElementType.StairFlight_Winder;
                    break;
                case 168:
                    type = ElementType.StairFlight_Spiral;
                    break;
                case 169:
                    type = ElementType.StairFlight_Curved;
                    break;
                case 170:
                    type = ElementType.StairFlight_Freeform;
                    break;
                case 171:
                    type = ElementType.StairFlight_Userdefined;
                    break;
                case 172:
                    type = ElementType.StairFlight_Notdefined;
                    break;
                case 173:
                    type = ElementType.Ramp_StraightRunRamp;
                    break;
                case 174:
                    type = ElementType.Ramp_TwoStraightRunRamp;
                    break;
                case 175:
                    type = ElementType.Ramp_QuarterTurnRamp;
                    break;
                case 176:
                    type = ElementType.Ramp_TwoQuarterTurnRamp;
                    break;
                case 177:
                    type = ElementType.Ramp_HalfTurnRamp;
                    break;
                case 178:
                    type = ElementType.Ramp_SpiralRamp;
                    break;
                case 179:
                    type = ElementType.Ramp_Userdefined;
                    break;
                case 180:
                    type = ElementType.Ramp_Notdefined;
                    break;
                case 181:
                    type = ElementType.Footing_Caisson_Foundation;
                    break;
                case 182:
                    type = ElementType.Footing_Footing_Beam;
                    break;
                case 183:
                    type = ElementType.Footing_Pad_Footing;
                    break;
                case 184:
                    type = ElementType.Footing_Pile_Cap;
                    break;
                case 185:
                    type = ElementType.Footing_Strip_Footing;
                    break;
                case 186:
                    type = ElementType.Footing_Userdefined;
                    break;
                case 187:
                    type = ElementType.Footing_Notdefined;
                    break;
                case 188:
                    type = ElementType.Pile_Bored;
                    break;
                case 189:
                    type = ElementType.Pile_Driven;
                    break;
                case 190:
                    type = ElementType.Pile_Jetgrouting;
                    break;
                case 191:
                    type = ElementType.Pile_Cohesion;
                    break;
                case 192:
                    type = ElementType.Pile_Friction;
                    break;
                case 193:
                    type = ElementType.Pile_Support;
                    break;
                case 194:
                    type = ElementType.Pile_Userdefined;
                    break;
                case 195:
                    type = ElementType.Pile_Notdefined;
                    break;
                case 196:
                    type = ElementType.Covering_Ceiling;
                    break;
                case 197:
                    type = ElementType.Covering_Flooring;
                    break;
                case 198:
                    type = ElementType.Covering_Cladding;
                    break;
                case 199:
                    type = ElementType.Covering_Roofing;
                    break;
                case 200:
                    type = ElementType.Covering_Molding;
                    break;
                case 201:
                    type = ElementType.Covering_Skirtingboard;
                    break;
                case 202:
                    type = ElementType.Covering_Insulation;
                    break;
                case 203:
                    type = ElementType.Covering_Membrane;
                    break;
                case 204:
                    type = ElementType.Covering_Sleeving;
                    break;
                case 205:
                    type = ElementType.Covering_Wrapping;
                    break;
                case 206:
                    type = ElementType.Covering_Userdefined;
                    break;
                case 207:
                    type = ElementType.Covering_Notdefined;
                    break;
                case 208:
                    type = ElementType.Member_Brace;
                    break;
                case 209:
                    type = ElementType.Member_Chord;
                    break;
                case 210:
                    type = ElementType.Member_Collar;
                    break;
                case 211:
                    type = ElementType.Member_Member;
                    break;
                case 212:
                    type = ElementType.Member_Mullion;
                    break;
                case 213:
                    type = ElementType.Member_Plate;
                    break;
                case 214:
                    type = ElementType.Member_Post;
                    break;
                case 215:
                    type = ElementType.Member_Purlin;
                    break;
                case 216:
                    type = ElementType.Member_Rafter;
                    break;
                case 217:
                    type = ElementType.Member_Stringer;
                    break;
                case 218:
                    type = ElementType.Member_Strut;
                    break;
                case 219:
                    type = ElementType.Member_Stud;
                    break;
                case 220:
                    type = ElementType.Member_Userdefined;
                    break;
                case 221:
                    type = ElementType.Member_Notdefined;
                    break;
                case 222:
                    type = ElementType.Roof_Flatroof;
                    break;
                case 223:
                    type = ElementType.Roof_Shedroof;
                    break;
                case 224:
                    type = ElementType.Roof_Gableroof;
                    break;
                case 225:
                    type = ElementType.Roof_Hiproof;
                    break;
                case 226:
                    type = ElementType.Roof_HippedGableRoof;
                    break;
                case 227:
                    type = ElementType.Roof_GambrelRoof;
                    break;
                case 228:
                    type = ElementType.Roof_MansardRoof;
                    break;
                case 229:
                    type = ElementType.Roof_BarrelRoof;
                    break;
                case 230:
                    type = ElementType.Roof_RainbowRoof;
                    break;
                case 231:
                    type = ElementType.Roof_ButterflyRoof;
                    break;
                case 232:
                    type = ElementType.Roof_PavilionRoof;
                    break;
                case 233:
                    type = ElementType.Roof_DomeRoof;
                    break;
                case 234:
                    type = ElementType.Roof_Freeform;
                    break;
                case 235:
                    type = ElementType.Roof_Userdefined;
                    break;
                case 236:
                    type = ElementType.Roof_Notdefined;
                    break;
                case 237:
                    type = ElementType.Reinforcingmesh_Userdefined;
                    break;
                case 238:
                    type = ElementType.Reinforcingmesh_Notdefined;
                    break;
                case 239:
                    type = ElementType.BuildingElementPart_Insulation;
                    break;
                case 240:
                    type = ElementType.BuildingElementPart_Precastpanel;
                    break;
                case 241:
                    type = ElementType.BuildingElementPart_Userdefined;
                    break;
                case 242:
                    type = ElementType.BuildingElementPart_Notdefined;
                    break;
                case 243:
                    type = ElementType.Fastener_Glue;
                    break;
                case 244:
                    type = ElementType.Fastener_Mortar;
                    break;
                case 245:
                    type = ElementType.Fastener_Weld;
                    break;
                case 246:
                    type = ElementType.Fastener_Userdefined;
                    break;
                case 247:
                    type = ElementType.Fastener_Notdefined;
                    break;
                case 248:
                    type = ElementType.DiscreteAccessory_Anchorplate;
                    break;
                case 249:
                    type = ElementType.DiscreteAccessory_Bracket;
                    break;
                case 250:
                    type = ElementType.DiscreteAccessory_Shoe;
                    break;
                case 251:
                    type = ElementType.DiscreteAccessory_Userdefined;
                    break;
                case 252:
                    type = ElementType.DiscreteAccessory_Notdefined;
                    break;
                case 253:
                    type = ElementType.TendonAnchor_Coupler;
                    break;
                case 254:
                    type = ElementType.TendonAnchor_FixedEnd;
                    break;
                case 255:
                    type = ElementType.TendonAnchor_TensioningEnd;
                    break;
                case 256:
                    type = ElementType.TendonAnchor_Userdefined;
                    break;
                case 257:
                    type = ElementType.TendonAnchor_Notdefined;
                    break;
                case 258:
                    type = ElementType.BuildingElementProxy_Complex;
                    break;
                case 259:
                    type = ElementType.BuildingElementProxy_Element;
                    break;
                case 260:
                    type = ElementType.BuildingElementProxy_Partial;
                    break;
                case 261:
                    type = ElementType.BuildingElementProxy_ProvisionForVoid;
                    break;
                case 262:
                    type = ElementType.BuildingElementProxy_Userdefined;
                    break;
                case 263:
                    type = ElementType.BuildingElementProxy_Notdefined;
                    break;
                case 264:
                    type = ElementType.TransportElement_Elevator;
                    break;
                case 265:
                    type = ElementType.TransportElement_Escalator;
                    break;
                case 266:
                    type = ElementType.TransportElement_MovingWalkWay;
                    break;
                case 267:
                    type = ElementType.TransportElement_Craneway;
                    break;
                case 268:
                    type = ElementType.TransportElement_LiftingGear;
                    break;
                case 269:
                    type = ElementType.TransportElement_Userdefined;
                    break;
                case 270:
                    type = ElementType.TransportElement_Notdefined;
                    break;
                case 271:
                    type = ElementType.ReinforcingBar_Anchoring;
                    break;
                case 272:
                    type = ElementType.ReinforcingBar_Edge;
                    break;
                case 273:
                    type = ElementType.ReinforcingBar_Ligature;
                    break;
                case 274:
                    type = ElementType.ReinforcingBar_Main;
                    break;
                case 275:
                    type = ElementType.ReinforcingBar_Punching;
                    break;
                case 276:
                    type = ElementType.ReinforcingBar_Ring;
                    break;
                case 277:
                    type = ElementType.ReinforcingBar_Shear;
                    break;
                case 278:
                    type = ElementType.ReinforcingBar_Stud;
                    break;
                case 279:
                    type = ElementType.ReinforcingBar_Userdefined;
                    break;
                case 280:
                    type = ElementType.ReinforcingBar_Notdefined;
                    break;
                case 281:
                    type = ElementType.DistributionChamberElement_FormedDuct;
                    break;
                case 282:
                    type = ElementType.DistributionChamberElement_InspectionChamber;
                    break;
                case 283:
                    type = ElementType.DistributionChamberElement_InspectionPit;
                    break;
                case 284:
                    type = ElementType.DistributionChamberElement_Manhole;
                    break;
                case 285:
                    type = ElementType.DistributionChamberElement_MeterChamber;
                    break;
                case 286:
                    type = ElementType.DistributionChamberElement_Sump;
                    break;
                case 287:
                    type = ElementType.DistributionChamberElement_Trench;
                    break;
                case 288:
                    type = ElementType.DistributionChamberElement_ValveChamber;
                    break;
                case 289:
                    type = ElementType.DistributionChamberElement_Userdefined;
                    break;
                case 290:
                    type = ElementType.DistributionChamberElement_Notdefined;
                    break;
                case 291:
                    type = ElementType.ElementAssembly_AccessoryAssembly;
                    break;
                case 292:
                    type = ElementType.ElementAssembly_Arch;
                    break;
                case 293:
                    type = ElementType.ElementAssembly_BeamGrid;
                    break;
                case 294:
                    type = ElementType.ElementAssembly_BracedFrame;
                    break;
                case 295:
                    type = ElementType.ElementAssembly_Girder;
                    break;
                case 296:
                    type = ElementType.ElementAssembly_ReinforcementUnit;
                    break;
                case 297:
                    type = ElementType.ElementAssembly_RigidFrame;
                    break;
                case 298:
                    type = ElementType.ElementAssembly_SlabField;
                    break;
                case 299:
                    type = ElementType.ElementAssembly_Truss;
                    break;
                case 300:
                    type = ElementType.ElementAssembly_Userdefined;
                    break;
                case 301:
                    type = ElementType.ElementAssembly_Notdefined;
                    break;
                case 302:
                    type = ElementType.MechanicalFastener_AnchorBolt;
                    break;
                case 303:
                    type = ElementType.MechanicalFastener_Bolt;
                    break;
                case 304:
                    type = ElementType.MechanicalFastener_Dowel;
                    break;
                case 305:
                    type = ElementType.MechanicalFastener_Nail;
                    break;
                case 306:
                    type = ElementType.MechanicalFastener_NailPlate;
                    break;
                case 307:
                    type = ElementType.MechanicalFastener_Rivet;
                    break;
                case 308:
                    type = ElementType.MechanicalFastener_Screw;
                    break;
                case 309:
                    type = ElementType.MechanicalFastener_ShearConnector;
                    break;
                case 310:
                    type = ElementType.MechanicalFastener_Staple;
                    break;
                case 311:
                    type = ElementType.MechanicalFastener_StudShearConnector;
                    break;
                case 312:
                    type = ElementType.MechanicalFastener_Userdefined;
                    break;
                case 313:
                    type = ElementType.MechanicalFastener_Notdefined;
                    break;
                case 314:
                    type = ElementType.Tendon_Bar;
                    break;
                case 315:
                    type = ElementType.Tendon_Coated;
                    break;
                case 316:
                    type = ElementType.Tendon_Strand;
                    break;
                case 317:
                    type = ElementType.Tendon_Wire;
                    break;
                case 318:
                    type = ElementType.Tendon_Userdefined;
                    break;
                case 319:
                    type = ElementType.Tendon_Notdefined;
                    break;

                default:
                    throw new ArgumentException("Element type not recognized => IntToType");
            }

            return type;
        }

        private static IfcLocalPlacement CreateLocalPlacement(IfcStore model, Plane insertPlane)
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

        private static void ApplyRepresentationAndPlacement(IfcStore model, IfcBuildingElement element, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var representation = model.Instances.New<IfcProductDefinitionShape>();
            representation.Representations.Add(shape);
            element.Representation = representation;

            var localPlacement = CreateLocalPlacement(model, insertPlane);
            element.ObjectPlacement = localPlacement;
        }

        public static void ApplyRepresentationAndPlacement(IfcStore model, IfcReinforcingElement element, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var representation = model.Instances.New<IfcProductDefinitionShape>();
            representation.Representations.Add(shape);
            element.Representation = representation;

            var localPlacement = CreateLocalPlacement(model, insertPlane);
            element.ObjectPlacement = localPlacement;
        }

        public static void ApplyRepresentationAndPlacement(IfcStore model, IfcElementComponent element, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var representation = model.Instances.New<IfcProductDefinitionShape>();
            representation.Representations.Add(shape);
            element.Representation = representation;

            var localPlacement = CreateLocalPlacement(model, insertPlane);
            element.ObjectPlacement = localPlacement;
        }

        public static void ApplyRepresentationAndPlacement(IfcStore model, IfcDistributionElement element, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var representation = model.Instances.New<IfcProductDefinitionShape>();
            representation.Representations.Add(shape);
            element.Representation = representation;

            var localPlacement = CreateLocalPlacement(model, insertPlane);
            element.ObjectPlacement = localPlacement;
        }

        public static void ApplyRepresentationAndPlacement(IfcStore model, IfcProxy element, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var representation = model.Instances.New<IfcProductDefinitionShape>();
            representation.Representations.Add(shape);
            element.Representation = representation;

            var localPlacement = CreateLocalPlacement(model, insertPlane);
            element.ObjectPlacement = localPlacement;
        }

        public static void ApplyRepresentationAndPlacement(IfcStore model, IfcElement element, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var representation = model.Instances.New<IfcProductDefinitionShape>();
            representation.Representations.Add(shape);
            element.Representation = representation;

            var localPlacement = CreateLocalPlacement(model, insertPlane);
            element.ObjectPlacement = localPlacement;
        }


        //Subtypes part 1 start here
        private static IfcFooting CreateFooting(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var footing = model.Instances.New<IfcFooting>();
            footing.Name = name;

            switch (type)
            {
                case ElementType.Footing_Pad_Footing:
                    footing.PredefinedType = IfcFootingTypeEnum.PAD_FOOTING;
                    break;
                case ElementType.Footing_Strip_Footing:
                    footing.PredefinedType = IfcFootingTypeEnum.STRIP_FOOTING;
                    break;
                case ElementType.Footing_Caisson_Foundation:
                    footing.PredefinedType = IfcFootingTypeEnum.CAISSON_FOUNDATION;
                    break;
                case ElementType.Footing_Footing_Beam:
                    footing.PredefinedType = IfcFootingTypeEnum.FOOTING_BEAM;
                    break;
                case ElementType.Footing_Pile_Cap:
                    footing.PredefinedType = IfcFootingTypeEnum.PILE_CAP;
                    break;
                case ElementType.Footing_Userdefined:
                    footing.PredefinedType = IfcFootingTypeEnum.USERDEFINED;
                    break;
                case ElementType.Footing_Notdefined:
                    footing.PredefinedType = IfcFootingTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Footing type not recognized, can be only Pad or Strip");
            }

            ApplyRepresentationAndPlacement(model, footing, shape, insertPlane);

            return footing;
        }

        private static IfcBeam CreateBeam(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var beam = model.Instances.New<IfcBeam>();
            beam.Name = name;

            switch (type)
            {
                case ElementType.Beam_Hollowcore:
                    beam.PredefinedType = IfcBeamTypeEnum.HOLLOWCORE;
                    break;
                case ElementType.Beam_Joist:
                    beam.PredefinedType = IfcBeamTypeEnum.JOIST;
                    break;
                case ElementType.Beam_Lintel:
                    beam.PredefinedType = IfcBeamTypeEnum.LINTEL;
                    break;
                case ElementType.Beam_Notdefined:
                    beam.PredefinedType = IfcBeamTypeEnum.NOTDEFINED;
                    break;
                case ElementType.Beam_Spandrel:
                    beam.PredefinedType = IfcBeamTypeEnum.SPANDREL;
                    break;
                case ElementType.Beam_T_Beam:
                    beam.PredefinedType = IfcBeamTypeEnum.T_BEAM;
                    break;
                case ElementType.Beam_Userdefined:
                    beam.PredefinedType = IfcBeamTypeEnum.USERDEFINED;
                    break;

                default:
                    throw new ArgumentException("Beam type not recognized");
            }

            ApplyRepresentationAndPlacement(model, beam, shape, insertPlane);

            return beam;
        }


        private static IfcColumn CreateColumn(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var column = model.Instances.New<IfcColumn>();
            column.Name = name;

            switch (type)
            {
                case ElementType.Column:
                    column.PredefinedType = IfcColumnTypeEnum.COLUMN;
                    break;
                case ElementType.Column_Notdefined:
                    column.PredefinedType = IfcColumnTypeEnum.NOTDEFINED;
                    break;
                case ElementType.Column_Pilaster:
                    column.PredefinedType = IfcColumnTypeEnum.PILASTER;
                    break;
                case ElementType.Column_Userdefined:
                    column.PredefinedType = IfcColumnTypeEnum.USERDEFINED;
                    break;

                default:
                    throw new ArgumentException("Column type not recognized");
            }

            ApplyRepresentationAndPlacement(model, column, shape, insertPlane);

            return column;
        }

        private static IfcDoor CreateDoor(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var door = model.Instances.New<IfcDoor>();
            door.Name = name;

            switch (type)
            {
                case ElementType.Door:
                    door.PredefinedType = IfcDoorTypeEnum.DOOR;
                    break;
                case ElementType.Door_Gate:
                    door.PredefinedType = IfcDoorTypeEnum.GATE;
                    break;
                case ElementType.Door_Trapdoor:
                    door.PredefinedType = IfcDoorTypeEnum.TRAPDOOR;
                    break;
                case ElementType.Door_Userdefined:
                    door.PredefinedType = IfcDoorTypeEnum.USERDEFINED;
                    break;
                case ElementType.Door_Notdefined:
                    door.PredefinedType = IfcDoorTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Door type not recognized");
            }

            ApplyRepresentationAndPlacement(model, door, shape, insertPlane);

            return door;
        }

        private static IfcStair CreateStair(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var stair = model.Instances.New<IfcStair>();
            stair.Name = name;

            switch (type)
            {
                case ElementType.Stair_CurvedRunStair:
                    stair.PredefinedType = IfcStairTypeEnum.CURVED_RUN_STAIR;
                    break;
                case ElementType.Stair_DoubleReturnStair:
                    stair.PredefinedType = IfcStairTypeEnum.DOUBLE_RETURN_STAIR;
                    break;
                case ElementType.Stair_HalfTurnStair:
                    stair.PredefinedType = IfcStairTypeEnum.HALF_TURN_STAIR;
                    break;
                case ElementType.Stair_HalfWindingStair:
                    stair.PredefinedType = IfcStairTypeEnum.HALF_WINDING_STAIR;
                    break;
                case ElementType.Stair_Notdefined:
                    stair.PredefinedType = IfcStairTypeEnum.NOTDEFINED;
                    break;
                case ElementType.Stair_QuarterTurnStair:
                    stair.PredefinedType = IfcStairTypeEnum.QUARTER_TURN_STAIR;
                    break;
                case ElementType.Stair_QuarterWindingStair:
                    stair.PredefinedType = IfcStairTypeEnum.QUARTER_WINDING_STAIR;
                    break;
                case ElementType.Stair_SpiralStair:
                    stair.PredefinedType = IfcStairTypeEnum.SPIRAL_STAIR;
                    break;
                case ElementType.Stair_StraightRunStair:
                    stair.PredefinedType = IfcStairTypeEnum.STRAIGHT_RUN_STAIR;
                    break;
                case ElementType.Stair_ThreeQuarterTurnStair:
                    stair.PredefinedType = IfcStairTypeEnum.THREE_QUARTER_TURN_STAIR;
                    break;
                case ElementType.Stair_ThreeQuarterWindingStair:
                    stair.PredefinedType = IfcStairTypeEnum.THREE_QUARTER_WINDING_STAIR;
                    break;
                case ElementType.Stair_TwoCurvedRunStair:
                    stair.PredefinedType = IfcStairTypeEnum.TWO_CURVED_RUN_STAIR;
                    break;
                case ElementType.Stair_TwoQuarterTurnStair:
                    stair.PredefinedType = IfcStairTypeEnum.TWO_QUARTER_TURN_STAIR;
                    break;
                case ElementType.Stair_TwoQuarterWindingStair:
                    stair.PredefinedType = IfcStairTypeEnum.TWO_QUARTER_WINDING_STAIR;
                    break;
                case ElementType.Stair_TwoStraightRunStair:
                    stair.PredefinedType = IfcStairTypeEnum.TWO_STRAIGHT_RUN_STAIR;
                    break;
                case ElementType.Stair_Userdefined:
                    stair.PredefinedType = IfcStairTypeEnum.USERDEFINED;
                    break;

                default:
                    throw new ArgumentException("Stair type not recognized");
            }

            ApplyRepresentationAndPlacement(model, stair, shape, insertPlane);

            return stair;
        }

        private static IfcProxy CreateProxy(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var proxy = model.Instances.New<IfcProxy>();
            proxy.Name = name;
            // IfcProxy doesn't have a predefined type parameter

            ApplyRepresentationAndPlacement(model, proxy, shape, insertPlane);

            return proxy;
        }

        private static IfcCovering CreateCovering(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var covering = model.Instances.New<IfcCovering>();
            covering.Name = name;

            switch (type)
            {
                case ElementType.Covering_Ceiling:
                    covering.PredefinedType = IfcCoveringTypeEnum.CEILING;
                    break;
                case ElementType.Covering_Cladding:
                    covering.PredefinedType = IfcCoveringTypeEnum.CLADDING;
                    break;
                case ElementType.Covering_Flooring:
                    covering.PredefinedType = IfcCoveringTypeEnum.FLOORING;
                    break;
                case ElementType.Covering_Insulation:
                    covering.PredefinedType = IfcCoveringTypeEnum.INSULATION;
                    break;
                case ElementType.Covering_Membrane:
                    covering.PredefinedType = IfcCoveringTypeEnum.MEMBRANE;
                    break;
                case ElementType.Covering_Molding:
                    covering.PredefinedType = IfcCoveringTypeEnum.MOLDING;
                    break;
                case ElementType.Covering_Notdefined:
                    covering.PredefinedType = IfcCoveringTypeEnum.NOTDEFINED;
                    break;
                case ElementType.Covering_Roofing:
                    covering.PredefinedType = IfcCoveringTypeEnum.ROOFING;
                    break;
                case ElementType.Covering_Skirtingboard:
                    covering.PredefinedType = IfcCoveringTypeEnum.SKIRTINGBOARD;
                    break;
                case ElementType.Covering_Sleeving:
                    covering.PredefinedType = IfcCoveringTypeEnum.SLEEVING;
                    break;
                case ElementType.Covering_Userdefined:
                    covering.PredefinedType = IfcCoveringTypeEnum.USERDEFINED;
                    break;
                case ElementType.Covering_Wrapping:
                    covering.PredefinedType = IfcCoveringTypeEnum.WRAPPING;
                    break;

                default:
                    throw new ArgumentException("Covering type not recognized");
            }

            ApplyRepresentationAndPlacement(model, covering, shape, insertPlane);

            return covering;
        }

        private static IfcCurtainWall CreateCurtainWall(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var curtainWall = model.Instances.New<IfcCurtainWall>();
            curtainWall.Name = name;

            switch (type)
            {
                case ElementType.CurtainWall_Userdefined:
                    curtainWall.PredefinedType = IfcCurtainWallTypeEnum.USERDEFINED;
                    break;
                case ElementType.CurtainWall_Notdefined:
                    curtainWall.PredefinedType = IfcCurtainWallTypeEnum.NOTDEFINED;
                    break;
                default:
                    throw new ArgumentException("Curtain Wall type not recognized");
            }

            ApplyRepresentationAndPlacement(model, curtainWall, shape, insertPlane);

            return curtainWall;
        }

        private static IfcMember CreateMember(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var member = model.Instances.New<IfcMember>();
            member.Name = name;

            switch (type)
            {
                case ElementType.Member_Brace:
                    member.PredefinedType = IfcMemberTypeEnum.BRACE;
                    break;
                case ElementType.Member_Chord:
                    member.PredefinedType = IfcMemberTypeEnum.CHORD;
                    break;
                case ElementType.Member_Collar:
                    member.PredefinedType = IfcMemberTypeEnum.COLLAR;
                    break;
                case ElementType.Member_Member:
                    member.PredefinedType = IfcMemberTypeEnum.MEMBER;
                    break;
                case ElementType.Member_Mullion:
                    member.PredefinedType = IfcMemberTypeEnum.MULLION;
                    break;
                case ElementType.Member_Notdefined:
                    member.PredefinedType = IfcMemberTypeEnum.NOTDEFINED;
                    break;
                case ElementType.Member_Plate:
                    member.PredefinedType = IfcMemberTypeEnum.PLATE;
                    break;
                case ElementType.Member_Post:
                    member.PredefinedType = IfcMemberTypeEnum.POST;
                    break;
                case ElementType.Member_Purlin:
                    member.PredefinedType = IfcMemberTypeEnum.PURLIN;
                    break;
                case ElementType.Member_Rafter:
                    member.PredefinedType = IfcMemberTypeEnum.RAFTER;
                    break;
                case ElementType.Member_Stringer:
                    member.PredefinedType = IfcMemberTypeEnum.STRINGER;
                    break;
                case ElementType.Member_Strut:
                    member.PredefinedType = IfcMemberTypeEnum.STRUT;
                    break;
                case ElementType.Member_Stud:
                    member.PredefinedType = IfcMemberTypeEnum.STRUT;
                    break;
                case ElementType.Member_Userdefined:
                    member.PredefinedType = IfcMemberTypeEnum.USERDEFINED;
                    break;

                default:
                    throw new ArgumentException("Member type not recognized");
            }
            ApplyRepresentationAndPlacement(model, member, shape, insertPlane);

            return member;
        }

        private static IfcPile CreatePile(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var pile = model.Instances.New<IfcPile>();
            pile.Name = name;

            switch (type)
            {
                case ElementType.Pile_Bored:
                    pile.PredefinedType = IfcPileTypeEnum.BORED;
                    break;
                case ElementType.Pile_Driven:
                    pile.PredefinedType = IfcPileTypeEnum.DRIVEN;
                    break;
                case ElementType.Pile_Jetgrouting:
                    pile.PredefinedType = IfcPileTypeEnum.JETGROUTING;
                    break;
                case ElementType.Pile_Cohesion:
                    pile.PredefinedType = IfcPileTypeEnum.COHESION;
                    break;
                case ElementType.Pile_Friction:
                    pile.PredefinedType = IfcPileTypeEnum.FRICTION;
                    break;
                case ElementType.Pile_Support:
                    pile.PredefinedType = IfcPileTypeEnum.SUPPORT;
                    break;
                case ElementType.Pile_Userdefined:
                    pile.PredefinedType = IfcPileTypeEnum.USERDEFINED;
                    break;
                case ElementType.Pile_Notdefined:
                    pile.PredefinedType = IfcPileTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Pile type not recognized");
            }
            ApplyRepresentationAndPlacement(model, pile, shape, insertPlane);

            return pile;
        }

        private static IfcPlate CreatePlate(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var plate = model.Instances.New<IfcPlate>();
            plate.Name = name;

            switch (type)
            {
                case ElementType.Plate_Curtain_Panel:
                    plate.PredefinedType = IfcPlateTypeEnum.CURTAIN_PANEL;
                    break;
                case ElementType.Plate_Sheet:
                    plate.PredefinedType = IfcPlateTypeEnum.SHEET;
                    break;
                case ElementType.Plate_Userdefined:
                    plate.PredefinedType = IfcPlateTypeEnum.USERDEFINED;
                    break;
                case ElementType.Plate_Notdefined:
                    plate.PredefinedType = IfcPlateTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Plate type not recognized");
            }

            ApplyRepresentationAndPlacement(model, plate, shape, insertPlane);


            return plate;
        }

        private static IfcRailing CreateRailing(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var railing = model.Instances.New<IfcRailing>();
            railing.Name = name;
            railing.PredefinedType = IfcRailingTypeEnum.USERDEFINED;
            ApplyRepresentationAndPlacement(model, railing, shape, insertPlane);

            return railing;
        }

        private static IfcRamp CreateRamp(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var ramp = model.Instances.New<IfcRamp>();
            ramp.Name = name;


            switch (type)
            {
                case ElementType.Ramp_StraightRunRamp:
                    ramp.PredefinedType = IfcRampTypeEnum.STRAIGHT_RUN_RAMP;
                    break;
                case ElementType.Ramp_TwoStraightRunRamp:
                    ramp.PredefinedType = IfcRampTypeEnum.TWO_STRAIGHT_RUN_RAMP;
                    break;
                case ElementType.Ramp_QuarterTurnRamp:
                    ramp.PredefinedType = IfcRampTypeEnum.QUARTER_TURN_RAMP;
                    break;
                case ElementType.Ramp_TwoQuarterTurnRamp:
                    ramp.PredefinedType = IfcRampTypeEnum.TWO_QUARTER_TURN_RAMP;
                    break;
                case ElementType.Ramp_HalfTurnRamp:
                    ramp.PredefinedType = IfcRampTypeEnum.HALF_TURN_RAMP;
                    break;
                case ElementType.RampFlight_Spiral:
                    ramp.PredefinedType = IfcRampTypeEnum.SPIRAL_RAMP;
                    break;
                case ElementType.Ramp_Userdefined:
                    ramp.PredefinedType = IfcRampTypeEnum.USERDEFINED;
                    break;
                case ElementType.Ramp_Notdefined:
                    ramp.PredefinedType = IfcRampTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Ramp type not recognized");
            }
            ApplyRepresentationAndPlacement(model, ramp, shape, insertPlane);

            return ramp;
        }

        private static IfcRampFlight CreateRampFlight(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var rampFlight = model.Instances.New<IfcRampFlight>();
            rampFlight.Name = name;

            switch (type)
            {
                case ElementType.RampFlight_Straight:
                    rampFlight.PredefinedType = IfcRampFlightTypeEnum.STRAIGHT;
                    break;
                case ElementType.RampFlight_Spiral:
                    rampFlight.PredefinedType = IfcRampFlightTypeEnum.SPIRAL;
                    break;
                case ElementType.RampFlight_Userdefined:
                    rampFlight.PredefinedType = IfcRampFlightTypeEnum.USERDEFINED;
                    break;
                case ElementType.RampFlight_Notdefined:
                    rampFlight.PredefinedType = IfcRampFlightTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Ramp Flight type not recognized");
            }

            ApplyRepresentationAndPlacement(model, rampFlight, shape, insertPlane);

            return rampFlight;
        }

        private static IfcRoof CreateRoof(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var roof = model.Instances.New<IfcRoof>();
            roof.Name = name;

            switch (type)
            {
                case ElementType.Roof_BarrelRoof:
                    roof.PredefinedType = IfcRoofTypeEnum.BARREL_ROOF;
                    break;
                case ElementType.Roof_ButterflyRoof:
                    roof.PredefinedType = IfcRoofTypeEnum.BUTTERFLY_ROOF;
                    break;
                case ElementType.Roof_DomeRoof:
                    roof.PredefinedType = IfcRoofTypeEnum.DOME_ROOF;
                    break;
                case ElementType.Roof_Flatroof:
                    roof.PredefinedType = IfcRoofTypeEnum.FLAT_ROOF;
                    break;
                case ElementType.Roof_Freeform:
                    roof.PredefinedType = IfcRoofTypeEnum.FREEFORM;
                    break;
                case ElementType.Roof_Gableroof:
                    roof.PredefinedType = IfcRoofTypeEnum.GABLE_ROOF;
                    break;
                case ElementType.Roof_GambrelRoof:
                    roof.PredefinedType = IfcRoofTypeEnum.GAMBREL_ROOF;
                    break;
                case ElementType.Roof_HippedGableRoof:
                    roof.PredefinedType = IfcRoofTypeEnum.HIPPED_GABLE_ROOF;
                    break;
                case ElementType.Roof_Hiproof:
                    roof.PredefinedType = IfcRoofTypeEnum.HIP_ROOF;
                    break;
                case ElementType.Roof_MansardRoof:
                    roof.PredefinedType = IfcRoofTypeEnum.MANSARD_ROOF;
                    break;
                case ElementType.Roof_Notdefined:
                    roof.PredefinedType = IfcRoofTypeEnum.NOTDEFINED;
                    break;
                case ElementType.Roof_PavilionRoof:
                    roof.PredefinedType = IfcRoofTypeEnum.PAVILION_ROOF;
                    break;
                case ElementType.Roof_RainbowRoof:
                    roof.PredefinedType = IfcRoofTypeEnum.RAINBOW_ROOF;
                    break;
                case ElementType.Roof_Shedroof:
                    roof.PredefinedType = IfcRoofTypeEnum.SHED_ROOF;
                    break;
                case ElementType.Roof_Userdefined:
                    roof.PredefinedType = IfcRoofTypeEnum.USERDEFINED;
                    break;

                default:
                    throw new ArgumentException("Roof type not recognized");
            }


            ApplyRepresentationAndPlacement(model, roof, shape, insertPlane);

            return roof;
        }

        private static IfcSlab CreateSlab(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var slab = model.Instances.New<IfcSlab>();
            slab.Name = name;

            switch (type)
            {
                case ElementType.Slab_Baseslab:
                    slab.PredefinedType = IfcSlabTypeEnum.BASESLAB;
                    break;
                case ElementType.Slab_Floor:
                    slab.PredefinedType = IfcSlabTypeEnum.FLOOR;
                    break;
                case ElementType.Slab_Landing:
                    slab.PredefinedType = IfcSlabTypeEnum.LANDING;
                    break;
                case ElementType.Slab_Roof:
                    slab.PredefinedType = IfcSlabTypeEnum.ROOF;
                    break;
                case ElementType.Slab_Userdefined:
                    slab.PredefinedType = IfcSlabTypeEnum.USERDEFINED;
                    break;
                case ElementType.Slab_Notdefined:
                    slab.PredefinedType = IfcSlabTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Slab type not recognized");
            }

            ApplyRepresentationAndPlacement(model, slab, shape, insertPlane);

            return slab;
        }


        private static IfcStairFlight CreateStairFlight(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var stairFlight = model.Instances.New<IfcStairFlight>();
            stairFlight.Name = name;

            switch (type)
            {
                case ElementType.StairFlight_Straight:
                    stairFlight.PredefinedType = IfcStairFlightTypeEnum.STRAIGHT;
                    break;
                case ElementType.StairFlight_Winder:
                    stairFlight.PredefinedType = IfcStairFlightTypeEnum.WINDER;
                    break;
                case ElementType.StairFlight_Spiral:
                    stairFlight.PredefinedType = IfcStairFlightTypeEnum.SPIRAL;
                    break;
                case ElementType.StairFlight_Curved:
                    stairFlight.PredefinedType = IfcStairFlightTypeEnum.CURVED;
                    break;
                case ElementType.StairFlight_Freeform:
                    stairFlight.PredefinedType = IfcStairFlightTypeEnum.FREEFORM;
                    break;
                case ElementType.StairFlight_Userdefined:
                    stairFlight.PredefinedType = IfcStairFlightTypeEnum.USERDEFINED;
                    break;
                case ElementType.StairFlight_Notdefined:
                    stairFlight.PredefinedType = IfcStairFlightTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Stair Flight type not recognized");
            }
            ApplyRepresentationAndPlacement(model, stairFlight, shape, insertPlane);

            return stairFlight;
        }

        private static IfcWall CreateWall(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var wall = model.Instances.New<IfcWall>();
            wall.Name = name;

            switch (type)
            {
                case ElementType.Wall_Elementedwall:
                    wall.PredefinedType = IfcWallTypeEnum.ELEMENTEDWALL;
                    break;
                case ElementType.Wall_Movable:
                    wall.PredefinedType = IfcWallTypeEnum.MOVABLE;
                    break;
                case ElementType.Wall_Notdefined:
                    wall.PredefinedType = IfcWallTypeEnum.NOTDEFINED;
                    break;
                case ElementType.Wall_Parapet:
                    wall.PredefinedType = IfcWallTypeEnum.PARAPET;
                    break;
                case ElementType.Wall_Partitioning:
                    wall.PredefinedType = IfcWallTypeEnum.PARTITIONING;
                    break;
                case ElementType.Wall_Plumbingwall:
                    wall.PredefinedType = IfcWallTypeEnum.PLUMBINGWALL;
                    break;
                case ElementType.Wall_Polygonal:
                    wall.PredefinedType = IfcWallTypeEnum.POLYGONAL;
                    break;
                case ElementType.Wall_Shear:
                    wall.PredefinedType = IfcWallTypeEnum.SHEAR;
                    break;
                case ElementType.Wall_Solidwall:
                    wall.PredefinedType = IfcWallTypeEnum.SOLIDWALL;
                    break;
                case ElementType.Wall_Standard:
                    wall.PredefinedType = IfcWallTypeEnum.STANDARD;
                    break;
                case ElementType.Wall_Userdefined:
                    wall.PredefinedType = IfcWallTypeEnum.USERDEFINED;
                    break;

                default:
                    throw new ArgumentException("Wall type not recognized");
            }
            ApplyRepresentationAndPlacement(model, wall, shape, insertPlane);

            return wall;
        }

        private static IfcWindow CreateWindow(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var window = model.Instances.New<IfcWindow>();
            window.Name = name;

            switch (type)
            {
                case ElementType.Window:
                    window.PredefinedType = IfcWindowTypeEnum.WINDOW;
                    break;
                case ElementType.Window_Lightdome:
                    window.PredefinedType = IfcWindowTypeEnum.LIGHTDOME;
                    break;
                case ElementType.Window_Notdefined:
                    window.PredefinedType = IfcWindowTypeEnum.NOTDEFINED;
                    break;
                case ElementType.Window_Skylight:
                    window.PredefinedType = IfcWindowTypeEnum.SKYLIGHT;
                    break;
                case ElementType.Window_Userdefined:
                    window.PredefinedType = IfcWindowTypeEnum.USERDEFINED;
                    break;

                default:
                    throw new ArgumentException("Window type not recognized");
            }

            ApplyRepresentationAndPlacement(model, window, shape, insertPlane);

            return window;
        }

        private static IfcBuildingElementProxy CreateBuildingElementProxy(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var BuildingElementProxy = model.Instances.New<IfcBuildingElementProxy>();
            BuildingElementProxy.Name = name;

            switch (type)
            {
                case ElementType.BuildingElementProxy_Complex:
                    BuildingElementProxy.PredefinedType = IfcBuildingElementProxyTypeEnum.COMPLEX;
                    break;
                case ElementType.BuildingElementProxy_Element:
                    BuildingElementProxy.PredefinedType = IfcBuildingElementProxyTypeEnum.ELEMENT;
                    break;
                case ElementType.BuildingElementProxy_Partial:
                    BuildingElementProxy.PredefinedType = IfcBuildingElementProxyTypeEnum.PARTIAL;
                    break;
                case ElementType.BuildingElementProxy_ProvisionForVoid:
                    BuildingElementProxy.PredefinedType = IfcBuildingElementProxyTypeEnum.PROVISIONFORVOID;
                    break;
                case ElementType.BuildingElementProxy_Userdefined:
                    BuildingElementProxy.PredefinedType = IfcBuildingElementProxyTypeEnum.USERDEFINED;
                    break;
                case ElementType.BuildingElementProxy_Notdefined:
                    BuildingElementProxy.PredefinedType = IfcBuildingElementProxyTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Building Element Proxy type not recognized");

            }

            ApplyRepresentationAndPlacement(model, BuildingElementProxy, shape, insertPlane);

            return BuildingElementProxy;
        }

        private static IfcBuildingElementPart CreateBuildingElementPart(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var BuildingElementPart = model.Instances.New<IfcBuildingElementPart>();
            BuildingElementPart.Name = name;

            switch (type)
            {
                case ElementType.BuildingElementPart_Insulation:
                    BuildingElementPart.PredefinedType = IfcBuildingElementPartTypeEnum.INSULATION;
                    break;
                case ElementType.BuildingElementPart_Precastpanel:
                    BuildingElementPart.PredefinedType = IfcBuildingElementPartTypeEnum.PRECASTPANEL;
                    break;
                case ElementType.BuildingElementPart_Userdefined:
                    BuildingElementPart.PredefinedType = IfcBuildingElementPartTypeEnum.USERDEFINED;
                    break;
                case ElementType.BuildingElementPart_Notdefined:
                    BuildingElementPart.PredefinedType = IfcBuildingElementPartTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Reinforing Mesh type not recognized");

            }

            ApplyRepresentationAndPlacement(model, BuildingElementPart, shape, insertPlane);

            return BuildingElementPart;
        }

        private static IfcReinforcingBar CreateReinforcingBar(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var ReinforcingBar = model.Instances.New<IfcReinforcingBar>();
            ReinforcingBar.Name = name;

            switch (type)
            {
                case ElementType.ReinforcingBar_Anchoring:
                    ReinforcingBar.PredefinedType = IfcReinforcingBarTypeEnum.ANCHORING;
                    break;
                case ElementType.ReinforcingBar_Edge:
                    ReinforcingBar.PredefinedType = IfcReinforcingBarTypeEnum.EDGE;
                    break;
                case ElementType.ReinforcingBar_Ligature:
                    ReinforcingBar.PredefinedType = IfcReinforcingBarTypeEnum.LIGATURE;
                    break;
                case ElementType.ReinforcingBar_Main:
                    ReinforcingBar.PredefinedType = IfcReinforcingBarTypeEnum.MAIN;
                    break;
                case ElementType.ReinforcingBar_Punching:
                    ReinforcingBar.PredefinedType = IfcReinforcingBarTypeEnum.PUNCHING;
                    break;
                case ElementType.ReinforcingBar_Ring:
                    ReinforcingBar.PredefinedType = IfcReinforcingBarTypeEnum.RING;
                    break;
                case ElementType.ReinforcingBar_Shear:
                    ReinforcingBar.PredefinedType = IfcReinforcingBarTypeEnum.SHEAR;
                    break;
                case ElementType.ReinforcingBar_Stud:
                    ReinforcingBar.PredefinedType = IfcReinforcingBarTypeEnum.STUD;
                    break;
                case ElementType.ReinforcingBar_Userdefined:
                    ReinforcingBar.PredefinedType = IfcReinforcingBarTypeEnum.USERDEFINED;
                    break;
                case ElementType.ReinforcingBar_Notdefined:
                    ReinforcingBar.PredefinedType = IfcReinforcingBarTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Reinforing Mesh type not recognized");

            }

            ApplyRepresentationAndPlacement(model, ReinforcingBar, shape, insertPlane);

            return ReinforcingBar;
        }

        private static IfcReinforcingMesh CreateReinforcingMesh(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var ReinforcingMesh = model.Instances.New<IfcReinforcingMesh>();
            ReinforcingMesh.Name = name;

            switch (type)
            {
                case ElementType.Reinforcingmesh_Userdefined:
                    ReinforcingMesh.PredefinedType = IfcReinforcingMeshTypeEnum.USERDEFINED;
                    break;
                case ElementType.Reinforcingmesh_Notdefined:
                    ReinforcingMesh.PredefinedType = IfcReinforcingMeshTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Reinforing Mesh type not recognized");
            }

            ApplyRepresentationAndPlacement(model, ReinforcingMesh, shape, insertPlane);

            return ReinforcingMesh;
        }

        private static IfcTendon CreateTendon(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var Tendon = model.Instances.New<IfcTendon>();
            Tendon.Name = name;

            switch (type)
            {
                case ElementType.Tendon_Bar:
                    Tendon.PredefinedType = IfcTendonTypeEnum.BAR;
                    break;
                case ElementType.Tendon_Coated:
                    Tendon.PredefinedType = IfcTendonTypeEnum.COATED;
                    break;
                case ElementType.Tendon_Strand:
                    Tendon.PredefinedType = IfcTendonTypeEnum.STRAND;
                    break;
                case ElementType.Tendon_Wire:
                    Tendon.PredefinedType = IfcTendonTypeEnum.WIRE;
                    break;
                case ElementType.Tendon_Userdefined:
                    Tendon.PredefinedType = IfcTendonTypeEnum.USERDEFINED;
                    break;
                case ElementType.Tendon_Notdefined:
                    Tendon.PredefinedType = IfcTendonTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Tendon type not recognized");
            }

            ApplyRepresentationAndPlacement(model, Tendon, shape, insertPlane);



            return Tendon;
        }

        private static IfcTendonAnchor CreateTendonAnchor(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var TendonAnchor = model.Instances.New<IfcTendonAnchor>();
            TendonAnchor.Name = name;

            switch (type)
            {
                case ElementType.TendonAnchor_Coupler:
                    TendonAnchor.PredefinedType = IfcTendonAnchorTypeEnum.COUPLER;
                    break;
                case ElementType.TendonAnchor_FixedEnd:
                    TendonAnchor.PredefinedType = IfcTendonAnchorTypeEnum.FIXED_END;
                    break;
                case ElementType.TendonAnchor_TensioningEnd:
                    TendonAnchor.PredefinedType = IfcTendonAnchorTypeEnum.TENSIONING_END;
                    break;
                case ElementType.TendonAnchor_Userdefined:
                    TendonAnchor.PredefinedType = IfcTendonAnchorTypeEnum.USERDEFINED;
                    break;
                case ElementType.TendonAnchor_Notdefined:
                    TendonAnchor.PredefinedType = IfcTendonAnchorTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Tendon Anchor type not recognized");
            }

            ApplyRepresentationAndPlacement(model, TendonAnchor, shape, insertPlane);

            return TendonAnchor;
        }

        private static IfcDistributionElement CreateDistributionElement(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var DistributionElement = model.Instances.New<IfcDistributionElement>();
            DistributionElement.Name = name;
            // IfcDistributionElement doesnt have a Predefined type, is too broad a class

            ApplyRepresentationAndPlacement(model, DistributionElement, shape, insertPlane);

            return DistributionElement;
        }

        private static IfcDistributionControlElement CreateDistributionControlElement(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var DistributionControlElement = model.Instances.New<IfcDistributionControlElement>();
            DistributionControlElement.Name = name;
            // IfcDistributionControlElement doesnt have a Predefined type, is too broad a class

            ApplyRepresentationAndPlacement(model, DistributionControlElement, shape, insertPlane);

            return DistributionControlElement;
        }

        private static IfcDistributionFlowElement CreateDistributionFlowElement(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var DistributionFlowElement = model.Instances.New<IfcDistributionFlowElement>();
            DistributionFlowElement.Name = name;
            // IfcDistributionFlowElementElement doesnt have a Predefined type, is too broad a class

            ApplyRepresentationAndPlacement(model, DistributionFlowElement, shape, insertPlane);

            return DistributionFlowElement;
        }

        private static IfcDistributionChamberElement CreateDistributionChamberElement(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var DistributionChamberElement = model.Instances.New<IfcDistributionChamberElement>();
            DistributionChamberElement.Name = name;

            switch (type)
            {
                case ElementType.DistributionChamberElement_FormedDuct:
                    DistributionChamberElement.PredefinedType = IfcDistributionChamberElementTypeEnum.FORMEDDUCT;
                    break;
                case ElementType.DistributionChamberElement_InspectionChamber:
                    DistributionChamberElement.PredefinedType = IfcDistributionChamberElementTypeEnum.INSPECTIONCHAMBER;
                    break;
                case ElementType.DistributionChamberElement_InspectionPit:
                    DistributionChamberElement.PredefinedType = IfcDistributionChamberElementTypeEnum.INSPECTIONPIT;
                    break;
                case ElementType.DistributionChamberElement_Manhole:
                    DistributionChamberElement.PredefinedType = IfcDistributionChamberElementTypeEnum.MANHOLE;
                    break;
                case ElementType.DistributionChamberElement_MeterChamber:
                    DistributionChamberElement.PredefinedType = IfcDistributionChamberElementTypeEnum.METERCHAMBER;
                    break;
                case ElementType.DistributionChamberElement_Sump:
                    DistributionChamberElement.PredefinedType = IfcDistributionChamberElementTypeEnum.SUMP;
                    break;
                case ElementType.DistributionChamberElement_Trench:
                    DistributionChamberElement.PredefinedType = IfcDistributionChamberElementTypeEnum.TRENCH;
                    break;
                case ElementType.DistributionChamberElement_ValveChamber:
                    DistributionChamberElement.PredefinedType = IfcDistributionChamberElementTypeEnum.VALVECHAMBER;
                    break;
                case ElementType.DistributionChamberElement_Userdefined:
                    DistributionChamberElement.PredefinedType = IfcDistributionChamberElementTypeEnum.USERDEFINED;
                    break;
                case ElementType.DistributionChamberElement_Notdefined:
                    DistributionChamberElement.PredefinedType = IfcDistributionChamberElementTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Distribution Chamber Element type not recognized");
            }

            ApplyRepresentationAndPlacement(model, DistributionChamberElement, shape, insertPlane);

            return DistributionChamberElement;
        }

        private static IfcEnergyConversionDevice CreateEnergyConversionDevice(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var EnergyConversionDevice = model.Instances.New<IfcEnergyConversionDevice>();
            EnergyConversionDevice.Name = name;
            // IfcEnergyConversionDevice doesnt have a Predefined type, is too broad a class

            ApplyRepresentationAndPlacement(model, EnergyConversionDevice, shape, insertPlane);

            return EnergyConversionDevice;
        }

        private static IfcFlowController CreateFlowController(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var FlowController = model.Instances.New<IfcFlowController>();
            FlowController.Name = name;
            // IfcFlowController doesnt have a Predefined type, is too broad a class

            ApplyRepresentationAndPlacement(model, FlowController, shape, insertPlane);

            return FlowController;
        }

        private static IfcFlowFitting CreateFlowFitting(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var FlowFitting = model.Instances.New<IfcFlowFitting>();
            FlowFitting.Name = name;
            // IfcFlowFitting doesnt have a Predefined type, is too broad a class

            ApplyRepresentationAndPlacement(model, FlowFitting, shape, insertPlane);

            return FlowFitting;
        }

        private static IfcFlowMovingDevice CreateFlowMovingDevice(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var FlowMovingDevice = model.Instances.New<IfcFlowMovingDevice>();
            FlowMovingDevice.Name = name;
            // IfcFlowMovingDeviceType doesnt have a Predefined type, is too broad a class

            ApplyRepresentationAndPlacement(model, FlowMovingDevice, shape, insertPlane);

            return FlowMovingDevice;
        }

        private static IfcFlowSegment CreateFlowSegment(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var FlowSegment = model.Instances.New<IfcFlowSegment>();
            FlowSegment.Name = name;
            // IfcFlowMovingDeviceType doesnt have a Predefined type, is too broad a class

            ApplyRepresentationAndPlacement(model, FlowSegment, shape, insertPlane);

            return FlowSegment;
        }

        private static IfcFlowStorageDevice CreateFlowStorageDevice(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var FlowStorageDevice = model.Instances.New<IfcFlowStorageDevice>();
            FlowStorageDevice.Name = name;
            // IfcFlowMovingDeviceType doesnt have a Predefined type, is too broad a class

            ApplyRepresentationAndPlacement(model, FlowStorageDevice, shape, insertPlane);

            return FlowStorageDevice;
        }

        private static IfcFlowTerminal CreateFlowTerminal(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var FlowTerminal = model.Instances.New<IfcFlowTerminal>();
            FlowTerminal.Name = name;
            // IfcFlowMovingDeviceType doesnt have a Predefined type, is too broad a class

            ApplyRepresentationAndPlacement(model, FlowTerminal, shape, insertPlane);

            return FlowTerminal;
        }

        private static IfcFlowTreatmentDevice CreateFlowTreatmentDevice(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var FlowTreatmentDevice = model.Instances.New<IfcFlowTreatmentDevice>();
            FlowTreatmentDevice.Name = name;
            // IfcFlowMovingDeviceType doesnt have a Predefined type, is too broad a class

            ApplyRepresentationAndPlacement(model, FlowTreatmentDevice, shape, insertPlane);

            return FlowTreatmentDevice;
        }

        private static IfcDiscreteAccessory CreateDiscreteAccessory(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var DiscreteAccessory = model.Instances.New<IfcDiscreteAccessory>();
            DiscreteAccessory.Name = name;

            switch (type)
            {
                case ElementType.DiscreteAccessory_Anchorplate:
                    DiscreteAccessory.PredefinedType = IfcDiscreteAccessoryTypeEnum.ANCHORPLATE;
                    break;
                case ElementType.DiscreteAccessory_Bracket:
                    DiscreteAccessory.PredefinedType = IfcDiscreteAccessoryTypeEnum.BRACKET;
                    break;
                case ElementType.DiscreteAccessory_Shoe:
                    DiscreteAccessory.PredefinedType = IfcDiscreteAccessoryTypeEnum.SHOE;
                    break;
                case ElementType.DiscreteAccessory_Userdefined:
                    DiscreteAccessory.PredefinedType = IfcDiscreteAccessoryTypeEnum.USERDEFINED;
                    break;
                case ElementType.DiscreteAccessory_Notdefined:
                    DiscreteAccessory.PredefinedType = IfcDiscreteAccessoryTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Discrete Accessory type not recognized");
            }

            ApplyRepresentationAndPlacement(model, DiscreteAccessory, shape, insertPlane);

            return DiscreteAccessory;
        }

        private static IfcElementAssembly CreateElementAssembly(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var ElementAssembly = model.Instances.New<IfcElementAssembly>();
            ElementAssembly.Name = name;

            switch (type)
            {
                case ElementType.ElementAssembly_AccessoryAssembly:
                    ElementAssembly.PredefinedType = IfcElementAssemblyTypeEnum.ACCESSORY_ASSEMBLY;
                    break;
                case ElementType.ElementAssembly_Arch:
                    ElementAssembly.PredefinedType = IfcElementAssemblyTypeEnum.ARCH;
                    break;
                case ElementType.ElementAssembly_BeamGrid:
                    ElementAssembly.PredefinedType = IfcElementAssemblyTypeEnum.BEAM_GRID;
                    break;
                case ElementType.ElementAssembly_BracedFrame:
                    ElementAssembly.PredefinedType = IfcElementAssemblyTypeEnum.BRACED_FRAME;
                    break;
                case ElementType.ElementAssembly_Girder:
                    ElementAssembly.PredefinedType = IfcElementAssemblyTypeEnum.GIRDER;
                    break;
                case ElementType.ElementAssembly_ReinforcementUnit:
                    ElementAssembly.PredefinedType = IfcElementAssemblyTypeEnum.REINFORCEMENT_UNIT;
                    break;
                case ElementType.ElementAssembly_RigidFrame:
                    ElementAssembly.PredefinedType = IfcElementAssemblyTypeEnum.RIGID_FRAME;
                    break;
                case ElementType.ElementAssembly_SlabField:
                    ElementAssembly.PredefinedType = IfcElementAssemblyTypeEnum.SLAB_FIELD;
                    break;
                case ElementType.ElementAssembly_Truss:
                    ElementAssembly.PredefinedType = IfcElementAssemblyTypeEnum.TRUSS;
                    break;
                case ElementType.ElementAssembly_Userdefined:
                    ElementAssembly.PredefinedType = IfcElementAssemblyTypeEnum.USERDEFINED;
                    break;
                case ElementType.ElementAssembly_Notdefined:
                    ElementAssembly.PredefinedType = IfcElementAssemblyTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Element Assembly type not recognized");
            }

            ApplyRepresentationAndPlacement(model, ElementAssembly, shape, insertPlane);

            return ElementAssembly;
        }

        private static IfcMechanicalFastener CreateMechanicalFastener(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var MechanicalFastener = model.Instances.New<IfcMechanicalFastener>();
            MechanicalFastener.Name = name;

            switch (type)
            {
                case ElementType.MechanicalFastener_AnchorBolt:
                    MechanicalFastener.PredefinedType = IfcMechanicalFastenerTypeEnum.ANCHORBOLT;
                    break;
                case ElementType.MechanicalFastener_Bolt:
                    MechanicalFastener.PredefinedType = IfcMechanicalFastenerTypeEnum.BOLT;
                    break;
                case ElementType.MechanicalFastener_Dowel:
                    MechanicalFastener.PredefinedType = IfcMechanicalFastenerTypeEnum.DOWEL;
                    break;
                case ElementType.MechanicalFastener_Nail:
                    MechanicalFastener.PredefinedType = IfcMechanicalFastenerTypeEnum.NAIL;
                    break;
                case ElementType.MechanicalFastener_NailPlate:
                    MechanicalFastener.PredefinedType = IfcMechanicalFastenerTypeEnum.NAILPLATE;
                    break;
                case ElementType.MechanicalFastener_Rivet:
                    MechanicalFastener.PredefinedType = IfcMechanicalFastenerTypeEnum.RIVET;
                    break;
                case ElementType.MechanicalFastener_Screw:
                    MechanicalFastener.PredefinedType = IfcMechanicalFastenerTypeEnum.SCREW;
                    break;
                case ElementType.MechanicalFastener_ShearConnector:
                    MechanicalFastener.PredefinedType = IfcMechanicalFastenerTypeEnum.SHEARCONNECTOR;
                    break;
                case ElementType.MechanicalFastener_Staple:
                    MechanicalFastener.PredefinedType = IfcMechanicalFastenerTypeEnum.STAPLE;
                    break;
                case ElementType.MechanicalFastener_StudShearConnector:
                    MechanicalFastener.PredefinedType = IfcMechanicalFastenerTypeEnum.STUDSHEARCONNECTOR;
                    break;
                case ElementType.MechanicalFastener_Userdefined:
                    MechanicalFastener.PredefinedType = IfcMechanicalFastenerTypeEnum.USERDEFINED;
                    break;
                case ElementType.MechanicalFastener_Notdefined:
                    MechanicalFastener.PredefinedType = IfcMechanicalFastenerTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Mechanical Fastener type not recognized");
            }

            ApplyRepresentationAndPlacement(model, MechanicalFastener, shape, insertPlane);

            return MechanicalFastener;
        }

        private static IfcFastener CreateFastener(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var Fastener = model.Instances.New<IfcFastener>();
            Fastener.Name = name;

            switch (type)
            {
                case ElementType.Fastener_Glue:
                    Fastener.PredefinedType = IfcFastenerTypeEnum.GLUE;
                    break;
                case ElementType.Fastener_Mortar:
                    Fastener.PredefinedType = IfcFastenerTypeEnum.MORTAR;
                    break;
                case ElementType.Fastener_Weld:
                    Fastener.PredefinedType = IfcFastenerTypeEnum.WELD;
                    break;
                case ElementType.Fastener_Userdefined:
                    Fastener.PredefinedType = IfcFastenerTypeEnum.USERDEFINED;
                    break;
                case ElementType.Fastener_Notdefined:
                    Fastener.PredefinedType = IfcFastenerTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Fastener type not recognized");
            }

            ApplyRepresentationAndPlacement(model, Fastener, shape, insertPlane);

            return Fastener;
        }

        private static IfcFurnishingElement CreateFurnishingElement(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var FurnishingElement = model.Instances.New<IfcFurnishingElement>();
            FurnishingElement.Name = name;
            // FurnishingElement does not have different types

            ApplyRepresentationAndPlacement(model, FurnishingElement, shape, insertPlane);

            return FurnishingElement;
        }

        private static IfcTransportElement CreateTransportElement(IfcStore model, ElementType type, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var TransportElement = model.Instances.New<IfcTransportElement>();
            TransportElement.Name = name;

            switch (type)
            {
                case ElementType.TransportElement_Elevator:
                    TransportElement.PredefinedType = IfcTransportElementTypeEnum.ELEVATOR;
                    break;
                case ElementType.TransportElement_Escalator:
                    TransportElement.PredefinedType = IfcTransportElementTypeEnum.ESCALATOR;
                    break;
                case ElementType.TransportElement_MovingWalkWay:
                    TransportElement.PredefinedType = IfcTransportElementTypeEnum.MOVINGWALKWAY;
                    break;
                case ElementType.TransportElement_Craneway:
                    TransportElement.PredefinedType = IfcTransportElementTypeEnum.CRANEWAY;
                    break;
                case ElementType.TransportElement_LiftingGear:
                    TransportElement.PredefinedType = IfcTransportElementTypeEnum.LIFTINGGEAR;
                    break;
                case ElementType.TransportElement_Userdefined:
                    TransportElement.PredefinedType = IfcTransportElementTypeEnum.USERDEFINED;
                    break;
                case ElementType.TransportElement_Notdefined:
                    TransportElement.PredefinedType = IfcTransportElementTypeEnum.NOTDEFINED;
                    break;

                default:
                    throw new ArgumentException("Transport Element type not recognized");
            }

            ApplyRepresentationAndPlacement(model, TransportElement, shape, insertPlane);

            return TransportElement;
        }

        private static IfcVirtualElement CreateVirtualElement(IfcStore model, string name, IfcShapeRepresentation shape, Plane insertPlane)
        {
            var VirtualElement = model.Instances.New<IfcVirtualElement>();
            VirtualElement.Name = name;
            // VirtualElement does not have different types

            ApplyRepresentationAndPlacement(model, VirtualElement, shape, insertPlane);

            return VirtualElement;
        }

        //subtypes part 2 start here
        public static List<IfcBuildingElement> CreateBuildingElements(IfcStore model, ElementType type, string name,
            IfcShapeRepresentation shape, List<Plane> insertPlanes, IfcRelAssociatesMaterial relAssociatesMaterial)
        {
            var buildingElements = new List<IfcBuildingElement>();

            switch (type)
            {
                case ElementType.PadFooting:
                case ElementType.StripFooting:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var footing = CreateFooting(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(footing);
                            buildingElements.Add(footing);
                        }
                        break;
                    }
                case ElementType.Beam_Joist:
                case ElementType.Beam_Hollowcore:
                case ElementType.Beam_Lintel:
                case ElementType.Beam_Spandrel:
                case ElementType.Beam_T_Beam:
                case ElementType.Beam_Userdefined:
                case ElementType.Beam_Notdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var beam = CreateBeam(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(beam);
                            buildingElements.Add(beam);
                        }
                        break;
                    }

                case ElementType.Column:
                case ElementType.Column_Pilaster:
                case ElementType.Column_Userdefined:
                case ElementType.Column_Notdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var column = CreateColumn(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(column);
                            buildingElements.Add(column);
                        }
                        break;
                    }

                case ElementType.Door:
                case ElementType.Door_Gate:
                case ElementType.Door_Trapdoor:
                case ElementType.Door_Userdefined:
                case ElementType.Door_Notdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var door = CreateDoor(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(door);
                            buildingElements.Add(door);
                        }
                        break;
                    }


                case ElementType.Stair_StraightRunStair:
                case ElementType.Stair_TwoStraightRunStair:
                case ElementType.Stair_QuarterWindingStair:
                case ElementType.Stair_QuarterTurnStair:
                case ElementType.Stair_HalfWindingStair:
                case ElementType.Stair_HalfTurnStair:
                case ElementType.Stair_TwoQuarterWindingStair:
                case ElementType.Stair_TwoQuarterTurnStair:
                case ElementType.Stair_ThreeQuarterWindingStair:
                case ElementType.Stair_ThreeQuarterTurnStair:
                case ElementType.Stair_SpiralStair:
                case ElementType.Stair_DoubleReturnStair:
                case ElementType.Stair_CurvedRunStair:
                case ElementType.Stair_TwoCurvedRunStair:
                case ElementType.Stair_Userdefined:
                case ElementType.Stair_Notdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var stair = CreateStair(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(stair);
                            buildingElements.Add(stair);
                        }
                        break;
                    }

                case ElementType.Covering_Ceiling:
                case ElementType.Covering_Cladding:
                case ElementType.Covering_Flooring:
                case ElementType.Covering_Insulation:
                case ElementType.Covering_Membrane:
                case ElementType.Covering_Molding:
                case ElementType.Covering_Notdefined:
                case ElementType.Covering_Roofing:
                case ElementType.Covering_Skirtingboard:
                case ElementType.Covering_Sleeving:
                case ElementType.Covering_Userdefined:
                case ElementType.Covering_Wrapping:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var covering = CreateCovering(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(covering);
                            buildingElements.Add(covering);
                        }
                        break;
                    }
                case ElementType.CurtainWall_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var curtainWall = CreateCurtainWall(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(curtainWall);
                            buildingElements.Add(curtainWall);
                        }
                        break;
                    }
                case ElementType.CurtainWall_Notdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var curtainWall = CreateCurtainWall(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(curtainWall);
                            buildingElements.Add(curtainWall);
                        }
                        break;
                    }
                case ElementType.Footing_Caisson_Foundation:
                case ElementType.Footing_Footing_Beam:
                case ElementType.Footing_Notdefined:
                case ElementType.Footing_Pad_Footing:
                case ElementType.Footing_Pile_Cap:
                case ElementType.Footing_Strip_Footing:
                case ElementType.Footing_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var footing = CreateFooting(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(footing);
                            buildingElements.Add(footing);
                        }
                        break;
                    }
                case ElementType.Member_Brace:
                case ElementType.Member_Chord:
                case ElementType.Member_Collar:
                case ElementType.Member_Member:
                case ElementType.Member_Mullion:
                case ElementType.Member_Notdefined:
                case ElementType.Member_Plate:
                case ElementType.Member_Post:
                case ElementType.Member_Purlin:
                case ElementType.Member_Rafter:
                case ElementType.Member_Stringer:
                case ElementType.Member_Strut:
                case ElementType.Member_Stud:
                case ElementType.Member_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var member = CreateMember(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(member);
                            buildingElements.Add(member);
                        }
                        break;
                    }
                case ElementType.Pile_Bored:
                case ElementType.Pile_Cohesion:
                case ElementType.Pile_Driven:
                case ElementType.Pile_Friction:
                case ElementType.Pile_Jetgrouting:
                case ElementType.Pile_Notdefined:
                case ElementType.Pile_Support:
                case ElementType.Pile_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var pile = CreatePile(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(pile);
                            buildingElements.Add(pile);
                        }
                        break;
                    }

                case ElementType.Plate_Curtain_Panel:
                case ElementType.Plate_Sheet:
                case ElementType.Plate_Userdefined:
                case ElementType.Plate_Notdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var plate = CreatePlate(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(plate);
                            buildingElements.Add(plate);
                        }
                        break;
                    }
                case ElementType.Railing_Handrail:
                case ElementType.Railing_Guardrail:
                case ElementType.Railing_Balustrade:
                case ElementType.Railing_Userdefined:
                case ElementType.Railing_Notdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var railing = CreateRailing(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(railing);
                            buildingElements.Add(railing);
                        }
                        break;
                    }
                case ElementType.Ramp_HalfTurnRamp:
                case ElementType.Ramp_Notdefined:
                case ElementType.Ramp_QuarterTurnRamp:
                case ElementType.Ramp_SpiralRamp:
                case ElementType.Ramp_StraightRunRamp:
                case ElementType.Ramp_TwoQuarterTurnRamp:
                case ElementType.Ramp_TwoStraightRunRamp:
                case ElementType.Ramp_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var ramp = CreateRamp(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(ramp);
                            buildingElements.Add(ramp);
                        }
                        break;
                    }
                case ElementType.RampFlight_Straight:
                case ElementType.RampFlight_Spiral:
                case ElementType.RampFlight_Userdefined:
                case ElementType.RampFlight_Notdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var rampFlight = CreateRampFlight(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(rampFlight);
                            buildingElements.Add(rampFlight);
                        }
                        break;
                    }

                case ElementType.Roof_BarrelRoof:
                case ElementType.Roof_ButterflyRoof:
                case ElementType.Roof_DomeRoof:
                case ElementType.Roof_Flatroof:
                case ElementType.Roof_Freeform:
                case ElementType.Roof_Gableroof:
                case ElementType.Roof_GambrelRoof:
                case ElementType.Roof_HippedGableRoof:
                case ElementType.Roof_Hiproof:
                case ElementType.Roof_MansardRoof:
                case ElementType.Roof_Notdefined:
                case ElementType.Roof_PavilionRoof:
                case ElementType.Roof_RainbowRoof:
                case ElementType.Roof_Shedroof:
                case ElementType.Roof_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var roof = CreateRoof(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(roof);
                            buildingElements.Add(roof);
                        }
                        break;
                    }

                case ElementType.Slab_Baseslab:
                case ElementType.Slab_Floor:
                case ElementType.Slab_Landing:
                case ElementType.Slab_Notdefined:
                case ElementType.Slab_Roof:
                case ElementType.Slab_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var slab = CreateSlab(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(slab);
                            buildingElements.Add(slab);
                        }
                        break;
                    }

                case ElementType.StairFlight_Curved:
                case ElementType.StairFlight_Freeform:
                case ElementType.StairFlight_Notdefined:
                case ElementType.StairFlight_Spiral:
                case ElementType.StairFlight_Straight:
                case ElementType.StairFlight_Userdefined:
                case ElementType.StairFlight_Winder:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var stairFlight = CreateStairFlight(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(stairFlight);
                            buildingElements.Add(stairFlight);
                        }
                        break;
                    }

                case ElementType.Wall_Movable:
                case ElementType.Wall_Parapet:
                case ElementType.Wall_Elementedwall:
                case ElementType.Wall_Notdefined:
                case ElementType.Wall_Partitioning:
                case ElementType.Wall_Plumbingwall:
                case ElementType.Wall_Polygonal:
                case ElementType.Wall_Shear:
                case ElementType.Wall_Solidwall:
                case ElementType.Wall_Standard:
                case ElementType.Wall_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var wall = CreateWall(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(wall);
                            buildingElements.Add(wall);
                        }
                        break;
                    }

                case ElementType.Window:
                case ElementType.Window_Skylight:
                case ElementType.Window_Lightdome:
                case ElementType.Window_Userdefined:
                case ElementType.Window_Notdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var window = CreateWindow(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(window);
                            buildingElements.Add(window);
                        }
                        break;
                    }


                case ElementType.BuildingElementProxy_Complex:
                case ElementType.BuildingElementProxy_Element:
                case ElementType.BuildingElementProxy_Notdefined:
                case ElementType.BuildingElementProxy_Partial:
                case ElementType.BuildingElementProxy_ProvisionForVoid:
                case ElementType.BuildingElementProxy_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var BuildingElementProxy = CreateBuildingElementProxy(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(BuildingElementProxy);
                            buildingElements.Add(BuildingElementProxy);
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Element type not recognized => CreateBuildElem");
            }

            return buildingElements;
        }

        public static List<IfcBuildingElement> CreateBuildingElements(IfcStore model, ElementType type, string name,
            List<IfcShapeRepresentation> shapes, List<Plane> insertPlanes, IfcRelAssociatesMaterial relAssociatesMaterial)
        {
            var buildingElements = new List<IfcBuildingElement>();

            switch (type)
            {
                case ElementType.PadFooting:
                case ElementType.StripFooting:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var footing = CreateFooting(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(footing);
                            buildingElements.Add(footing);
                        }
                        break;
                    }
                case ElementType.Beam_Joist:
                case ElementType.Beam_Hollowcore:
                case ElementType.Beam_Lintel:
                case ElementType.Beam_Spandrel:
                case ElementType.Beam_T_Beam:
                case ElementType.Beam_Userdefined:
                case ElementType.Beam_Notdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var beam = CreateBeam(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(beam);
                            buildingElements.Add(beam);
                        }
                        break;
                    }
                case ElementType.Column:
                case ElementType.Column_Pilaster:
                case ElementType.Column_Userdefined:
                case ElementType.Column_Notdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var column = CreateColumn(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(column);
                            buildingElements.Add(column);
                        }
                        break;
                    }
                case ElementType.Door:
                case ElementType.Door_Gate:
                case ElementType.Door_Trapdoor:
                case ElementType.Door_Userdefined:
                case ElementType.Door_Notdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var door = CreateDoor(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(door);
                            buildingElements.Add(door);
                        }
                        break;
                    }


                case ElementType.Stair_StraightRunStair:
                case ElementType.Stair_TwoStraightRunStair:
                case ElementType.Stair_QuarterWindingStair:
                case ElementType.Stair_QuarterTurnStair:
                case ElementType.Stair_HalfWindingStair:
                case ElementType.Stair_HalfTurnStair:
                case ElementType.Stair_TwoQuarterWindingStair:
                case ElementType.Stair_TwoQuarterTurnStair:
                case ElementType.Stair_ThreeQuarterWindingStair:
                case ElementType.Stair_ThreeQuarterTurnStair:
                case ElementType.Stair_SpiralStair:
                case ElementType.Stair_DoubleReturnStair:
                case ElementType.Stair_CurvedRunStair:
                case ElementType.Stair_TwoCurvedRunStair:
                case ElementType.Stair_Userdefined:
                case ElementType.Stair_Notdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var stair = CreateStair(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(stair);
                            buildingElements.Add(stair);
                        }
                        break;
                    }
                case ElementType.Covering_Ceiling:
                case ElementType.Covering_Cladding:
                case ElementType.Covering_Flooring:
                case ElementType.Covering_Insulation:
                case ElementType.Covering_Membrane:
                case ElementType.Covering_Molding:
                case ElementType.Covering_Notdefined:
                case ElementType.Covering_Roofing:
                case ElementType.Covering_Skirtingboard:
                case ElementType.Covering_Sleeving:
                case ElementType.Covering_Userdefined:
                case ElementType.Covering_Wrapping:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var Covering = CreateCovering(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(Covering);
                            buildingElements.Add(Covering);
                        }
                        break;
                    }
                case ElementType.CurtainWall_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var CurtainWall = CreateCurtainWall(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(CurtainWall);
                            buildingElements.Add(CurtainWall);
                        }
                        break;
                    }
                case ElementType.CurtainWall_Notdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var CurtainWall = CreateCurtainWall(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(CurtainWall);
                            buildingElements.Add(CurtainWall);
                        }
                        break;
                    }
                case ElementType.Member_Brace:
                case ElementType.Member_Chord:
                case ElementType.Member_Collar:
                case ElementType.Member_Member:
                case ElementType.Member_Mullion:
                case ElementType.Member_Notdefined:
                case ElementType.Member_Plate:
                case ElementType.Member_Post:
                case ElementType.Member_Purlin:
                case ElementType.Member_Rafter:
                case ElementType.Member_Stringer:
                case ElementType.Member_Strut:
                case ElementType.Member_Stud:
                case ElementType.Member_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var Member = CreateMember(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(Member);
                            buildingElements.Add(Member);
                        }
                        break;
                    }
                case ElementType.Footing_Caisson_Foundation:
                case ElementType.Footing_Footing_Beam:
                case ElementType.Footing_Notdefined:
                case ElementType.Footing_Pad_Footing:
                case ElementType.Footing_Pile_Cap:
                case ElementType.Footing_Strip_Footing:
                case ElementType.Footing_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var Footing = CreateFooting(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(Footing);
                            buildingElements.Add(Footing);
                        }
                        break;
                    }
                case ElementType.Pile_Bored:
                case ElementType.Pile_Cohesion:
                case ElementType.Pile_Driven:
                case ElementType.Pile_Friction:
                case ElementType.Pile_Jetgrouting:
                case ElementType.Pile_Notdefined:
                case ElementType.Pile_Support:
                case ElementType.Pile_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var Pile = CreatePile(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(Pile);
                            buildingElements.Add(Pile);
                        }
                        break;
                    }
                case ElementType.Plate_Curtain_Panel:
                case ElementType.Plate_Sheet:
                case ElementType.Plate_Userdefined:
                case ElementType.Plate_Notdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var Plate = CreatePlate(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(Plate);
                            buildingElements.Add(Plate);
                        }
                        break;
                    }
                case ElementType.Railing_Handrail:
                case ElementType.Railing_Guardrail:
                case ElementType.Railing_Balustrade:
                case ElementType.Railing_Userdefined:
                case ElementType.Railing_Notdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var Railing = CreateRailing(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(Railing);
                            buildingElements.Add(Railing);
                        }
                        break;
                    }
                case ElementType.Ramp_HalfTurnRamp:
                case ElementType.Ramp_Notdefined:
                case ElementType.Ramp_QuarterTurnRamp:
                case ElementType.Ramp_SpiralRamp:
                case ElementType.Ramp_StraightRunRamp:
                case ElementType.Ramp_TwoQuarterTurnRamp:
                case ElementType.Ramp_TwoStraightRunRamp:
                case ElementType.Ramp_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var Ramp = CreateRamp(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(Ramp);
                            buildingElements.Add(Ramp);
                        }
                        break;
                    }
                case ElementType.RampFlight_Straight:
                case ElementType.RampFlight_Spiral:
                case ElementType.RampFlight_Userdefined:
                case ElementType.RampFlight_Notdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var RampFlight = CreateRampFlight(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(RampFlight);
                            buildingElements.Add(RampFlight);
                        }
                        break;
                    }
                case ElementType.Roof_BarrelRoof:
                case ElementType.Roof_ButterflyRoof:
                case ElementType.Roof_DomeRoof:
                case ElementType.Roof_Flatroof:
                case ElementType.Roof_Freeform:
                case ElementType.Roof_Gableroof:
                case ElementType.Roof_GambrelRoof:
                case ElementType.Roof_HippedGableRoof:
                case ElementType.Roof_Hiproof:
                case ElementType.Roof_MansardRoof:
                case ElementType.Roof_Notdefined:
                case ElementType.Roof_PavilionRoof:
                case ElementType.Roof_RainbowRoof:
                case ElementType.Roof_Shedroof:
                case ElementType.Roof_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var Roof = CreateRoof(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(Roof);
                            buildingElements.Add(Roof);
                        }
                        break;
                    }
                case ElementType.Slab_Baseslab:
                case ElementType.Slab_Floor:
                case ElementType.Slab_Landing:
                case ElementType.Slab_Notdefined:
                case ElementType.Slab_Roof:
                case ElementType.Slab_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var Slab = CreateSlab(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(Slab);
                            buildingElements.Add(Slab);
                        }
                        break;
                    }

                case ElementType.StairFlight_Curved:
                case ElementType.StairFlight_Freeform:
                case ElementType.StairFlight_Notdefined:
                case ElementType.StairFlight_Spiral:
                case ElementType.StairFlight_Straight:
                case ElementType.StairFlight_Userdefined:
                case ElementType.StairFlight_Winder:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var StairFlight = CreateStairFlight(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(StairFlight);
                            buildingElements.Add(StairFlight);
                        }
                        break;
                    }
                case ElementType.Wall:
                case ElementType.Wall_Movable:
                case ElementType.Wall_Parapet:
                case ElementType.Wall_Elementedwall:
                case ElementType.Wall_Notdefined:
                case ElementType.Wall_Partitioning:
                case ElementType.Wall_Plumbingwall:
                case ElementType.Wall_Polygonal:
                case ElementType.Wall_Shear:
                case ElementType.Wall_Solidwall:
                case ElementType.Wall_Standard:
                case ElementType.Wall_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var Wall = CreateWall(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(Wall);
                            buildingElements.Add(Wall);
                        }
                        break;
                    }

                case ElementType.Window:
                case ElementType.Window_Skylight:
                case ElementType.Window_Lightdome:
                case ElementType.Window_Userdefined:
                case ElementType.Window_Notdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var Window = CreateWindow(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(Window);
                            buildingElements.Add(Window);
                        }
                        break;
                    }
                case ElementType.BuildingElementProxy_Complex:
                case ElementType.BuildingElementProxy_Element:
                case ElementType.BuildingElementProxy_Notdefined:
                case ElementType.BuildingElementProxy_Partial:
                case ElementType.BuildingElementProxy_ProvisionForVoid:
                case ElementType.BuildingElementProxy_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var BuildingElementProxy = CreateBuildingElementProxy(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(BuildingElementProxy);
                            buildingElements.Add(BuildingElementProxy);
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Element type not recognized => CreateBuildElem2");
            }

            return buildingElements;
        }

        public static List<IfcReinforcingElement> CreateReinforcingElements(IfcStore model, ElementType type, string name,
            IfcShapeRepresentation shape, List<Plane> insertPlanes, IfcRelAssociatesMaterial relAssociatesMaterial)
        {
            var reinforcingElements = new List<IfcReinforcingElement>();

            switch (type)
            {
                case ElementType.ReinforcingBar_Anchoring:
                case ElementType.ReinforcingBar_Edge:
                case ElementType.ReinforcingBar_Ligature:
                case ElementType.ReinforcingBar_Main:
                case ElementType.ReinforcingBar_Notdefined:
                case ElementType.ReinforcingBar_Punching:
                case ElementType.ReinforcingBar_Ring:
                case ElementType.ReinforcingBar_Shear:
                case ElementType.ReinforcingBar_Stud:
                case ElementType.ReinforcingBar_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var ReinforcingBar = CreateReinforcingBar(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(ReinforcingBar);
                            reinforcingElements.Add(ReinforcingBar);
                        }
                        break;
                    }
                case ElementType.Reinforcingmesh_Notdefined:
                case ElementType.Reinforcingmesh_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var ReinforcingMesh = CreateReinforcingMesh(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(ReinforcingMesh);
                            reinforcingElements.Add(ReinforcingMesh);
                        }
                        break;
                    }
                case ElementType.Tendon_Bar:
                case ElementType.Tendon_Coated:
                case ElementType.Tendon_Notdefined:
                case ElementType.Tendon_Strand:
                case ElementType.Tendon_Userdefined:
                case ElementType.Tendon_Wire:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var Tendon = CreateTendon(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(Tendon);
                            reinforcingElements.Add(Tendon);
                        }
                        break;
                    }
                case ElementType.TendonAnchor_Coupler:
                case ElementType.TendonAnchor_FixedEnd:
                case ElementType.TendonAnchor_Notdefined:
                case ElementType.TendonAnchor_TensioningEnd:
                case ElementType.TendonAnchor_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var TendonAnchor = CreateTendonAnchor(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(TendonAnchor);
                            reinforcingElements.Add(TendonAnchor);
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Element type not recognized => CreateReinforceElem");
            }

            return reinforcingElements;
        }

        public static List<IfcReinforcingElement> CreateReinforcingElements(IfcStore model, ElementType type, string name,
            List<IfcShapeRepresentation> shapes, List<Plane> insertPlanes, IfcRelAssociatesMaterial relAssociatesMaterial)
        {
            var reinforcingElements = new List<IfcReinforcingElement>();

            switch (type)
            {
                case ElementType.ReinforcingBar_Anchoring:
                case ElementType.ReinforcingBar_Edge:
                case ElementType.ReinforcingBar_Ligature:
                case ElementType.ReinforcingBar_Main:
                case ElementType.ReinforcingBar_Notdefined:
                case ElementType.ReinforcingBar_Punching:
                case ElementType.ReinforcingBar_Ring:
                case ElementType.ReinforcingBar_Shear:
                case ElementType.ReinforcingBar_Stud:
                case ElementType.ReinforcingBar_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var ReinforcingBar = CreateReinforcingBar(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(ReinforcingBar);
                            reinforcingElements.Add(ReinforcingBar);
                        }
                        break;
                    }
                case ElementType.Reinforcingmesh_Userdefined:
                case ElementType.Reinforcingmesh_Notdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var ReinforcingMesh = CreateReinforcingMesh(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(ReinforcingMesh);
                            reinforcingElements.Add(ReinforcingMesh);
                        }
                        break;
                    }
                case ElementType.Tendon_Bar:
                case ElementType.Tendon_Coated:
                case ElementType.Tendon_Notdefined:
                case ElementType.Tendon_Strand:
                case ElementType.Tendon_Userdefined:
                case ElementType.Tendon_Wire:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var Tendon = CreateTendon(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(Tendon);
                            reinforcingElements.Add(Tendon);
                        }
                        break;
                    }
                case ElementType.TendonAnchor_Coupler:
                case ElementType.TendonAnchor_FixedEnd:
                case ElementType.TendonAnchor_Notdefined:
                case ElementType.TendonAnchor_TensioningEnd:
                case ElementType.TendonAnchor_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var TendonAnchor = CreateTendonAnchor(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(TendonAnchor);
                            reinforcingElements.Add(TendonAnchor);
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Element type not recognized => CreateReinforceElem2");
            }

            return reinforcingElements;
        }

        //IfcElementComponent
        public static List<IfcElementComponent> CreateElementComponent(IfcStore model, ElementType type, string name,
            IfcShapeRepresentation shape, List<Plane> insertPlanes, IfcRelAssociatesMaterial relAssociatesMaterial)
        {
            var elementComponents = new List<IfcElementComponent>();

            switch (type)
            {
                case ElementType.BuildingElementPart_Insulation:
                case ElementType.BuildingElementPart_Notdefined:
                case ElementType.BuildingElementPart_Precastpanel:
                case ElementType.BuildingElementPart_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var BuildingElementPart = CreateBuildingElementPart(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(BuildingElementPart);
                            elementComponents.Add(BuildingElementPart);
                        }
                        break;
                    }
                case ElementType.MechanicalFastener_AnchorBolt:
                case ElementType.MechanicalFastener_Bolt:
                case ElementType.MechanicalFastener_Dowel:
                case ElementType.MechanicalFastener_Nail:
                case ElementType.MechanicalFastener_NailPlate:
                case ElementType.MechanicalFastener_Notdefined:
                case ElementType.MechanicalFastener_Rivet:
                case ElementType.MechanicalFastener_Screw:
                case ElementType.MechanicalFastener_ShearConnector:
                case ElementType.MechanicalFastener_Staple:
                case ElementType.MechanicalFastener_StudShearConnector:
                case ElementType.MechanicalFastener_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var MechanicalFastener = CreateMechanicalFastener(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(MechanicalFastener);
                            elementComponents.Add(MechanicalFastener);
                        }
                        break;
                    }
                case ElementType.Fastener_Glue:
                case ElementType.Fastener_Mortar:
                case ElementType.Fastener_Notdefined:
                case ElementType.Fastener_Userdefined:
                case ElementType.Fastener_Weld:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var Fastener = CreateFastener(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(Fastener);
                            elementComponents.Add(Fastener);
                        }
                        break;
                    }
                case ElementType.DiscreteAccessory_Anchorplate:
                case ElementType.DiscreteAccessory_Bracket:
                case ElementType.DiscreteAccessory_Notdefined:
                case ElementType.DiscreteAccessory_Shoe:
                case ElementType.DiscreteAccessory_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var DiscreteAccessory = CreateDiscreteAccessory(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(DiscreteAccessory);
                            elementComponents.Add(DiscreteAccessory);
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Element type not recognized => CreateElementComp");
            }

            return elementComponents;
        }

        public static List<IfcElementComponent> CreateElementComponent(IfcStore model, ElementType type, string name,
            List<IfcShapeRepresentation> shapes, List<Plane> insertPlanes, IfcRelAssociatesMaterial relAssociatesMaterial)
        {
            var elementComponents = new List<IfcElementComponent>();

            switch (type)
            {
                case ElementType.BuildingElementPart_Insulation:
                case ElementType.BuildingElementPart_Notdefined:
                case ElementType.BuildingElementPart_Precastpanel:
                case ElementType.BuildingElementPart_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var BuildingElementPart = CreateBuildingElementPart(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(BuildingElementPart);
                            elementComponents.Add(BuildingElementPart);
                        }
                        break;
                    }
                case ElementType.MechanicalFastener_AnchorBolt:
                case ElementType.MechanicalFastener_Bolt:
                case ElementType.MechanicalFastener_Dowel:
                case ElementType.MechanicalFastener_Nail:
                case ElementType.MechanicalFastener_NailPlate:
                case ElementType.MechanicalFastener_Notdefined:
                case ElementType.MechanicalFastener_Rivet:
                case ElementType.MechanicalFastener_Screw:
                case ElementType.MechanicalFastener_ShearConnector:
                case ElementType.MechanicalFastener_Staple:
                case ElementType.MechanicalFastener_StudShearConnector:
                case ElementType.MechanicalFastener_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var MechanicalFastener = CreateMechanicalFastener(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(MechanicalFastener);
                            elementComponents.Add(MechanicalFastener);
                        }
                        break;
                    }
                case ElementType.Fastener_Glue:
                case ElementType.Fastener_Mortar:
                case ElementType.Fastener_Notdefined:
                case ElementType.Fastener_Userdefined:
                case ElementType.Fastener_Weld:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var Fastener = CreateFastener(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(Fastener);
                            elementComponents.Add(Fastener);
                        }
                        break;
                    }
                case ElementType.DiscreteAccessory_Anchorplate:
                case ElementType.DiscreteAccessory_Bracket:
                case ElementType.DiscreteAccessory_Notdefined:
                case ElementType.DiscreteAccessory_Shoe:
                case ElementType.DiscreteAccessory_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var DiscreteAccessory = CreateDiscreteAccessory(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(DiscreteAccessory);
                            elementComponents.Add(DiscreteAccessory);
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Element type not recognized => CreateElementComp2");
            }

            return elementComponents;
        }

        //IfcDistributionComponent
        public static List<IfcDistributionElement> CreateDistributionElement(IfcStore model, ElementType type, string name,
            IfcShapeRepresentation shape, List<Plane> insertPlanes, IfcRelAssociatesMaterial relAssociatesMaterial)
        {
            var elementComponents = new List<IfcDistributionElement>();

            switch (type)
            {
                case ElementType.DistributionElement:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var DistributionElement = CreateDistributionElement(model, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(DistributionElement);
                            elementComponents.Add(DistributionElement);
                        }
                        break;
                    }
                case ElementType.DistributionControlElement:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var DistributionControlElement = CreateDistributionControlElement(model, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(DistributionControlElement);
                            elementComponents.Add(DistributionControlElement);
                        }
                        break;
                    }
                case ElementType.DistributionFlowElement:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var DistributionFlowElement = CreateDistributionFlowElement(model, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(DistributionFlowElement);
                            elementComponents.Add(DistributionFlowElement);
                        }
                        break;
                    }
                case ElementType.DistributionChamberElement_FormedDuct:
                case ElementType.DistributionChamberElement_InspectionChamber:
                case ElementType.DistributionChamberElement_InspectionPit:
                case ElementType.DistributionChamberElement_Manhole:
                case ElementType.DistributionChamberElement_MeterChamber:
                case ElementType.DistributionChamberElement_Notdefined:
                case ElementType.DistributionChamberElement_Sump:
                case ElementType.DistributionChamberElement_Trench:
                case ElementType.DistributionChamberElement_Userdefined:
                case ElementType.DistributionChamberElement_ValveChamber:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var DistributionChamberElement = CreateDistributionChamberElement(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(DistributionChamberElement);
                            elementComponents.Add(DistributionChamberElement);
                        }
                        break;
                    }
                case ElementType.EnergyConversionDevice:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var EnergyConversionDevice = CreateEnergyConversionDevice(model, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(EnergyConversionDevice);
                            elementComponents.Add(EnergyConversionDevice);
                        }
                        break;
                    }
                case ElementType.FlowController:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var FlowController = CreateFlowController(model, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(FlowController);
                            elementComponents.Add(FlowController);
                        }
                        break;
                    }
                case ElementType.FlowFitting:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var FlowFitting = CreateFlowFitting(model, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(FlowFitting);
                            elementComponents.Add(FlowFitting);
                        }
                        break;
                    }
                case ElementType.FlowMovingDevice:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var FlowMovingDevice = CreateFlowMovingDevice(model, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(FlowMovingDevice);
                            elementComponents.Add(FlowMovingDevice);
                        }
                        break;
                    }
                case ElementType.FlowSegment:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var FlowSegment = CreateFlowSegment(model, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(FlowSegment);
                            elementComponents.Add(FlowSegment);
                        }
                        break;
                    }
                case ElementType.FlowStorageDevice:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var FlowStorageDevice = CreateFlowStorageDevice(model, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(FlowStorageDevice);
                            elementComponents.Add(FlowStorageDevice);
                        }
                        break;
                    }
                case ElementType.FlowTerminal:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var FlowTerminal = CreateFlowTerminal(model, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(FlowTerminal);
                            elementComponents.Add(FlowTerminal);
                        }
                        break;
                    }
                case ElementType.FlowTreatmentDevice:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var FlowTreatmentDevice = CreateFlowTreatmentDevice(model, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(FlowTreatmentDevice);
                            elementComponents.Add(FlowTreatmentDevice);
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Element type not recognized => CreateDistributionElement");
            }

            return elementComponents;
        }

        public static List<IfcDistributionElement> CreateDistributionElement(IfcStore model, ElementType type, string name,
            List<IfcShapeRepresentation> shapes, List<Plane> insertPlanes, IfcRelAssociatesMaterial relAssociatesMaterial)
        {
            var elementComponents = new List<IfcDistributionElement>();

            switch (type)
            {
                case ElementType.DistributionElement:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var BuildingElementPart = CreateDistributionElement(model, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(BuildingElementPart);
                            elementComponents.Add(BuildingElementPart);
                        }
                        break;
                    }
                case ElementType.DistributionControlElement:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var DistributionControlElement = CreateDistributionControlElement(model, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(DistributionControlElement);
                            elementComponents.Add(DistributionControlElement);
                        }
                        break;
                    }
                case ElementType.DistributionFlowElement:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var DistributionFlowElement = CreateDistributionFlowElement(model, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(DistributionFlowElement);
                            elementComponents.Add(DistributionFlowElement);
                        }
                        break;
                    }
                case ElementType.DistributionChamberElement_FormedDuct:
                case ElementType.DistributionChamberElement_InspectionChamber:
                case ElementType.DistributionChamberElement_InspectionPit:
                case ElementType.DistributionChamberElement_Manhole:
                case ElementType.DistributionChamberElement_MeterChamber:
                case ElementType.DistributionChamberElement_Notdefined:
                case ElementType.DistributionChamberElement_Sump:
                case ElementType.DistributionChamberElement_Trench:
                case ElementType.DistributionChamberElement_Userdefined:
                case ElementType.DistributionChamberElement_ValveChamber:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var DistributionChamberElement = CreateDistributionChamberElement(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(DistributionChamberElement);
                            elementComponents.Add(DistributionChamberElement);
                        }
                        break;
                    }
                case ElementType.EnergyConversionDevice:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var EnergyConversionDevice = CreateEnergyConversionDevice(model, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(EnergyConversionDevice);
                            elementComponents.Add(EnergyConversionDevice);
                        }
                        break;
                    }
                case ElementType.FlowController:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var FlowController = CreateFlowController(model, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(FlowController);
                            elementComponents.Add(FlowController);
                        }
                        break;
                    }
                case ElementType.FlowFitting:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var FlowFitting = CreateFlowFitting(model, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(FlowFitting);
                            elementComponents.Add(FlowFitting);
                        }
                        break;
                    }
                case ElementType.FlowMovingDevice:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var FlowMovingDevice = CreateFlowMovingDevice(model, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(FlowMovingDevice);
                            elementComponents.Add(FlowMovingDevice);
                        }
                        break;
                    }
                case ElementType.FlowSegment:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var FlowSegment = CreateFlowSegment(model, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(FlowSegment);
                            elementComponents.Add(FlowSegment);
                        }
                        break;
                    }
                case ElementType.FlowStorageDevice:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var FlowStorageDevice = CreateFlowStorageDevice(model, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(FlowStorageDevice);
                            elementComponents.Add(FlowStorageDevice);
                        }
                        break;
                    }
                case ElementType.FlowTerminal:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var FlowTerminal = CreateFlowTerminal(model, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(FlowTerminal);
                            elementComponents.Add(FlowTerminal);
                        }
                        break;
                    }
                case ElementType.FlowTreatmentDevice:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var FlowTreatmentDevice = CreateFlowTreatmentDevice(model, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(FlowTreatmentDevice);
                            elementComponents.Add(FlowTreatmentDevice);
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Element type not recognized => CreateDistributionElement2");
            }

            return elementComponents;
        }

        //IfcProxy
        public static List<IfcProxy> CreateProxy(IfcStore model, ElementType type, string name,
            IfcShapeRepresentation shape, List<Plane> insertPlanes, IfcRelAssociatesMaterial relAssociatesMaterial)
        {
            var proxyList = new List<IfcProxy>();

            switch (type)
            {
                case ElementType.Proxy:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var Proxy = CreateProxy(model, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(Proxy);
                            proxyList.Add(Proxy);
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Element type not recognized => CreateProxy");
            }

            return proxyList;
        }

        public static List<IfcProxy> CreateProxy(IfcStore model, ElementType type, string name,
            List<IfcShapeRepresentation> shapes, List<Plane> insertPlanes, IfcRelAssociatesMaterial relAssociatesMaterial)
        {
            var proxyList = new List<IfcProxy>();

            switch (type)
            {
                case ElementType.Proxy:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var proxy = CreateProxy(model, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(proxy);
                            proxyList.Add(proxy);
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Element type not recognized => CreateProxy2");
            }

            return proxyList;
        }

        //IfcElement
        public static List<IfcElement> CreateElement(IfcStore model, ElementType type, string name,
            IfcShapeRepresentation shape, List<Plane> insertPlanes, IfcRelAssociatesMaterial relAssociatesMaterial)
        {
            var elementList = new List<IfcElement>();

            switch (type)
            {
                case ElementType.ElementAssembly_AccessoryAssembly:
                case ElementType.ElementAssembly_Arch:
                case ElementType.ElementAssembly_BeamGrid:
                case ElementType.ElementAssembly_BracedFrame:
                case ElementType.ElementAssembly_Girder:
                case ElementType.ElementAssembly_Notdefined:
                case ElementType.ElementAssembly_ReinforcementUnit:
                case ElementType.ElementAssembly_RigidFrame:
                case ElementType.ElementAssembly_SlabField:
                case ElementType.ElementAssembly_Truss:
                case ElementType.ElementAssembly_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var ElementAssembly = CreateElementAssembly(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(ElementAssembly);
                            elementList.Add(ElementAssembly);
                        }
                        break;
                    }
                case ElementType.FurnishingElement:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var FurnishingElement = CreateFurnishingElement(model, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(FurnishingElement);
                            elementList.Add(FurnishingElement);
                        }
                        break;
                    }
                case ElementType.TransportElement_Craneway:
                case ElementType.TransportElement_Elevator:
                case ElementType.TransportElement_Escalator:
                case ElementType.TransportElement_LiftingGear:
                case ElementType.TransportElement_MovingWalkWay:
                case ElementType.TransportElement_Notdefined:
                case ElementType.TransportElement_Userdefined:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var TransportElement = CreateTransportElement(model, type, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(TransportElement);
                            elementList.Add(TransportElement);
                        }
                        break;
                    }
                case ElementType.VirtualElement:
                    {
                        foreach (var insertPlane in insertPlanes)
                        {
                            var VirtualElement = CreateVirtualElement(model, name, shape, insertPlane);
                            relAssociatesMaterial.RelatedObjects.Add(VirtualElement);
                            elementList.Add(VirtualElement);
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Element type not recognized => CreateElements");
            }

            return elementList;
        }

        public static List<IfcElement> CreateElement(IfcStore model, ElementType type, string name,
            List<IfcShapeRepresentation> shapes, List<Plane> insertPlanes, IfcRelAssociatesMaterial relAssociatesMaterial)
        {
            var elementList = new List<IfcElement>();

            switch (type)
            {
                case ElementType.ElementAssembly_AccessoryAssembly:
                case ElementType.ElementAssembly_Arch:
                case ElementType.ElementAssembly_BeamGrid:
                case ElementType.ElementAssembly_BracedFrame:
                case ElementType.ElementAssembly_Girder:
                case ElementType.ElementAssembly_Notdefined:
                case ElementType.ElementAssembly_ReinforcementUnit:
                case ElementType.ElementAssembly_RigidFrame:
                case ElementType.ElementAssembly_SlabField:
                case ElementType.ElementAssembly_Truss:
                case ElementType.ElementAssembly_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var ElementAssembly = CreateElementAssembly(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(ElementAssembly);
                            elementList.Add(ElementAssembly);
                        }
                        break;
                    }
                case ElementType.FurnishingElement:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var FurnishingElement = CreateFurnishingElement(model, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(FurnishingElement);
                            elementList.Add(FurnishingElement);
                        }
                        break;
                    }
                case ElementType.TransportElement_Craneway:
                case ElementType.TransportElement_Elevator:
                case ElementType.TransportElement_Escalator:
                case ElementType.TransportElement_LiftingGear:
                case ElementType.TransportElement_MovingWalkWay:
                case ElementType.TransportElement_Notdefined:
                case ElementType.TransportElement_Userdefined:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var TransportElement = CreateTransportElement(model, type, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(TransportElement);
                            elementList.Add(TransportElement);
                        }
                        break;
                    }
                case ElementType.VirtualElement:
                    {
                        for (int i = 0; i < insertPlanes.Count; i++)
                        {
                            var VirtualElement = CreateVirtualElement(model, name, shapes[i], insertPlanes[i]);
                            relAssociatesMaterial.RelatedObjects.Add(VirtualElement);
                            elementList.Add(VirtualElement);
                        }
                        break;
                    }
                default:
                    throw new ArgumentException("Element type not recognized => CreateElements2");
            }

            return elementList;
        }

    }
}