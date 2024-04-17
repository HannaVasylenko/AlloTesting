﻿using AlloTesting;
using Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlloPageObjects
{
    public class AlloLibraryPage : BasePage
    {
        public AlloLibraryPage(Driver webDriver) : base(webDriver) { }

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

        public List<string> GetBooksNamesByGenre(string genre)
        {
            List<string> books = new List<string>();

            driver.WaitUntil(e =>
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
            });
            return books;
        }

        public List<string> GetBooksNamesByAuthor(string author)
        {
            List<string> books = new List<string>();
            driver.WaitUntil(e =>
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
            });
            return books;
        }
    }
}