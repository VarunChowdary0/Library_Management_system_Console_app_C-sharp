namespace LibraryManagementSystem;

public class Record
{
    private Student student;
    private Book book;
    private string status;
    private DateTime date;
    
    public Student Student { get => student; set => student = value; }
    public Book Book { get => book; set => book = value; }
    public string Status { get => status; set => status = value; }
    public DateTime Date { get => date; set => date = value; }
    
    public Record(Student student, Book book)
    {
        this.student = student;
        this.book = book;
        this.status = "Not Returned";
        this.date = new DateTime();
    }
}