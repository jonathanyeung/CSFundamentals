using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interviews.OODesign
{
    public class CallCenter
    {
        private Queue<Call> incomingQueue;

        private List<Employee> AllEmployees;

        private EmployeeTier Respondents;

        private EmployeeTier Managers;

        private EmployeeTier Directors;

        private bool EmployeeAvailable;

        public void DispatchCall(Call incomingCall)
        {
            incomingQueue.Enqueue(incomingCall);
        }

        public void Operate()
        {
            while (true)
            {
                if (incomingQueue.Count != 0 && EmployeeAvailable)
                {
                    if (Respondents.NumberAvailable > 0)
                    {
                        Respondents.HandleCall(incomingQueue.Dequeue());
                    }
                    else if (Managers.NumberAvailable > 0)
                    {
                        Managers.HandleCall(incomingQueue.Dequeue());
                    }
                    else if(Directors.NumberAvailable > 0)
                    {
                        Directors.HandleCall(incomingQueue.Dequeue());
                    }
                }
            }
        }
    }

    public class EmployeeTier
    {
        public EmployeeLevel Level;

        public List<Employee> Employees;

        public int NumberAvailable;

        public void HandleCall(Call incoming) { }
    }

    public class Call
    {
        string cellNumber;
        string contents;
    }

    public enum EmployeeLevel
    {
        Respondent,
        Manager,
        Director
    }

    public abstract class Employee
    {
        public bool isAvailable;

        public EmployeeLevel Level;

        public bool HandleCall()
        {
            if (!isAvailable)
            {
                return false;
            }

            isAvailable = false;

            //DoWork();

            isAvailable = true;

            return true;
        }
    }

    public class Director: Employee{}
    public class Manager: Employee{}
    public class Respondent: Employee{}
}
