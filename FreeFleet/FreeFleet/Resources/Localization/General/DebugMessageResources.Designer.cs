﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreeFleet.Resources.Localization.General {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class DebugMessageResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DebugMessageResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FreeFleet.Resources.Localization.General.DebugMessageResources", typeof(DebugMessageResources).Assembly);
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
        ///   Looks up a localized string similar to Event fleet refreshed with empty results..
        /// </summary>
        internal static string EventFleetRefreshEmptyFleet {
            get {
                return ResourceManager.GetString("EventFleetRefreshEmptyFleet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed requesting event fleet.
        ///Reason: {0}
        ///Response Body: {1}.
        /// </summary>
        internal static string EventFleetRefreshFail {
            get {
                return ResourceManager.GetString("EventFleetRefreshFail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Logged out..
        /// </summary>
        internal static string EventFleetRefreshLoggedOut {
            get {
                return ResourceManager.GetString("EventFleetRefreshLoggedOut", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Server rejected..
        /// </summary>
        internal static string EventFleetRefreshRejected {
            get {
                return ResourceManager.GetString("EventFleetRefreshRejected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Event fleet refreshed..
        /// </summary>
        internal static string EventFleetRefreshSuccess {
            get {
                return ResourceManager.GetString("EventFleetRefreshSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Offensive mission detected..
        /// </summary>
        internal static string OffensiveMissionDetected {
            get {
                return ResourceManager.GetString("OffensiveMissionDetected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User {0} Login..
        /// </summary>
        internal static string UserLogin {
            get {
                return ResourceManager.GetString("UserLogin", resourceCulture);
            }
        }
    }
}