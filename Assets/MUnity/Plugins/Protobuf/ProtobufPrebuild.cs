/*
*类     名  : Protobuf.cs
*版     本  : 1.0
*作     者  : 
*Unity版本  : 5.4.1f1
*说     明  : 
*创     建  : 2017-01-17
*/


using UnityEngine;
using System.Collections;
using ProtoBuf.Meta;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace ProtoBuf
{
    #if UNITY_EDITOR
    [InitializeOnLoad]
    #endif

    public sealed class ProtobufPrebuild
    {
        static bool IsInitialize;
        static ProtobufPrebuild()
        {
            ProtobufPrebuild.InitProtoTypes();
        }

        public static void InitProtoTypes()
        {
            if (IsInitialize)
                return;
            RuntimeTypeModel.Default.Add(typeof(Vector3), false).Add(1, "x").Add(2, "y").Add(3, "z");
            IsInitialize = true;
        }
    }
}