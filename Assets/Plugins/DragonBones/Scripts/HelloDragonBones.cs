﻿using UnityEngine;
using DragonBones;

/**
 * How to use
 * 1. factory.LoadDragonBonesData("DragonBonesDataPath");
 *    factory.LoadTextureAtlasData("TextureAtlasDataPath");
 * 2. armatureComponent = factory.BuildArmatureComponent("armatureName");
 * 3. armatureComponent.animation.Play("animationName");
 */
public class HelloDragonBones :MonoBehaviour
{
    void Start()
    {
        UnityFactory.factory.LoadDragonBonesData("BirdAndCentaur/skeleton");
        UnityFactory.factory.LoadTextureAtlasData("BirdAndCentaur/texture");

        var armatureComponent = UnityFactory.factory.BuildArmatureComponent("centaur/charactor");
        armatureComponent.armature.flipX = true;
        armatureComponent.animation.Play("run");

        armatureComponent.transform.localPosition = new Vector3(5.0f, 0.0f, 0.0f);
    }
}