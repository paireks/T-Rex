using System.Collections.Generic;
using dotbim;
using T_RexEngine.Interfaces;

namespace T_RexEngine
{
    public class Dotbim
    {
        public Dotbim(List<ElementGroup> elementGroups, string projectName, string buildingName, string path)
        {
            List<IElementSetConvertable> elementSetConvertables = new List<IElementSetConvertable>();
            foreach (var elementGroup in elementGroups) 
            {
                elementSetConvertables.Add(elementGroup);    
            }
            
            Dictionary<string, string> info = new Dictionary<string, string>
            {
                {"Project Name", projectName},
                {"Building Name", buildingName},
            };

            File file = Tools.CreateFile(elementSetConvertables, info);
            file.Save(path);
        }
    }
}