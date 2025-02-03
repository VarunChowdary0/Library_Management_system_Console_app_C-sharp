namespace LibraryManagementSystem;

public class Book
{
    private string title;
    private string author;
    private string publisher;
    private int quantity;

    public string Title { get => title; set => title = value; }
    public string Author { get => author; set => author = value; }  
    public string Publisher { get => publisher; set => publisher = value; }
    public int Quantity { get => quantity; set => quantity = value; }
    
    public Book(string title, string author, string publisher, int quantity)
    {
        this.title = title;
        this.author = author;
        this.publisher = publisher;
        this.quantity = quantity;
    }
}