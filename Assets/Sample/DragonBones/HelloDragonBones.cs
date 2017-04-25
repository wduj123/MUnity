// ========================================================
// 描 述：NewBehaviourScript.cs 
// 作 者：郑贤春 
// 时 间：2017/01/02 21:25:19 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System.Collections;
using DragonBones;

public class HelloDragonBones : MonoBehaviour
{
    void Start()
    {
        // Load data.
        UnityFactory.factory.LoadDragonBonesData("DragonBone/NewProject_ske"); // DragonBones file path (without suffix)
        UnityFactory.factory.LoadTextureAtlasData("DragonBone/NewProject_tex"); //Texture atlas file path (without suffix) 
        // Create armature.
        var armatureComponent = UnityFactory.factory.BuildArmatureComponent("Armature"); // Input armature name
                                                                                      // Play animation.
        armatureComponent.animation.Play("Run");

        // Change armatureposition.
        armatureComponent.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }
}