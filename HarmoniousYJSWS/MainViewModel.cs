﻿using System;
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
            this.MakeBackupCommand = new DelegateCommand(DoMakeBackupCommand);
            this.RecoverCommand = new DelegateCommand(DoRecoverCommand);
            this.PatchCommand = new DelegateCommand(DoPatchCommand);
            this.StartGameCommand = new DelegateCommand(DoStartGame);
        }
        public bool IncludeVoice { get; set; }
        public string NativeClientPath { get; set; } = @"??\YiJieShiWuSuo";
        public string OtherClientAssetPath { get; set; }
        public string Info { get; set; } =
            "如果之前用过复制只读文件的伊丽莎白资源替换方法，需要把所有的只读都去掉，修复客户端，然后删掉C盘下的那个资源文件夹。\r\n" +
            "使用方法：首先在上面的框输入国服安装路径，然后点备份资源，备份只需要做一次就行，以后直接启动游戏即可。\r\n" +
            "点启动游戏，等到登录完成，各种活动通知的框跳出来，此时不要进游戏，先点修改到目标资源，然后再进游戏。\r\n" +
            "退出游戏后点下还原到国服，不点也没事，下次点启动游戏会自动还原一次的。\r\n" +
            "请各位不要拿这个玩具来直播，录视频做节目，截图大肆发帖之类的。\r\n" +
            "扩散下载地址时也不要提反这个那个的。不然逼运营去改程序大家都麻烦。\r\n" +
            "更新地址：https://github.com/yekehui001/HarmoniousYJSWS \r\n";
        public DelegateCommand MakeBackupCommand { get; set; }
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
                Log("国服目录不存在");
                return false;
            }
            nativeAssetPath = Path.Combine(NativeClientPath, @"client\Data\StreamingAssets");
            if (!Directory.Exists(nativeAssetPath))
            {
                nativeAssetPath = Path.Combine(NativeClientPath, @"Games\client\Data\StreamingAssets");
                if (!Directory.Exists(nativeAssetPath))
                {
                    Log("国服资源目录不存在");
                    return false;
                }
            }
            Log(string.Format("国服资源目录为{0}", nativeAssetPath));
            foreach (var dir in Directory.EnumerateDirectories(@"C:\Users\"))
            {
                if (Directory.Exists(Path.Combine(dir, @"AppData\LocalLow\studioBside\异界事务所\Assetbundles")))
                {
                    hotfixAssetPath = Path.Combine(dir, @"AppData\LocalLow\studioBside\异界事务所\Assetbundles");
                    Log(string.Format("服务器资源更新目录为{0}", hotfixAssetPath));
                    break;
                }
            }
            if (!Directory.Exists(hotfixAssetPath))
            {
                Log(string.Format("没有发现服务器资源更新目录"));
            }
            this.pathChecked = true;
            return true;
        }
        private void DoStartGame(object para)
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
            var p = new Process();
            FileInfo info = new FileInfo(exepath);
            p.StartInfo.WorkingDirectory = Path.Combine(NativeClientPath, info.DirectoryName);
            p.StartInfo.FileName = exepath;           
            try
            {
                p.Start();
            }
            catch (Exception e)
            {
                Log(string.Format("游戏启动失败，因为{0}", e.Message));
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
                }
            }
            Log(string.Format("已转换为目标资源"));
        }
        private void DoMakeResourcePackage()
        {

        }
        private void DoMakeBackupCommand(object para)
        {
            if (!CheckPath())
            {
                return;
            }
            DoRecoverCommand(null);
            if (!Directory.Exists(targetAssetPath))
            {
                Log(@"请先下载替换的资源包，放置路径.\TargetAsset");
            }
            foreach (var targetfilename in Directory.EnumerateFiles(targetAssetPath))
            {
                var targetFileInfo = new FileInfo(targetfilename);
                var nativeFilename = Path.Combine(nativeAssetPath, targetFileInfo.Name);
                if (File.Exists(nativeFilename))
                {
                    if (File.Exists(nativeFilename + "-b"))
                    {
                        //should not be here.
                        Log(string.Format("似乎已经是目标服的资源，是不是重复按了备份？操作终止。\r\n" +
                            "如果你不清楚当前的状态，可以尝试还原到国服资源，或者重装游戏。", targetfilename));
                        return;
                    }
                    try
                    {
                        File.Copy(targetfilename, nativeFilename + "-h", true);
                    }
                    catch (Exception e)
                    {
                        Log(string.Format("复制{0}失败，因为{1}", nativeFilename + "-h", e.Message));
                    }
                }
                else
                {
                    Log(string.Format("{0}的对应资源不存在,可能是未来版本的资源。", targetfilename));
                }
            }
            Log(string.Format("备份结束"));
        }
    }
}
