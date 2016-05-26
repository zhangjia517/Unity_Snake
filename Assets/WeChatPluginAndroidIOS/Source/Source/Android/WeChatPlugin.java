package com.yym.wechatplugin;

import android.view.View;
import android.widget.Toast;
import java.io.File;
import java.net.URL;

import com.tencent.mm.sdk.openapi.WXVideoObject;
import com.tencent.mm.sdk.openapi.WXWebpageObject;
import com.tencent.mm.sdk.openapi.WXImageObject;
import com.tencent.mm.sdk.openapi.WXTextObject;
import com.tencent.mm.sdk.openapi.WXMusicObject;
import com.tencent.mm.sdk.openapi.WXMediaMessage;
import com.tencent.mm.sdk.openapi.SendMessageToWX;
import com.tencent.mm.sdk.openapi.IWXAPI;
import com.tencent.mm.sdk.openapi.WXAPIFactory;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.os.Environment;

import android.content.pm.PackageManager.NameNotFoundException;
import android.content.pm.PackageManager;

import com.unity3d.player.UnityPlayer;


public class WeChatPlugin /*extends ActionBarActivity*/ {

    private static final int THUMB_SIZE = 150;

    private static final String SDCARD_ROOT = Environment.getExternalStorageDirectory().getAbsolutePath();

    public IWXAPI api;
    private static WeChatPlugin self = null;
    public static String appID;
    public static int ReqType;

    private static byte[] thumbImageDataArray;

    public static WeChatPlugin instance() {
        if(self == null) {
            self = new WeChatPlugin();
        }
        return self;
    }

    public static void _initWeChat(String appid)
    {
        WeChatPlugin.appID = appid;
        self.api = WXAPIFactory.createWXAPI(UnityPlayer.currentActivity, appid);
    }

    public static byte[] _getImageFromURL(String url)
    {
        try{
            Bitmap bmp = BitmapFactory.decodeStream(new URL(url).openStream());
            Bitmap thumbBmp = Bitmap.createScaledBitmap(bmp, THUMB_SIZE, THUMB_SIZE, true);
            bmp.recycle();
            return Util.bmpToByteArray(thumbBmp, true);
        } catch(Exception e) {
            e.printStackTrace();
        }

        return null;
    }

    public static byte[] _getImageFromPath(String path)
    {
        String formatpath = SDCARD_ROOT + path;
        File file = new File(formatpath);
        if (!file.exists()) {
            String tip = "image not exist";
            Toast.makeText(UnityPlayer.currentActivity, tip + " path = " + formatpath, Toast.LENGTH_LONG).show();
            return null;
        }

        Bitmap bmp = BitmapFactory.decodeFile(path);
        Bitmap thumbBmp = Bitmap.createScaledBitmap(bmp, THUMB_SIZE, THUMB_SIZE, true);
        bmp.recycle();
        return Util.bmpToByteArray(thumbBmp, true);
    }

    public static void _setThumbImageDataURL(String url)
    {
        self._setThumbImageDataImage(_getImageFromURL(url));
    }

    public static void _setThumbImageDataPath(String path)
    {
        self._setThumbImageDataImage(_getImageFromPath(path));
    }

    public static void _setThumbImageDataImage(byte[] thumbImageArray)
    {
        self.thumbImageDataArray = thumbImageArray;
    }

    public static void _setIsMoments(boolean value)
    {
        if(self._isWeChatTimeLineSupported() && value) {
            self.ReqType = SendMessageToWX.Req.WXSceneTimeline;
            return;
        }
        self.ReqType = SendMessageToWX.Req.WXSceneSession;
    }

    public static void _shareImage(final byte[] imageArray, final String title, final String desc)
    {
        Bitmap bitmap = BitmapFactory.decodeByteArray(imageArray, 0, imageArray.length);
        WXImageObject imgObj = new WXImageObject(bitmap);

        WXMediaMessage msg = new WXMediaMessage();
        msg.title = title;
        msg.description = desc;
        msg.mediaObject = imgObj;

        msg.thumbData = self.thumbImageDataArray;

        self.weChatShare(msg, "img");
    }

    public static void _shareImageWithPath(final String path, final String title, final String desc) {
        self._shareImage(self._getImageFromPath(path), title, desc);
    }

    public static void _shareImageWithURL(final String url, final String title, final String desc)
    {
        self._shareImage(self._getImageFromURL(url), title, desc);
    }

    public static void _shareText(final String text, final String desc)
    {
        WXTextObject textObject = new WXTextObject();
        textObject.text = text;

        WXMediaMessage msg = new WXMediaMessage();
        msg.description = desc;
        msg.mediaObject = textObject;
        msg.thumbData = self.thumbImageDataArray;

        self.weChatShare(msg, "text");
    }

    public static void _shareWebPage(final String linkurl, final String title, final String desc)
    {
        WXWebpageObject webpage = new WXWebpageObject();
        webpage.webpageUrl = linkurl;
        WXMediaMessage msg = new WXMediaMessage(webpage);
        msg.title = title;
        msg.description = desc;
        msg.thumbData = self.thumbImageDataArray;

        self.weChatShare(msg,"webpage");
    }

    public static void _shareMusic(final String linkurl, final String title, final String desc)
    {
        WXMusicObject musicObject = new WXMusicObject();
        musicObject.musicUrl = linkurl;
        WXMediaMessage msg = new WXMediaMessage(musicObject);
        msg.title = title;
        msg.description = desc;
        msg.thumbData = self.thumbImageDataArray;

        self.weChatShare(msg,"music");
    }

    public static void _shareMusicLowBand(final String linkurl, final String title, final String desc)
    {
        WXMusicObject musicObject = new WXMusicObject();
        musicObject.musicLowBandUrl = linkurl;
        WXMediaMessage msg = new WXMediaMessage(musicObject);
        msg.title = title;
        msg.description = desc;
        msg.thumbData = self.thumbImageDataArray;

        self.weChatShare(msg,"music");
    }

    public static void _shareVideo(final String linkurl, final String title, final String desc)
    {
        WXVideoObject videoObject = new WXVideoObject();
        videoObject.videoUrl = linkurl;
        WXMediaMessage msg = new WXMediaMessage(videoObject);
        msg.title = title;
        msg.description = desc;
        msg.thumbData = self.thumbImageDataArray;

        self.weChatShare(msg,"video");
    }

    public static void _shareVideoLowBand(final String linkurl, final String title, final String desc)
    {
        WXVideoObject videoObject = new WXVideoObject();
        videoObject.videoLowBandUrl = linkurl;
        WXMediaMessage msg = new WXMediaMessage(videoObject);
        msg.title = title;
        msg.description = desc;
        msg.thumbData = self.thumbImageDataArray;

        self.weChatShare(msg,"video");
    }

    public static void weChatShare(WXMediaMessage msg, String transactionString)
    {
        SendMessageToWX.Req req = new SendMessageToWX.Req();
        req.transaction = self.buildTransaction(transactionString);
        req.message = msg;

        req.scene = self.ReqType;

        self.api.sendReq(req);
    }

    public static boolean _isWeChatInstalled()
    {
        PackageManager pm = UnityPlayer.currentActivity.getApplicationContext().getPackageManager();
        try {
            pm.getPackageInfo("com.tencent.mm", PackageManager.GET_ACTIVITIES);
            return true;
        } catch (NameNotFoundException e) {
            return false;
        }
    }

    private static final int TIMELINE_SUPPORTED_VERSION = 0x21020001;
    public static boolean _isWeChatTimeLineSupported(){
        int wxSdkVersion = self.api.getWXAppSupportAPI();
        return wxSdkVersion >= TIMELINE_SUPPORTED_VERSION;
    }

    private String buildTransaction(final String type) {
        return (type == null) ? String.valueOf(System.currentTimeMillis()) : type + System.currentTimeMillis();
    }


}
