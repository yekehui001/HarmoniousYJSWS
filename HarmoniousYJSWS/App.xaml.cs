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

        private void ExtactVoice(string from,string to)
        {
            if(!Directory.Exists(to))
            {
                Directory.CreateDirectory(to);
            }
            foreach(var filename in Directory.EnumerateFiles(from))
            {
                if(filename.EndsWith(".vkor"))
                {
                    FileInfo info = new FileInfo(filename);
                    File.Copy(filename, Path.Combine(to, info.Name.Replace(".vkor",".vchn")));
                }
            }
        }

        private MainWindow view;
        protected override void OnStartup(StartupEventArgs e)
        {
            //ExtactVoice(@"D:\CountersideFW\client\Data\StreamingAssets", @"Voices");
            //return;
            base.OnStartup(e);
            MainViewModel vm = new MainViewModel();
            if(File.Exists("path.txt"))
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
