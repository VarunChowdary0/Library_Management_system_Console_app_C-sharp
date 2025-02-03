namespace LibraryManagementSystem;

public class BookTaken:Book
{
    private DateTime date;
    private string status;
    
    public string Status { get => status; set => status = value; }
    public DateTime Date { get => date; set => date = value; }
    public BookTaken(string title, string author, string publisher, int quantity,DateTime date,string status) 
    :base(title, author, publisher, quantity)
    {
        this.date = date;
        this.status = status;
    }
}