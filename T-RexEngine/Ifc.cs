using System;
using System.Collections.Generic;
using System.Linq;
using T_RexEngine.Enums;
using Xbim.Common;
using Xbim.Common.Step21;
using Xbim.Ifc;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.MeasureResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.StructuralElementsDomain;
using Xbim.IO;

namespace T_RexEngine
{
    public class Ifc
    {
        public Ifc(List<ElementGroup> elementGroups, string projectName, string buildingName, string path)
        {
            using (IfcStore model = CreateAndInitModel(projectName))
            {
                if (model != null)
                {
                    IfcBuilding building = CreateBuilding(model, buildingName);

                    foreach (var elementGroup in elementGroups)
                    {
                        switch (elementGroup.ElementType)
                        {
                            case ElementType.PadFooting:
                            case ElementType.StripFootings:
                            case ElementType.Beams:
                            case ElementType.Columns:
                            {
                                List<IfcBuildingElement> currentElementGroup = elementGroup.ToBuildingElementIfc(model);
                                foreach (var buildingElement in currentElementGroup)
                                {
                                    using (var transaction = model.BeginTransaction("Add element"))
                                    {
                                        building.AddElement(buildingElement);
                                        transaction.Commit();
                                    }
                                }

                                break;
                            }
                            case ElementType.Rebar:
                            {
                                List<IfcReinforcingElement> currentElementGroup = elementGroup.ToReinforcingElementIfc(model);
                                foreach (var buildingElement in currentElementGroup)
                                {
                                    using (var transaction = model.BeginTransaction("Add element"))
                                    {
                                        building.AddElement(buildingElement);
                                        transaction.Commit();
                                    }
                                }

                                break;
                            }
                            default:
                                throw new ArgumentException("Unknown element type");
                        }
                    }

                    try
                    {
                        model.SaveAs(path, StorageType.Ifc);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        private static IfcStore CreateAndInitModel(string projectName)
        {
            var credentials = new XbimEditorCredentials
            {
                ApplicationDevelopersName = "xbim developer",
                ApplicationFullName = "T-Rex",
                ApplicationIdentifier = "T-Rex",
                ApplicationVersion = "0.2",
                EditorsFamilyName = "Team",
                EditorsGivenName = "xbim",
                EditorsOrganisationName = "xbim developer"
            };

            var model = IfcStore.Create(credentials, XbimSchemaVersion.Ifc4x1, XbimStoreType.InMemoryModel);

            using (ITransaction transaction = model.BeginTransaction("Initialise Model"))
            {
                IfcProject project = model.Instances.New<IfcProject>();
                project.Initialize(ProjectUnits.SIUnitsUK);
                project.Name = projectName;
                transaction.Commit();
            }

            return model;
        }

        private static IfcBuilding CreateBuilding(IfcStore model, string buildingName)
        {
            using (ITransaction transaction = model.BeginTransaction("Create Building"))
            {
                IfcBuilding building = model.Instances.New<IfcBuilding>();
                building.Name = buildingName;
                building.CompositionType = IfcElementCompositionEnum.ELEMENT;

                IfcLocalPlacement localPlacement = model.Instances.New<IfcLocalPlacement>();
                IfcAxis2Placement3D placement = model.Instances.New<IfcAxis2Placement3D>();

                localPlacement.RelativePlacement = placement;
                placement.Location = model.Instances.New<IfcCartesianPoint>(p => p.SetXYZ(0, 0, 0));

                IfcProject project = model.Instances.OfType<IfcProject>().FirstOrDefault();
                project?.AddBuilding(building);
                transaction.Commit();
                
                return building; 
            }
        }
    }
}