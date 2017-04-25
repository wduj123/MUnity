// ========================================================
// 描 述：AssetBundlePacker.cs 
// 作 者：郑贤春 
// 时 间：2017/02/24 15:19:22 
// 版 本：5.4.1f1 
// ========================================================
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace MUnity.Editor.AssetBundle
{
    public class AssetBundlePacker : EditorWindow
    {
        public static void ShowWindow()
        {
            GetWindow<AssetBundlePacker>(true, typeof(AssetBundlePacker).Name);
        }

        public static void PackTogether(UnityEngine.Object[] objs)
        {
            AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

            

            string[] enemyAssets = new string[] {
                "Assets/Resources/Textures/gqxg_back_1.png",
                "Assets/Resources/Textures/image4.png"
            };
            string[] heroAssets = new string[] { "Assets/Resources/Textures/headImage3.png" };
            buildMap[0].assetNames = enemyAssets;
            buildMap[1].assetNames = heroAssets;

            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles("Assets", buildMap, BuildAssetBundleOptions.None, BuildTarget.Android);
            //string[] names = manifest.GetAllAssetBundles();
            AssetDatabase.Refresh();
        }
    }
}