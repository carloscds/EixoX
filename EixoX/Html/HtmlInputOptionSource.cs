﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public interface HtmlInputOptionSource
    {
        IEnumerable<HtmlInputOption> GetInputOptions();
    }
}