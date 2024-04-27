using AlloPageObjects;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace AlloTests
{
    public class AlloLibraryTests : BaseTest
    {
        [Test]
        public void VerifySelectFilterByGenreAndAuthor()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            string filterByGenre = config["filterByGenre"];
            string filterByAuthor = config["filterByAuthor"];


            InitialPage initialPage = new InitialPage(driver);
            initialPage.footer.SelectFooterLink(config["footerLink3"]);
            AlloLibraryPage alloLibraryPage = new AlloLibraryPage(driver);

            alloLibraryPage.SelectFilterByVariant(config["filterByGenre"]);
            alloLibraryPage.SelectFilterByVariant(config["filterByAuthor"]);

            List<string> bookListByGenre = alloLibraryPage.GetBooksNamesByGenre(config["filterByGenre"]);
            foreach (var bookGenre in bookListByGenre)
            {
                Assert.That(bookGenre, Is.EqualTo(filterByGenre), "Another genre is selected");
            }

            List<string> bookListByAuthor = alloLibraryPage.GetBooksNamesByAuthor(config["filterByAuthor"]);
            foreach (var bookAuthor in bookListByAuthor)
            {
                Assert.That(bookAuthor, Is.EqualTo(filterByAuthor), "Another author is selected");
            }
        }
    }
}
