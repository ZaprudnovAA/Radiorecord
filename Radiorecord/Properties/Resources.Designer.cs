﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Radio.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Radio.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Using the program is easy!
        ///- Select your favorite station
        ///- Press the play button at the selected station
        ///- PROFIT
        ///
        ///Hotkeys:
        ///ENTER - start the first station
        ///ESC - stop playing
        ///ScrollLock - stop / play the selected station
        ///
        ///Autor: ZaprudnovAA.
        /// </summary>
        internal static string Form1_Form1_KeyDown_ {
            get {
                return ResourceManager.GetString("Form1_Form1_KeyDown_", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Help.
        /// </summary>
        internal static string Form1_Form1_KeyDown_microHelp {
            get {
                return ResourceManager.GetString("Form1_Form1_KeyDown_microHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Volume .
        /// </summary>
        internal static string Form1_trackBar1_Scroll_Volume_ {
            get {
                return ResourceManager.GetString("Form1_trackBar1_Scroll_Volume_", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap record {
            get {
                object obj = ResourceManager.GetObject("record", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.IO.UnmanagedMemoryStream similar to System.IO.MemoryStream.
        /// </summary>
        internal static System.IO.UnmanagedMemoryStream Record_Jingle {
            get {
                return ResourceManager.GetStream("Record_Jingle", resourceCulture);
            }
        }
    }
}
