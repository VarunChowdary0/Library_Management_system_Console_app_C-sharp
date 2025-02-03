namespace LibraryManagementSystem;

public class Student
{
    private string name;
    private string rollNumber;
    
    public string Name { get => name; set => name = value; }
    public string RollNumber { get => rollNumber; set => rollNumber = value; }

    public Student(string name, string rollNumber)
    {
        this.name = name;
        this.rollNumber = rollNumber;
    }
}