﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using Dulcet.Twitter;
using Inscribe.Common;
using Inscribe.ViewModels;
using Inscribe.ViewModels.Timeline;

namespace Mystique.Views.Converters.Particular
{
    public class SourceToInlinesConverter : OneWayConverter<TweetViewModel, IEnumerable<Inline>>
    {
        static Regex SourceRegex = new Regex("=\"(.*?)\".*?>(.*?)<", RegexOptions.Singleline | RegexOptions.Compiled);

        public override IEnumerable<Inline> ToTarget(TweetViewModel input, object parameter)
        {
            var status = input.Status as TwitterStatus;
            if (status != null)
            {
                var src = status.Source;
                Match m = null;
                if (!String.IsNullOrEmpty(src) && (m = SourceRegex.Match(src.Replace("\\", ""))).Success)
                {
                    var hlink = new Hyperlink(new Run(m.Groups[2].Value));
                    hlink.Click += (o, e) => Browser.Start(m.Groups[1].Value);
                    hlink.Foreground = Brushes.DodgerBlue;
                    return new[] { hlink };
                }
            }
            // ELSE
            return new Run[0];
        }
    }
}