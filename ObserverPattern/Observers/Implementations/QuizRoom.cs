using ObserverPattern.Observers.Interfaces;

public class QuizRoom : IQuizRoom
{
    protected string? _question;

    protected QuizRoom(string quesion){
        _question = quesion;
    }

    public event EventHandler<string>? QuestionChanged;
}