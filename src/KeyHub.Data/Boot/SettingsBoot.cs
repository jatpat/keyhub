﻿using System.ComponentModel.Composition;
using KeyHub.Core.Kernel;
using KeyHub.Core.Settings;

namespace KeyHub.Data.Boot
{


    // Boot Settings Context


    ///// <summary>
    ///// Initializes all settings classes supported by the Framework
    ///// </summary>
    //[Export(typeof(IKernelEvent))]
    //[ExportMetadata("Order", 2)]
    //public class SettingsBoot : IKernelEvent
    //{
    //    /// <summary>
    //    /// Boots the FaceBoot procedure
    //    /// </summary>
    //    /// <returns></returns>
    //    public KernelEventCompletedArguments Execute()
    //    {
    //        // Triggers creation of the singleton class
    //        var settings = SettingsContext.Instance;

    //        // Return empty arguments
    //        return new KernelEventCompletedArguments() { AllowContinue = true, KernelEventSucceeded = true };
    //    }

    //    /// <summary>
    //    /// Gets the display name for the Settings boot procedure
    //    /// </summary>
    //    public string DisplayName
    //    {
    //        get { return "Settings boot"; }
    //    }

    //    public KernelEventsTypes EventType
    //    {
    //        get { return KernelEventsTypes.Startup; }
    //    }
    //}
}