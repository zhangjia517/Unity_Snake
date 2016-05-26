using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DemoScript : MonoBehaviour {

    [SerializeField]
    WeChatPluginScript m_weChatShareObject;
    [SerializeField]
    Toggle toggle;
    [SerializeField]
    Texture2D shareImage;
    [SerializeField]
    Text weChatInstalled;

    void Start()
    {
        weChatInstalled.text = weChatInstalled.text + m_weChatShareObject.m_isWeChatInstalled;
        if(!m_weChatShareObject.m_isMomentsSupported)
        {
            m_weChatShareObject.m_isMoments = false;
            toggle.gameObject.SetActive(false);
        }
    }

    
    private string imageUrl = "http://nebula.wsimg.com/154e3bd987c15826434fe99e26e4d5e9?AccessKeyId=648D1DCC51B695AEB0E5&disposition=0&alloworigin=1";

    void setupThumbAndMoments()
    {
        m_weChatShareObject.m_isMoments = toggle.isOn;
        m_weChatShareObject.m_thumbType = WeChatPluginScript.imageUploadType.TYPE_TEXTURE;
        m_weChatShareObject.m_thumbImage = shareImage;
        m_weChatShareObject.m_title = "test title";
        m_weChatShareObject.m_desc = "test desc";
    }

    public void ShareLink()
    {
        setupThumbAndMoments();
        m_weChatShareObject.m_shareType = WeChatPluginScript.ShareType.SHARETYPE_LINK;
        m_weChatShareObject.m_contentString = "http://www.google.com/";
        m_weChatShareObject.Share();
    }

    public void ShareImageUrl()
    {
        setupThumbAndMoments();
        m_weChatShareObject.m_shareType = WeChatPluginScript.ShareType.SHARETYPE_IMAGE;
        m_weChatShareObject.m_contentImageType = WeChatPluginScript.imageUploadType.TYPE_URL;
        m_weChatShareObject.m_contentString = imageUrl;
        m_weChatShareObject.Share();
    }

    public void ShareImagePath()
    {
        setupThumbAndMoments();
        m_weChatShareObject.m_shareType = WeChatPluginScript.ShareType.SHARETYPE_IMAGE;
        m_weChatShareObject.m_contentImageType = WeChatPluginScript.imageUploadType.TYPE_PATH;
        m_weChatShareObject.m_contentString = "/test.png";
        m_weChatShareObject.Share();
    }

    public void ShareImageTexture()
    {
        setupThumbAndMoments();
        m_weChatShareObject.m_shareType = WeChatPluginScript.ShareType.SHARETYPE_IMAGE;
        m_weChatShareObject.m_contentImageType = WeChatPluginScript.imageUploadType.TYPE_TEXTURE;
        m_weChatShareObject.m_contentImage = shareImage;
        m_weChatShareObject.Share();
    }

    public void ShareText()
    {
        setupThumbAndMoments();
        m_weChatShareObject.m_shareType = WeChatPluginScript.ShareType.SHARETYPE_TEXT;
        m_weChatShareObject.m_contentString = "test text";
        m_weChatShareObject.Share();
    }

    public void ShareMusicUrl()
    {
        setupThumbAndMoments();
        m_weChatShareObject.m_shareType = WeChatPluginScript.ShareType.SHARETYPE_MUSIC;
        m_weChatShareObject.m_contentMediaType = WeChatPluginScript.mediaUploadType.TYPE_URL;
        m_weChatShareObject.m_contentString = "http://staff2.ustc.edu.cn/~wdw/softdown/index.asp/0042515_05.ANDY.mp3";
        m_weChatShareObject.Share();
    }

    public void ShareMusicLowBandUrl()
    {
        setupThumbAndMoments();
        m_weChatShareObject.m_shareType = WeChatPluginScript.ShareType.SHARETYPE_MUSIC;
        m_weChatShareObject.m_contentMediaType = WeChatPluginScript.mediaUploadType.TYPE_LOWBANDURL;
        m_weChatShareObject.m_contentString = "http://www.qq.com";
        m_weChatShareObject.Share();
    }

    public void ShareVideoUrl()
    {
        setupThumbAndMoments();
        m_weChatShareObject.m_shareType = WeChatPluginScript.ShareType.SHARETYPE_VIDEO;
        m_weChatShareObject.m_contentMediaType = WeChatPluginScript.mediaUploadType.TYPE_URL;
        m_weChatShareObject.m_contentString = "http://www.baidu.com";
        m_weChatShareObject.Share();
    }

    public void ShareVideoLowBandUrl()
    {
        setupThumbAndMoments();
        m_weChatShareObject.m_shareType = WeChatPluginScript.ShareType.SHARETYPE_VIDEO;
        m_weChatShareObject.m_contentMediaType = WeChatPluginScript.mediaUploadType.TYPE_LOWBANDURL;
        m_weChatShareObject.m_contentString = "http://www.qq.com";
        m_weChatShareObject.Share();
    }
}
