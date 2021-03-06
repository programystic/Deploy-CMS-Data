﻿using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Services;

namespace DeployCmsData.UpgradeScripts_7.UpgradeScripts
{
    [DoNotAutoRun]
    public class ClearTheDecks : IUpgradeScript
    {
        public bool RunScript()
        {
            var library = new UmbracoLibrary();

            library.DeleteAllContent();
            library.DeleteAllDocumentTypes();
            library.DeleteAllDocumentTypeFolders();
            //Library.DeleteAllTemplates();

            return true;
        }
    }
}
