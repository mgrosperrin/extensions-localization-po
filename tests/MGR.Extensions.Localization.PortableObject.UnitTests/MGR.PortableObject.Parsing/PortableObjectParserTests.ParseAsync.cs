using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MGR.PortableObject.Parsing
{
    public partial class PortableObjectParserTests
    {
        public class ParseAsync
        {
            [Fact]
            public async Task ParseReturnsSimpleEntry()
            {
                var entries = await ParseText("SimpleEntry");

                Assert.Equal(1, entries.Count);

                var translation = entries.GetTranslation("Unknown system error");

                Assert.True(translation.HasTranslation);
                Assert.Single(translation.Translations);
                Assert.Equal("Erreur système inconnue", translation.GetTranslation());
            }

            [Fact]
            public async Task ParseIgnoresEntryWithoutTranslation()
            {
                var entries = await ParseText("EntryWithoutTranslation");

                Assert.Equal(0, entries.Count);
            }

            [Fact]
            public async Task ParseIgnoresPoeditHeader()
            {
                var entries = await ParseText("PoeditHeader");

                Assert.Equal(1, entries.Count);
                var translation = entries.GetTranslation("Unknown system error");
                Assert.Single(translation.Translations);
                Assert.True(translation.HasTranslation);
                Assert.Equal("Erreur système inconnue", translation.GetTranslation());
            }

            [Fact]
            public async Task ParseSetsContext()
            {
                var entries = await ParseText("EntryWithContext");

                var translation = entries.GetTranslation("Unknown system error", "MGR.Localization");
                Assert.True(translation.HasTranslation);
                Assert.Equal("Erreur système inconnue", translation.GetTranslation());
            }

            [Fact]
            public async Task ParseIgnoresComments()
            {
                var entries = await ParseText("EntryWithComments");

                var translation = entries.GetTranslation("Unknown system error", "MGR.Localization");
                Assert.True(translation.HasTranslation);
                Assert.Equal("Erreur système inconnue", translation.GetTranslation());
            }

            [Fact]
            public async Task ParseOnlyTrimsLeadingAndTrailingQuotes()
            {
                var entries = await ParseText("EntryWithQuotes");

                var translation = entries.GetTranslation("\"{0}\"");
                Assert.True(translation.HasTranslation);
                Assert.Equal("\"{0}\"", translation.GetTranslation());
            }

            [Fact]
            public async Task ParseHandleUnclosedQuote()
            {
                var entries = await ParseText("EntryWithUnclosedQuote");

                var translation = entries.GetTranslation("Foo \"{0}\"", "");
                Assert.True(translation.HasTranslation);
                Assert.Equal("Foo \"{0}\"", translation.GetTranslation());
            }

            [Fact]
            public async Task ParseHandlesMultilineEntry()
            {
                var entries = await ParseText("EntryWithMultilineText");

                var translation = entries.GetTranslation(
                    "Here is an example of how one might continue a very long string\nfor the common case the string represents multi-line output.");
                Assert.True(translation.HasTranslation);
                Assert.Equal(
                    "Ceci est un exemple de comment une traduction très longue peut continuer\npour le cas commun où le texte serait sur plusieurs lignes.",
                    translation.GetTranslation());
            }

            [Fact]
            public async Task ParsePreservesEscapedCharacters()
            {
                var entries = await ParseText("EntryWithEscapedCharacters");

                var translation = entries.GetTranslation("Line:\t\"{0}\"\n");
                Assert.True(translation.HasTranslation);
                Assert.Equal("Ligne :\t\"{0}\"\n", translation.GetTranslation());
            }

            [Fact]
            public async Task ParseReadsPluralTranslations()
            {
                var entries = await ParseText("EntryWithPlural");

                var translation = entries.GetTranslation("book");
                Assert.True(translation.HasTranslation);
                Assert.Equal("livres", translation.GetPluralTranslation(0));
                Assert.Equal("livre", translation.GetPluralTranslation(1));
                Assert.Equal("livres", translation.GetPluralTranslation(2));
            }

            [Fact]
            public async Task ParseReadsMultipleEntries()
            {
                var entries = await ParseText("MultipleEntries");

                Assert.Equal(2, entries.Count);

                var translation = entries.GetTranslation("File {0} does not exist", "MGR.File");

                Assert.True(translation.HasTranslation);
                Assert.Equal("Le fichier {0} n'existe pas", translation.GetTranslation());

                translation = entries.GetTranslation("Directory {0} does not exist", "MGR.Directory");

                Assert.True(translation.HasTranslation);
                Assert.Equal("Le répertoire {0} n'existe pas", translation.GetTranslation());
            }

            private async Task<PortableObjectTranslations> ParseText(string resourceName)
            {
                var fullResourceName = $"MGR.PortableObject.Parsing.Resources.{resourceName}.po";
                var parser = new PortableObjectParser();

                var testAssembly = typeof(ParseAsync).Assembly;
                using (var resource =
                    testAssembly.GetManifestResourceStream(fullResourceName) ??
                    throw new ArgumentException(nameof(resourceName)))
                {
                    using (var reader = new StreamReader(resource))
                    {
                        return await parser.ParseAsync(reader);
                    }
                }
            }
        }
    }
}