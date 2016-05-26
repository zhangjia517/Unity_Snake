using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

public class WechatPluginIOS : MonoBehaviour {

	public static void Init(string app_id)
	{
		InitWeChat (app_id);
	}

	private static WechatPluginIOS instance;

	[DllImport ("__Internal")]
	private static extern void SetIsMoments (bool value);

	[DllImport ("__Internal")]
	private static extern void SetThumbImageWithUrl (string url);

	[DllImport ("__Internal")]
	private static extern void SetThumbImageWithPath (string url);

	[DllImport ("__Internal")]
	private static extern void SetThumbImageWithTexture ();

	[DllImport ("__Internal")]
	private static extern void ShareImageWithTexture (string title, string description);

	[DllImport ("__Internal")]
	private static extern void ShareImageWithPath (string path, string title, string description);

	[DllImport ("__Internal")]
	private static extern void ShareImageWithURL (string path, string title, string description);

	[DllImport ("__Internal")]
	private static extern void ShareText (string text, string description);

	[DllImport ("__Internal")]
	private static extern void ShareLink (string url , string title, string description);

	[DllImport ("__Internal")]
	private static extern void ShareMusicWithLink (string url , string title, string description);

	[DllImport ("__Internal")]
	private static extern void ShareMusicWithLowBandUrl (string url , string title, string description);

	[DllImport ("__Internal")]
	private static extern void ShareVideoWithLink (string url , string title, string description);
	
	[DllImport ("__Internal")]
	private static extern void ShareVideoWithLowBandURL (string url , string title, string description);

	[DllImport ("__Internal")]
	private static extern bool isWeChatInstalled ();

	[DllImport ("__Internal")]
	private static extern void InitWeChat (string app_id);

	public static void _SetIsMoments(bool value)
	{
		SetIsMoments (value);
	}

	public static void _SetThumbImageWithUrl(string url)
	{
		SetThumbImageWithUrl (url);
	}

	public static void _SetThumbImageWithPath(string url)
	{
		SetThumbImageWithPath (url);
	}

	public static void _SetThumbImageWithTexture(Texture2D imageData)
	{
		byte[] imageArray = imageData.EncodeToPNG();
		File.WriteAllBytes (Application.persistentDataPath + "/wechatshare.png", imageArray);
		SetThumbImageWithTexture ();
	}

	public static void _ShareImageWithTexture(Texture2D image, string title, string description)
	{
		byte[] imageArray = image.EncodeToPNG();
		File.WriteAllBytes (Application.persistentDataPath + "/wechatshare.png", imageArray);
		ShareImageWithTexture (title, description);
	}

	public static void _ShareImageWithPath(string url , string title, string description)
	{
		ShareImageWithPath (url, title, description);
	}

	public static void _ShareImageWithURL(string url , string title, string description)
	{
		ShareImageWithURL (url, title, description);
	}

	public static void _ShareText(string text, string description)
	{
		ShareText (text, description);
	}

	public static void _ShareLink(string url , string title, string description)
	{
		ShareLink (url, title, description);
	}

	public static void _ShareMusicWithLink(string url , string title, string description)
	{
		ShareMusicWithLink (url, title, description);
	}

	public static void _ShareMusicWithLowBandUrl(string url , string title, string description)
	{
		ShareMusicWithLowBandUrl (url, title, description);
	}

	public static void _ShareVideoWithLink(string url , string title, string description)
	{
		ShareImageWithURL (url, title, description);
	}

	public static void _ShareVideoWithLowBandURL(string url , string title, string description)
	{
		ShareImageWithURL (url, title, description);
	}

	public static bool _isWeChatInstalled()
	{
		bool isInstalled = isWeChatInstalled ();
		return isInstalled;
	}

	public static void _InitWeChat(string app_id)
	{
		InitWeChat (app_id);
	}
}
