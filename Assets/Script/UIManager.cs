using UnityEngine;

public class UIManager : MonoBehaviour
{
		[SerializeField]
		private WeChatPluginScript m_weChatShareObject;

		private void Start ()
		{
		}

		private void Update ()
		{
		}

		public void OnClick ()
		{
				Debug.Log ("666666");
				m_weChatShareObject.Share ();
		}
}