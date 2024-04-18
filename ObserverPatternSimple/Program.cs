namespace Observer.NetOptimized;

using static System.Console;
using System;


public class Program
{
    public static void Main()
    {

        var questionRoom = new QuestionRoom("");

        questionRoom.Attach(new Student { Name = "User1" });
        questionRoom.Attach(new Student { Name = "User2" });

        questionRoom.Question = "What is the capital of France?";
        questionRoom.Question = "What is the capital of Bangladesh?";
        questionRoom.Question = "What is the capital of India?";
        questionRoom.Question = "What is the capital of Pakistan?";

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
    protected string? question;

    protected Room(string question)
    {
        this.question = question;
    }


    public event EventHandler<ChangeEventArgs>? Change;

    protected void OnChange(ChangeEventArgs e)
    {
        Change?.Invoke(this, e);
    }

    public void Attach(IStudent student)
    {
        Change += student.Update;
    }

    public void Detach(IStudent student)
    {
        Change -= student.Update;
    }

    public string Question
    {
        get => question;
        set
        {
            if (question != value)
            {
                question = value;
                OnChange(new ChangeEventArgs { Question = question });
                Console.WriteLine();
            }
        }
    }
}

public class QuestionRoom : Room
{
    public QuestionRoom(string question) : base(question)
    {
        this.question = question;
    }
}

public interface IStudent
{
    void Update(object? sender, ChangeEventArgs e);
}



public class Student : IStudent
{
    public string Name { get; set; } = null!;
    public void Update(object sender, ChangeEventArgs e)
    {
        WriteLine(
            $"{Name} says he got question {e.Question}"
        );
    }
}

