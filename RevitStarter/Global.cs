using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitStarter
{
    public class Global
    {
        public static string[] RevitAddins = new string[]
        {
            @"C:\ProgramData\Autodesk\Revit\Addins\{0}" ,
            @"C:\Program Files\Autodesk\Revit {0}\AddIns\{0}",
            @$"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Roaming\Autodesk\Revit\Addins\" + "{0}"

        };
        public static string[] ApplicationPlugins = new string[]
        {
        @"C:\ProgramData\Autodesk\ApplicationPlugins",
        @$"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Roaming\Autodesk\ApplicationPlugins"
        };
         
        public static string CustomSuffix = ".ljsgo";

        public static string AddinSuffix = ".addin";
    }
}
