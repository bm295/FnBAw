using System.Text;

namespace FnBManagement.Web.Application.Search;

public static class SearchTermParser
{
    public static IReadOnlyList<string> Parse(string? searchTerm)
    {
        var terms = new List<string>();
        var token = new StringBuilder();
        bool inDoubleQuote = false;

        void AddTerm()
        {
            var term = token.ToString().Trim();
            if (term.Length > 0)
                terms.Add(term);
            token.Clear();
        }

        foreach (char c in searchTerm?.Trim() ?? string.Empty)
        {
            if (c is '"' or '\u201C' or '\u201D')
            {
                AddTerm();
                inDoubleQuote = c switch
                {
                    '\u201C' => true,
                    '\u201D' => false,
                    _ => !inDoubleQuote
                };
            }
            else if (char.IsWhiteSpace(c) && !inDoubleQuote)
            {
                AddTerm();
            }
            else
            {
                token.Append(c);
            }
        }

        // An unclosed quote keeps the remaining text together as one phrase.
        AddTerm();
        return terms;
    }
}
