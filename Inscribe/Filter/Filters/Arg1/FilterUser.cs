﻿using System.Collections.Generic;
using Inscribe.Filter.Filters.Common;

namespace Inscribe.Filter.Filters.Arg1
{
    class FilterUser : TextFilterBase
    {
        private FilterUser() { }

        public FilterUser(string needle) : this(needle, false) { }

        public FilterUser(string needle, bool isCaseSensitive)
        {
            this.needle = needle;
            this.isCaseSensitive = isCaseSensitive;
        }

        protected override bool FilterStatus(Dulcet.Twitter.TwitterStatusBase status)
        {
            return Match(status.User.ScreenName, needle, isCaseSensitive);
        }

        public override string Identifier
        {
            get { return "user"; }
        }

        public override string Description
        {
            get { return "ユーザー@ID"; }
        }
    }
}