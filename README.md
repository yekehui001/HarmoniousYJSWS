# HarmoniousYJSWS
## 异界事务所美术资源替换工具
#### 替换工具更新地址
+ https://github.com/yekehui001/HarmoniousYJSWS/releases
+ 有问题可以点上面的Issues提交。

#### PC端新年版本立绘语音整合包1.1.4版本
+ 链接：https://pan.baidu.com/s/1nkDlj1Y2RsNX05Uh5Y8miQ 
+ 提取码：v08w

#### 另外推荐一下贴吧涯S做的替换工具：
+ https://tieba.baidu.com/p/7649001346?pn=1
+ 真一键替换。这个工具启动后会顶替紫龙的更新服务器，然后“更新”掉和谐的衣服，非常强。

#### 安卓官服轻量化客户端
+ 参考日服架构裁剪到100M，第一次运行请勾上完整性检测。
+ 可以任意替换素材到Android\data\com.zlongame.cn.coside\files\Assetbundles
+ 注意PC端和安卓端的素材是不兼容的。
+ 装了官服或其他人做的客户端需要先卸载掉。
+ 不支持合作方登录，需要将合作方账号绑定到手机号才能登录。
+ 更新到圣诞版本
+ 链接：https://pan.baidu.com/s/1-AOcO-uLhAZfvkFbqaM84Q 
+ 提取码：s3s2

#### 安卓端立绘包（老师傅版本很老了，新的都没有，推荐去找其他人做的新点的）
+ 安装完客户端，等更新结束之后复制到Android\data\com.zlongame.cn.coside\files\Assetbundles下覆盖原文件
+ 链接：https://pan.baidu.com/s/1E3p7wbRGHyIVpJnCaGsl0w 
+ 提取码：1682 


### 安卓端制作方法：
#### 1. 制做轻量化客户端。
+ 下载你要玩的那个服的apk文件，以cn_yjsws.apk代称。下载apktool。下载，安装jdk。配置好系统PATH。
+ 打开cmd，转到apk的路径。
+ apktool d cn_yjsws.apk
+ 手动删除 cn_yjsws\assets\Assetbundles下的所有内容
+ apktool b cn_yjsws
+ keytool -genkey -alias test -keyalg RSA -validity 10000 -keystore test.keystore
+ 会叫你写个密码，这个记住，其他的随便填。最后问你是否确认，输入是
+ jarsigner -verbose -keystore test.keystore cn_yjsws\dist\cn_yjsws.apk test
+ 中间会叫你填上面你写的密码。

#### 2. 制作资源包（注意PC和安卓的资源文件是不互通的）
+ 下载省服的apk，扩展名改rar，复制assets/Assetbundles/中的
+ ab_fx_skill_cutin_nkm_*
+ ab_ui_nkm_ui_*
+ ab_unit_game_spine_nkm_*
+ ab_unit_illust_*
+ ab_login_screen_*
+ 把上述文件的扩展名从.asset改.cn
+ 如果需要语音的话还有
+ ab_ui_unit_voice_*
+ 把上述文件的扩展名从.vkor改.vchn
+ 安装省服apk，更新，去省服热更新目录再次寻找以上提到的资源文件,并重复改名操作。（建议做个批处理脚本或者自己做个改名程序）
+ 省服热更新文件夹似乎是Android\data\com.gamebeans.coside\files\Assetbundles,具体记不得了，反正认准gamebeans和coside。
+ 国服热更新文件夹是Android\data\com.zlongame.cn.coside\files\Assetbundles


#### 3. 安装过程
+ 卸载原来的客户端，安装轻量化客户端，安装好后进游戏，点开完整性检测，更新完退出游戏，再进游戏，再更新。
+ 打开国服热更新目录，将上述提取的省服资源包覆盖进去。

+ 再次启动游戏确认替换是否成功。
+ 此后每次有小更新就把省服资源包重新覆盖一边，有大更新就重新做一次客户端。

