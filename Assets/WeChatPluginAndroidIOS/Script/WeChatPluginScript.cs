using UnityEngine;
using System.Collections;

public class WeChatPluginScript : MonoBehaviour
{

    public enum ShareType
    {
        SHARETYPE_IMAGE,
        SHARETYPE_LINK,
        SHARETYPE_TEXT,
        SHARETYPE_MUSIC,
        SHARETYPE_VIDEO
    }

    public enum imageUploadType
    {
        TYPE_PATH,
        TYPE_TEXTURE,
        TYPE_URL
    }

    public enum mediaUploadType
    {
        TYPE_URL,
        TYPE_LOWBANDURL
    }

    public string m_AppID;
    public ShareType m_shareType;

    public bool m_isMoments;
    public string m_desc;

    public imageUploadType m_thumbType;
    public string m_thumbString;
    public Texture2D m_thumbImage;

    public string m_title;

    public imageUploadType m_contentImageType;
    public mediaUploadType m_contentMediaType;
    public string m_contentString;
    public Texture2D m_contentImage;

    public bool m_isWeChatInstalled;
    public bool m_isMomentsSupported;


    // Use this for initialization
    void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        WeChatPluginAndroid.Init(m_AppID);
        m_isWeChatInstalled = WeChatPluginAndroid.IsWeChatInstalled();
        m_isMomentsSupported = WeChatPluginAndroid.IsMomentsSupported();
#endif
#if UNITY_IOS && !UNITY_EDITOR
		WechatPluginIOS.Init(m_AppID);
		m_isWeChatInstalled = WechatPluginIOS._isWeChatInstalled();
		//m_isMomentsSupported = WeChatPluginAndroid.IsMomentsSupported();
#endif
    }

    public void Share()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        WeChatPluginAndroid.SetIsMoments(m_isMoments);
        switch (m_thumbType)
        {
            case imageUploadType.TYPE_PATH:
                WeChatPluginAndroid.SetThumbImageWithPath(m_thumbString);
                break;
            case imageUploadType.TYPE_URL:
                WeChatPluginAndroid.SetThumbImageWithURL(m_thumbString);
                break;
            case imageUploadType.TYPE_TEXTURE:
                WeChatPluginAndroid.SetThumbImageWithTexture(m_thumbImage);
                break;
        }

        switch (m_shareType)
        {
            case ShareType.SHARETYPE_TEXT:
                WeChatPluginAndroid.ShareText(m_contentString, m_desc);
                break;
            case ShareType.SHARETYPE_LINK:
                WeChatPluginAndroid.ShareLink(m_contentString, m_title, m_desc);
                break;
            case ShareType.SHARETYPE_IMAGE:
                switch (m_contentImageType)
                {
                    case imageUploadType.TYPE_PATH:
                        WeChatPluginAndroid.ShareImageWithPath(m_contentString, m_title, m_desc);
                        break;
                    case imageUploadType.TYPE_URL:
                        WeChatPluginAndroid.ShareImageWithURL(m_contentString, m_title, m_desc);
                        break;
                    case imageUploadType.TYPE_TEXTURE:
                        WeChatPluginAndroid.ShareImageWithTexture(m_contentImage, m_title, m_desc);
                        break;
                }
                break;
            case ShareType.SHARETYPE_MUSIC:
                switch (m_contentMediaType)
                {
                    case mediaUploadType.TYPE_URL:
                        WeChatPluginAndroid.ShareMusicWithLink(m_contentString, m_title, m_desc);
                        break;
                    case mediaUploadType.TYPE_LOWBANDURL:
                        WeChatPluginAndroid.ShareMusicWithLowBandUrl(m_contentString, m_title, m_desc);
                        break;
                }
                break;
            case ShareType.SHARETYPE_VIDEO:
                switch (m_contentMediaType)
                {
                    case mediaUploadType.TYPE_URL:
                        WeChatPluginAndroid.ShareVideoWithLink(m_contentString, m_title, m_desc);
                        break;
                    case mediaUploadType.TYPE_LOWBANDURL:
                        WeChatPluginAndroid.ShareVideoWithLowBandURL(m_contentString, m_title, m_desc);
                        break;
                }
                break;
        }
#endif
#if UNITY_IOS && !UNITY_EDITOR

		WechatPluginIOS._SetIsMoments(m_isMoments);
		switch (m_thumbType)
		{
		case imageUploadType.TYPE_PATH:
			WechatPluginIOS._SetThumbImageWithPath(m_thumbString);
			break;
		case imageUploadType.TYPE_URL:
			WechatPluginIOS._SetThumbImageWithUrl(m_thumbString);
			break;
		case imageUploadType.TYPE_TEXTURE:
			WechatPluginIOS._SetThumbImageWithTexture(m_thumbImage);
			break;
		}
		
		switch (m_shareType)
		{
		case ShareType.SHARETYPE_TEXT:
			WechatPluginIOS._ShareText(m_contentString, m_desc);
			break;
		case ShareType.SHARETYPE_LINK:
			WechatPluginIOS._ShareLink(m_contentString, m_title, m_desc);
			break;
		case ShareType.SHARETYPE_IMAGE:
			switch (m_contentImageType)
			{
			case imageUploadType.TYPE_PATH:
				WechatPluginIOS._ShareImageWithPath(m_contentString, m_title, m_desc);
				break;
			case imageUploadType.TYPE_URL:
				WechatPluginIOS._ShareImageWithURL(m_contentString, m_title, m_desc);
				break;
			case imageUploadType.TYPE_TEXTURE:
				WechatPluginIOS._ShareImageWithTexture(m_contentImage, m_title, m_desc);
				break;
			}
			break;
		case ShareType.SHARETYPE_MUSIC:
			switch (m_contentMediaType)
			{
			case mediaUploadType.TYPE_URL:
				WechatPluginIOS._ShareMusicWithLink(m_contentString, m_title, m_desc);
				break;
			case mediaUploadType.TYPE_LOWBANDURL:
				WechatPluginIOS._ShareMusicWithLowBandUrl(m_contentString, m_title, m_desc);
				break;
			}
			break;
		case ShareType.SHARETYPE_VIDEO:
			switch (m_contentMediaType)
			{
			case mediaUploadType.TYPE_URL:
				WechatPluginIOS._ShareVideoWithLink(m_contentString, m_title, m_desc);
				break;
			case mediaUploadType.TYPE_LOWBANDURL:
				WechatPluginIOS._ShareVideoWithLowBandURL(m_contentString, m_title, m_desc);
				break;
			}
			break;
		}
#endif
    }
}

