// ========================================================
// 描 述：CsharpGernerator.cs 
// 作 者：郑贤春 
// 时 间：2017/01/02 19:09:31 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace MUnity.MEditor.MGenerator
{
    public interface IClassGenerator
    {
        void SetNamespace(string name);

        void SetClassName(string name);

        void SetBaseClass(string className);

        void SetInterfaces(string[] interfaceNames);

        void SetUsingName(string[] usingName);

        void SetAuthority(ClassAuthorityType type);

        void SetDecorate(ClassDecorateType type);

        void AddMethod(IMethodGenerator method);

        string ToString();

        void ToClass(string path);
    }

    public interface IMethodGenerator
    {
        string ToString();

        void SetMethodName(string name);

        void SetAuthority(MethodAuthorityType type);

        void SetDecorate(MethodDecorateType type);

        void SetParms(string[] parms);

        void SetReturn(string returnType);
    }

    public class ClassGernerator : IClassGenerator
    {
        const string CONTENT =
            "#UsingNames#" + 
            "#NamespaceBegin#" + 
            "#Authority##DecorateType#class #ClassName##ExtendsName#\r\n" + 
            "{\r\n" + 
            "#MethodContent#\r\n" + 
            "}\r\n" + 
            "#NamespaceEnd#";

        const string AUTHORITY = "|public|internal|private";
        const string DECORATE = "|abstract";

        string[] authorityTypes
        {
            get
            {
                return AUTHORITY.Split('|');
            }
        }
        string[] decorateTypes
        {
            get
            {
                return DECORATE.Split('|');
            }
        }

        string m_name;
        string m_baseName;
        string m_content;
        string[] m_interfaceNames;
        string[] m_usingNames;
        string m_namespace;
        List<IMethodGenerator> m_methods;
        ClassAuthorityType m_authorityType;
        ClassDecorateType m_decorateType;

        public ClassGernerator(string className)
        {
            this.m_content = CONTENT;
            this.m_name = className;
        }

        public void AddMethod(IMethodGenerator method)
        {
            if (this.m_methods == null) this.m_methods = new List<IMethodGenerator>();
            this.m_methods.Add(method);
        }

        public void SetAuthority(ClassAuthorityType type)
        {
            this.m_authorityType = type;
        }

        public void SetBaseClass(string baseName)
        {
            this.m_baseName = baseName;
        }

        public void SetClassName(string name)
        {
            this.m_name = name;
        }

        public void SetDecorate(ClassDecorateType type)
        {
            this.m_decorateType = type;
        }

        public void SetInterfaces(string[] interfaceNames)
        {
            this.m_interfaceNames = interfaceNames;
        }

        public void SetNamespace(string name)
        {
            this.m_namespace = name;
        }

        public void SetUsingName(string[] usingNames)
        {
            this.m_usingNames = usingNames;
        }

        string GetUsingNames(string[] names)
        {
            string useringNames = "";
            if(names == null || names.Length < 1)
            {
                return useringNames;
            }
            else
            {
                for(int i=0;i<names.Length;i++)
                {
                    useringNames += string.Format("using {0};\r\n", names[i]);
                }
                useringNames += "\r\n";
                return useringNames;
            }
        }

        string[] GetNamespace(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return new string[] { "", "" };
            }
            string begin = string.Format("namespace {0}{1}\r\n", name,"{");
            string end = "}";
            return new string[] { begin, end};
        }

        string GetAuthority(ClassAuthorityType type)
        {
            if(type == ClassAuthorityType.Default)
            {
                return "";
            }
            else
            {
                return this.authorityTypes[(int)type] + " ";
            }
        }

        string GetDecorate(ClassDecorateType type)
        {
            string decorateType = "";
            if(type == ClassDecorateType.Default)
            {
                return decorateType;
            }
            decorateType = this.decorateTypes[(int)type] + " ";
            return decorateType;
        }

        string GetExtends(string baseName,string[] interfaces)
        {
            if(string.IsNullOrEmpty(baseName) && interfaces == null || interfaces.Length < 1)
            {
                return "";
            }
            else
            {
                string extents = " : ";
                if(!string.IsNullOrEmpty(baseName))
                {
                    extents += string.Format("{0},", baseName);
                }
                if(interfaces != null && interfaces.Length > 0)
                {
                    for(int i=0;i<interfaces.Length;i++)
                    {
                        extents += string.Format("{0},", interfaces[i]);
                    }
                    
                }
                return extents.Substring(0, extents.Length - 1);
            }
        }

        string GetMethods(List<IMethodGenerator> methods)
        {
            if(methods == null || methods.Count < 1) return "";
            string methodValue = "";
            for(int i=0;i<methods.Count;i++)
            {
                methodValue += methods[i].ToString();
                if (i != methods.Count - 1) methodValue += "\r\n";
            }
            return methodValue;
        }

        public override string ToString()
        {
            this.m_content = this.m_content.Replace("#UsingNames#", GetUsingNames(this.m_usingNames));
            this.m_content = this.m_content.Replace("#NamespaceBegin#", GetNamespace(this.m_namespace)[0]);
            this.m_content = this.m_content.Replace("#NamespaceEnd#", GetNamespace(this.m_namespace)[1]);
            this.m_content = this.m_content.Replace("#Authority#", GetAuthority(this.m_authorityType));
            this.m_content = this.m_content.Replace("#DecorateType#", GetDecorate(this.m_decorateType));
            this.m_content = this.m_content.Replace("#ClassName#", this.m_name);
            this.m_content = this.m_content.Replace("#ExtendsName#", GetExtends(this.m_baseName,this.m_interfaceNames));
            this.m_content = this.m_content.Replace("#MethodContent#", GetMethods(this.m_methods));
            return this.m_content;
        }

        public void ToClass(string path)
        {
            
        }
    }

    public class MethodGenerator : IMethodGenerator
    {
        const string AUTHORITY = "|public|internal|private";
        const string DECORATE = "|abstract|override|virtual";
        const string CONTENT = "    #Authority##DecorateType##ReturnType# #MethodName#(#Parms#)#MethodContent#";
        const string METHODCONTENT = 
            "   \r\n" +
            "   {\r\n" +
            "      #ReturnValue#\r\n" +
            "   }";

        string m_content;
        string m_methodContent;

        MethodAuthorityType m_authorityType = MethodAuthorityType.Default;

        MethodDecorateType m_decorateType = MethodDecorateType.Default;

        string[] m_parms;

        string m_name;

        string m_returnType;

        string[] m_authorityTypes;
        string[] m_decorateTypes;

        public MethodGenerator(string methodName)
        {
            this.m_name = methodName;
            this.m_content = CONTENT;
            this.m_methodContent = METHODCONTENT;
            this.m_authorityTypes = AUTHORITY.Split('|');
            this.m_decorateTypes = DECORATE.Split('|');
        }

        public void SetAuthority(MethodAuthorityType type)
        {
            this.m_authorityType = type;
        }

        public void SetDecorate(MethodDecorateType type)
        {
            this.m_decorateType = type;
        }

        public void SetMethodName(string name)
        {
            this.m_name = name;
        }

        public void SetReturn(string returnType)
        {
            this.m_returnType = returnType;
        }

        public void SetParms(string[] parms)
        {
            this.m_parms = parms;
        }

        string GetReturn(string type)
        {
            string intValue = "Int32|int|double|Double|float|Single|";
            if (type.Equals("Boolean") || type.Equals("bool")) return "true";
            string[] intValues = intValue.Split('|');
            for (int i = 0; i < intValues.Length; i++)
            {
                if(type.Equals(intValues[i])) return "0";
            }
            return "null";
        }

        string GetAuthority(MethodAuthorityType type)
        {
            string authorityValue = "";
            if (type == MethodAuthorityType.Default)
            {
                authorityValue = this.m_authorityTypes[(int)type];
            }
            else
            {
                authorityValue = this.m_authorityTypes[(int)type] + " ";
            }
            return authorityValue;
        }

        string GetDecorate(MethodDecorateType type)
        {
            string decorateValue = "";
            if (type == MethodDecorateType.Default)
            {
                decorateValue = this.m_decorateTypes[(int)type];
            }
            else
            {
                decorateValue = this.m_decorateTypes[(int)type] + " ";
            }
            return decorateValue;
        }

        string GetParms(string[] parms)
        {
            string parmsValue = "";
            if (parms != null && parms.Length > 0)
            {
                for (int i = 0; i < parms.Length; i++)
                {
                    parmsValue += string.Format("{0} {1}Value,", parms[i], parms[i]);
                }
                parmsValue = parmsValue.Substring(0, parmsValue.Length - 1);
            }
            return parmsValue;
        }

        string GetReturnType(string returnType)
        {
            if (returnType == null)
            {
                returnType = "void";
            }
            return returnType;
        }

        string GetMethodContent(MethodDecorateType type,string returnType)
        {
            if (type == MethodDecorateType.Abstract)
            {
                this.m_methodContent = ";";
            }
            else
            {
                string returnValue = null;
                if (returnType != null)
                {
                    returnValue = string.Format("return {0};", GetReturn(returnType));
                }
                else
                {
                    returnValue = "";
                }
                this.m_methodContent = this.m_methodContent.Replace("#ReturnValue#", returnValue);
            }
            return this.m_methodContent;
        }

        public override string ToString()
        {
            this.m_content = this.m_content.Replace("#Authority#", GetAuthority(this.m_authorityType));
            this.m_content = this.m_content.Replace("#DecorateType#", GetDecorate(this.m_decorateType));
            this.m_content = this.m_content.Replace("#ReturnType#", GetReturnType(this.m_returnType));
            this.m_content = this.m_content.Replace("#MethodContent#", GetMethodContent(this.m_decorateType,this.m_returnType));
            this.m_content = this.m_content.Replace("#MethodName#", this.m_name);
            this.m_content = this.m_content.Replace("#Parms#", GetParms(this.m_parms));
            return this.m_content;
        }
    }

    public enum MethodAuthorityType
    {
        Default = 0,
        Public,
        Internal,
        Private
    }

    public enum MethodDecorateType
    {
        Default = 0,
        Abstract,
        Override,
        Virtual
    }

    public enum ClassAuthorityType
    {
        Default = 0,
        Public,
        Internal,
        Private
    }

    public enum ClassDecorateType
    {
        Default = 0,
        Abstract,
    }
}

