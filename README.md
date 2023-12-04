# Gator Library

A C# Implementation of Gator Library, a virtual library managing its book storage and reservations using Red Black Trees and Heaps.

Each node in the tree represents a book and has the following structure:

- **BookId** - Integer ID
- **BookName** - Name of the book
- **AuthorName** - Name of the Author
- **AvailabilityStatus** - To indicate whether it is currently borrowed
- **BorrowedBy** - ID of the Patron who borrowed the book
- **ReservationHeap**: Binary Min-heap for managing book reservations and waitlists for the book ordered by the patron’s priority which is an integer. Priority 1 has precedence over Priority 2 and so on. Ties are broken by considering the timestamp at which the reservation was made (first come first serve basis). Every node of the Min-heap contains (patronID, priorityNumber, timeOfReservation)

## Supported Operations

1. **PrintBook(bookID):** Prints information about a specific book identified by its unique bookID.
2. **PrintBooks(bookID1, bookID2):** Prints information about all books with bookIDs in the range [bookID1, bookID2].
3. **InsertBook(bookID, bookName, authorName, availabilityStatus, borrowedBy):** Adds a new book to the library.
4. **BorrowBook(patronID, bookID, patronPriority):** Allows a patron to borrow a book that is available
   and update the status of the book. If a book is currently unavailable, creates a reservation node in the heap
   as per the patron’s priority (patronPriority).<br>
   _Note: It is assumed that a book will not be borrowed by same patron twice._
5. **ReturnBook(patronID, bookID):** Allows a patron to return a borrowed book. Updates the book's status and assigns the book to the patron with highest priority in the Reservation Heap. (If there’s a reservation).
6. **DeleteBook(bookID):** Deletes the book from the library and notifies the patrons in the reservation list that the book is no longer available to borrow.
7. **FindClosestBook(targetID) :** Finds the book with an ID closest to the given ID (checking on both sides
   of the ID). Prints all the details about the book. In case of ties, prints both the books ordered by bookIDs.
8. **ColorFlipCount():** GatorLibrary's Red-Black tree structure requires an analytics tool to monitor and analyze the frequency of color flips in the Red-Black tree. This function helps track the occurrence of color changes in the Red-Black tree nodes during tree operations, such as insertion, deletion, and rotations.

## Running the Project on Your Machine

1. Install .NET Framework on your machine.
2. Setup the repo on your machine by running the following command: `git clone https://github.com/rb3198/gator-library.git`
3. Run `dotnet build`
4. Run `dotnet run <input_file_name>`
   Note: `input_file` should be in the `/inputs` directory w.r.t. the root of the project.

## Running the Project on Your Machine using Docker

1. Run `docker pull ronitbhatia98/uf:gatorlibrary` to download the image.
2. Create two directories for storing your inputs and outputs, respectively.
3. Run the image using `docker run -v <absolute_path_to_input_directory>:/gatorLibrary/inputs -v <absolute_path_to_output_directory>:/gatorLibrary/outputs ronitbhatia98/uf:gatorlibrary <input_file_name>`

Check your output directory: An output should be written in a file named `<input_file_name>_output.txt`

## Implementation Details

1. Data-agnostic Red Black Tree & Min Binary Heap structures are implemented in the `structures` folder. The Nodes can have data of any type `T`.
2. Next, classes & objects to implement the library, namely `Book` & `Library` itself, are created inside the `library` folder.
   - Book stores a reservation heap, which is a `MinBinaryHeap<int>`, since the data stored in each heap node is patron ID.
   - Library stores a Red Black Tree of Books, i.e. `RedBlackTree<Book>`, since the data stored in each node of the red black tree is a book.
3. The Program takes an input file, maps it to commands, and executes the appropriate function with the args provided.
