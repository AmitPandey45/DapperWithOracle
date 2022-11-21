using CoreFramework.Oracle;
using System;

namespace DapperNetCoreWithOracle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            employeeRepository.GetGames();
        }
    }
}
