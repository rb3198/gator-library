using GatorLibrary.Structures.RedBlackTree;

namespace GatorLibrary
{
    public class Library
    {
        private RedBlackTree<Book> BookTree { get; set; }

        public Library()
        {
            BookTree = new RedBlackTree<Book>(null);
        }

        public string PrintBook(int bookId)
        {
            try
            {
                Node<Book> bookNode = BookTree.SearchTree(new Book(bookId));
                Book book = bookNode.Data;
                string borrowedBy = book.BorrowedBy == 0 ? "None" : book.BorrowedBy.ToString();
                return @$"
                BookID = {book.BookId}
                Title = {book.BookName}
                Author = {book.AuthorName}
                Availability = {book.AvailabilityStatus}
                BorrowedBy = {borrowedBy}
                Reservations = {book.ReservationHeap.GetHeapData()}
                ";
            }
            catch (KeyNotFoundException)
            {
                return "BookID not found in the Library.";
            }
            catch (ApplicationException)
            {
                return "BookID not found in the Library.";
            }
        }

        private static string PrintBook(Book book)
        {
            string borrowedBy = book.BorrowedBy == 0 ? "None" : book.BorrowedBy.ToString();
            return @$"
            BookID = {book.BookId}
            Title = {book.BookName}
            Author = {book.AuthorName}
            Availability = {book.AvailabilityStatus}
            BorrowedBy = {borrowedBy}
            Reservations = {book.ReservationHeap.GetHeapData()}
            ";
        }

        public string PrintBooks(int bookId1, int bookId2)
        {
            try
            {
                List<Node<Book>> books = BookTree.SearchTree(new Book(bookId1), new Book(bookId2));
                if (books.Count == 0)
                {
                    return "No Books in the specified range.";
                }
                string output = string.Empty;
                books.ForEach((bookNode) =>
                {
                    output += $"{PrintBook(bookNode.Data)}\n";
                });
                return output;
            }
            catch (KeyNotFoundException)
            {
                return "No Books in the specified range.";
            }
            catch (ApplicationException)
            {
                return "No Books in the specified range.";
            }
        }

        public string InsertBook(int bookId, string bookName, string authorName, string availabilityStatus, int borrowedBy = 0)
        {
            try
            {
                Book book = new(bookId, bookName, authorName, availabilityStatus, borrowedBy);
                BookTree.Insert(book);
                return ".";
            }
            catch (InvalidDataException)
            {
                return $"Book with ID {bookId} already exists.";
            }
        }

        public string BorrowBook(int patronId, int bookId, int patronPriority)
        {
            try
            {
                Node<Book> bookNode = BookTree.SearchTree(new Book(bookId));
                return bookNode.Data.BorrowBook(patronId, patronPriority);
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException || ex is KeyNotFoundException)
                {
                    return $"Book with ID {bookId} not found in the library.";
                }
                throw;
            }
        }

        public string ReturnBook(int patronId, int bookId)
        {
            try
            {
                Node<Book> bookNode = BookTree.SearchTree(new Book(bookId));
                return bookNode.Data.ReturnBook(patronId);
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException || ex is KeyNotFoundException)
                {
                    return $"Book with ID {bookId} not found in the library.";
                }
                throw;
            }
        }

        public string DeleteBook(int bookId)
        {
            try
            {
                Book deletedBook = BookTree.Delete(new Book(bookId));
                string output = $"Book {bookId} is no longer available.";
                if (deletedBook.ReservationHeap.GetRoot() != null)
                {
                    output += $" Reservations made by patrons ";
                    List<int> reservedPatrons = new();
                    while (deletedBook.ReservationHeap.GetRoot() != null)
                    {
                        reservedPatrons.Add(deletedBook.ReservationHeap.RemoveMin().Data);
                    }
                    output += string.Join(", ", reservedPatrons);
                    output += " have been cancelled!";
                }
                return output;
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException || ex is ApplicationException)
                {
                    return $"Book with ID {bookId} not found.";
                }
                throw;
            }
        }

        public string FindClosestBook(int bookId)
        {
            List<Book> closestMatchBooks = BookTree.SearchClosest(new Book(bookId));
            string output = string.Empty;
            closestMatchBooks.ForEach(book =>
            {
                output += $"{PrintBook(book)}\n";
            });
            return output;
        }

        public string ColorFlipCount()
        {
            return $"Color Flip Count: {BookTree.FlipCount}";
        }
    }
}