﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17626
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MySQL.ForExcel.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(MySQL.ForExcel.MySQLForExcelSettings))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ExportDetectDatatype {
            get {
                return ((bool)(this["ExportDetectDatatype"]));
            }
            set {
                this["ExportDetectDatatype"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(MySQL.ForExcel.MySQLForExcelSettings))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ExportAddBufferToVarchar {
            get {
                return ((bool)(this["ExportAddBufferToVarchar"]));
            }
            set {
                this["ExportAddBufferToVarchar"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(MySQL.ForExcel.MySQLForExcelSettings))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ExportAutoIndexIntColumns {
            get {
                return ((bool)(this["ExportAutoIndexIntColumns"]));
            }
            set {
                this["ExportAutoIndexIntColumns"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(MySQL.ForExcel.MySQLForExcelSettings))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ExportAutoAllowEmptyNonIndexColumns {
            get {
                return ((bool)(this["ExportAutoAllowEmptyNonIndexColumns"]));
            }
            set {
                this["ExportAutoAllowEmptyNonIndexColumns"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(MySQL.ForExcel.MySQLForExcelSettings))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ExportUseFormattedValues {
            get {
                return ((bool)(this["ExportUseFormattedValues"]));
            }
            set {
                this["ExportUseFormattedValues"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(MySQL.ForExcel.MySQLForExcelSettings))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ExportShowCopySQLButton {
            get {
                return ((bool)(this["ExportShowCopySQLButton"]));
            }
            set {
                this["ExportShowCopySQLButton"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(MySQL.ForExcel.MySQLForExcelSettings))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool AppendPerformAutoMap {
            get {
                return ((bool)(this["AppendPerformAutoMap"]));
            }
            set {
                this["AppendPerformAutoMap"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(MySQL.ForExcel.MySQLForExcelSettings))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool AppendAutoStoreColumnMapping {
            get {
                return ((bool)(this["AppendAutoStoreColumnMapping"]));
            }
            set {
                this["AppendAutoStoreColumnMapping"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(MySQL.ForExcel.MySQLForExcelSettings))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool AppendReloadColumnMapping {
            get {
                return ((bool)(this["AppendReloadColumnMapping"]));
            }
            set {
                this["AppendReloadColumnMapping"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(MySQL.ForExcel.MySQLForExcelSettings))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool AppendUseFormattedValues {
            get {
                return ((bool)(this["AppendUseFormattedValues"]));
            }
            set {
                this["AppendUseFormattedValues"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(MySQL.ForExcel.MySQLForExcelSettings))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Generic.List<MySQLColumnMapping> StoredDataMappings {
            get {
                return ((global::System.Collections.Generic.List<MySQLColumnMapping>)(this["StoredDataMappings"]));
            }
            set {
                this["StoredDataMappings"] = value;
            }
        }
    }
}
