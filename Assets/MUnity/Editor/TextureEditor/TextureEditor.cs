using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using LitJson;

namespace MUnity.MEditor
{
    public class TextureEditor : MonoBehaviour
    {
        public static void TrimTexture()
        {
            if(Selection.activeObject == null || !(Selection.activeObject is TextAsset))
            {
                EditorUtility.DisplayDialog("提示", "请右键选择贴图对应的Json文件执行操作。", "确定");
                return;
            }
            TextAsset jsonAsset = (TextAsset)Selection.activeObject;
            string jsonpath = AssetDatabase.GetAssetPath(jsonAsset);
            if(!".json".Equals(jsonpath.Extention()))
            {
                EditorUtility.DisplayDialog("提示", "请右键选择贴图对应的Json文件执行操作。", "确定");
                return;
            }
            SpriteJsonData jsonMeta = new SpriteJsonData(jsonAsset.text);
            string texturePath = jsonpath.Replace (".json", jsonMeta.imageExtention);
			AssetImporter jsonImporter = AssetImporter.GetAtPath (jsonpath);
			TextureImporter textureImporter = (TextureImporter)AssetImporter.GetAtPath (texturePath);
            List<SpriteMetaData> oldSheet = new List<SpriteMetaData>(textureImporter.spritesheet);
            SpriteMetaData[] newSheet = jsonMeta.spritesheet;
            int count = jsonMeta.count;
            string info = "";
            float progress = 0;
            for (int i = 0; i < count; i++)
            {
                SpriteMetaData olds = oldSheet.Find(x => x.name == newSheet[i].name);
                if (!string.IsNullOrEmpty(olds.name)) newSheet[i].border = olds.border;
                info = "切图中" + (int)((float)i / count * 100) + "%";
                progress = (float)i / count;
                EditorUtility.DisplayProgressBar("正在切图", info, progress);
            }
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.spriteImportMode = SpriteImportMode.Multiple;
            textureImporter.spritePackingTag = "tag";
            textureImporter.spritesheet = newSheet;
            textureImporter.linearTexture = false;
			textureImporter.filterMode = FilterMode.Bilinear;
			textureImporter.maxTextureSize = 1024;
            textureImporter.textureFormat = TextureImporterFormat.AutomaticTruecolor;
			
            EditorUtility.ClearProgressBar();
            AssetDatabase.ImportAsset(texturePath, ImportAssetOptions.ForceUpdate);
        }


        private class SpriteJsonData
        {
            private TextureJsonStruct m_jsonStruct;

            private SpriteMetaData[] m_spritesheet;
            public SpriteMetaData[] spritesheet
            {
                get
                {
                    if(this.m_spritesheet == null)
                    {
                        List<SpriteMetaData> metas = new List<SpriteMetaData>();
                        foreach (KeyValuePair<string, JsonFrame> pair in this.m_jsonStruct.frames)
                        {
                            SpriteMetaData meta = new SpriteMetaData();
                            meta.alignment = (int)SpriteAlignment.Center;
                            meta.border = Vector4.zero;
                            meta.name = pair.Key.NameWithoutExtention();
                            meta.pivot = Vector2.one * 0.5f;
                            meta.rect = ToReal(pair.Value.frame.rect);
                            metas.Add(meta);
                        }
                        this.m_spritesheet = metas.ToArray();
                    }
                    return this.m_spritesheet;
                }
            }

            public int count
            {
                get{return spritesheet.Length;}
            }

            public string imageExtention
            {
                get{
                    string image = this.m_jsonStruct.meta.image;
                    return image.Extention();
                }
            }

            private SpriteJsonData(){}

            public SpriteJsonData(string jsonData)
            {
                 this.m_jsonStruct = JsonMapper.ToObject<TextureJsonStruct>(new JsonReader(jsonData));
            }

            private Rect ToReal(Rect rect)
            {
                Vector2 size = this.m_jsonStruct.meta.size.vector2;
                return new Rect(rect.x, size.y - rect.y - rect.height, rect.width, rect.height);
            }
            
            [System.Serializable]
            public class TextureJsonStruct
            {
                public Dictionary<string, JsonFrame> frames;
                public JsonMeta meta;
            }

            [System.Serializable]
            public class JsonFrame
            {
                public JsonRect frame;
                public bool rotated;
                public bool trimmed;
                public JsonRect spriteSourceSize;
                public JsonVector2 sourceSize;
            }

            [System.Serializable]
            public class JsonMeta
            {
                public string app;
                public string version;
                public string image;
                public string format;
                public JsonVector2 size;
                public string scale;
                public string smartupdate;
            }

            [System.Serializable]
            public struct JsonRect
            {
                public Rect rect
                {
                    get{return new Rect(x, y, w, h);}
                }
                public float x;
                public float y;
                public float w;
                public float h;
            }

            [System.Serializable]
            public struct JsonVector2
            {
                public Vector2 vector2
                {
                    get{return new Vector2(w, h);}
                }
                public float w;
                public float h;
            }
        }
    }
}

