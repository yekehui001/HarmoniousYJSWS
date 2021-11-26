using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmoniousYJSWS
{
    class ResourceExtractFunctions : Singleton<ResourceExtractFunctions>
    {
        public void ExtactVoice(string from, string to)
        {
            if (!Directory.Exists(to))
            {
                Directory.CreateDirectory(to);
            }
            foreach (var filename in Directory.EnumerateFiles(from))
            {
                if (filename.EndsWith(".vkor"))
                {
                    FileInfo info = new FileInfo(filename);
                    File.Copy(filename, Path.Combine(to, info.Name.Replace(".vkor", ".vchn")));
                }
            }
        }
        public void PickAndriod(string an, string pc, string to)
        {
            foreach (var an_name in Directory.EnumerateFiles(an))
            {
                FileInfo an_info = new FileInfo(an_name);
                if (an_name.EndsWith(".asset"))
                {
                    var pc_name = Path.Combine(pc, an_info.Name).Replace(".asset", ".cn");
                    if (File.Exists(pc_name))
                    {
                        File.Copy(an_name, Path.Combine(to, an_info.Name.Replace(".asset", ".cn")), true);
                    }
                }
            }
        }
        public void ExtactIllution(string kor, string chn, string to)
        {
            if (!Directory.Exists(to))
            {
                Directory.CreateDirectory(to);
            }
            foreach (var filename in Directory.EnumerateFiles(chn))
            {
                FileInfo info = new FileInfo(filename);
                if ((info.Name.StartsWith("ab_fx_skill_cutin_nkm_")
                    || info.Name.StartsWith("ab_ui_nkm_ui_")
                    || info.Name.StartsWith("ab_unit_game_spine_nkm_")
                    || info.Name.Contains("icon")
                    || info.Name.StartsWith("ab_unit_illust_"))                    
                    && info.Name.EndsWith(".cn")
                    ||  info.Name.StartsWith("ab_login_screen_"))
                {
                    string fromName = Path.Combine(kor, info.Name.Replace(".cn", ".asset"));
                    if (!File.Exists(fromName))
                    {
                        fromName = fromName.Replace(".asset", ".twn");
                    }
                    if (!File.Exists(fromName))
                    {
                        Console.WriteLine(fromName);
                        continue;
                    }
                    File.Copy(fromName, Path.Combine(to, info.Name), true);
                }
            }
        }
    }
}
