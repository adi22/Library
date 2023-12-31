﻿using Library.DataAccess.Data.Entities;
using Library.DataAccess.Data.Repositories;

namespace Library.ApplicationServices.Components.DataProviders
{
    public class BooksProvider<TRepository> : IBooksProvider<TRepository> 
        where TRepository : class, IRepository<Book>
    {
        private readonly TRepository _booksRepository;

        public BooksProvider(
            TRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public IEnumerable<string> GetUniqueAuthors()
        {
            var books = _booksRepository.GetAll();
            return books
            .OrderBy(x => x.Author)
            .Select(b => b.Author)
            .Distinct()
            .ToList();
        }

        public int GetMaximalLengthFromAllBooks()
        {
            var books = _booksRepository.GetAll();
            return books.Select(x => x.Length).Max();
        }

        public IEnumerable<Book> OrderByTitle()
        {
            var books = _booksRepository.GetAll();
            return books.OrderBy(x => x.Title).ToList();
        }

        public double GetAverageRatingOfAllBooks()
        {
            var books = _booksRepository.GetAll();
            return Math.Round(books.Select(x => x.Rating).Average(), 2);
        }

        public IEnumerable<Book> ResultsToPage(int page)
        {
            var books = _booksRepository.GetAll();

            if (IsEnteredPageInRepositoryRange(page))
            {
                return books
                    .OrderBy(x => x.Title)
                    .Skip((page - 1) * 100)
                    .Take(100)
                    .ToList();
            }
            else
            {
                throw new Exception("Page is out of range");
            }

        }

        private bool IsEnteredPageInRepositoryRange(int enteredPage)
        {
            var lastPage = (_booksRepository.GetAll().Count() - 1) / 100 + 1;

            if (enteredPage <= lastPage)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}