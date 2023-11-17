using GatorLibrary;


void TestCase1()
{
    Library gatorLibrary = new();
    Console.WriteLine(gatorLibrary.InsertBook(101, "Introduction to Algorithms", "Thomas H. Cormen", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(48, "Data Structures and Algorithms", "Sartaj Sahni", "Yes"));
    Console.WriteLine(gatorLibrary.PrintBook(48));
    Console.WriteLine(gatorLibrary.InsertBook(132, "Operating System Concepts", "Abraham Silberschatz", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(25, "Computer Networks", "Andrew S. Tanenbaum", "Yes"));
    Console.WriteLine(gatorLibrary.BorrowBook(120, 48, 2));
    Console.WriteLine(gatorLibrary.BorrowBook(132, 101, 1));
    Console.WriteLine(gatorLibrary.InsertBook(73, "Introduction to the Theory of Computation", "Michael Sipser", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(12, "Artificial Intelligence: A Modern Approach", "Stuart Russell", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(6, "Database Management Systems", "Raghu Ramakrishnan", "Yes"));
    Console.WriteLine(gatorLibrary.BorrowBook(144, 48, 3));
    Console.WriteLine(gatorLibrary.BorrowBook(140, 48, 3));
    Console.WriteLine(gatorLibrary.BorrowBook(142, 48, 2));
    Console.WriteLine(gatorLibrary.BorrowBook(138, 12, 4));
    Console.WriteLine(gatorLibrary.BorrowBook(150, 12, 3));
    Console.WriteLine(gatorLibrary.BorrowBook(162, 12, 1));
    Console.WriteLine(gatorLibrary.ReturnBook(120, 48));
    Console.WriteLine(gatorLibrary.FindClosestBook(9));
    Console.WriteLine(gatorLibrary.DeleteBook(12));
    Console.WriteLine(gatorLibrary.ColorFlipCount());
    Console.WriteLine(gatorLibrary.InsertBook(125, "Computer Organization and Design", "David A. Patterson", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(180, "Introduction to Software Engineering", "Ian Sommerville", "Yes"));
    Console.WriteLine(gatorLibrary.BorrowBook(111, 73, 3));
    Console.WriteLine(gatorLibrary.BorrowBook(52, 73, 1));
    Console.WriteLine(gatorLibrary.InsertBook(115, "Operating Systems: Internals and Design Principles", "William Stallings", "Yes"));
    Console.WriteLine(gatorLibrary.BorrowBook(153, 25, 2));
    Console.WriteLine(gatorLibrary.PrintBooks(10, 150));
    Console.WriteLine(gatorLibrary.InsertBook(210, "Machine Learning: A Probabilistic Perspective", "Kevin P.Murphy", "Yes"));
    Console.WriteLine(gatorLibrary.BorrowBook(171, 25, 3));
    Console.WriteLine(gatorLibrary.BorrowBook(2, 132, 2));
    Console.WriteLine(gatorLibrary.FindClosestBook(50));
    Console.WriteLine(gatorLibrary.BorrowBook(18, 101, 2));
    Console.WriteLine(gatorLibrary.InsertBook(80, "Software Engineering: A Practitioner's Approach", "Roger S.Pressman", "Yes"));
    Console.WriteLine(gatorLibrary.BorrowBook(210, 210, 1));
    Console.WriteLine(gatorLibrary.BorrowBook(43, 73, 1));
    Console.WriteLine(gatorLibrary.InsertBook(60, "Introduction to Computer Graphics", "David F. Rogers", "Yes"));
    Console.WriteLine(gatorLibrary.PrintBook(210));
    Console.WriteLine(gatorLibrary.InsertBook(4, "Design Patterns: Elements of Reusable Object-Oriented Software", "Erich Gamma", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(2, "Introduction to the Theory of Computation", "Michael Sipser", "Yes"));
    Console.WriteLine(gatorLibrary.BorrowBook(34, 210, 2));
    Console.WriteLine(gatorLibrary.InsertBook(65, "Computer Networks: Principles, Protocols, and Practice", "Olivier Bonaventure", "Yes"));
    Console.WriteLine(gatorLibrary.ColorFlipCount());
    Console.WriteLine(gatorLibrary.DeleteBook(125));
    Console.WriteLine(gatorLibrary.DeleteBook(115));
    Console.WriteLine(gatorLibrary.DeleteBook(210));
    Console.WriteLine(gatorLibrary.ColorFlipCount());
    Console.WriteLine(gatorLibrary.DeleteBook(25));
    Console.WriteLine(gatorLibrary.DeleteBook(80));
    Console.WriteLine(gatorLibrary.ColorFlipCount());
}

void TestCase2()
{
    Library gatorLibrary = new();
    Console.WriteLine(gatorLibrary.InsertBook(5, "The Secret Garden", "Jane Smith", "Yes"));
    Console.WriteLine(gatorLibrary.BorrowBook(101, 5, 2));
    Console.WriteLine(gatorLibrary.InsertBook(3, "The Great Gatsby", "Mark Johnson", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(12, "To Kill a Mockingbird", "Sarah Lee", "Yes"));
    Console.WriteLine(gatorLibrary.BorrowBook(101, 3, 2));
    Console.WriteLine(gatorLibrary.DeleteBook(12));
    Console.WriteLine(gatorLibrary.PrintBooks(1, 10));
    Console.WriteLine(gatorLibrary.DeleteBook(3));
    Console.WriteLine(gatorLibrary.DeleteBook(5));
    Console.WriteLine(gatorLibrary.PrintBook(5));
    Console.WriteLine(gatorLibrary.InsertBook(50, "The Catcher in the Rye", "Michael Brown", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(22, "The Alchemist", "Paul Coelho", "Yes"));
    Console.WriteLine(gatorLibrary.BorrowBook(104, 22, 3));
    Console.WriteLine(gatorLibrary.BorrowBook(171, 22, 1));
    Console.WriteLine(gatorLibrary.BorrowBook(103, 22, 2));
    Console.WriteLine(gatorLibrary.InsertBook(10, "The Hobbit", "J.R.R. Tolkien", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(72, "Brave New World", "Aldous Huxley", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(94, "1984", "George Orwell", "Yes"));
    Console.WriteLine(gatorLibrary.ColorFlipCount());
    Console.WriteLine(gatorLibrary.BorrowBook(171, 94, 1));
    Console.WriteLine(gatorLibrary.ReturnBook(104, 22));
    Console.WriteLine(gatorLibrary.BorrowBook(132, 50, 3));
    Console.WriteLine(gatorLibrary.BorrowBook(103, 10, 2));
    Console.WriteLine(gatorLibrary.InsertBook(28, "Lord of the Flies", "William Golding", "Yes"));
    Console.WriteLine(gatorLibrary.BorrowBook(109, 72, 4));
    Console.WriteLine(gatorLibrary.BorrowBook(101, 50, 2));
    Console.WriteLine(gatorLibrary.DeleteBook(94));
    Console.WriteLine(gatorLibrary.BorrowBook(105, 22, 1));
    Console.WriteLine(gatorLibrary.PrintBooks(5, 100));
    Console.WriteLine(gatorLibrary.FindClosestBook(26));
    Console.WriteLine(gatorLibrary.ReturnBook(171, 22));
    Console.WriteLine(gatorLibrary.BorrowBook(171, 28, 1));
    Console.WriteLine(gatorLibrary.DeleteBook(50));
    Console.WriteLine(gatorLibrary.ColorFlipCount());
    Console.WriteLine(gatorLibrary.BorrowBook(107, 22, 2));
    Console.WriteLine(gatorLibrary.ReturnBook(103, 10));
    Console.WriteLine(gatorLibrary.BorrowBook(121, 10, 3));
    Console.WriteLine(gatorLibrary.DeleteBook(10));
    Console.WriteLine(gatorLibrary.DeleteBook(22));
    Console.WriteLine(gatorLibrary.ColorFlipCount());
}

void TestCase3()
{
    Library gatorLibrary = new();
    Console.WriteLine(gatorLibrary.InsertBook(1, "Book1", "Author1", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(2, "Book2", "Author2", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(3, "Book3", "Author3", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(4, "Book4", "Author4", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(5, "Book5", "Author5", "Yes"));
    Console.WriteLine(gatorLibrary.BorrowBook(102, 2, 2));
    Console.WriteLine(gatorLibrary.BorrowBook(103, 3, 3));
    Console.WriteLine(gatorLibrary.InsertBook(6, "Book6", "Author6", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(7, "Book7", "Author7", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(8, "Book8", "Author8", "Yes"));
    Console.WriteLine(gatorLibrary.BorrowBook(104, 4, 4));
    Console.WriteLine(gatorLibrary.BorrowBook(105, 5, 5));
    Console.WriteLine(gatorLibrary.ReturnBook(102, 2));
    Console.WriteLine(gatorLibrary.ReturnBook(103, 3));
    Console.WriteLine(gatorLibrary.ReturnBook(104, 4));
    Console.WriteLine(gatorLibrary.ReturnBook(105, 5));
    Console.WriteLine(gatorLibrary.BorrowBook(101, 6, 1));
    Console.WriteLine(gatorLibrary.BorrowBook(102, 7, 2));
    Console.WriteLine(gatorLibrary.BorrowBook(103, 8, 3));
    Console.WriteLine(gatorLibrary.InsertBook(9, "Book9", "Author9", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(10, "Book10", "Author10", "Yes"));
}

void TestCase4()
{
    Library gatorLibrary = new();
    Console.WriteLine(gatorLibrary.InsertBook(1, "Book1", "Author1", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(2, "Book2", "Author2", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(3, "Book3", "Author3", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(4, "Book4", "Author4", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(5, "Book5", "Author5", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(6, "Book6", "Author6", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(7, "Book7", "Author7", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(8, "Book8", "Author8", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(9, "Book9", "Author9", "Yes"));
    Console.WriteLine(gatorLibrary.InsertBook(10, "Book10", "Author10", "Yes"));
    Console.WriteLine(gatorLibrary.BorrowBook(101, 1, 1));
    Console.WriteLine(gatorLibrary.BorrowBook(102, 2, 2));
    Console.WriteLine(gatorLibrary.BorrowBook(103, 3, 3));
    Console.WriteLine(gatorLibrary.BorrowBook(104, 4, 4));
    Console.WriteLine(gatorLibrary.BorrowBook(105, 5, 5));
    Console.WriteLine(gatorLibrary.ReturnBook(101, 1));
    Console.WriteLine(gatorLibrary.BorrowBook(106, 1, 1));
    Console.WriteLine(gatorLibrary.BorrowBook(107, 2, 1));
    Console.WriteLine(gatorLibrary.BorrowBook(108, 3, 1));
    Console.WriteLine(gatorLibrary.BorrowBook(109, 4, 1));
    Console.WriteLine(gatorLibrary.BorrowBook(110, 5, 1));
    Console.WriteLine(gatorLibrary.ReturnBook(102, 2));
    Console.WriteLine(gatorLibrary.DeleteBook(1));
    Console.WriteLine(gatorLibrary.FindClosestBook(10));
    Console.WriteLine(gatorLibrary.InsertBook(11, "Book11", "Author11", "Yes"));
    Console.WriteLine(gatorLibrary.ReturnBook(103, 3));
    Console.WriteLine(gatorLibrary.BorrowBook(112, 11, 1));
    Console.WriteLine(gatorLibrary.DeleteBook(11));
}

TestCase2();