using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

public class WeChatPluginAndroid {
	private static AndroidJavaObject shareItem;

	public static void Init(string wechatID) {
		Debug.Log("Unity Init");
		if (Application.platform == RuntimePlatform.Android) {
            var pluginClass = new AndroidJavaClass("com.yym.wechatplugin.WeChatPlugin");
			shareItem = pluginClass.CallStatic<AndroidJavaObject>("instance");
			shareItem.CallStatic("_initWeChat", wechatID);
		}
	}

	public static void SetThumbImageWithURL(string url) {
		if (Application.platform == RuntimePlatform.Android) {
			shareItem.CallStatic("_setThumbImageDataURL", url);
		}
	}

    public static void SetThumbImageWithPath(string url) {
		if (Application.platform == RuntimePlatform.Android) {
			shareItem.CallStatic("_setThumbImageDataPath", url);
		}
	}

    public static void SetThumbImageWithTexture(Texture2D image) {
		if (Application.platform == RuntimePlatform.Android) {
			shareItem.CallStatic("_setThumbImageDataImage", image.EncodeToPNG());
		}
	}

    public static void SetIsMoments(bool value) {
		if (Application.platform == RuntimePlatform.Android) {
			shareItem.CallStatic("_setIsMoments", value);
		}
	}

    public static void ShareImageWithTexture(Texture2D image, string title, string desc) {
		if (Application.platform == RuntimePlatform.Android) {
			shareItem.CallStatic("_shareImage", image.EncodeToPNG(), title, desc);
		}
	}

    public static void ShareImageWithPath(string path, string title, string desc) {
		if (Application.platform == RuntimePlatform.Android) {
			shareItem.CallStatic("_shareImageWithPath", path, title, desc);
		}
	}

    public static void ShareImageWithURL(string url, string title, string desc) {
		if (Application.platform == RuntimePlatform.Android) {
			shareItem.CallStatic("_shareImageWithURL", url, title, desc);
		}
	}

    public static void ShareText(string text, string desc) {
		if (Application.platform == RuntimePlatform.Android) {
			shareItem.CallStatic("_shareText", text, desc);
		}
	}

    public static void ShareLink(string url, string title, string desc) {
		if (Application.platform == RuntimePlatform.Android) {
			shareItem.CallStatic("_shareWebPage", url, title, desc);
		}
	}

    public static void ShareMusicWithLink(string url, string title, string desc) {
		if (Application.platform == RuntimePlatform.Android) {
			shareItem.CallStatic("_shareMusic", url, title, desc);
		}
	}

    public static void ShareMusicWithLowBandUrl(string url, string title, string desc) {
		if (Application.platform == RuntimePlatform.Android) {
			shareItem.CallStatic("_shareMusicLowBand", url, title, desc);
		}
	}

    public static void ShareVideoWithLink(string url, string title, string desc) {
		if (Application.platform == RuntimePlatform.Android) {
			shareItem.CallStatic("_shareVideo", url, title, desc);
		}
	}

    public static void ShareVideoWithLowBandURL(string url, string title, string desc) {
		if (Application.platform == RuntimePlatform.Android) {
			shareItem.CallStatic("_shareVideoLowBand", url, title, desc);
		}
	}

    public static bool IsWeChatInstalled()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return shareItem.CallStatic<bool>("_isWeChatInstalled");
        }
        return false;
    }

    public static bool IsMomentsSupported()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return shareItem.CallStatic<bool>("_isWeChatTimeLineSupported");
        }
        return false;
    }
}