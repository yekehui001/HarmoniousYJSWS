# HarmoniousYJSWS
异界很凉快
PC端整合包1.0.5
链接：https://pan.baidu.com/s/1W6jIjHtYk7LPQkpzqvcHHA 
提取码：8kkc 

PC端婚纱版本修正包
链接：https://pan.baidu.com/s/1y1RrHXaolIeCPrzztNx0Mw 
提取码：5mt3 

婚纱版本人物头像消失问题：
点一下还原按钮，删除国服安装路径资料夹下的ab_unit_face_card_loc.cn-h，ab_unit_face_card.asset-h，ab_inven_icon_unit_loc.cn-h。


安卓端制作方法：
1、制作那个啥客户端。
下载你要的那个服的apk文件，以a.apk代称。下载jdk，安装jdk，配置好系统PATH。
a.apk改名a.zip。打开，删除META-INFO文件夹，删除assets/Assetbundles/下所有的
ab_fx_skill_cutin_nkm_*
ab_ui_nkm_ui_*
ab_unit_game_spine_nkm_*
ab_unit_illust_*
ab_login_screen_*
要换语音的话再删掉assets/Assetbundles/下所有的
ab_ui_unit_voice_*
然后把扩展名改回apk。

打开cmd，创建签名：
keytool -genkey -alias test -keyalg RSA -validity 10000 -keystore test.keystore
会叫你写个密码，这个记住，其他的随便填。最后问你是否确认，输入是
执行签名:
jarsigner -verbose -keystore test.keystore a\dist\a.apk test
中间会叫你填上面你写的密码。

2.制作资源包
下载省服的apk，扩展名改rar，复制其中的
ab_fx_skill_cutin_nkm_*
ab_ui_nkm_ui_*
ab_unit_game_spine_nkm_*
ab_unit_illust_*
ab_login_screen_*
如果需要的话还有
ab_ui_unit_voice_*
安装省服apk，更新，去省服热更新目录再次寻找以上提到的资源文件。


3.安装过程
a.apk安装好后进游戏，点开完整性检测，更新完报错退出游戏，再进游戏，再更新，退出游戏。
打开国服热更新目录，与省服提取的资源文件进行对比，如果扩展名为.asset就忽略；扩展名为.cn就把省服的文件改成.cn覆盖。如果要换语言，把省服的所有.vkor改成.vchn，覆盖国服文件。

再次启动游戏确认替换是否成功。
