using Sqids;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYouOwnIt.Shared.Helpers
{
    public class SlugHelper
    {
        private static readonly Dictionary<char, string> _slugReplacements = new()
        {
            /* Symbols */
            {'&', "-and-" }, {'#', "-no-" }, {'@', "-at-" }, {' ',"-"},
            {'$', "-dollars-" }, {'%', "-percent-" },
            {'^', "" }, {'*', "" },
            {'+', "plus" },{'=', "-equals-" },
            {'<', "" }, {'>', "" }, {'~', "" },
            {'\\', "" }, {'|', "" }, {'/', "-" },
            {'?', "" },{':', "" }, // Question Mark, Colon, Exclamation Mark
            {'[', "" }, {']', "" }, {'—', "-"}, // Brackets, Em Dash
            /* German Characters */
            {'ß', "ss" }, {'ä', "a" }, {'ö', "o" }, {'ü', "u" },
            {'Ä', "A" }, {'Ö', "O" }, {'Ü', "U" }, { 'ẞ', "SS" },
            /* French Characters */
            {'é', "e" }, {'è', "e" }, {'ê', "e" }, {'ë', "e" },
            {'ç', "c" }, {'à', "a" }, {'â', "a" }, {'î', "i" },
            {'ô', "o" }, {'ù', "u" }, {'û', "u" }, {'ÿ', "y" },
            /* Spanish Characters */
            {'ñ', "n" }, {'á', "a" }, {'í', "i" }, {'ó', "o" },
            {'ú', "u" }, {'¿', "" }, {'¡', "" },
            {'Á', "A" }, {'É', "E" }, {'Í', "I" }, {'Ó', "O" }, // Uppercase Spanish characters
            {'Ú', "U" }, {'Ñ', "N" },
            /* Greek Letters */
            {'Α', "alpha" }, {'Β', "beta" }, {'Γ', "gamma" }, {'Δ', "delta" },
            {'Ε', "epsilon" }, {'Ζ', "zeta" }, {'Η', "eta" }, {'Θ', "theta" },
            {'Ι', "iota" }, {'Κ', "kappa" }, {'Λ', "lambda" }, {'Μ', "mu" },
            {'Ν', "nu" }, {'Ξ', "xi" }, {'Ο', "omicron" }, {'Π', "pi" },
            {'Ρ', "rho" }, {'Σ', "sigma" }, {'Τ', "tau" }, {'Υ', "upsilon" },
            {'Φ', "phi" }, {'Χ', "chi" }, {'Ψ', "psi" }, {'Ω', "omega" },
            /* Scandinavian Characters */
            {'æ', "ae" }, {'ø', "o" }, {'œ', "oe" }, {'å', "a" },
            {'Æ', "Ae" }, {'Ø', "O" }, {'Œ', "Oe" }, {'Å', "A" },
            /* Miscellaneous Characters */
            {'π', "pi" }, {'°', "" }, {'ϴ', "theta" }, {'ϕ', "phi" },
            {'ϑ', "theta" }, {'ϖ', "pi" }, // Greek symbols
            /* Additional Characters */
            {'“', "" }, {'”', "" }, {'‘', "" }, {'’', "" }, // Smart quotes
            {'–', "-"}, // En Dash, Em Dash
            {'…', "..." }, // Ellipsis
            {'·', "-"}, // Middle Dot
            {'§', "section-"}, // Section symbol
            {'™', ""}, // Trademark symbol
            {'®', ""}, // Registered trademark symbol
            {'©', "" } // Copyright symbol
        };

        public static string GenerateSlug(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }
            // Replace special characters with their replacements
            foreach (var replacement in _slugReplacements)
            {
                name = name.Replace(replacement.Key.ToString(), replacement.Value);
            }
            // Remove any remaining unwanted characters
            name = new string(name.Where(c => char.IsLetterOrDigit(c) || c == '-' || c == '_').ToArray());
            // Convert to lowercase and trim any leading/trailing hyphens or underscores
            return name.ToLowerInvariant().Trim('-', '_');

        }

        public static string GenerateIdHash (int Id)
        {
            // Generates IDHash Here
            return hash;
        }

        public static string GenerateProductSlug(string name, DateOnly launchDate, int categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }
            if (categoryId == null || categoryId <= 0)
            {
                throw new ArgumentException("CatgoryID needed");
            }
            var slug = GenerateSlug(name);
            var categoryHash = GenerateIdHash(categoryId);
            return $"{slug}-{launchDate.Year}-{categoryHash}";
        }

        public static string GenerateFormatSlug(string type, string edition)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(type));
            }
            var slug = GenerateSlug(type);
            if (string.IsNullOrWhiteSpace(edition))
            {
                return $"{slug}";
            }
            var editionSlug = GenerateSlug(edition);
            return $"{slug}-{editionSlug}";
        }

        public class SlugUniqueAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
            {
                if (value is string slug && !string.IsNullOrWhiteSpace(slug))
                {
                    var generatedSlug = GenerateSlug(slug);
                    if (generatedSlug != slug)
                    {
                        return new ValidationResult($"The slug '{slug}' is not valid. Generated slug: '{generatedSlug}'.");
                    }
                }
                return ValidationResult.Success;
            }
        }
    }
}
