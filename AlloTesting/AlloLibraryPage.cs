using AlloTesting;
using Framework;
using OpenQA.Selenium;

namespace AlloPageObjects
{
    /// <summary>
    /// The class displays a separate page with the title Allo Library. 
    /// The user can select an audiobook by filtering (by genre, author, and artist)
    /// </summary>
    public class AlloLibraryPage : BasePage
    {
        public AlloLibraryPage(Driver webDriver) : base(webDriver) { }

        /// <summary>
        /// Selects a filter by its variant on the Allo Library page.
        /// </summary>
        /// <param name="filterVariant">The variant of the filter (genre, author and actor) to be selected.</param>
        public void SelectFilterByVariant(string filterVariant)
        {
            Element filterName = driver.FindElementByXpath($"//div[contains(@class,'Filters_content')]/div//span[text()='{filterVariant}']");
            driver.WaitUntil(e =>
            {
                try
                {
                    filterName.Click();
                    return true;
                }
                catch (ElementClickInterceptedException ex)
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// Retrieves the names of books by a specified genre on the Allo Library page.
        /// </summary>
        /// <param name="genre">The genre of books to retrieve.</param>
        /// <returns>A list of book names matching the specified genre.</returns>
        public List<string> GetBooksNamesByGenre(string genre)
        {
            List<string> books = new List<string>();
            driver.WaitUntil(e =>
            {
                try
                {
                    books = driver.FindElementsByXpath("//div[contains(@class, 'BookItem_card')]//div[contains(@class, 'BookItem_genre')]")
                        .ToList()
                        .ConvertAll(e => e.GetText());
                    foreach (var el in books)
                    {
                        if (!genre.Equals(el))
                        {
                            return false;
                        }
                    }
                    return true;
                } catch (StaleElementReferenceException ex)
                {
                    return false;
                }
            });
            return books;
        }

        /// <summary>
        /// Retrieves the names of books by a specified author on the Allo Library page.
        /// </summary>
        /// <param name="author">The author of the books to retrieve.</param>
        /// <returns>A list of book names written by the specified author.</returns>
        public List<string> GetBooksNamesByAuthor(string author)
        {
            List<string> books = new List<string>();
            driver.WaitUntil(e =>
            {
                try
                {
                    books = driver.FindElementsByXpath("//div[contains(@class, 'BookItem_caption')]/child::*[@data-testid='PersonOutlineOutlinedIcon']/following-sibling::*")
                        .ToList()
                        .ConvertAll(e => e.GetText());
                    foreach (var el in books)
                    {
                        if (!author.Equals(el))
                        {
                            return false;
                        }
                    }
                    return true;
                } catch (StaleElementReferenceException ex)
                {
                    return false;
                }
            });
            return books;
        }
    }
}
