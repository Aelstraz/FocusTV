﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FocusTV.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.5.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("None")]
        public string audioDeviceName {
            get {
                return ((string)(this["audioDeviceName"]));
            }
            set {
                this["audioDeviceName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int screenID {
            get {
                return ((int)(this["screenID"]));
            }
            set {
                this["screenID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("500")]
        public int updateTime {
            get {
                return ((int)(this["updateTime"]));
            }
            set {
                this["updateTime"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <string>devenv</string>
  <string>TextInputHost</string>
  <string>ShellExperienceHost</string>
  <string>NVIDIA Share</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection processBlacklist {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["processBlacklist"]));
            }
            set {
                this["processBlacklist"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool runOnStartup {
            get {
                return ((bool)(this["runOnStartup"]));
            }
            set {
                this["runOnStartup"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool keepMouseInScreenBounds {
            get {
                return ((bool)(this["keepMouseInScreenBounds"]));
            }
            set {
                this["keepMouseInScreenBounds"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("6")]
        public int keybindModifiers {
            get {
                return ((int)(this["keybindModifiers"]));
            }
            set {
                this["keybindModifiers"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("8")]
        public int keybindKey {
            get {
                return ((int)(this["keybindKey"]));
            }
            set {
                this["keybindKey"] = value;
            }
        }
    }
}
