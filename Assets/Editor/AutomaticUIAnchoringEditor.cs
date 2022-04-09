using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[InitializeOnLoad]
public class AutomaticUIAnchoringEditor : Editor
{
    private static void DisplayAboutDialog()
    {
        string text = "Thank you for using the Automatic UI Anchoring extension for Unity created by Andrew Riseborough."
            + System.Environment.NewLine + System.Environment.NewLine +
            "If you would like to check out my awesome side projects you can find them on my social media accounts at:"
            + System.Environment.NewLine +
            "https://www.unityconnect.com/AndrewCodes200"
            + System.Environment.NewLine +
            "https://www.twitter.com/AndrewCodes200"
            + System.Environment.NewLine +
            "https://www.instagram.com/AndrewCodes200"
            + System.Environment.NewLine + System.Environment.NewLine +
            "I hope you found this tool useful and it saved you some time."
            + System.Environment.NewLine + System.Environment.NewLine +
            "If it did feel free to buy me a coffee if you would like: https://www.paypal.me/AndrewCodes200"
            + System.Environment.NewLine + System.Environment.NewLine +
            "Contact:"
             + System.Environment.NewLine +
            "Email: AndrewCodes200@gmail.com";

        EditorUtility.DisplayDialog("About Automatic UI Anchoring", text, "Ok", string.Empty);
    }

    private static void Anchor(RectTransform rectTransform)
    {
        Slider slider = rectTransform.GetComponentInParent<Slider>();
        Scrollbar scrollbar = rectTransform.GetComponentInParent<Scrollbar>();
        Dropdown dropdown = rectTransform.GetComponentInParent<Dropdown>();
        InputField inputField = rectTransform.GetComponentInParent<InputField>();
        ScrollRect scrollRect = rectTransform.GetComponentInParent<ScrollRect>();
		
		RectTransform parentRectTransform = null;
		if (rectTransform.transform.parent)
			parentRectTransform = rectTransform.transform.parent.GetComponent<RectTransform>();
		
        if (!parentRectTransform)
            return;
        else
        {
			if (rectTransform.GetComponent<ContentSizeFitter>() || rectTransform.transform.parent.GetComponent<LayoutGroup>())
				return;
			else if (slider && (rectTransform.transform == slider.handleRect.parent || rectTransform == slider.handleRect || rectTransform.transform == slider.fillRect.parent || rectTransform == slider.fillRect))
                return;
            else if (scrollbar && (rectTransform.transform == scrollbar.handleRect.parent || rectTransform == scrollbar.handleRect))
                return;
            else if (dropdown && (rectTransform == dropdown.template || rectTransform == dropdown.captionText.rectTransform))
                return;
            else if (inputField && (rectTransform == inputField.textComponent.rectTransform || inputField.placeholder.rectTransform || rectTransform.gameObject.name.Equals("InputField Input Caret")))
                return;
            else if (scrollRect && (rectTransform == scrollRect.viewport || rectTransform == scrollRect.content || scrollbar))
				return;
        }

        Undo.RecordObject(rectTransform, "Anchor UI Object");
        Rect parentRect = parentRectTransform.rect;
        rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x + (rectTransform.offsetMin.x / parentRect.width), rectTransform.anchorMin.y + (rectTransform.offsetMin.y / parentRect.height));
        rectTransform.anchorMax = new Vector2(rectTransform.anchorMax.x + (rectTransform.offsetMax.x / parentRect.width), rectTransform.anchorMax.y + (rectTransform.offsetMax.y / parentRect.height));
		rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
    }

    [MenuItem("Tools/Automatic UI Anchoring/Anchor All UI Objects")]
    private static void AutomaticallyAnchorAllUIObjects()
    {
		
        RectTransform[] rectTransforms = Resources.FindObjectsOfTypeAll(typeof(RectTransform)) as RectTransform[];
        for (int i = 0; i < rectTransforms.Length; i++)
		{
			Object prefab = null;
			try { prefab = PrefabUtility.GetCorrespondingObjectFromSource(rectTransforms[i].gameObject); } catch { }
			
			if (prefab)
			{
				string prefabPath = AssetDatabase.GetAssetPath(prefab);

				try { PrefabUtility.UnpackPrefabInstance(rectTransforms[i].gameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction); } catch { }

				Anchor(rectTransforms[i]);

				try { CreateNewPrefab(rectTransforms[i].gameObject, prefabPath); } catch { }
			}
			else
			{
				try { Anchor(rectTransforms[i]); } catch { }
			}
		}
    }

    [MenuItem("Tools/Automatic UI Anchoring/Anchor Selected UI Objects")]
    private static void AutomaticallyAnchorSelectedUIObjects()
    {
        for (int i = 0; i < Selection.gameObjects.Length; i++)
        {
            RectTransform rectTransform = Selection.gameObjects[i].GetComponent<RectTransform>();
            if (rectTransform)
                Anchor(rectTransform);
        }
    }

    [MenuItem("Tools/Automatic UI Anchoring/About")]
    private static void About()
    {
        DisplayAboutDialog();
    }
	
    private static void CreateNewPrefab(GameObject obj, string localPath)
    {
        Object prefab = PrefabUtility.CreatePrefab(localPath, obj);
        PrefabUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ConnectToPrefab);
    }
}
