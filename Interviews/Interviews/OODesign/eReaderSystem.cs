using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews.OODesign
{
    // Design the classes for an eReader system

    enum Genre
    {
        Fiction,
        NonFiction
    }

    public class Book
    {
        // All have public gets, private sets.
        public Author author;
        //public Genre _genre;
        public string Title;
        public Metadata metadata;
        public Contents contents;


    }

    public class ISBN
    {
        Guid _ISBN;
    }

    public class Library
    {
        private Dictionary<ISBN, Book> ISBNDictionary;

        private Dictionary<Author, List<ISBN>> AuthorDictionary;

        //etc.

        public List<Book> GetBooksByAuthor(Author author)
        {
            throw new NotImplementedException();
        }
    }
    
    public class Contents
    {
        List<Chapter> Chapters;
    }

    public class Metadata
    {
        //Tons of string entires, such as publisher.
    }

    public class Author
    {
        public string bio;
        public string imgpath;
    }

    public class Picture
    {
        public string path;
        public int width;
        public int height;
        //etc.
    }

    public class Chapter
    {
        string title;
        int number;
        string subTitle;

        string body;
    }

    // Entities: Book, Author, Genre.  Need to be able to sort by each one
    // Reading Experience: Chapters, Text, Pictures


}
