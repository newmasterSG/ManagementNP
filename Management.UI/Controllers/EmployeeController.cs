using Management.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Management.UI.Controllers;

public class EmployeeController : Controller
{
    private static List<Employee> _employees = new List<Employee>
    {
        new Employee 
        { 
            Id = 1, 
            FirstName = "Іван", 
            LastName = "Петров", 
            Patronimyc = "Олександрович", 
            Birthday = new DateTime(1985, 5, 15),
            Department = "HR", 
            Position = "Менеджер", 
            Salary = 50000 
        },
        new Employee
        {
            Id = 2, 
            FirstName = "Олена", 
            LastName = "Сидорова", 
            Patronimyc = "Андріївна", 
            Birthday = new DateTime(1990, 8, 22), 
            Department = "IT", 
            Position = "Розробник", 
            Salary = 70000
        },
        new Employee
        {
            Id = 3, 
            FirstName = "Марія", 
            LastName = "Іванова", 
            Patronimyc = "Петрівна", 
            Birthday = new DateTime(1982, 3, 10), 
            Department = "Фінанси", 
            Position = "Фінансист", 
            Salary = 55000
        },
        new Employee
        {
            Id = 4, 
            FirstName = "Олександр", 
            LastName = "Семенов", 
            Patronimyc = "Миколайович", 
            Birthday = new DateTime(1977, 12, 5), 
            Department = "IT", 
            Position = "Тестер", 
            Salary = 60000
        },
        new Employee
        {
            Id = 5, 
            FirstName = "Надія", 
            LastName = "Григоренко", 
            Patronimyc = "Ігорівна",
            Birthday = new DateTime(1995, 6, 18), 
            Department = "Маркетинг", 
            Position = "Маркетолог", 
            Salary = 52000
        }
    };

    public ActionResult Index()
    {
        return View(_employees);
    }
    
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Employee employee)
    {
        _employees.Add(employee);
        
        return RedirectToAction("Index");
    }
    
    public ActionResult Edit(int id)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
        {
            return View();
        }
        return View(employee);
    }
    
    [HttpPost]
    public ActionResult Edit(Employee updatedEmployee)
    {
        var existingEmployee = _employees.FirstOrDefault(e => e.Id == updatedEmployee.Id);
        if (existingEmployee == null)
        {
           
        }
        existingEmployee.FirstName = updatedEmployee.FirstName;
        existingEmployee.LastName = updatedEmployee.LastName;
        existingEmployee.Patronimyc = updatedEmployee.Patronimyc;
        existingEmployee.Birthday = updatedEmployee.Birthday;
        existingEmployee.Department = updatedEmployee.Department;
        existingEmployee.Position = updatedEmployee.Position;
        existingEmployee.Salary = updatedEmployee.Salary;

        return RedirectToAction("Index");
    }

    // Видалення співробітника
    public ActionResult Delete(int id)
    {
        var employeeToDelete = _employees.FirstOrDefault(e => e.Id == id);
        if (employeeToDelete != null)
        {
            _employees.Remove(employeeToDelete);
        }

        return RedirectToAction("Index");
    }
    
    public ActionResult Search(string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return RedirectToAction("Index");
        }

        searchTerm = searchTerm.ToUpper();

        var searchResults = _employees.Where(e =>
            e.FirstName.ToUpper().Contains(searchTerm) ||
            e.LastName.ToUpper().Contains(searchTerm) ||
            e.Patronimyc.ToUpper().Contains(searchTerm) ||
            e.Department.ToUpper().Contains(searchTerm) ||
            e.Position.ToUpper().Contains(searchTerm)
        ).ToList();

        return View("Index", searchResults);
    }
}