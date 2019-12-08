using System.IO;
using System.Threading.Tasks;

namespace MGR.PortableObject.Parsing
{
    public class PortableObjectParser
    {
        /*
         * https://www.gnu.org/software/gettext/manual/html_node/PO-Files.html
         * https://www.gnu.org/software/gettext/manual/html_node/Plural-forms.html#Plural-forms
         * https://www.gnu.org/software/gettext/manual/html_node/Translating-plural-forms.html#Translating-plural-forms
         */

        public PortableObjectTranslations Parse(TextReader textReader)
        {
            var translationsBuilder = new PortableObjectTranslationsBuilder();
            string? line;
            while ((line = textReader.ReadLine()) != null)
            {

                translationsBuilder.ParseLine(line);
            }
            var translations = translationsBuilder.Build();
            return translations;
        }
        public async Task<PortableObjectTranslations> ParseAsync(TextReader textReader)
        {
            var translationsBuilder = new PortableObjectTranslationsBuilder();
            string? line;
            while ((line = await textReader.ReadLineAsync()) != null)
            {

                translationsBuilder.ParseLine(line);
            }
            var translations = translationsBuilder.Build();
            return translations;
        }
    }
}

