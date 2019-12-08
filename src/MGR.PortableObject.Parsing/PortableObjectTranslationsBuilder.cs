using System;
using System.Collections.Generic;
using System.Linq;

namespace MGR.PortableObject.Parsing
{
    internal class PortableObjectTranslationsBuilder
    {
        private const string ContextPrefix = "msgctxt";
        private const string KeyPrefix = "msgid";
        private const string TranslationPrefix = "msgstr";

        private readonly Dictionary<PortableObjectKey, string[]> _translations = new Dictionary<PortableObjectKey, string[]>();
        private readonly List<string> _currentTranslations = new List<string>();
        private string? _currentContext;
        private string _currentId = string.Empty;
        private LineType _lastLineType = LineType.Unknown;

        public void ParseLine(string line)
        {
            if (line.StartsWithQuote())
            {
                var lineContent = line.Trim().TrimQuote().Unescape();
                AppendLineContent(lineContent);
                return;
            }

            var keyAndValue = line.Split(null, 2);
            if (keyAndValue.Length != 2)
            {
                return;
            }

            var content = keyAndValue[1].Trim().TrimQuote().Unescape();
            switch (keyAndValue[0])
            {
                case ContextPrefix:
                    FlushEntry();
                    _currentContext = content;
                    _lastLineType = LineType.Context;
                    break;
                case KeyPrefix:
                    FlushEntry();
                    _currentId = content;
                    _lastLineType = LineType.Id;
                    break;
                case var key when key.StartsWith(TranslationPrefix, StringComparison.Ordinal):
                    _currentTranslations.Add(content);
                    _lastLineType = LineType.Translation;
                    break;
            }
        }

        private void AppendLineContent(string lineContent)
        {
            switch (_lastLineType)
            {
                case LineType.Context:
                    _currentContext += lineContent;
                    break;
                case LineType.Id:
                    _currentId += lineContent;
                    break;
                case LineType.Translation:
                    _currentTranslations[_currentTranslations.Count - 1] += lineContent;
                    break;
            }
        }

        private void FlushEntry()
        {
            if (_currentTranslations.Count > 0 && !string.IsNullOrEmpty(_currentId))
            {
                var key = new PortableObjectKey(_currentId, _currentContext);
                _translations.Add(key, _currentTranslations.ToArray());

                _currentContext = null;
                _currentId = string.Empty;
            }
            _currentTranslations.Clear();
        }

        public PortableObjectTranslations Build()
        {
            FlushEntry();
            return new PortableObjectTranslations(_translations.ToDictionary(_ => _.Key, _ => _.Value.ToArray()));
        }

        private enum LineType
        {
            Unknown = 0,
            Text = 1,
            Context = 2,
            Id = 3,
            Translation = 4
        }
    }
}