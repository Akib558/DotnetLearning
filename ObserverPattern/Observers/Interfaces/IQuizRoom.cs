using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObserverPattern.Subjects.Interfaces;

namespace ObserverPattern.Observers.Interfaces
{
    public interface IQuizRoom
    {
        AddStudent(IStudent student);
        RemoveStudent(IStudent student);
        Notify();

    }
}