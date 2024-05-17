using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.Android;
//using UnityEditor.iOS;
using System.Linq;

public class IconToolKit : EditorWindow
{

	public int index = 0;

	public Object roundedRectIcon;
	public Object roundIcon;
	public Object squareIcon;

	[MenuItem("Commandoo/Icon Toolkit")]
	static void Init() {
		EditorWindow window = GetWindow(typeof(IconToolKit));
		window.titleContent = new GUIContent("Icon Toolkit");
		window.Show();
	}

	private void OnGUI() {
		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Rounded Rect Icon:");
		roundedRectIcon = EditorGUILayout.ObjectField(roundedRectIcon, typeof(Texture2D), false, GUILayout.Width(64), GUILayout.Height(64));
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Round Icon:");
		roundIcon = EditorGUILayout.ObjectField(roundIcon, typeof(Texture2D), false, GUILayout.Width(64), GUILayout.Height(64));
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Square Icon(iOS + Adaptive):");
		squareIcon = EditorGUILayout.ObjectField(squareIcon, typeof(Texture2D), false, GUILayout.Width(64), GUILayout.Height(64));
		EditorGUILayout.EndHorizontal();
		if (GUILayout.Button("Apply")) {
			if (roundedRectIcon != null) {
				CorrectImportIconAsset(roundedRectIcon);
				SetIconsForTargets(BuildTargetGroup.Android, AndroidPlatformIconKind.Legacy, roundedRectIcon, false);
			} else {
				Debug.LogError("The rounded Rect icon for Android is missing");
			}
			if (roundIcon != null) {
				CorrectImportIconAsset(roundIcon);
				SetIconsForTargets(BuildTargetGroup.Android, AndroidPlatformIconKind.Round, roundIcon, false);
			} else {
				Debug.LogError("The round icon for Android is missing");
			}
			if (squareIcon != null) {
				CorrectImportIconAsset(squareIcon);
				//SetIconsForTargets(BuildTargetGroup.iOS, iOSPlatformIconKind.Application, squareIcon, true);
				//SetIconsForTargets(BuildTargetGroup.iOS, iOSPlatformIconKind.Spotlight, squareIcon, true);
				//SetIconsForTargets(BuildTargetGroup.iOS, iOSPlatformIconKind.Settings, squareIcon, true);
				//SetIconsForTargets(BuildTargetGroup.iOS, iOSPlatformIconKind.Notification, squareIcon, true);
				//SetIconsForTargets(BuildTargetGroup.iOS, iOSPlatformIconKind.Marketing, squareIcon, true);
				SetIconsForTargets(BuildTargetGroup.Android, AndroidPlatformIconKind.Adaptive, squareIcon, false);
			} else {
				Debug.LogError("The square icon for iOS is missing");
			}			
			ShowNotification(new GUIContent("Apply icons success, please commit your changes"), 4);
		}
		EditorGUILayout.EndVertical();
	}

	private void CorrectImportIconAsset(Object asset) {
		string path = AssetDatabase.GetAssetPath(asset);
		TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(path);
		importer.isReadable = true;
		importer.alphaIsTransparency = true;
		importer.textureCompression = TextureImporterCompression.Uncompressed;
		importer.textureType = TextureImporterType.Sprite;
		AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
	}

	private void SetIconsForTargets(BuildTargetGroup target, PlatformIconKind kind, Object iconAsset, bool ignoreKind) {
		PlatformIcon[] platformIcons = PlayerSettings.GetPlatformIcons(target, kind);
		string parentDir = PreparePathForIcons(target, kind, ignoreKind);
		Object adaptiveForegroundIcon = null;
		if (kind == AndroidPlatformIconKind.Adaptive) {
			adaptiveForegroundIcon = GenerateForegroundAdaptive(iconAsset);
		}
		for (int i = 0; i < platformIcons.Length; i++) {
			PlatformIcon icon = platformIcons[i];
			string path = Path.Combine(parentDir, icon.width.ToString() + ".png");
			if (kind != AndroidPlatformIconKind.Adaptive) {
				GenerateTextureForSize(iconAsset, icon.width, icon.height, path);
				icon.SetTexture((Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D)));
			} else {
				Texture2D iconAssetWithPadding = GenerateBackgroundAdaptiveIcon(iconAsset);
				GenerateTextureForSize(iconAssetWithPadding, icon.width, icon.height, path);
				string foregroundAdaptivePath = Path.Combine(parentDir, icon.width.ToString() + "_foreground.png");
				GenerateTextureForSize(adaptiveForegroundIcon, icon.width, icon.height, foregroundAdaptivePath);
				Texture2D[] iconTextures = new Texture2D[] {
					(Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D)),
					(Texture2D)AssetDatabase.LoadAssetAtPath(foregroundAdaptivePath, typeof(Texture2D)),
				};
				icon.SetTextures(iconTextures);
			}
		}
		PlayerSettings.SetPlatformIcons(target, kind, platformIcons);
	}

	private string PreparePathForIcons(BuildTargetGroup target, PlatformIconKind kind, bool ignoreKind) {
		string rootDir = "Assets/CMDGameIcons";
		if (!AssetDatabase.IsValidFolder("Assets/CMDGameIcons")) {
			AssetDatabase.CreateFolder("Assets", "CMDGameIcons");
		}
		string targetDir = Path.Combine(rootDir, target.ToString());
		if (!AssetDatabase.IsValidFolder(targetDir)) {
			AssetDatabase.CreateFolder(rootDir, target.ToString());
		}
		if (ignoreKind) {
			return targetDir;
		}
		string kindDir = Path.Combine(targetDir, kind.ToString());
		if (!AssetDatabase.IsValidFolder(kindDir)){
			AssetDatabase.CreateFolder(targetDir, kind.ToString());
		}
		return kindDir;
	}

	private void GenerateTextureForSize(Object src, int width, int height, string path) {
		Texture2D tex = (Texture2D)src;
		Texture2D clone = GPUTextureScaler.Scaled(tex, width, height, FilterMode.Bilinear);
		File.WriteAllBytes(path, clone.EncodeToPNG());
		AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
		TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(path);
		importer.textureCompression = TextureImporterCompression.Uncompressed;
		importer.textureType = TextureImporterType.Sprite;
		AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
		//AssetDatabase.CreateAsset(src, "Assets/CMDGameIcons/temp");
		DestroyImmediate(clone);
	}

	private Texture2D GenerateBackgroundAdaptiveIcon(Object src) {
		Texture2D tex = (Texture2D)src;
		Texture2D ret = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
		int innerWidth = (int)(tex.width * 0.67f);
		int innerHeight = (int)(tex.height * 0.67f);
		Color transparentColor = new Color(0, 0, 0, 0);
		Color[] pixels = Enumerable.Repeat(transparentColor, ret.width * ret.height).ToArray();
		ret.SetPixels(pixels);
		Texture2D innerTex = GPUTextureScaler.Scaled(tex, innerWidth, innerHeight);
		ret.SetPixels((ret.width - innerTex.width) / 2, (ret.height - innerTex.height) / 2, innerTex.width, innerTex.height, innerTex.GetPixels());
		ret.Apply();
		return ret;
	}

	private Texture2D GenerateForegroundAdaptive(Object src) {
		Texture2D tex = (Texture2D)src;
		Texture2D ret = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
		Color transparentColor = new Color(0, 0, 0, 0);
		for (int i = 0; i < ret.width; i++) {
			for (int j = 0; j < ret.height; j++) {
				ret.SetPixel(i, j, transparentColor);
			}
		}
		ret.Apply();
		return ret;
	}
}
