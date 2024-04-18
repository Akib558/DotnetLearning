using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObserverPattern.Subjects.Interfaces
{
    public interface IStudent
    {
        void Update(string Question, string Answer, string CorrectAnswer);


    }
}