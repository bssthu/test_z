# eso_zh_server
Chinese translation for Elder Scrolls Online


## 原理

程序运行时，每秒截屏一次，检查屏幕中的二维码，
从中提取文字，并调用在线翻译API将文字翻译成中文，
显示在程序界面/网页上。

本程序分为插件（Addon）及外置程序两部分。
本页面为外置程序的项目主页： https://github.com/esozh/eso_zh_server

插件说明、安装配置方法请见 https://github.com/esozh/eso_zh


## 配置方法

双击运行即可使用基本功能。
需要将 zxing.dll 与 eso_zh_server.exe 放在同一路径下。


#### 网页版配置

需要添加 URL 保留项。

以管理员身份打开命令提示符（cmd），
输入以下命令并执行：
```bash
netsh http add urlacl url=http://+:20528/ user=users
```

之后再启动 eso_zh_server.exe ，
即可通过 http://[计算机IP地址]:20528/ 页面查看翻译结果。

（需自行替换[计算机IP地址]。网页每隔2秒自动刷新一次。）


#### AppKey

程序使用 Yandex 的翻译 API 。

exe 中内置了默认 AppKey 。
如果 AppKey 过期或超过每日使用次数限制，
可以到 https://tech.yandex.com/ 申请新的 AppKey 。


## 问题反馈

https://github.com/esozh/eso_zh_server/issues


## 许可协议/License

GPLv3

## 致谢/Credit

- Yandex Translate API https://tech.yandex.com/
- QR decode lib https://github.com/zxing/zxing, http://zxingnet.codeplex.com/
