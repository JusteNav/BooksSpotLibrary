using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace BooksSpotLibrary.Constants
{
    public static class Publisher
    {
        private static readonly string Publisher1 = "Kitten Publishing Group";
        private static readonly string Publisher2 = "Melodrama Publishers";
        private static readonly string Publisher3 = "Wonderland Productions";
        private static readonly string Publisher4 = "Three Rivers LtD";
        private static readonly string Publisher5 = "Serious Books Only";

        public static readonly string[] Publishers = { Publisher1, Publisher2, Publisher3, Publisher3, Publisher4, Publisher5 };
    }
}
