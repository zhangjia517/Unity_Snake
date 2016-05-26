using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeChatPluginScript)), CanEditMultipleObjects]
public class WeChatPluginScriptEditor : Editor
{
    public SerializedProperty
         appID_Prop,
         shareType_Prop,
         isMoments_Prop,
         desc_Prop,
         thumbType_Prop,
         thumbString_Prop,
         thumbImage_Prop,
         title_Prop,
         contentString_Prop,
         contentImage_Prop,
         contentImageType_Prop,
         contentMediaType_Prop;

    void OnEnable()
    {
        // Setup the SerializedProperties
        appID_Prop = serializedObject.FindProperty("m_AppID");
        shareType_Prop = serializedObject.FindProperty("m_shareType");

        isMoments_Prop = serializedObject.FindProperty("m_isMoments");
        desc_Prop = serializedObject.FindProperty("m_desc");
        thumbType_Prop = serializedObject.FindProperty("m_thumbType");
        thumbString_Prop = serializedObject.FindProperty("m_thumbString");
        thumbImage_Prop = serializedObject.FindProperty("m_thumbImage");
        title_Prop = serializedObject.FindProperty("m_title");
        contentString_Prop = serializedObject.FindProperty("m_contentString");
        contentImage_Prop = serializedObject.FindProperty("m_contentImage");
        contentImageType_Prop = serializedObject.FindProperty("m_contentImageType");
        contentMediaType_Prop = serializedObject.FindProperty("m_contentMediaType");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(appID_Prop);
        EditorGUILayout.PropertyField(isMoments_Prop);
        EditorGUILayout.PropertyField(shareType_Prop);
        WeChatPluginScript.ShareType st = (WeChatPluginScript.ShareType)shareType_Prop.enumValueIndex;
        switch (st)
        {
            case WeChatPluginScript.ShareType.SHARETYPE_TEXT:
                EditorGUILayout.PropertyField(contentString_Prop, new GUIContent("Text"));
                break;
            case WeChatPluginScript.ShareType.SHARETYPE_LINK:
                EditorGUILayout.PropertyField(contentString_Prop, new GUIContent("Url"));
                EditorGUILayout.PropertyField(title_Prop);
                break;
            case WeChatPluginScript.ShareType.SHARETYPE_IMAGE:
                EditorGUILayout.PropertyField(contentImageType_Prop, new GUIContent("Image Sharing Type"));
                WeChatPluginScript.imageUploadType imageType = (WeChatPluginScript.imageUploadType)contentImageType_Prop.enumValueIndex;
                switch (imageType)
                {
                    case WeChatPluginScript.imageUploadType.TYPE_TEXTURE:
                        EditorGUILayout.PropertyField(contentImage_Prop, new GUIContent("Image Texture"));
                        break;
                    case WeChatPluginScript.imageUploadType.TYPE_URL:
                        EditorGUILayout.PropertyField(contentString_Prop, new GUIContent("Image Url"));
                        break;
                    case WeChatPluginScript.imageUploadType.TYPE_PATH:
                        EditorGUILayout.PropertyField(contentString_Prop, new GUIContent("Image Path"));
                        break;
                }
                EditorGUILayout.PropertyField(title_Prop);
                break;
            case WeChatPluginScript.ShareType.SHARETYPE_MUSIC:
                {
                    EditorGUILayout.PropertyField(contentMediaType_Prop, new GUIContent("Music Sharing Type"));
                    WeChatPluginScript.mediaUploadType mediaType = (WeChatPluginScript.mediaUploadType)contentMediaType_Prop.enumValueIndex;
                    switch (mediaType)
                    {
                        case WeChatPluginScript.mediaUploadType.TYPE_URL:
                            EditorGUILayout.PropertyField(contentString_Prop, new GUIContent("Music Url"));
                            break;
                        case WeChatPluginScript.mediaUploadType.TYPE_LOWBANDURL:
                            EditorGUILayout.PropertyField(contentString_Prop, new GUIContent("Music Low Band Url"));
                            break;
                    }
                    EditorGUILayout.PropertyField(title_Prop);
                }
                break;
            case WeChatPluginScript.ShareType.SHARETYPE_VIDEO:
                {
                    EditorGUILayout.PropertyField(contentMediaType_Prop, new GUIContent("Video Sharing Type"));
                    WeChatPluginScript.mediaUploadType mediaType = (WeChatPluginScript.mediaUploadType)contentMediaType_Prop.enumValueIndex;
                    switch (mediaType)
                    {
                        case WeChatPluginScript.mediaUploadType.TYPE_URL:
                            EditorGUILayout.PropertyField(contentString_Prop, new GUIContent("Video Url"));
                            break;
                        case WeChatPluginScript.mediaUploadType.TYPE_LOWBANDURL:
                            EditorGUILayout.PropertyField(contentString_Prop, new GUIContent("Video Low Band Url"));
                            break;
                    }
                    EditorGUILayout.PropertyField(title_Prop);
                }
                break;
        }
        EditorGUILayout.PropertyField(desc_Prop);

        EditorGUILayout.PropertyField(thumbType_Prop);
        WeChatPluginScript.imageUploadType thumbType = (WeChatPluginScript.imageUploadType)thumbType_Prop.enumValueIndex;
        switch (thumbType)
        {
            case WeChatPluginScript.imageUploadType.TYPE_TEXTURE:
                EditorGUILayout.PropertyField(thumbImage_Prop, new GUIContent("Thumb Texture"));
                break;
            case WeChatPluginScript.imageUploadType.TYPE_URL:
                EditorGUILayout.PropertyField(thumbString_Prop, new GUIContent("Thumb Url"));
                break;
            case WeChatPluginScript.imageUploadType.TYPE_PATH:
                EditorGUILayout.PropertyField(thumbString_Prop, new GUIContent("Thumb Path"));
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}