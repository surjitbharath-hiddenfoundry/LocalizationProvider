﻿namespace DbLocalizationProvider.MvcSample.Resources
{
    [LocalizedResource]
    public class CommonResources
    {
        [LocalizedResource]
        public class DialogResources
        {
            public static string YesButton { get; set; }
        }
    }

    public class ForeignResources
    {
        public static string ForeignProperty => "Foreign property";
    }
}