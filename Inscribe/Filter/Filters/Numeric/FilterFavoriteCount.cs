﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inscribe.Filter.Core;
using Inscribe.Storage;

namespace Inscribe.Filter.Filters.Numeric
{
    public class FilterFavoriteCount : FilterBase
    {
        private LongRange range;

        private FilterFavoriteCount() { }

        public FilterFavoriteCount(LongRange range)
        {
            this.range = range;
        }

        public FilterFavoriteCount(long pivot)
        {
            this.range = LongRange.FromPivotValue(pivot);
        }
        
        protected override bool FilterStatus(Dulcet.Twitter.TwitterStatusBase status)
        {
            return this.range.Check(TweetStorage.Get(status.Id).FavoredUsers.Count());
        }

        public override string Identifier
        {
            get { return "fav_count"; }
        }

        public override IEnumerable<object> GetArgumentsForQueryify()
        {
            yield return this.range;
        }

        public override string Description
        {
            get { return "被お気に入り数"; }
        }

        public override string FilterStateString
        {
            get { return "被Fav数が " + this.range.ToString() + " であるもの"; }
        }
    }
}
