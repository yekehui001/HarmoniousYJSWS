# HarmoniousYJSWS
## 异界事务所美术资源替换工具
#### 替换工具更新地址
+ https://github.com/yekehui001/HarmoniousYJSWS/releases
+ 有问题可以点上面的Issues提交。

#### PC端新年版本立绘语音整合包1.1.4版本
+ 有默认替换日语音的bug，需要下1.1.5版修正
+ 链接：https://pan.baidu.com/s/1nkDlj1Y2RsNX05Uh5Y8miQ 
+ 提取码：v08w
#### PC端日语音 220216更新 ESPR小队，情人节皮肤，觉醒米娜，中二副总，罗没病
+ 链接：https://pan.baidu.com/s/1bov_NTH77FCXddwQ0-SfvQ 
+ 提取码：1pya
#### PC端立绘 220217更新 领航员技能图片，解决副总开技能卡死的问题
+ 链接：https://pan.baidu.com/s/1vNuAigL_eX56NwVc6oGkCw 
+ 提取码：fgvr
#### 删除ab_unit_game_spine_nkm_unit_c_strega_evelyn.asset可以解决飞天奶开大卡死的问题。紫龙似乎悄悄改了这个文件，把ry加上去了。代价是换了韩服文件会卡死ORZ

#### 另外推荐一下贴吧涯S做的替换工具：
+ https://tieba.baidu.com/p/7649001346?pn=1
+ 真一键替换。这个工具启动后会顶替紫龙的更新服务器，然后“更新”掉和谐的衣服，非常强。

#### 安卓官服轻量化客户端
+ 第一次运行请勾上完整性检测。
+ 可以任意替换素材到Android\data\com.zlongame.cn.coside\files\Assetbundles
+ 注意PC端和安卓端的素材是不兼容的。
+ 不支持合作方登录，需要将合作方账号绑定到手机号才能登录。
+ 更新到空狙版本
+ 链接：https://pan.baidu.com/s/1OShRttsMuvACT4Q3c1oWtA 
+ 提取码：k348

#### 安卓端立绘包
+ 安装完客户端，等更新结束之后复制到Android\data\com.zlongame.cn.coside\files\Assetbundles下覆盖原文件
+ 链接：https://pan.baidu.com/s/1rnD43j84C5FNDMZ_E1njoA 
+ 提取码：lk2s

### 安卓端制作方法：
#### 1. 制做轻量化客户端
+ 下载官方的apk文件
+ 用MT管理器、rar浏览器之类的工具打开apk的文件结构
+ 删除 cn_yjsws\assets\Assetbundles下的所有内容
+ 给apk重新加个签名（MT管理器自带签名功能，其他签名工具可以百度）

#### 2. 制作资源包（注意PC和安卓的资源文件是不互通的）
+ 下载省服的apk
+ 用MT管理器、rar浏览器之类的工具打开apk的文件结构，复制assets/Assetbundles/中的
+ ab_fx_skill_cutin_*
+ ab_ui_nkm_ui_*
+ ab_unit_game_spine_nkm_*
+ ab_unit_illust_*
+ ab_login_screen_*
+ 把上述文件的扩展名从.asset改.cn
+ 如果需要语音的话还有
+ \*.vkor
+ 把上述文件的扩展名从.vkor改.vchn(替换汉语）或者.vjpn（补全日语）
+ 安装省服apk，更新，去省服热更新目录再次寻找以上提到的资源文件,并重复改名操作。（建议做个批处理脚本或者自己做个改名程序）
+ 省服热更新文件夹似乎是Android\data\com.gamebeans.coside\files\Assetbundles,具体记不得了，反正认准gamebeans和coside。
+ 国服热更新文件夹是Android\data\com.zlongame.cn.coside\files\Assetbundles


#### 3. 安装过程
+ 卸载原来的客户端（签名相同的可以不卸载），安装轻量化客户端，安装好后进游戏，点开完整性检测，更新完退出游戏，再进游戏，再更新。
+ 打开国服热更新目录，将上述提取的省服资源包覆盖进去。

+ 再次启动游戏确认替换是否成功。
+ 此后每次有小更新就把省服资源包重新覆盖一边，有大更新就重新做一次客户端。

