﻿using NowMine.Enumerates;
using System.Collections.Generic;

namespace NowMine.WebHandlers
{
    class YTNowMine : IWebHandler
    {
        //private const string LOCALSITEADDRESS = @"local://index.html";
        private const string LOCALSITEADDRESS = @"index.html";

        public List<string> GetAfterLoadScripts()
        {
            return new List<string>();
        }

        public List<string> GetOnLoadScripts()
        {
            return new List<string>();
        }

        public string GetVideoURL(string id)
        {
            //return LOCALSITEADDRESS;
            return @"http://fritzthescientis.byethost18.com/testy/index.html";
        }

        public IWebHandler GetErrorHandler()
        {
            return new YTTV();
        }

        public string GetHomePage()
        {
            //return LOCALSITEADDRESS;
            //return "home.html";
            return @"http://fritzthescientis.byethost18.com/testy/index.html";
        }

        public NextVideoType NextVideoType()
        {
            return Enumerates.NextVideoType.Script;
        }
    }
}
