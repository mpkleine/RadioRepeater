﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



namespace RadioRepeater
{
    public partial class App : global::Windows.UI.Xaml.Markup.IXamlMetadataProvider
    {
    private global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlTypeInfoProvider _provider;

        /// <summary>
        /// GetXamlType(Type)
        /// </summary>
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(global::System.Type type)
        {
            if(_provider == null)
            {
                _provider = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByType(type);
        }

        /// <summary>
        /// GetXamlType(String)
        /// </summary>
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(string fullName)
        {
            if(_provider == null)
            {
                _provider = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByName(fullName);
        }

        /// <summary>
        /// GetXmlnsDefinitions()
        /// </summary>
        public global::Windows.UI.Xaml.Markup.XmlnsDefinition[] GetXmlnsDefinitions()
        {
            return new global::Windows.UI.Xaml.Markup.XmlnsDefinition[0];
        }
    }
}

namespace RadioRepeater.RadioRepeater_XamlTypeInfo
{
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal partial class XamlTypeInfoProvider
    {
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByType(global::System.Type type)
        {
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByType.TryGetValue(type, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByType(type);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByName.TryGetValue(typeName, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByName(typeName);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlMember GetMemberByLongName(string longMemberName)
        {
            if (string.IsNullOrEmpty(longMemberName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlMember xamlMember;
            if (_xamlMembers.TryGetValue(longMemberName, out xamlMember))
            {
                return xamlMember;
            }
            xamlMember = CreateXamlMember(longMemberName);
            if (xamlMember != null)
            {
                _xamlMembers.Add(longMemberName, xamlMember);
            }
            return xamlMember;
        }

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByName = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByType = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>
                _xamlMembers = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>();

        string[] _typeNameTable = null;
        global::System.Type[] _typeTable = null;

        private void InitTypeTables()
        {
            _typeNameTable = new string[8];
            _typeNameTable[0] = "RadioRepeater.MainPage";
            _typeNameTable[1] = "Windows.UI.Xaml.Controls.Page";
            _typeNameTable[2] = "Windows.UI.Xaml.Controls.UserControl";
            _typeNameTable[3] = "Int32";
            _typeNameTable[4] = "Boolean";
            _typeNameTable[5] = "TimeSpan";
            _typeNameTable[6] = "System.ValueType";
            _typeNameTable[7] = "Object";

            _typeTable = new global::System.Type[8];
            _typeTable[0] = typeof(global::RadioRepeater.MainPage);
            _typeTable[1] = typeof(global::Windows.UI.Xaml.Controls.Page);
            _typeTable[2] = typeof(global::Windows.UI.Xaml.Controls.UserControl);
            _typeTable[3] = typeof(global::System.Int32);
            _typeTable[4] = typeof(global::System.Boolean);
            _typeTable[5] = typeof(global::System.TimeSpan);
            _typeTable[6] = typeof(global::System.ValueType);
            _typeTable[7] = typeof(global::System.Object);
        }

        private int LookupTypeIndexByName(string typeName)
        {
            if (_typeNameTable == null)
            {
                InitTypeTables();
            }
            for (int i=0; i<_typeNameTable.Length; i++)
            {
                if(0 == string.CompareOrdinal(_typeNameTable[i], typeName))
                {
                    return i;
                }
            }
            return -1;
        }

        private int LookupTypeIndexByType(global::System.Type type)
        {
            if (_typeTable == null)
            {
                InitTypeTables();
            }
            for(int i=0; i<_typeTable.Length; i++)
            {
                if(type == _typeTable[i])
                {
                    return i;
                }
            }
            return -1;
        }

        private object Activate_0_MainPage() { return new global::RadioRepeater.MainPage(); }

        private global::Windows.UI.Xaml.Markup.IXamlType CreateXamlType(int typeIndex)
        {
            global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlSystemBaseType xamlType = null;
            global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType userType;
            string typeName = _typeNameTable[typeIndex];
            global::System.Type type = _typeTable[typeIndex];

            switch (typeIndex)
            {

            case 0:   //  RadioRepeater.MainPage
                userType = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_0_MainPage;
                userType.AddMemberName("RXCOSPin");
                userType.AddMemberName("RXCTCSSPin");
                userType.AddMemberName("TXPTTPin");
                userType.AddMemberName("TXCTCSSPin");
                userType.AddMemberName("TXCWIDPin");
                userType.AddMemberName("RXCOSActive");
                userType.AddMemberName("RXCOSTimeout");
                userType.AddMemberName("RXCTCSSActive");
                userType.AddMemberName("TXPTTActive");
                userType.AddMemberName("TXCTCSSActive");
                userType.AddMemberName("TXCWIDActive");
                userType.AddMemberName("TXCWIDPulse");
                userType.AddMemberName("TXPTTPulse");
                userType.AddMemberName("TXCWIDTimeout");
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 1:   //  Windows.UI.Xaml.Controls.Page
                xamlType = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 2:   //  Windows.UI.Xaml.Controls.UserControl
                xamlType = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 3:   //  Int32
                xamlType = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 4:   //  Boolean
                xamlType = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 5:   //  TimeSpan
                userType = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("System.ValueType"));
                userType.SetIsReturnTypeStub();
                xamlType = userType;
                break;

            case 6:   //  System.ValueType
                userType = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                xamlType = userType;
                break;

            case 7:   //  Object
                xamlType = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;
            }
            return xamlType;
        }


        private object get_0_MainPage_RXCOSPin(object instance)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            return that.RXCOSPin;
        }
        private void set_0_MainPage_RXCOSPin(object instance, object Value)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            that.RXCOSPin = (global::System.Int32)Value;
        }
        private object get_1_MainPage_RXCTCSSPin(object instance)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            return that.RXCTCSSPin;
        }
        private void set_1_MainPage_RXCTCSSPin(object instance, object Value)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            that.RXCTCSSPin = (global::System.Int32)Value;
        }
        private object get_2_MainPage_TXPTTPin(object instance)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            return that.TXPTTPin;
        }
        private void set_2_MainPage_TXPTTPin(object instance, object Value)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            that.TXPTTPin = (global::System.Int32)Value;
        }
        private object get_3_MainPage_TXCTCSSPin(object instance)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            return that.TXCTCSSPin;
        }
        private void set_3_MainPage_TXCTCSSPin(object instance, object Value)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            that.TXCTCSSPin = (global::System.Int32)Value;
        }
        private object get_4_MainPage_TXCWIDPin(object instance)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            return that.TXCWIDPin;
        }
        private void set_4_MainPage_TXCWIDPin(object instance, object Value)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            that.TXCWIDPin = (global::System.Int32)Value;
        }
        private object get_5_MainPage_RXCOSActive(object instance)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            return that.RXCOSActive;
        }
        private void set_5_MainPage_RXCOSActive(object instance, object Value)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            that.RXCOSActive = (global::System.Boolean)Value;
        }
        private object get_6_MainPage_RXCOSTimeout(object instance)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            return that.RXCOSTimeout;
        }
        private void set_6_MainPage_RXCOSTimeout(object instance, object Value)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            that.RXCOSTimeout = (global::System.TimeSpan)Value;
        }
        private object get_7_MainPage_RXCTCSSActive(object instance)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            return that.RXCTCSSActive;
        }
        private void set_7_MainPage_RXCTCSSActive(object instance, object Value)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            that.RXCTCSSActive = (global::System.Boolean)Value;
        }
        private object get_8_MainPage_TXPTTActive(object instance)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            return that.TXPTTActive;
        }
        private void set_8_MainPage_TXPTTActive(object instance, object Value)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            that.TXPTTActive = (global::System.Boolean)Value;
        }
        private object get_9_MainPage_TXCTCSSActive(object instance)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            return that.TXCTCSSActive;
        }
        private void set_9_MainPage_TXCTCSSActive(object instance, object Value)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            that.TXCTCSSActive = (global::System.Boolean)Value;
        }
        private object get_10_MainPage_TXCWIDActive(object instance)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            return that.TXCWIDActive;
        }
        private void set_10_MainPage_TXCWIDActive(object instance, object Value)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            that.TXCWIDActive = (global::System.Boolean)Value;
        }
        private object get_11_MainPage_TXCWIDPulse(object instance)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            return that.TXCWIDPulse;
        }
        private void set_11_MainPage_TXCWIDPulse(object instance, object Value)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            that.TXCWIDPulse = (global::System.TimeSpan)Value;
        }
        private object get_12_MainPage_TXPTTPulse(object instance)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            return that.TXPTTPulse;
        }
        private void set_12_MainPage_TXPTTPulse(object instance, object Value)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            that.TXPTTPulse = (global::System.TimeSpan)Value;
        }
        private object get_13_MainPage_TXCWIDTimeout(object instance)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            return that.TXCWIDTimeout;
        }
        private void set_13_MainPage_TXCWIDTimeout(object instance, object Value)
        {
            var that = (global::RadioRepeater.MainPage)instance;
            that.TXCWIDTimeout = (global::System.TimeSpan)Value;
        }

        private global::Windows.UI.Xaml.Markup.IXamlMember CreateXamlMember(string longMemberName)
        {
            global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlMember xamlMember = null;
            global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType userType;

            switch (longMemberName)
            {
            case "RadioRepeater.MainPage.RXCOSPin":
                userType = (global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType)GetXamlTypeByName("RadioRepeater.MainPage");
                xamlMember = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlMember(this, "RXCOSPin", "Int32");
                xamlMember.Getter = get_0_MainPage_RXCOSPin;
                xamlMember.Setter = set_0_MainPage_RXCOSPin;
                break;
            case "RadioRepeater.MainPage.RXCTCSSPin":
                userType = (global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType)GetXamlTypeByName("RadioRepeater.MainPage");
                xamlMember = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlMember(this, "RXCTCSSPin", "Int32");
                xamlMember.Getter = get_1_MainPage_RXCTCSSPin;
                xamlMember.Setter = set_1_MainPage_RXCTCSSPin;
                break;
            case "RadioRepeater.MainPage.TXPTTPin":
                userType = (global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType)GetXamlTypeByName("RadioRepeater.MainPage");
                xamlMember = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlMember(this, "TXPTTPin", "Int32");
                xamlMember.Getter = get_2_MainPage_TXPTTPin;
                xamlMember.Setter = set_2_MainPage_TXPTTPin;
                break;
            case "RadioRepeater.MainPage.TXCTCSSPin":
                userType = (global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType)GetXamlTypeByName("RadioRepeater.MainPage");
                xamlMember = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlMember(this, "TXCTCSSPin", "Int32");
                xamlMember.Getter = get_3_MainPage_TXCTCSSPin;
                xamlMember.Setter = set_3_MainPage_TXCTCSSPin;
                break;
            case "RadioRepeater.MainPage.TXCWIDPin":
                userType = (global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType)GetXamlTypeByName("RadioRepeater.MainPage");
                xamlMember = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlMember(this, "TXCWIDPin", "Int32");
                xamlMember.Getter = get_4_MainPage_TXCWIDPin;
                xamlMember.Setter = set_4_MainPage_TXCWIDPin;
                break;
            case "RadioRepeater.MainPage.RXCOSActive":
                userType = (global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType)GetXamlTypeByName("RadioRepeater.MainPage");
                xamlMember = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlMember(this, "RXCOSActive", "Boolean");
                xamlMember.Getter = get_5_MainPage_RXCOSActive;
                xamlMember.Setter = set_5_MainPage_RXCOSActive;
                break;
            case "RadioRepeater.MainPage.RXCOSTimeout":
                userType = (global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType)GetXamlTypeByName("RadioRepeater.MainPage");
                xamlMember = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlMember(this, "RXCOSTimeout", "TimeSpan");
                xamlMember.Getter = get_6_MainPage_RXCOSTimeout;
                xamlMember.Setter = set_6_MainPage_RXCOSTimeout;
                break;
            case "RadioRepeater.MainPage.RXCTCSSActive":
                userType = (global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType)GetXamlTypeByName("RadioRepeater.MainPage");
                xamlMember = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlMember(this, "RXCTCSSActive", "Boolean");
                xamlMember.Getter = get_7_MainPage_RXCTCSSActive;
                xamlMember.Setter = set_7_MainPage_RXCTCSSActive;
                break;
            case "RadioRepeater.MainPage.TXPTTActive":
                userType = (global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType)GetXamlTypeByName("RadioRepeater.MainPage");
                xamlMember = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlMember(this, "TXPTTActive", "Boolean");
                xamlMember.Getter = get_8_MainPage_TXPTTActive;
                xamlMember.Setter = set_8_MainPage_TXPTTActive;
                break;
            case "RadioRepeater.MainPage.TXCTCSSActive":
                userType = (global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType)GetXamlTypeByName("RadioRepeater.MainPage");
                xamlMember = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlMember(this, "TXCTCSSActive", "Boolean");
                xamlMember.Getter = get_9_MainPage_TXCTCSSActive;
                xamlMember.Setter = set_9_MainPage_TXCTCSSActive;
                break;
            case "RadioRepeater.MainPage.TXCWIDActive":
                userType = (global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType)GetXamlTypeByName("RadioRepeater.MainPage");
                xamlMember = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlMember(this, "TXCWIDActive", "Boolean");
                xamlMember.Getter = get_10_MainPage_TXCWIDActive;
                xamlMember.Setter = set_10_MainPage_TXCWIDActive;
                break;
            case "RadioRepeater.MainPage.TXCWIDPulse":
                userType = (global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType)GetXamlTypeByName("RadioRepeater.MainPage");
                xamlMember = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlMember(this, "TXCWIDPulse", "TimeSpan");
                xamlMember.Getter = get_11_MainPage_TXCWIDPulse;
                xamlMember.Setter = set_11_MainPage_TXCWIDPulse;
                break;
            case "RadioRepeater.MainPage.TXPTTPulse":
                userType = (global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType)GetXamlTypeByName("RadioRepeater.MainPage");
                xamlMember = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlMember(this, "TXPTTPulse", "TimeSpan");
                xamlMember.Getter = get_12_MainPage_TXPTTPulse;
                xamlMember.Setter = set_12_MainPage_TXPTTPulse;
                break;
            case "RadioRepeater.MainPage.TXCWIDTimeout":
                userType = (global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlUserType)GetXamlTypeByName("RadioRepeater.MainPage");
                xamlMember = new global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlMember(this, "TXCWIDTimeout", "TimeSpan");
                xamlMember.Getter = get_13_MainPage_TXCWIDTimeout;
                xamlMember.Setter = set_13_MainPage_TXCWIDTimeout;
                break;
            }
            return xamlMember;
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlSystemBaseType : global::Windows.UI.Xaml.Markup.IXamlType
    {
        string _fullName;
        global::System.Type _underlyingType;

        public XamlSystemBaseType(string fullName, global::System.Type underlyingType)
        {
            _fullName = fullName;
            _underlyingType = underlyingType;
        }

        public string FullName { get { return _fullName; } }

        public global::System.Type UnderlyingType
        {
            get
            {
                return _underlyingType;
            }
        }

        virtual public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name) { throw new global::System.NotImplementedException(); }
        virtual public bool IsArray { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsCollection { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsConstructible { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsDictionary { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsMarkupExtension { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsBindable { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsReturnTypeStub { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsLocalType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType ItemType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType KeyType { get { throw new global::System.NotImplementedException(); } }
        virtual public object ActivateInstance() { throw new global::System.NotImplementedException(); }
        virtual public void AddToMap(object instance, object key, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void AddToVector(object instance, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void RunInitializer()   { throw new global::System.NotImplementedException(); }
        virtual public object CreateFromString(string input)   { throw new global::System.NotImplementedException(); }
    }
    
    internal delegate object Activator();
    internal delegate void AddToCollection(object instance, object item);
    internal delegate void AddToDictionary(object instance, object key, object item);

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlUserType : global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlSystemBaseType
    {
        global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlTypeInfoProvider _provider;
        global::Windows.UI.Xaml.Markup.IXamlType _baseType;
        bool _isArray;
        bool _isMarkupExtension;
        bool _isBindable;
        bool _isReturnTypeStub;
        bool _isLocalType;

        string _contentPropertyName;
        string _itemTypeName;
        string _keyTypeName;
        global::System.Collections.Generic.Dictionary<string, string> _memberNames;
        global::System.Collections.Generic.Dictionary<string, object> _enumValues;

        public XamlUserType(global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlTypeInfoProvider provider, string fullName, global::System.Type fullType, global::Windows.UI.Xaml.Markup.IXamlType baseType)
            :base(fullName, fullType)
        {
            _provider = provider;
            _baseType = baseType;
        }

        // --- Interface methods ----

        override public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { return _baseType; } }
        override public bool IsArray { get { return _isArray; } }
        override public bool IsCollection { get { return (CollectionAdd != null); } }
        override public bool IsConstructible { get { return (Activator != null); } }
        override public bool IsDictionary { get { return (DictionaryAdd != null); } }
        override public bool IsMarkupExtension { get { return _isMarkupExtension; } }
        override public bool IsBindable { get { return _isBindable; } }
        override public bool IsReturnTypeStub { get { return _isReturnTypeStub; } }
        override public bool IsLocalType { get { return _isLocalType; } }

        override public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty
        {
            get { return _provider.GetMemberByLongName(_contentPropertyName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType ItemType
        {
            get { return _provider.GetXamlTypeByName(_itemTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType KeyType
        {
            get { return _provider.GetXamlTypeByName(_keyTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name)
        {
            if (_memberNames == null)
            {
                return null;
            }
            string longName;
            if (_memberNames.TryGetValue(name, out longName))
            {
                return _provider.GetMemberByLongName(longName);
            }
            return null;
        }

        override public object ActivateInstance()
        {
            return Activator(); 
        }

        override public void AddToMap(object instance, object key, object item) 
        {
            DictionaryAdd(instance, key, item);
        }

        override public void AddToVector(object instance, object item)
        {
            CollectionAdd(instance, item);
        }

        override public void RunInitializer() 
        {
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(UnderlyingType.TypeHandle);
        }

        override public object CreateFromString(string input)
        {
            if (_enumValues != null)
            {
                int value = 0;

                string[] valueParts = input.Split(',');

                foreach (string valuePart in valueParts) 
                {
                    object partValue;
                    int enumFieldValue = 0;
                    try
                    {
                        if (_enumValues.TryGetValue(valuePart.Trim(), out partValue))
                        {
                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                        }
                        else
                        {
                            try
                            {
                                enumFieldValue = global::System.Convert.ToInt32(valuePart.Trim());
                            }
                            catch( global::System.FormatException )
                            {
                                foreach( string key in _enumValues.Keys )
                                {
                                    if( string.Compare(valuePart.Trim(), key, global::System.StringComparison.OrdinalIgnoreCase) == 0 )
                                    {
                                        if( _enumValues.TryGetValue(key.Trim(), out partValue) )
                                        {
                                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        value |= enumFieldValue; 
                    }
                    catch( global::System.FormatException )
                    {
                        throw new global::System.ArgumentException(input, FullName);
                    }
                }

                return value; 
            }
            throw new global::System.ArgumentException(input, FullName);
        }

        // --- End of Interface methods

        public Activator Activator { get; set; }
        public AddToCollection CollectionAdd { get; set; }
        public AddToDictionary DictionaryAdd { get; set; }

        public void SetContentPropertyName(string contentPropertyName)
        {
            _contentPropertyName = contentPropertyName;
        }

        public void SetIsArray()
        {
            _isArray = true; 
        }

        public void SetIsMarkupExtension()
        {
            _isMarkupExtension = true;
        }

        public void SetIsBindable()
        {
            _isBindable = true;
        }

        public void SetIsReturnTypeStub()
        {
            _isReturnTypeStub = true;
        }

        public void SetIsLocalType()
        {
            _isLocalType = true;
        }

        public void SetItemTypeName(string itemTypeName)
        {
            _itemTypeName = itemTypeName;
        }

        public void SetKeyTypeName(string keyTypeName)
        {
            _keyTypeName = keyTypeName;
        }

        public void AddMemberName(string shortName)
        {
            if(_memberNames == null)
            {
                _memberNames =  new global::System.Collections.Generic.Dictionary<string,string>();
            }
            _memberNames.Add(shortName, FullName + "." + shortName);
        }

        public void AddEnumValue(string name, object value)
        {
            if (_enumValues == null)
            {
                _enumValues = new global::System.Collections.Generic.Dictionary<string, object>();
            }
            _enumValues.Add(name, value);
        }
    }

    internal delegate object Getter(object instance);
    internal delegate void Setter(object instance, object value);

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlMember : global::Windows.UI.Xaml.Markup.IXamlMember
    {
        global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlTypeInfoProvider _provider;
        string _name;
        bool _isAttachable;
        bool _isDependencyProperty;
        bool _isReadOnly;

        string _typeName;
        string _targetTypeName;

        public XamlMember(global::RadioRepeater.RadioRepeater_XamlTypeInfo.XamlTypeInfoProvider provider, string name, string typeName)
        {
            _name = name;
            _typeName = typeName;
            _provider = provider;
        }

        public string Name { get { return _name; } }

        public global::Windows.UI.Xaml.Markup.IXamlType Type
        {
            get { return _provider.GetXamlTypeByName(_typeName); }
        }

        public void SetTargetTypeName(string targetTypeName)
        {
            _targetTypeName = targetTypeName;
        }
        public global::Windows.UI.Xaml.Markup.IXamlType TargetType
        {
            get { return _provider.GetXamlTypeByName(_targetTypeName); }
        }

        public void SetIsAttachable() { _isAttachable = true; }
        public bool IsAttachable { get { return _isAttachable; } }

        public void SetIsDependencyProperty() { _isDependencyProperty = true; }
        public bool IsDependencyProperty { get { return _isDependencyProperty; } }

        public void SetIsReadOnly() { _isReadOnly = true; }
        public bool IsReadOnly { get { return _isReadOnly; } }

        public Getter Getter { get; set; }
        public object GetValue(object instance)
        {
            if (Getter != null)
                return Getter(instance);
            else
                throw new global::System.InvalidOperationException("GetValue");
        }

        public Setter Setter { get; set; }
        public void SetValue(object instance, object value)
        {
            if (Setter != null)
                Setter(instance, value);
            else
                throw new global::System.InvalidOperationException("SetValue");
        }
    }
}

