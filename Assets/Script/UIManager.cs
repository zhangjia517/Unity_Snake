using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private WeChatPluginScript m_weChatShareObject;

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void OnClick()
    {
        m_weChatShareObject.Share();
    }
}