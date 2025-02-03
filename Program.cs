using LibraryManagementSystem;

class Program
{
    public static int id = 242423;
    public static Dictionary<int, Record> records = new Dictionary<int, Record>();
    public static List<Book> books = new List<Book>(){
        new Book("The Catcher in the Rye", "J.D. Salinger", "Little, Brown and Company", 277),
        new Book("To Kill a Mockingbird", "Harper Lee", "J.B. Lippincott & Co.", 281),
        new Book("1984", "George Orwell", "Secker & Warburg", 328),
        new Book("The Great Gatsby", "F. Scott Fitzgerald", "Charles Scribner's Sons", 180),
        new Book("Moby-Dick", "Herman Melville", "Harper & Brothers", 635)};
    private static List<Student> students = new List<Student>()
    {
        new Student("sai varun","22951a05g8"),
        new Student("sai teja","22951a05g6"),
        new Student("sai vamsi","22951a05g7"),
        new Student("revan","22951a05e5"),
        new Student("shiva karthik","22951a05j7"),
    };
    
    private static string takeSting(string title)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write(title);
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Blue;
        string res = Console.ReadLine();
        Console.ResetColor();
        return res;
    }    
    
    private static int takeInt(string title)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write(title);
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Magenta;
        int res = int.Parse(Console.ReadLine());
        Console.ResetColor();
        return res;
    }
    private static void PrintError(string Err)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(Err);
        Console.ResetColor();
    }

    // check's if the student have the given un-returned book.
    private static bool HasBook(Student student, Book book)
    {
        foreach (var d in records.Keys)
        {
            if (records[d].Student == student &&
                records[d].Book.Title == book.Title &&
                records[d].Status == "Not Returned")
            {
                return true;
            }
        }
        return false;
    }
    
    // give's a book to student;
    private static void GiveBook()
    {
        Title("Give Book");
        string bookName = takeSting("Enter book name: ");
        Book finding = books.Find(x => x.Title == bookName);
        if ( finding == null)
        {
            PrintError("There is no book with the title!");
        }
        else
        {
            if (finding.Quantity == 0)
            {
                PrintError($"The Book {finding.Title} is out of stock!");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("There is a book with the title: " + bookName);
            Console.ForegroundColor = ConsoleColor.Cyan;
            string studentRollNumber = takeSting("Enter student Roll Number: ");
            Student studentFound = students.Find(s => s.RollNumber == studentRollNumber);
            if (studentFound != null)
            {
                Console.WriteLine($"Welcome {studentFound.Name} : {studentFound.RollNumber}");
                string input = takeSting("Take Book (y or n): ");
                if (input != "n")
                {
                    if (HasBook(studentFound, finding))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Student Already has an un-returned  {bookName}");
                        Console.ResetColor();
                        return;
                    }
                    Record transaction = new Record(studentFound, finding);
                    finding.Quantity -= 1;
                    records.Add(id, transaction);

                    if (records.TryGetValue(id, out Record record))
                    {
                        Console.Write("Book ");
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(transaction.Book.Title);
                        Console.ResetColor();
                        Console.Write(" to -> ");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine(transaction.Student.Name);
                        Console.ResetColor();
                        id+=new Random().Next(1,10);
                    }
                    else
                    {
                        PrintError("Book transcation failed!");
                    }
                }
                else
                {
                    Console.WriteLine("Exiting ........");
                    return;
                }
            }
            else
            {
                PrintError("There is no student with that RollNumber");
                return;
            }
        }
    }

    private static void TakeBook()
    {
        Title("Return Book");
        string studentRoll = takeSting("Student Roll Number: ");
        List<Book> taken = new List<Book>();
        foreach (var id in records.Keys)
        {
            if (records[id].Student.RollNumber == studentRoll && records[id].Status == "Not Returned")
            {
                taken.Add(records[id].Book);
            }
        }

        if (taken.Count == 0)
        {
            PrintError("There is no book with that Roll Number");
        }
        else
        {
            Console.Write($"Books with {studentRoll} : ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(string.Join(", ", new List<string>(taken.Select(dfo=>dfo.Title))));
            Console.ResetColor();
            string bookReturn = takeSting("Title of Book returning: ");
            Book bookFound = books.Find(x => x.Title == bookReturn);
            if (bookFound != null)
            {
                foreach (var dfo in records.Keys)
                {
                    if (records[dfo].Student.RollNumber == studentRoll && records[dfo].Book == bookFound)
                    {
                        records[dfo].Status = "Returned";
                        books.Find(bjs => bjs.Title == bookFound.Title).Quantity += 1;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{bookFound.Title} Returned to the Library...");
                        Console.ResetColor();
                    }
                }
            }
            else
            {
                PrintError($"There is no {bookReturn} with  {studentRoll}");
            }
        }
    }
    private static void Title(string title)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n---------------------------------- {title} -------------------------------------\n");
        Console.ResetColor();
    }
    
    // Display's Student book transactions.
    private static void SeeStudentTransactions(Student student,bool OnlyNotReturned)
    {
        Title("Given Books to "+student.Name);
        List<BookTaken> booksTaken = new List<BookTaken>();
        foreach (var record in records.Keys)
        {
            if (records[record].Student == student && ( OnlyNotReturned ? records[record].Status == "Not Returned" : true ))
            {
                booksTaken.Add(
                    new BookTaken(
                        records[record].Book.Title,
                        records[record].Book.Author,
                        records[record].Book.Publisher,
                        records[record].Book.Quantity,
                        records[record].Date,
                        records[record].Status
                    )
                );
            }
        }

        string l1 = "|------------------------------------------------------------------|";
        string l2 = "|--------------------------------------------------------------------------------------------|";

        if (booksTaken.Count > 0)
        {
            Console.WriteLine(OnlyNotReturned?"Pending Books ":"All Books");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(OnlyNotReturned ? l1 : l2);
            Console.WriteLine($"Name: {formater(student.Name,37)} Roll Number: {student.RollNumber}");
            Console.WriteLine(OnlyNotReturned ? l1 : l2);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            booksTaken.ForEach(bk => Console.Write($"Title: {formater(bk.Title,30)}" +
                                                   $" Date: {bk.Date}{(!OnlyNotReturned?$"\tStatus: {bk.Status}":"")}. \n"));
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(OnlyNotReturned ? l1 : l2);
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"No books");
            Console.ResetColor();
        }
        
    }

    private static string formater(string S,int n)
    {
        if (S.Length > n)
        {
            return S.Substring(0, n-4)+"... ";
        }
        else
        {
            while (n > S.Length)
            {
                S += " ";
            }
        }

        return S;
    }
    private static void DiaplayBook(Book book)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"{formater(book.Title, 30)}" +
                          $"{formater(book.Author, 30)}" +
                          $"{formater(book.Publisher, 30)}" +
                          $"{book.Quantity}");
        Console.ResetColor();
    }

    private static void DisplayStudent(Student student)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"{formater(student.Name, 30)}" +
                          $"{student.RollNumber}");
    }
    private static void DispalyAllBooks()
    {
        Title("<------- Inventory ------->");
        PrintError($"{formater("Title",30)}{formater("Author",30)}{formater("Publisher",30)}Stock-Left\n");
        books.ForEach(B=> DiaplayBook(B));
    }

    private static void DisplayllStudents()
    {
        Title("All Students");
        PrintError($"{formater("Name",30)} Roll Number\n");
        students.ForEach(x => DisplayStudent(x));
    }
    

    private static void AddBook()
    {
        Title("Add new book");
        string title = takeSting("tile of the book: ");

        if (books.Exists(b => b.Title == title))
        {
            PrintError("Book already exists");
        }
        else
        {
            string author = takeSting($"author of {title}: ");
            string publisher = takeSting($"publisher of {title}: ");
            int quantity = takeInt("quantity of the book: ");
            
            books.Add(new Book(title, author, publisher, quantity));
        }
    }
    // Display's book data.
    private static void AddStudent()
    {
        Title("Add new student");
        string name = takeSting("name : ");
        string rollNumber = takeSting("Roll number : ");

        if (students.Exists(b => b.RollNumber == rollNumber))
        {
            PrintError("Student already exists");
        }
        else
        {
            students.Add(new Student(name, rollNumber));
            Console.WriteLine($"Name: {name} ,Roll Number: {rollNumber} Added Successfully....");
        }
    }
    private static void StudentTransactions()
    {
        string studentRollNumber = takeSting("Enter student roll number: ");
        Student s1 = students.Find(stu => stu.RollNumber == studentRollNumber);
        string pending = takeSting("All books ever taken(n) || Pending Books (y) : ");
        if (s1 != null)
        {
            SeeStudentTransactions(s1,OnlyNotReturned: pending=="y");
        }
        else
        {
            PrintError("There is no student with that Roll Number");
        }
        
    }

    private static void PendingBooks()
    {
        Title("Pending books");
        foreach (var s in records.Values)
        {
            SeeStudentTransactions(s.Student, OnlyNotReturned: true);
        }
    }
    static void Main(string[] args)
    {   
        Title($"Welcome to the Library Management System! {new DateTime()}");
        bool running = true;
        while (running)
        {
            Console.WriteLine("1. Add Book" +
                              "\n2. Add Student" +
                              "\n3. Give Book" +
                              "\n4. Take back Book" +
                              "\n5. Inventory" +
                              "\n6. Display Students" +
                              "\n7. Student Book transaction" +
                              "\n8. Pending Books" +
                              "\n9. Exit");
            int input = takeInt("Enter option: ");

            switch (input)
            {
                case 1:
                    AddBook();
                    break;
                case 2:
                    AddStudent();
                    break;
                case 3:
                    GiveBook();
                    break;
                case 4:
                    TakeBook();
                    break;
                case 5:
                    DispalyAllBooks();
                    break;
                case 6:
                    DisplayllStudents();
                    break;
                case 7:
                    StudentTransactions();  
                    break;
                case 8:
                    PendingBooks();
                    break;
                case 9:
                    return;
                default:
                    break;
            }

            if (takeSting("\nClear Console ? No -> n :  >>  ") != "n")
            {
                Console.Clear();
            }
        }
    }
}