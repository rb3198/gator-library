using GatorLibrary.Structures.BinaryHeap;

namespace GatorLibrary
{
    public class Book : IComparable
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string AvailabilityStatus { get => BorrowedBy == 0 ? "Yes" : "No"; }
        public int BorrowedBy { get; set; } // Patron ID of the Patron in possession of the book
        public MinBinaryHeap<int> ReservationHeap { get; set; }

        public Book(int bookId, string bookName = "", string authorName = "", string availabilityStatus = "", int borrowedBy = 0)
        {
            BookId = bookId;
            BookName = bookName;
            AuthorName = authorName;
            BorrowedBy = borrowedBy;
            ReservationHeap = new(null);
        }

        public int CompareTo(object? obj)
        {
            if (obj is not Book comparedBook)
            {
                return -1;
            }
            return BookId - comparedBook.BookId;
        }

        public string BorrowBook(int patronId, int patronPriority)
        {
            if (BorrowedBy == 0)
            {
                // No one has borrowed this book yet.
                BorrowedBy = patronId;
                return $"Book {BookId} Borrowed by Patron {patronId}";
            }
            // Book already borrowed by someone. Add the patron to the reservation queue.
            ReservationHeap.Add(patronId, patronPriority);
            return $"Book {BookId} Reserved by Patron {patronId}";
        }

        public string ReturnBook(int patronId)
        {
            if (patronId != BorrowedBy)
            {
                throw new InvalidDataException("A patron cannot return a book that has not been borrowed by him/her.");
            }
            string output = $"Book {BookId} Returned by Patron {patronId}";
            if (ReservationHeap.GetRoot() == null)
            {
                BorrowedBy = 0;
                return output;
            }
            Node<int> firstPatronInQueue = ReservationHeap.RemoveMin();
            BorrowedBy = firstPatronInQueue.Data;
            output += $"\nBook {BookId} Allotted to Patron {BorrowedBy}";
            return output;
        }
    }
}