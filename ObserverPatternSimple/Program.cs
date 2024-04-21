using static System.Console;
using System;


public class Program
{
    public static void Main()
    {

        var questionRoom = new QuestionRoom();

        questionRoom.Attach(new Student { Name = "User1", category = "Student" });
        questionRoom.Attach(new Student { Name = "User2", category = "Student" });
        questionRoom.Attach(new Student { Name = "User3", category = "Teacher" });
        questionRoom.Attach(new Student { Name = "User4", category = "Teacher" });
        questionRoom.Attach(new Student { Name = "User5", category = "Teacher" });


        // questionRoom.Question("What is the capital of France1?", "Student");
        // questionRoom.Question("What is the capital of France2?", "Student");
        // questionRoom.Question("What is the capital of France3?", "Teacher");
        questionRoom.Question("What is the capital of France4?", "Teacher");

        Console.ReadKey();
    }
}

// Custom event arguments
public class ChangeEventArgs : EventArgs
{
    public string Question { get; set; } = null;
}




// Abstract Subject class
public class Room
{
    protected string question;

    public event EventHandler<ChangeEventArgs>? Change;
    public event EventHandler<ChangeEventArgs>? Change2;

    protected void OnChange(ChangeEventArgs e)
    {
        Change?.Invoke(this, e);
    }

    protected void OnChange2(ChangeEventArgs e)
    {
        Change2?.Invoke(this, e);
    }

    public void Attach(IStudent student)
    {
        if (student.Category() == "Student")
            Change += student.Update;
        else
            Change2 += student.Update;
    }

    public void Detach(IStudent student)
    {
        if (student.Category() == "Student")
            Change -= student.Update;
        else
            Change2 -= student.Update;
    }

    public string Question(string question, string category)
    {
        if (this.question != question)
        {
            this.question = question;
            if (category == "Student")
                OnChange(new ChangeEventArgs { Question = question });
            else
                OnChange2(new ChangeEventArgs { Question = question });
            Console.WriteLine();

        }

        return this.question;
    }
}

public class QuestionRoom : Room
{
    // No need to modify this class
}

public interface IStudent
{
    string Category();
    void Update(object? sender, ChangeEventArgs e);
}

public class Student : IStudent
{
    public string Name { get; set; } = null!;
    public string category { get; set; } = null!;
    public string Category()
    {
        return category;
    }

    public void Update(object sender, ChangeEventArgs e)
    {
        WriteLine(
            $"{category}: {Name} says he got question {e.Question}"
        );
    }
}
