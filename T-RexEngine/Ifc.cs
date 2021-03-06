using System;
using System.Collections.Generic;
using System.Linq;
using Xbim.Common;
using Xbim.Common.Step21;
using Xbim.Ifc;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.ProductExtension;
using Xbim.IO;

namespace T_RexEngine
{
    public class Ifc
    {
        public Ifc(List<Element> elements, string path)
        {
            using (IfcStore model = CreateAndInitModel("My Model", ProjectUnits.SIUnitsUK))
            {
                if (model != null)
                {
                    IfcBuilding building = CreateBuilding(model, "Default Building");

                    foreach (var element in elements)
                    {
                        IfcBuildingElement currentElement = element.ToIfc(model);
                        using (var transaction = model.BeginTransaction("Add element"))
                        {
                            building.AddElement(currentElement);
                            transaction.Commit();
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

        private static IfcStore CreateAndInitModel(string projectName, ProjectUnits units)
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
                project.Initialize(units);
                project.Name = projectName;
                transaction.Commit();
            }

            return model;
        }

        private static IfcBuilding CreateBuilding(IfcStore model, string buildingName)
        {
            using (ITransaction transaction = model.BeginTransaction("Create Buidling"))
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