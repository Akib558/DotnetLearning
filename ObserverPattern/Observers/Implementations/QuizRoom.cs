using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObserverPattern.Observers.Interfaces;
using ObserverPattern.Subjects.Interfaces;

namespace ObserverPattern.Observers.Implementations
{

    public class ChangeEventArgs : EventArgs
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string CorrectAnswer { get; set; }
    }

    public class QuizRoom(string Question, string Answer, string CorrectAnswer) : IQuizRoom
    {
        protected string _question;
        protected string _answer;
        protected string _correctAnswer;


        public event EventHandler<ChangeEventArgs> Change = null;

        public void Notify(ChangeEventArgs events)
        {
            Change?.Invoke(this, events);
        }

        public void AddStudent(IStudent student)
        {
            Change += student.Update;
        }

        public void RemoveStudent(IStudent student)
        {
            Change -= student.Update;
        }


        // private List<IStudent> _students = new List<IStudent>();
        // private string _question;
        // private string _answer;
        // private string _correctAnswer;

        // public QuizRoom(string Question, string Answer, string CorrectAnswer)
        // {
        //     _question = Question;
        //     _answer = Answer;
        //     _correctAnswer = CorrectAnswer;
        // }

        // public void AddStudent(IStudent student)
        // {
        //     _students.Add(student);
        // }

        // public void RemoveStudent(IStudent student)
        // {
        //     _students.Remove(student);
        // }

        // public void Notify()
        // {
        //     foreach (var student in _students)
        //     {
        //         student.Update(_question, _answer, _correctAnswer);
        //     }
        // }
    }
}