using Grasshopper.Kernel;
using System;
using System.Drawing;

using Rhino.Geometry;
using System.Collections.Generic;
using Grasshopper.Kernel.Types;
using Grasshopper.GUI.Canvas;

//using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Rex

{
    public class IFC_Types : GH_Component
    {
        public IFC_Types(): base("IFC Types", "IFC Types", "Component to choose the type of IfcEntity", "T-Rex", "Concrete")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Type", "I", "The Type is: ", GH_ParamAccess.item);
            pManager.AddTextParameter("Subtype", "S", "The Subtype is: ", GH_ParamAccess.item, "Notdefined");
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Type", "O", "The Type is: ", GH_ParamAccess.item);

        }
        
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string TypeText = string.Empty;
            string SubtypeText = string.Empty;
            DA.GetData(0, ref TypeText);
            DA.GetData(1, ref SubtypeText);
            string type = TypeText.ToLower();
            string subt = SubtypeText.ToLower();
            switch(type)
            {
                case "ifcpadfooting":
                case "padfooting":
                case "pad pooting":
                    DA.SetData(0, 0);
                    break;

                case "ifcstripfooting":
                case "stripfooting":
                case "strip footing":
                    DA.SetData(0, 1);
                    break;

                case "ifcbeam":
                case "beam":
                    {
                        if (subt == "beam")
                            DA.SetData(0, 2);
                        else if (subt == "joist")
                            DA.SetData(0, 122);
                        else if (subt == "hollowcore")
                            DA.SetData(0, 123);
                        else if (subt == "lintel")
                            DA.SetData(0, 124);
                        else if (subt == "spandrel")
                            DA.SetData(0, 125);
                        else if (subt == "t_beam" || subt=="tbeam")
                            DA.SetData(0, 126);
                        else if (subt == "userdefined")
                            DA.SetData(0, 127);
                        else if (subt == "notdefined")
                            DA.SetData(0, 128);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifccolumn":
                case "column":
                    {
                        if (subt == "column")
                            DA.SetData(0, 3);
                        else if (subt == "pilaster")
                            DA.SetData(0, 129);
                        else if (subt == "userdefined")
                            DA.SetData(0, 130);
                        else if (subt == "notdefined")
                            DA.SetData(0, 131);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcdoor":
                case "door":
                    {
                        if (subt == "door")
                            DA.SetData(0, 4);
                        else if (subt == "gate")
                            DA.SetData(0, 101);
                        else if (subt == "trapdoor")
                            DA.SetData(0, 102);
                        else if (subt == "userdefined")
                            DA.SetData(0, 103);
                        else if (subt == "notdefined")
                            DA.SetData(0, 104);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                        break;

                case "ifcstair":
                case "stair":
                    {
                        if (subt == "straightrunstair" || subt=="straight run stair" || subt=="straight_run_stair")
                            DA.SetData(0, 132);
                        else if (subt == "twostraightrunstair" || subt=="two straight run stair" || subt=="two_straight_run_stair")
                            DA.SetData(0, 133);
                        else if (subt == "quarterwindingstair" || subt=="quarter winding stair" || subt=="quarter_winding_stair")
                            DA.SetData(0, 134);
                        else if (subt == "quarterturnstair" || subt=="quarter turn stair" || subt=="quarter_turn_stair")
                            DA.SetData(0, 135);
                        else if (subt == "halfwindingstair" || subt=="half winding stair" || subt=="half_winding_stair")
                            DA.SetData(0, 136);
                        else if (subt == "half_turn_stair" || subt=="halfturnstair" || subt=="half turn stair")
                            DA.SetData(0, 137);
                        else if (subt == "twoquarterwindingstair" || subt=="two quarter winding stair" || subt=="two_quarter_winding_stair")
                            DA.SetData(0, 138);
                        else if (subt == "twoquarterturnstair" || subt=="two quarter turn stair" || subt=="two_quarter_turn_stair")
                            DA.SetData(0, 139);
                        else if (subt == "threequarterwindingstair" || subt=="three quarter winding stair" || subt=="three_quarter_winding_stair")
                            DA.SetData(0, 140);
                        else if (subt == "spiral_stair")
                            DA.SetData(0, 142);
                        else if (subt == "double_return_stair" || subt=="doublereturnstair" || subt=="double return stair")
                            DA.SetData(0, 143);
                        else if (subt == "curvedrunstair" || subt=="curved_run_stair" || subt=="curved run stair")
                            DA.SetData(0, 144);
                        else if (subt == "twocurvedrunstair" || subt=="two_curved_run_stair" || subt=="two curved run stair")
                            DA.SetData(0, 145);
                        else if (subt == "threequarterturnstair" || subt=="three quarter turn stair" || subt=="three_quarter_turn_stair")
                            DA.SetData(0, 141);
                        else if (subt == "userdefined")
                            DA.SetData(0, 146);
                        else if (subt == "notdefined")
                            DA.SetData(0, 147);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcproxy":
                case "proxy":
                    DA.SetData(0, 6);
                    break;

                case "ifccovering":
                case "covering":
                    {
                        if (subt == "ceiling")
                            DA.SetData(0, 196);
                        else if (subt == "flooring")
                            DA.SetData(0, 197);
                        else if (subt == "cladding")
                            DA.SetData(0, 198);
                        else if (subt == "roofing")
                            DA.SetData(0, 199);
                        else if (subt == "molding")
                            DA.SetData(0, 200);
                        else if (subt == "skirtingboard" || subt == "skirting board" || subt == "skirting_board")
                            DA.SetData(0, 201);
                        else if (subt == "insulation")
                            DA.SetData(0, 202);
                        else if (subt == "membrane")
                            DA.SetData(0, 203);
                        else if (subt == "sleeving")
                            DA.SetData(0, 204);
                        else if (subt == "wrapping")
                            DA.SetData(0, 205);
                        else if (subt == "userdefined")
                            DA.SetData(0, 206);
                        else if (subt == "notdefined")
                            DA.SetData(0, 207);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifccurtainwall":
                case "curtainwall":
                case "curtain wall":
                case "curtain_wall":
                    {
                        if (subt == "userdefined")
                            DA.SetData(0, 8);
                        else if (subt == "notdefined")
                            DA.SetData(0, 156);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcfooting":
                case "footing":
                    {
                        if (subt == "caissonfoundation" || subt == "caisson foundation" || subt == "caisson_foundation")
                            DA.SetData(0, 181);
                        else if (subt == "footingbeam" || subt == "footing beam" || subt == "footing_beam")
                            DA.SetData(0, 182);
                        else if (subt == "padfooting" || subt == "pad_footing" || subt == "pad footing")
                            DA.SetData(0, 183);
                        else if (subt == "pilecap" || subt == "pile cap" || subt == "pile_cap")
                            DA.SetData(0, 184);
                        else if (subt == "stripfooting" || subt == "strip footing" || subt == "strip_footing")
                            DA.SetData(0, 185);
                        else if (subt == "userdefined")
                            DA.SetData(0, 186);
                        else if (subt == "notdefined")
                            DA.SetData(0, 187);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcmember":
                case "member":
                    {
                        if (subt == "brace")
                            DA.SetData(0, 208);
                        else if (subt == "chord")
                            DA.SetData(0, 209);
                        else if (subt == "collar")
                            DA.SetData(0, 210);
                        else if (subt == "member")
                            DA.SetData(0, 211);
                        else if (subt == "mullion")
                            DA.SetData(0, 212);
                        else if (subt == "plate")
                            DA.SetData(0, 213);
                        else if (subt == "post")
                            DA.SetData(0, 214);
                        else if (subt == "purlin")
                            DA.SetData(0, 215);
                        else if (subt == "rafter")
                            DA.SetData(0, 216);
                        else if (subt == "stringer")
                            DA.SetData(0, 217);
                        else if (subt == "strut")
                            DA.SetData(0, 218);
                        else if (subt == "stud")
                            DA.SetData(0, 219);
                        else if (subt == "userdefined")
                            DA.SetData(0, 220);
                        else if (subt == "notdefined")
                            DA.SetData(0, 221);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcpile":
                case "pile":
                    {
                        if (subt == "bored")
                            DA.SetData(0, 188);
                        else if (subt == "driven")
                            DA.SetData(0, 189);
                        else if (subt == "jetgrouting" || subt == "jet grouting" || subt == "jet_grouting")
                            DA.SetData(0, 190);
                        else if (subt == "cohesion")
                            DA.SetData(0, 191);
                        else if (subt == "friction")
                            DA.SetData(0, 192);
                        else if (subt == "support")
                            DA.SetData(0, 193);
                        else if (subt == "userdefined")
                            DA.SetData(0, 159);
                        else if (subt == "notdefined")
                            DA.SetData(0, 160);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcplate":
                case "plate":
                    {
                        if (subt == "curtain_panel" || subt=="curtainpanel" || subt=="curtain panel")
                            DA.SetData(0, 157);
                        else if (subt == "sheet")
                            DA.SetData(0, 158);
                        else if (subt == "userdefined")
                            DA.SetData(0, 159);
                        else if (subt == "notdefined")
                            DA.SetData(0, 160);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcrailing":
                case "railing":
                    {
                        if (subt == "handrail")
                            DA.SetData(0, 161);
                        else if (subt == "guardrail")
                            DA.SetData(0, 162);
                        else if (subt == "balustrade")
                            DA.SetData(0, 163);
                        else if (subt == "userdefined")
                            DA.SetData(0, 164);
                        else if (subt == "notdefined")
                            DA.SetData(0, 165);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcramp":
                case "ramp":
                    {
                        if (subt == "straightrunramp" || subt=="straight run ramp" || subt=="straight_run_ramp")
                            DA.SetData(0, 173);
                        else if (subt == "twostraightrunramp" || subt=="two straight run ramp" || subt=="two_straight_run_ramp")
                            DA.SetData(0, 174);
                        else if (subt == "quarterturnramp" || subt=="quarter turn ramp" || subt=="quarter_turn_ramp")
                            DA.SetData(0, 175);
                        else if (subt == "twoquarterturnramp" || subt == "two quarter turn ramp" || subt == "two_quarter_turn_ramp")
                            DA.SetData(0, 176);
                        else if (subt == "halfturnramp" || subt == "half turn ramp" || subt == "half_turn_ramp")
                            DA.SetData(0, 177);
                        else if (subt == "spiralramp" || subt == "spiral ramp" || subt == "spiral_ramp")
                            DA.SetData(0, 178);
                        else if (subt == "userdefined")
                            DA.SetData(0, 179);
                        else if (subt == "notdefined")
                            DA.SetData(0, 180);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcrampflight":
                case "rampflight":
                case "ramp flight":
                case "ramp_flight":
                    {
                        if (subt == "straight")
                            DA.SetData(0, 152);
                        else if (subt == "spiral")
                            DA.SetData(0, 153);
                        else if (subt == "userdefined")
                            DA.SetData(0, 154);
                        else if (subt == "notdefined")
                            DA.SetData(0, 155);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcroof":
                case "roof":
                    {
                        if (subt == "flatroof" || subt == "flat roof" || subt == "flat_roof")
                            DA.SetData(0, 222);
                        else if (subt == "shedroof" || subt == "shed_roof" || subt == "shed roof")
                            DA.SetData(0, 223);
                        else if (subt == "gableroof" || subt == "gable_roof" || subt == "gable_roof")
                            DA.SetData(0, 224);
                        else if (subt == "hiproof" || subt == "hip roof" || subt == "hip_roof")
                            DA.SetData(0, 225);
                        else if (subt == "hippedgableroof" || subt == "hipped_gable_roof" || subt == "hipped gable roof")
                            DA.SetData(0, 226);
                        else if (subt == "gambrelroof" || subt == "gambrel roof" || subt == "gambrel_roof")
                            DA.SetData(0, 227);
                        else if (subt == "mansardroof" || subt == "mansard roof" || subt == "mansard_roof")
                            DA.SetData(0, 228);
                        else if (subt == "barrelroof" || subt == "barrel roof" || subt == "barrel_roof")
                            DA.SetData(0, 239);
                        else if (subt == "rainbowroof" || subt == "rainbow roof" || subt == "rainbow_roof")
                            DA.SetData(0, 230);
                        else if (subt == "butterflyroof" || subt == "butterfly roof" || subt == "butterfly_roof")
                            DA.SetData(0, 231);
                        else if (subt == "pavilionroof" || subt == "pavilion_roof" || subt == "pavilion roof")
                            DA.SetData(0, 232);
                        else if (subt == "domeroof" || subt == "dome roof" || subt == "dome_roof")
                            DA.SetData(0, 233);
                        else if (subt == "freeform" || subt == "free form" || subt == "free_form")
                            DA.SetData(0, 234);
                        else if (subt == "userdefined")
                            DA.SetData(0, 235);
                        else if (subt == "notdefined")
                            DA.SetData(0, 236);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcslab":
                case "slab":
                    {
                        if (subt == "floor")
                            DA.SetData(0, 116);
                        else if (subt == "roof")
                            DA.SetData(0, 117);
                        else if (subt == "landing")
                            DA.SetData(0, 118);
                        else if (subt == "baseslab")
                            DA.SetData(0, 119);
                        else if (subt == "userdefined")
                            DA.SetData(0, 120);
                        else if (subt == "notdefined")
                            DA.SetData(0, 121);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    //DA.SetData(0, 17);
                    break;

                case "ifcstairflight":
                case "stairflight":
                case "stair flight":
                case "stair_flight":
                    {
                        if (subt == "straight")
                            DA.SetData(0, 166);
                        else if (subt == "winder")
                            DA.SetData(0, 167);
                        else if (subt == "spiral")
                            DA.SetData(0, 168);
                        else if (subt == "curved")
                            DA.SetData(0, 169);
                        else if (subt == "freeform" || subt=="free form" || subt=="free_form")
                            DA.SetData(0, 170);
                        else if (subt == "userdefined")
                            DA.SetData(0, 171);
                        else if (subt == "notdefined")
                            DA.SetData(0, 172);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcwall":
                case "wall":
                    {
                        if(subt == "movable")
                            DA.SetData(0, 105);
                        else if(subt == "parapet")
                            DA.SetData(0, 106);
                        else if (subt == "partitioning")
                            DA.SetData(0, 107);
                        else if (subt == "plumbingwall")
                            DA.SetData(0, 108);
                        else if (subt == "shear")
                            DA.SetData(0, 109);
                        else if (subt == "solidwall")
                            DA.SetData(0, 110);
                        else if (subt == "standard")
                            DA.SetData(0, 111);
                        else if (subt == "polygonal")
                            DA.SetData(0, 112);
                        else if (subt == "elementedwall")
                            DA.SetData(0, 113);
                        else if (subt == "userdefined")
                            DA.SetData(0, 114);
                        else if (subt == "notdefined")
                            DA.SetData(0, 115);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcwindow":
                case "window":
                    {
                        if (subt == "window")
                            DA.SetData(0, 20);
                        else if (subt == "skylight")
                            DA.SetData(0, 148);
                        else if (subt == "lightdome")
                            DA.SetData(0, 149);
                        else if (subt == "userdefined")
                            DA.SetData(0, 150);
                        else if (subt == "notdefined")
                            DA.SetData(0, 151);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcbuildingelementproxy":
                case "buildingelementproxy":
                case "building element proxy":
                case "building_element_proxy":
                    {
                        if (subt == "complex")
                            DA.SetData(0, 258);
                        else if (subt == "element")
                            DA.SetData(0, 259);
                        else if (subt == "partial")
                            DA.SetData(0, 260);
                        else if (subt == "provisionforvoid" || subt == "provision for void" || subt == "provision_for_void")
                            DA.SetData(0, 261);
                        else if (subt == "userdefined")
                            DA.SetData(0, 262);
                        else if (subt == "notdefined")
                            DA.SetData(0, 263);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcbuildingelementpart":
                case "buildingelementpart":
                case "building element part":
                case "building_element_part":
                    {
                        if (subt == "insulation")
                            DA.SetData(0, 239);
                        else if (subt == "precastpanel" || subt == "pre cast panel" || subt == "pre_cast_panel")
                            DA.SetData(0, 240);
                        else if (subt == "userdefined")
                            DA.SetData(0, 241);
                        else if (subt == "notdefined")
                            DA.SetData(0, 242);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcreinforcingbar":
                case "reinforcingbar":
                case "reinforcing bar":
                case "reinforcing_bar":
                    {
                        if (subt == "anchoring")
                            DA.SetData(0, 271);
                        else if (subt == "edge")
                            DA.SetData(0, 272);
                        else if (subt == "ligature")
                            DA.SetData(0, 273);
                        else if (subt == "main")
                            DA.SetData(0, 274);
                        else if (subt == "punching")
                            DA.SetData(0, 275);
                        else if (subt == "ring")
                            DA.SetData(0, 276);
                        else if (subt == "shear")
                            DA.SetData(0, 277);
                        else if (subt == "stud")
                            DA.SetData(0, 278);
                        else if (subt == "userdefined")
                            DA.SetData(0, 279);
                        else if (subt == "notdefined")
                            DA.SetData(0, 280);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcreinforcingmesh":
                case "reinforcingmesh":
                case "reinforcing mesh":
                case "reinforcing_mesh":
                    {
                        if (subt == "userdefined")
                            DA.SetData(0, 237);
                        else if (subt == "notdefined")
                            DA.SetData(0, 238);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifctendon":
                case "tendon":
                    {
                        if (subt == "bar")
                            DA.SetData(0, 314);
                        else if (subt == "coated")
                            DA.SetData(0, 315);
                        else if (subt == "strand")
                            DA.SetData(0, 316);
                        else if (subt == "wire")
                            DA.SetData(0, 317);
                        else if (subt == "userdefined")
                            DA.SetData(0, 318);
                        else if (subt == "notdefined")
                            DA.SetData(0, 319);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifctendonanchor":
                case "tendonanchor":
                case "tendon anchor":
                case "tendon_anchor":
                    {
                        if (subt == "coupler")
                            DA.SetData(0, 253);
                        else if (subt == "fixed_end" || subt == "fixed end" || subt == "fixedend")
                            DA.SetData(0, 254);
                        else if (subt == "tensioning_end" || subt == "tensioning end" || subt == "tensioningend")
                            DA.SetData(0, 255);
                        else if (subt == "userdefined")
                            DA.SetData(0, 256);
                        else if (subt == "notdefined")
                            DA.SetData(0, 257);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcdistributionelement":
                case "distributionelement":
                case "distribution element":
                case "distribution_element":
                    DA.SetData(0, 27);
                    break;

                case "ifcdistributioncontrolelement":
                case "distributioncontrolelement":
                case "distribution control element":
                case "distribution_control_element":
                    DA.SetData(0, 28);
                    break;

                case "ifcdistributionflowelement":
                case "distributionflowelement":
                case "distribution_flow_element":
                case "distribution flow element":
                    DA.SetData(0, 29);
                    break;

                case "ifcdistributionchamberelement":
                case "distributionchamberelement":
                case "distribution_chamber_element":
                case "distribution chamber element":
                    {
                        if (subt == "formedduct" || subt == "formed_duct" || subt == "formed duct")
                            DA.SetData(0, 281);
                        else if (subt == "inspectionchamber" || subt == "inspection chamber" || subt == "inspection_chamber")
                            DA.SetData(0, 282);
                        else if (subt == "inspectionpit" || subt == "inspection pit" || subt == "inspection_pit")
                            DA.SetData(0, 283);
                        else if (subt == "manhole" || subt == "man hole" || subt == "man_hole")
                            DA.SetData(0, 284);
                        else if (subt == "meterchamber" || subt == "meter chamber" || subt == "meter_chamber")
                            DA.SetData(0, 285);
                        else if (subt == "sump")
                            DA.SetData(0, 286);
                        else if (subt == "trench")
                            DA.SetData(0, 287);
                        else if (subt == "valvechamber" || subt == "valve chamber" || subt == "valve_chamber")
                            DA.SetData(0, 288);
                        else if (subt == "userdefined")
                            DA.SetData(0, 289);
                        else if (subt == "notdefined")
                            DA.SetData(0, 290);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcenergyconversiondevice":
                case "energyconversiondevice":
                case "energy_conversion_device":
                case "energy conversion device":
                    DA.SetData(0, 31);
                    break;

                case "ifcflowcontroller":
                case "flowcontroller":
                case "flow_controller":
                case "flow controller":
                    DA.SetData(0, 32);
                    break;

                case "ifcflowfitting":
                case "flowfitting":
                case "flow_fitting":
                case "flow fitting":
                    DA.SetData(0, 33);
                    break;

                case "ifcflowmovingdevice":
                case "flowmovingdevice":
                case "flow_moving_device":
                case "flow moving device":
                    DA.SetData(0, 34);
                    break;

                case "ifcflowsegment":
                case "flowsegment":
                case "flow_segment":
                case "flow segment":
                    DA.SetData(0, 35);
                    break;

                case "ifcflowstoragedevice":
                case "flowstoragedevice":
                case "flow_storage_device":
                case "flow storage device":
                    DA.SetData(0, 36);
                    break;

                case "IfcFlowTerminal":
                case "IFCFlowTerminal":
                case "FlowTerminal":
                case "Flow Terminal":
                    DA.SetData(0, 37);
                    break;

                case "ifcflowtreatmentdevice":
                case "flowtreatmentdevice":
                case "flow_treatment_device":
                case "flow treatment device":
                    DA.SetData(0, 38);
                    break;

                case "ifcelementassembly":
                case "elementassembly":
                case "element_assembly":
                case "element assembly":
                    {
                        if (subt == "accessoryassembly" || subt == "accessory assembly" || subt == "accessory_assembly")
                            DA.SetData(0, 291);
                        else if (subt == "arch")
                            DA.SetData(0, 292);
                        else if (subt == "beamgrid" || subt == "beam_grid" || subt == "beam grid")
                            DA.SetData(0, 293);
                        else if (subt == "bracedframe" || subt == "braced_frame" || subt == "braced frame")
                            DA.SetData(0, 294);
                        else if (subt == "girder")
                            DA.SetData(0, 295);
                        else if (subt == "reinforcementunit" || subt == "reinforcement_unit" || subt == "reinforcement unit")
                            DA.SetData(0, 296);
                        else if (subt == "rigidframe" || subt == "rigid_frame" || subt == "rigid frame")
                            DA.SetData(0, 297);
                        else if (subt == "slabfield" || subt == "slab_field" || subt == "slab field")
                            DA.SetData(0, 298);
                        else if (subt == "truss")
                            DA.SetData(0, 299);
                        else if (subt == "userdefined")
                            DA.SetData(0, 300);
                        else if (subt == "notdefined")
                            DA.SetData(0, 301);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcdiscreteaccessory":
                case "discreteaccessory":
                case "discrete_accessory":
                case "discrete accessory":
                    {
                        if (subt == "anchorplate")
                            DA.SetData(0, 248);
                        else if (subt == "bracket")
                            DA.SetData(0, 249);
                        else if (subt == "shoe")
                            DA.SetData(0, 250);
                        else if (subt == "userdefined")
                            DA.SetData(0, 251);
                        else if (subt == "notdefined")
                            DA.SetData(0, 252);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcmechanicalfastener":
                case "mechanicalfastener":
                case "mechanical_fastener":
                case "mechanical fastener":
                    {
                        if (subt == "anchorbolt" || subt == "anchor bolt" || subt == "anchor_bolt")
                            DA.SetData(0, 302);
                        else if (subt == "bolt")
                            DA.SetData(0, 303);
                        else if (subt == "dowel")
                            DA.SetData(0, 304);
                        else if (subt == "nail")
                            DA.SetData(0, 305);
                        else if (subt == "nailplate" || subt == "nail plate" || subt == "nail_plate")
                            DA.SetData(0, 306);
                        else if (subt == "rivet")
                            DA.SetData(0, 307);
                        else if (subt == "screw")
                            DA.SetData(0, 308);
                        else if (subt == "shearconnector" || subt == "shear connector" || subt == "shear_connector")
                            DA.SetData(0, 309);
                        else if (subt == "staple")
                            DA.SetData(0, 310);
                        else if (subt == "studshearconnector" || subt == "stud shear connector" || subt == "stud_shear_connector")
                            DA.SetData(0, 311);
                        else if (subt == "userdefined")
                            DA.SetData(0, 312);
                        else if (subt == "notdefined")
                            DA.SetData(0, 313);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcfastener":
                case "fastener":
                    {
                        if (subt == "glue")
                            DA.SetData(0, 243);
                        else if (subt == "mortar")
                            DA.SetData(0, 244);
                        else if (subt == "weld")
                            DA.SetData(0, 245);
                        else if (subt == "userdefined")
                            DA.SetData(0, 246);
                        else if (subt == "notdefined")
                            DA.SetData(0, 247);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcfurnishingelement":
                case "furnishingelement":
                case "furnishing_element":
                case "furnishing element":
                    DA.SetData(0, 43);
                    break;

                case "ifctransportelement":
                case "transportelement":
                case "transport_element":
                case "transport element":
                    {
                        if (subt == "elevator")
                            DA.SetData(0, 264);
                        else if (subt == "escalator")
                            DA.SetData(0, 265);
                        else if (subt == "movingwalkway" || subt == "moving walk way" || subt == "moving walkway" || subt == "moving_walk_way" || subt == "moving_walkway")
                            DA.SetData(0, 266);
                        else if (subt == "craneway" || subt == "crane_way" || subt == "crane way")
                            DA.SetData(0, 267);
                        else if (subt == "liftinggear" || subt == "lifting_gear" || subt == "lifting gear")
                            DA.SetData(0, 268);
                        else if (subt == "userdefined")
                            DA.SetData(0, 269);
                        else if (subt == "notdefined")
                            DA.SetData(0, 270);
                        else
                            throw new ArgumentException("Element Sub Type type not recognized");
                    }
                    break;

                case "ifcvirtualelement":
                case "virtualelement":
                case "virtual element":
                case "virtual_element":
                    DA.SetData(0, 45);
                    break;

                default:
                    throw new ArgumentException("Element type not recognized");
                    break;
            }

        }
         
        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("c2d738eb-831b-433a-8c74-d312fdbf79fd");
            }
        }
    }
}
