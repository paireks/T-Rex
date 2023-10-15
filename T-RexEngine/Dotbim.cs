using System.Collections.Generic;
using dotbim;
using T_RexEngine.Interfaces;

namespace T_RexEngine
{
    public class Dotbim
    {
        public Dotbim(List<ElementGroup> elementGroups, string projectName, string buildingName, double scaleFactor, string path)
        {
            List<IElementSetConvertable> elementSetConvertables = new List<IElementSetConvertable>();
            foreach (var elementGroup in elementGroups) 
            {
                elementSetConvertables.AddRange(elementGroup.ToElementSetList());    
            }
            
            Dictionary<string, string> info = new Dictionary<string, string>
            {
                {"Project Name", projectName},
                {"Building Name", buildingName},
            };

            File file = Tools.CreateFile(elementSetConvertables, info, scaleFactor);
            file.Save(path);
        }
    }
}