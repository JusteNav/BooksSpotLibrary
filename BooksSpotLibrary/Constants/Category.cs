using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace BooksSpotLibrary.Constants
{
    public static class BookCategory
    {
        private static readonly string NonFiction = "Non-fiction";
        private static readonly string ForChildren = "For Children";
        private static readonly string ForTeens = "For Teens";
        private static readonly string Fantasy = "Fantasy";
        private static readonly string Horror = "Horror";
        private static readonly string Drama = "Drama";
        private static readonly string Romance = "Romance";
        private static readonly string Biography = "Biography";
        private static readonly string Crime = "Crime";
        private static readonly string Poetry = "Poetry";

        public static readonly string[] Categories = { NonFiction, ForChildren, ForTeens, 
            Fantasy, Horror, Drama, Romance, Biography, Crime, Poetry};

    }
}
