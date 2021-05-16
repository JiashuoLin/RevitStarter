using Microsoft.Win32;
using RevitStarter.ApplicationPackageModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitStarter
{
    public class Utils
    {
        /// <summary>
        /// Get revit install location
        /// </summary>
        /// <param name="Version"></param>
        /// <returns></returns>
        public static void GetIntallPathInfo(string Version)
        {

        }

        /// <summary>
        /// get all revit application info
        /// </summary>
        /// <returns></returns>
        public static List<RevitAppInfo> GetAllRevitAppInfo()
        {

            string ExeName = "Revit.exe";
            var revitAppInfos = new List<RevitAppInfo>();

            var revitAppSubKeyNameBase = @"SOFTWARE\Autodesk\Revit\";

            var versions = new List<string>
            {
                "2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022", "2023"
            };

            foreach (var item in versions)
            {
                var revitAppSubKeyName = $"{revitAppSubKeyNameBase}{item}\\REVIT-05:0804";
                var revitRegistryKey = Registry.LocalMachine.OpenSubKey(revitAppSubKeyName);
                if (revitRegistryKey == null)
                {
                    continue;
                }
                var installationLocation = revitRegistryKey.GetValue("InstallationLocation", "").ToString();

                if (string.IsNullOrEmpty(installationLocation))
                {
                    continue;
                }

                var revitAppInfo = new RevitAppInfo
                {
                    Location = Path.Combine(installationLocation, ExeName),
                    Version = item,
                };

                revitAppInfos.Add(revitAppInfo);
            }

            return revitAppInfos;

        }

        /// <summary>
        /// Get all Revit plugins
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static List<RevitPlugingInfo> GetAllAddIns(string version)
        {
            var revitPlugingInfos = new List<RevitPlugingInfo>();
            foreach (var item in Global.RevitAddins)
            {
                var addinDir = string.Format(item, version);
                if (!Directory.Exists(addinDir))
                {
                    continue;
                }
                var files = Directory.GetFiles(addinDir, $"*{Global.AddinSuffix}", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    var pluginPath = file;
                    var isSelected = true;
                    if (!File.Exists(pluginPath))
                    {
                        isSelected = false;
                        pluginPath = pluginPath.Replace(Global.AddinSuffix, Global.CustomSuffix);
                        if (!File.Exists(pluginPath))
                        {
                            continue;
                        }
                    }
                    revitPlugingInfos.Add(new RevitPlugingInfo
                    {
                        Name = fileInfo.Name,
                        Path = pluginPath,
                        IsSelected = isSelected
                    });
                }
            }

            foreach (var item in Global.ApplicationPlugins)
            {
                if (!Directory.Exists(item))
                {
                    continue;
                }
                foreach (var dir in Directory.GetDirectories(item))
                {
                    var dirInfo = new DirectoryInfo(dir);
                    if (!dirInfo.Name.Contains(".bundle"))
                    {
                        continue;
                    }
                    var packageContentsXmlPath = Path.Combine(dir, "PackageContents.xml");
                    if (!File.Exists(packageContentsXmlPath))
                    {
                        continue;
                    }
                    var revitPluginInfo = GetRevitPlugingInfo(packageContentsXmlPath, version);
                    revitPlugingInfos.Add(revitPluginInfo);
                }
            }

            return revitPlugingInfos;
        }

        /// <summary>
        /// Get specific versions of Revit plugins 
        /// </summary>
        /// <param name="packageContentsXmlPath"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static RevitPlugingInfo GetRevitPlugingInfo(string packageContentsXmlPath, string version)
        {
            var fileInfo = new FileInfo(packageContentsXmlPath);
            var dir = fileInfo.Directory.FullName;

            var result = XmlHelper.Deserialize<ApplicationPackage>(packageContentsXmlPath);

            foreach (var item in result.Components)
            {
                var currentVersion = ConvertVersion(item.RuntimeRequirements.SeriesMin);
                if (currentVersion == version)
                {
                    var pluginPath = Path.Combine(dir, Trim(item.ComponentEntry.ModuleName, 2));
                    var isSelected = true;
                    if (!File.Exists(pluginPath))
                    {
                        isSelected = false;
                        pluginPath = pluginPath.Replace(Global.AddinSuffix, Global.CustomSuffix); 
                        if (!File.Exists(pluginPath))
                        {
                            continue;
                        }
                    }
                    return new RevitPlugingInfo
                    {
                        Name = item.ComponentEntry.AppName,
                        IsSelected = isSelected,
                        Path = pluginPath,
                    };
                }
            }

            return null;

        }

        private static string ConvertVersion(string version)
        {
            switch (version)
            {
                case "R2016":
                    {
                        return "2016";
                    }
                case "R2017":
                    {
                        return "2017";
                    }
                case "R2018":
                    {
                        return "2018";
                    }
                case "R2019":
                    {
                        return "2019";
                    }
                case "R2020":
                    {
                        return "2020";
                    }
                case "R2021":
                    {
                        return "2021";
                    }
                case "R2022":
                    {
                        return "2022";
                    }
                default:
                    return "";
            }
        }

        private static string Trim(string str, int count)
        {
            var chars = str.Skip(count);
            return new string(chars.ToArray());
        }
    }
}
