using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HarmoniousYJSWS
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        private void ExtactVoice(string from, string to)
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
        private void PickAndriod(string an, string pc, string to)
        {
            foreach (var an_name in Directory.EnumerateFiles(an))
            {
                FileInfo an_info = new FileInfo(an_name);
                if (an_name.EndsWith(".asset"))
                {
                    var pc_name = Path.Combine(pc, an_info.Name).Replace(".asset", ".cn");
                    if (File.Exists(pc_name))
                    {
                        File.Copy(an_name, Path.Combine(to, an_info.Name.Replace(".asset", ".cn")),true);
                    }
                }
            }
        }
        private void ExtactIllution(string kor, string chn, string to)
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
                    || info.Name.StartsWith("ab_unit_illust_"))
                    && info.Name.EndsWith(".cn"))
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
        private MainWindow view;
        protected override void OnStartup(StartupEventArgs e)
        {
            //ExtactVoice(@"D:\CountersideFW\client\Data\StreamingAssets", @"Voices");
            //ExtactIllution(@"D:\CountersideFW\client\Data\StreamingAssets", @"D:\YiJieShiWuSuo\client\Data\StreamingAssets", "Illusions");
            //PickAndriod(@"D:\Assetbundles", "TargetAsset", "TargetAsset_Android");
            //return;
            base.OnStartup(e);
            MainViewModel vm = new MainViewModel();
            if (File.Exists("path.txt"))
            {
                vm.NativeClientPath = File.ReadAllText("path.txt");
            }
            view = new MainWindow();
            view.DataContext = vm;
            this.MainWindow = view;
            view.Show();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            var vm = view.DataContext as MainViewModel;
            File.WriteAllText("path.txt", vm.NativeClientPath);
            base.OnExit(e);
        }
    }
}
