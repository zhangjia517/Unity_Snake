## WeChat Plugin for Android and IOS - An easier solution for integrating WeChat share to your games

## What is WeChat Plugin for Android and IOS

We Chat plugin for android and is a Unity3D package to help you using We Chat Sharing on Android and IOS platform. you can set up a sharing with no coding involved at all! except to bind your button to our object.

## Main features:
* Extremely easy to set up, includes a easily configurable scene object.
* All source code of C# script and native plugin is included for a reference.
* Supports Share Music, Video, Link, Text, Image.

## Set Up Guide

Please do see the Upgrade Guide Section below if you are upgrading from The WeChatPluginAndroid

The most important thing in setting this up is to register and get your app approved in the we chat website.
http://developers.wechat.com/

the support for the english we chat developers website is horrible. all wierd error messages.
I suggest to register in the chinese we chat website
https://open.weixin.qq.com/

After registering on the developers website and created the app there. take note of your app ID.
enter the app ID on WeChatPluginScript attached to a gameobject.

After you have already registered your application and got confirmation from WeChat.

1. Download and import the WeChatPluginAndroidIOS into your project file.
if you already have AndroidManifest in your project. you can exclude that. and add the folling lines instead.
Add in the permissions before the "<application" tag
 
<uses-permission android:name="android.permission.INTERNET" />
    
<uses-permission android:name="android.permission.MODIFY_AUDIO_SETTINGS"/>
    
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE"/>

2. Drag the prefab from "WeChatPluginAndroidIOS/Prefab/WeChatPluginAndroidIOS.prefab" into the scene you desired to share.
3. Add in your AppID from we chat site to the App ID in the editor of the prefab.
4. Configure what you want to share, using the editor of the prefab. Documentation for the configuration is below.
5. Add a reference of the objects WeChatPluginScript in your button's script.
6. call the share. [referenceVariableName].Share();
7. Make sure your project package name is the same as the one you registered in WeChat site.

** you can start Enjoying now if you are only planning to setup IOS.
** for android only 
8. Use the app_signatures.apk included inside the WeChatPluginAndroidIOS/Source.zip, install it on your android device.
9. Install your app in the same android device.
10. Open the app_signatures app. and get the app signature from your app.
11. set the your app signature appropriately in the wechat site.

12. Enjoy! now your we chat share should work properly.

Thats all for setting it up.
Refer to the DemoScene for how to use it.

## Demo Example


There is a step-by-step demo to help you get started with WeChatPluginAndroidIOS. You can open the DemoScene under WeChatPluginAndroidIOS/Demo folder to play with it. In that demo, you can get a basic idea how WeChatPluginAndroid works calling different type of share. You can follow the DemoScript.cs to setup your customized sharing using script. And of course don't forget we have a `WeChatPluginAndroid` prefab in WeChatPluginAndroid/Prefab folder for you to quickly start without any other effort.


## WeChatPluginAndroidIOS Editor configuration definitions.

# App ID - the Application ID you can from We chat site after registering.
# Is Moments - Check if you want to share to the wechat moments timeline. uncheck if you want to share it to a specific friend on we chat.
# Share Type - Which type of sharing do you want to make. following types are available.
-SHARETYPE_IMAGE - when you want to share image.
-SHARETYPE_LINK - when you want to share link.
-SHARETYPE_TEXT - when you want to share text.
-SHARETYPE_MUSIC - when you want to share music.
-SHARETYPE_VIDEO - when you want to share video.

# Image Sharing Type
TYPE_PATH - when you want to share image from users device.
TYPE_URL - when you want to share image from a link.
TYPE_TEXTURE - when you want to share image from a Texture2d.

#Thumb Type - where you want the thumbnail image of your share to come from.
TYPE_PATH - when you want to the thumb from users device.
TYPE_URL - when you want to the thumb from a link.
TYPE_TEXTURE - when you want to the thumb from a Texture2d.

FAQ
#Nothing Happens When I Share, Whats Wrong?

A.If nothing at all happens
*Alot of things may cause sharing to fail.
*Check if your libammsdk.jar is in Plugins/Android Folder and properly configured.
*Check if your WeChatPlugin.jar is in Plugins/Android Folder and properly configured.
*An Texture that you are using might have not been set as Read/Write Enabled. or wrong format. (refer to the texture from the demo)
*We Chat is not installed on your device.

B.If The We chat App Opens And Then Closed
* IMPORTANT: your application might have not been approved by we chat yet. you need to go through the approval process from their site.
* you have not set the package name for your app in the we chat site properly
* you have not set the application signature in the we chat site properly. use the app_signature.apk included in the source to find out your application signature. (this value might change if you sign the app with another keystore when releasing it to google playstore remember to change it).
* you have not set the appID properly in We chat plugin script (you can get your app id in we chat site)

C. Errors when you sharing Image or thumb using Texture2D.
* Please set there Texture import type of the Image to Advanced ensure the Read/Write Enabled and the Format to RGBA 32 bit.

## Upgrade Guide
If you are upgrading from the WeChatAndroidPlugin. 
Please delete the following files to ensure errorless upgrade:
* The Folder WeChatPluginAndroid and all its contents.
* The Folder Editor/WeChatPluginAndroid and all its contents.
