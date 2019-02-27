﻿// Copyright (c) 2019 Valdis Iljuconoks.
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DbLocalizationProvider.Export;
using Newtonsoft.Json;

namespace DbLocalizationProvider.Import
{
    public class JsonResourceFormatParser : IResourceFormatParser
    {
        private static readonly string[] _extensions = { ".json" };

        public string FormatName => "JSON";

        public string[] SupportedFileExtensions => _extensions;

        public string ProviderId => "json";

        public ParseResult Parse(string fileContent)
        {
            var result = JsonConvert.DeserializeObject<ICollection<LocalizationResource>>(fileContent, JsonResourceExporter.DefaultSettings);
            var detectedLanguages = result.SelectMany(r => r.Translations != null && r.Translations.Any() ? r.Translations?.Select(t => t.Language) : new []{ string.Empty })
                .Distinct()
                .Where(l => !string.IsNullOrEmpty(l));

            return new ParseResult(result, detectedLanguages.Select(l => new CultureInfo(l)).ToList());
        }
    }
}
