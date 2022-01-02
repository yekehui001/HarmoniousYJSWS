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


        private MainWindow view;
        protected override void OnStartup(StartupEventArgs e)
        {
            //var funcs = ResourceExtractFunctions.Instance;
            //funcs.ExtactVoice(@"D:\CounterSideJP\Assetbundles", @"VoicesJP");
            //funcs.ExtactIllution(@"D:\CountersideFW\client\Data\StreamingAssets", @"D:\YiJieShiWuSuo\client\Data\StreamingAssets", "Illusions");
            //funcs.ExtactIllution(@"D:\CountersideFW\client\Data\StreamingAssets", @"C:\Users\GE63VR\AppData\LocalLow\studioBside\异界事务所\Assetbundles", "Illusions");
            //funcs.ExtactIllution(@"C:\Users\GE63VR\AppData\LocalLow\studioBside\未來戰\Assetbundles", @"D:\YiJieShiWuSuo\client\Data\StreamingAssets", "Illusions");
            //funcs.ExtactIllution(@"C:\Users\GE63VR\AppData\LocalLow\studioBside\未來戰\Assetbundles", @"C:\Users\GE63VR\AppData\LocalLow\studioBside\异界事务所\Assetbundles", "Illusions");
            //Environment.Exit(0);
            //PickAndriod(@"D:\Assetbundles", "TargetAsset", "TargetAsset_Android");

            base.OnStartup(e);
            MainViewModel vm = new MainViewModel();
            if (File.Exists("path.txt"))
            {
                var lines = File.ReadAllLines("path.txt");
                if (lines.Length > 0)
                {
                    vm.NativeClientPath = lines[0];
                }
                if (lines.Length > 1)
                {
                    if (lines[1] == "Mainland")
                    {
                        vm.IsMainland = true;
                    }
                    else
                    {
                        vm.IsMainland = false;
                    }
                }
            }
            view = new MainWindow();
            view.DataContext = vm;
            this.MainWindow = view;
            view.Show();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            var vm = view.DataContext as MainViewModel;
            File.WriteAllText("path.txt", vm.NativeClientPath + "\r\n" + (vm.IsMainland ? "Mainland" : "Taiwan/SEA"));
            base.OnExit(e);
        }
    }
}
