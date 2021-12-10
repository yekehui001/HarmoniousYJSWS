using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Diagnostics;

namespace HarmoniousYJSWS
{
    class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.InstallCommand = new DelegateCommand(DoInstallCommand);
            this.UninstallCommand = new DelegateCommand(DoUninstallCommand);
            this.RecoverCommand = new DelegateCommand(DoRecoverCommand);
            this.PatchCommand = new DelegateCommand(DoPatchCommand);
            this.StartGameCommand = new DelegateCommand(DoStartGameCommand);
        }
        public bool IncludeVoice { get; set; }
        public string NativeClientPath { get; set; } = @"??\YiJieShiWuSuo";
        public string Info { get; set; } =
            "使用方法：" +
            "首次启动时在上面的框输入国服安装路径，然后点安装资源。\r\n" +
            "之后点启动游戏，等到更新完成，提示\"点击继续\"时点转换资源，然后再进游戏。\r\n" +
            "请各位不要拿这个玩具来直播，录视频做节目，截图大肆发帖之类的。\r\n" +
            "更新地址：https://github.com/yekehui001/HarmoniousYJSWS \r\n";
           
        public DelegateCommand InstallCommand { get; set; }
        public DelegateCommand UninstallCommand { get; set; }
        public DelegateCommand RecoverCommand { get; set; }
        public DelegateCommand PatchCommand { get; set; }
        public DelegateCommand StartGameCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private string nativeAssetPath;
        private string targetAssetPath = "TargetAsset";
        private string hotfixAssetPath = string.Empty;
        private void Log(string v)
        {
            Application.Current.Dispatcher.Invoke(() => Info += v + "\r\n");
        }
        private bool pathChecked = false;
        private bool CheckPath()
        {
            if (pathChecked)
            {
                return true;
            }
            if (!Directory.Exists(NativeClientPath))
            {
                Log("国服安装目录设定不正确。");
                return false;
            }
            nativeAssetPath = Path.Combine(NativeClientPath, @"client\Data\StreamingAssets");
            if (!Directory.Exists(nativeAssetPath))
            {
                nativeAssetPath = Path.Combine(NativeClientPath, @"Games\client\Data\StreamingAssets");
                if (!Directory.Exists(nativeAssetPath))
                {
                    Log("国服安装目录设定不正确。");
                    return false;
                }
            }
            Log(string.Format("发现客户端资源目录:{0}", nativeAssetPath));
            foreach (var dir in Directory.EnumerateDirectories(@"C:\Users\"))
            {
                if (Directory.Exists(Path.Combine(dir, @"AppData\LocalLow\studioBside\异界事务所\Assetbundles")))
                {
                    hotfixAssetPath = Path.Combine(dir, @"AppData\LocalLow\studioBside\异界事务所\Assetbundles");
                    Log(string.Format("发现热更新目录：{0}", hotfixAssetPath));
                    break;
                }
            }
            if (!Directory.Exists(hotfixAssetPath))
            {
                Log(string.Format("没有发现热更新目录。"));
            }
            this.pathChecked = true;
            return true;
        }
        private void DoUninstallCommand(object para)
        {
            DoRecoverCommand(para);
            if (!CheckPath())
            {
                return;
            }
            int count = 0;
            Log("开始卸载资源包。");
            foreach (var filename in Directory.EnumerateFiles(nativeAssetPath))
            {
                if (filename.EndsWith("-h"))
                {
                    try
                    {
                        File.Delete(filename);

                    }
                    catch (Exception e)
                    {
                        Log("未能删除{0},因为{1}。".Format(filename, e.Message));
                    }
                    if ((count++ % 100) == 0)
                    {
                        Log("已处理{0}文件".Format(count));
                    }
                }
            }
            Log("资源包卸载完成。");
        }
        private void DoStartGameCommand(object para)
        {
            DoRecoverCommand(para);
            var exepath = Path.Combine(NativeClientPath, @"Launcher\PDLauncher.exe");
            if (!File.Exists(exepath))
            {
                exepath = Path.Combine(NativeClientPath, @"client\CounterSide.exe");
            }
            if (!File.Exists(exepath))
            {
                exepath = Path.Combine(NativeClientPath, @"Games\Launcher\PDLauncher.exe");
            }
            if (!File.Exists(exepath))
            {
                exepath = Path.Combine(NativeClientPath, @"Games\client\CounterSide.exe");
            }
            if (!File.Exists(exepath))
            {
                Log("没有发现游戏执行程序");
                return;
            }
            var minSizeFileName = Directory.EnumerateFiles(hotfixAssetPath)
                .Select(x => new FileInfo(x))
                .Where(x => x.Name.StartsWith("ab_"))
                .OrderBy(x => x.Length).FirstOrDefault()
                ?.FullName;
            if (!string.IsNullOrEmpty(minSizeFileName))
            {
                try
                {
                    File.Delete(minSizeFileName);
                    Log("触发更新成功，请在更新结束提示“点击继续”时点击修改资源按钮。");
                }
                catch
                {
                    Log("触发更新失败，请在登录界面点击修改资源按钮。");
                }
            }
            else
            {
                Log("触发更新失败，请在登录界面点击修改资源按钮。");
            }
            var p = new Process();
            FileInfo info = new FileInfo(exepath);
            p.StartInfo.WorkingDirectory = Path.Combine(NativeClientPath, info.DirectoryName);
            p.StartInfo.FileName = exepath;
            try
            {
                Log(string.Format("正在启动客户端。"));
                p.Start();
            }
            catch (Exception e)
            {
                Log(string.Format("客户端启动失败，因为{0}", e.Message));
            }

        }
        private void HotfixRecheck()
        {
            if (!CheckPath())
            {
                return;
            }
            foreach (var hffilename in Directory.EnumerateFiles(hotfixAssetPath))
            {
                var hfFileInfo = new FileInfo(hffilename);
                var nativeFilename = Path.Combine(nativeAssetPath, hfFileInfo.Name);
                if (!File.Exists(nativeFilename))
                {
                    try
                    {
                        File.Copy(hffilename, nativeFilename);
                        var targetFilename = Path.Combine(targetAssetPath, hfFileInfo.Name);
                        if (File.Exists(targetFilename))
                        {
                            File.Copy(targetFilename, nativeFilename + "-h");
                        }
                    }
                    catch (Exception e)
                    {
                        Log(string.Format("复制{0}失败，因为{1}", hffilename, e.Message));
                    }
                }
            }
        }
        private void DoRecoverCommand(object para)
        {
            if (!CheckPath())
            {
                return;
            }
            Log(string.Format("开始还原。"));
            int count = 0;
            foreach (var filename in Directory.EnumerateFiles(nativeAssetPath))
            {
                if (filename.EndsWith("-b"))
                {
                    var nativeName = filename.Substring(0, filename.Length - 2);
                    try
                    {
                        File.Move(nativeName, nativeName + "-h");
                        File.Move(filename, nativeName);
                    }
                    catch (Exception e)
                    {
                        Log(string.Format("还原{0}失败，因为{1}", nativeName, e.Message));
                    }
                    var nativeFileInfo = new FileInfo(nativeName);
                    var serverUpdateFilename = Path.Combine(hotfixAssetPath, nativeFileInfo.Name);
                    if (File.Exists(serverUpdateFilename))
                    {
                        try
                        {
                            File.Delete(serverUpdateFilename);
                            File.Move(serverUpdateFilename + "-b", serverUpdateFilename);
                        }
                        catch (Exception e)
                        {
                            Log(string.Format("转换{0}失败,因为{1}", serverUpdateFilename, e.Message));
                        }
                    }
                    if ((count++ % 100) == 0)
                    {
                        Log("已处理{0}文件".Format(count));
                    }
                }
            }
            Log(string.Format("还原到国服资源"));
        }
        private void DoPatchCommand(object para)
        {
            if (!CheckPath())
            {
                return;
            }
            HotfixRecheck();
            Log(string.Format("开始转换，可能需要数秒时间。"));
            int count = 0;
            foreach (var filename in Directory.EnumerateFiles(nativeAssetPath))
            {
                if (filename.EndsWith("-b"))
                {
                    Log(string.Format("{0}似乎已经是目标资源了", filename.Substring(0, filename.Length - 2)));
                    continue;
                }
                if (filename.EndsWith("-h"))
                {
                    var nativeName = filename.Substring(0, filename.Length - 2);
                    try
                    {
                        File.Move(nativeName, nativeName + "-b");
                        File.Move(filename, nativeName);
                    }
                    catch (Exception e)
                    {
                        Log(string.Format("转换{0}失败,因为{1}", nativeName, e.Message));
                    }
                    var nativeFileInfo = new FileInfo(nativeName);
                    var serverUpdateFilename = Path.Combine(hotfixAssetPath, nativeFileInfo.Name);
                    if (File.Exists(serverUpdateFilename))
                    {
                        try
                        {
                            File.Move(serverUpdateFilename, serverUpdateFilename + "-b");
                            File.Copy(nativeName, serverUpdateFilename);
                        }
                        catch (Exception e)
                        {
                            Log(string.Format("转换{0}失败,因为{1}", serverUpdateFilename, e.Message));
                        }
                    }
                    if ((count++ % 100) == 0)
                    {
                        Log("已处理{0}文件".Format(count));
                    }
                }
            }
            Log(string.Format("已完成转换。"));
        }
        private void DoMakeResourcePackage()
        {

        }
        class FileNoExtComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                var a = Path.GetFileNameWithoutExtension(x);
                var b = Path.GetFileNameWithoutExtension(y);
                return string.Equals(a, b);
            }

            public int GetHashCode(string obj)
            {
                var a = Path.GetFileNameWithoutExtension(obj);
                return a.GetHashCode();
            }
        }
        private void ExecuteUpdateScript()
        {
            var updateScriptName = Path.Combine(targetAssetPath, "UpdateScript.bat");
            if (File.Exists(updateScriptName))
            {
                Log(@"发现资源包升级脚本，开始执行。");
                foreach (var line in File.ReadAllLines(updateScriptName))
                {
                    if (line[0] == '-')
                    {
                        var removeFilename = line.Substring(1);
                        try
                        {
                            Log("删除{0}".Format(removeFilename));
                            File.Delete(removeFilename);
                        }
                        catch (Exception e)
                        {
                            Log(string.Format("删除{0}失败，因为{1}", removeFilename, e.Message));
                        }
                    }
                }
            }
        }
        private void DoInstallCommand(object para)
        {
            if (!CheckPath())
            {
                return;
            }
            DoUninstallCommand(null);
            if (!Directory.Exists(targetAssetPath))
            {
                Log(@"请先下载替换的资源包，放置路径.\TargetAsset");
                return;
            }
            ExecuteUpdateScript();
            int count = 0;
            Log(string.Format("开始安装"));
            HashSet<string> nativeFilenames = new HashSet<string>(Directory.EnumerateFiles(nativeAssetPath), new FileNoExtComparer());
            foreach (var targetfilename in Directory.EnumerateFiles(targetAssetPath))
            {
                var targetFileInfo = new FileInfo(targetfilename);
                var nativeFilenameNoEx = Path.GetFileNameWithoutExtension(Path.Combine(nativeAssetPath, targetFileInfo.Name));
                string nativeFilename = string.Empty;
                if (nativeFilenames.TryGetValue(nativeFilenameNoEx, out nativeFilename))
                {
                    if (Path.GetExtension(nativeFilename) == ".cn" || Path.GetExtension(nativeFilename) == ".vchn")
                    {
                        try
                        {
                            File.Copy(targetfilename, nativeFilename + "-h", true);
                        }
                        catch (Exception e)
                        {
                            Log(string.Format("复制{0}失败，因为{1}", nativeFilename + "-h", e.Message));
                        }
                        if ((count++ % 100) == 0)
                        {
                            Log("已处理{0}文件".Format(count));
                        }
                    }
                }
            }
            Log(string.Format("安装结束"));
        }
    }
}
