using LINQ_tasks;
using System.Xml.Linq;

//Employee Data
var employees = new List<Employee>
            {
                new Employee{ Id=1, Name="Ravi", Department="IT", Salary=85000, Experience=5, Location="Bangalore"},
                new Employee{ Id=2, Name="Priya", Department="HR", Salary=52000, Experience=4, Location="Pune"},
                new Employee{ Id=3, Name="Kiran", Department="Finance", Salary=73000, Experience=6, Location="Hyderabad"},
                new Employee{ Id=4, Name="Asha", Department="IT", Salary=95000, Experience=8, Location="Bangalore"},
                new Employee{ Id=5, Name="Vijay", Department="Marketing", Salary=68000, Experience=5, Location="Mumbai"},
                new Employee{ Id=6, Name="Deepa", Department="HR", Salary=61000, Experience=7, Location="Delhi"},
                new Employee{ Id=7, Name="Arjun", Department="Finance", Salary=82000, Experience=9, Location="Bangalore"},
                new Employee{ Id=8, Name="Sneha", Department="IT", Salary=78000, Experience=4, Location="Pune"},
                new Employee{ Id=9, Name="Rohit", Department="Marketing", Salary=90000, Experience=10, Location="Delhi"},
                new Employee{ Id=10, Name="Meena", Department="Finance", Salary=66000, Experience=3, Location="Mumbai"}
            };

//Product Data

var products = new List<Product>
            {
                new Product{ Id=1, Name="Laptop", Category="Electronics", Price=75000, Stock=15 },
                new Product{ Id=2, Name="Smartphone", Category="Electronics", Price=55000, Stock=25 },
                new Product{ Id=3, Name="Tablet", Category="Electronics", Price=30000, Stock=10 },
                new Product{ Id=4, Name="Headphones", Category="Accessories", Price=2000, Stock=100 },
                new Product{ Id=5, Name="Shirt", Category="Fashion", Price=1500, Stock=50 },
                new Product{ Id=6, Name="Jeans", Category="Fashion", Price=2200, Stock=30 },
                new Product{ Id=7, Name="Shoes", Category="Fashion", Price=3500, Stock=20 },
                new Product{ Id=8, Name="Refrigerator", Category="Appliances", Price=45000, Stock=8 },
                new Product{ Id=9, Name="Washing Machine", Category="Appliances", Price=38000, Stock=6 },
                new Product{ Id=10, Name="Microwave", Category="Appliances", Price=12000, Stock=12 }
            };

//Student Data

var students = new List<Student>
            {
                new Student{ Id=1, Name="Asha", Course="C#", Marks=92, City="Bangalore"},
                new Student{ Id=2, Name="Ravi", Course="Java", Marks=85, City="Pune"},
                new Student{ Id=3, Name="Sneha", Course="Python", Marks=78, City="Hyderabad"},
                new Student{ Id=4, Name="Kiran", Course="C#", Marks=88, City="Delhi"},
                new Student{ Id=5, Name="Meena", Course="Python", Marks=95, City="Bangalore"},
                new Student{ Id=6, Name="Vijay", Course="C#", Marks=82, City="Chennai"},
                new Student{ Id=7, Name="Deepa", Course="Java", Marks=91, City="Mumbai"},
                new Student{ Id=8, Name="Arjun", Course="Python", Marks=89, City="Hyderabad"},
                new Student{ Id=9, Name="Priya", Course="C#", Marks=97, City="Pune"},
                new Student{ Id=10, Name="Rohit", Course="Java", Marks=74, City="Delhi"}
            };

//Order Data

var orders = new List<Order>
            {
                new Order{ OrderId=1001, CustomerId=1, Amount=2500, OrderDate=new DateTime(2025,5,12)},
                new Order{ OrderId=1002, CustomerId=2, Amount=1800, OrderDate=new DateTime(2025,5,13)},
                new Order{ OrderId=1003, CustomerId=1, Amount=4500, OrderDate=new DateTime(2025,5,20)},
                new Order{ OrderId=1004, CustomerId=3, Amount=6700, OrderDate=new DateTime(2025,6,01)},
                new Order{ OrderId=1005, CustomerId=4, Amount=2500, OrderDate=new DateTime(2025,6,02)},
                new Order{ OrderId=1006, CustomerId=2, Amount=5600, OrderDate=new DateTime(2025,6,10)},
                new Order{ OrderId=1007, CustomerId=5, Amount=3100, OrderDate=new DateTime(2025,6,12)},
                new Order{ OrderId=1008, CustomerId=3, Amount=7100, OrderDate=new DateTime(2025,7,01)},
                new Order{ OrderId=1009, CustomerId=4, Amount=4200, OrderDate=new DateTime(2025,7,05)},
                new Order{ OrderId=1010, CustomerId=5, Amount=2900, OrderDate=new DateTime(2025,7,10)}
            };

//EMPLOYEE TASKS
//1. Find all employees in the "IT" department.
Console.WriteLine("All employees working in the IT department.");
var ITEmployees = employees.Where(emp => emp.Department == "IT");
foreach(var e in ITEmployees)
{
    Console.WriteLine( e.Name);
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("Names and salaries of employees who earn more than 70,000");
var Richemp = employees.Where(e => e.Salary > 70000);
foreach (var p in Richemp)
{
    Console.WriteLine($"{p.Name} - {p.Salary}");
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("All employees located in Bangalore");
var BangEmp = employees.Where(e => e.Location == "Bangalore");
foreach (var t in BangEmp)
{
    Console.WriteLine($"{t.Name} - {t.Location}");
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("Employees having more than 5 years of experience");
var EXPemp = employees.Where(e => e.Experience > 5);
foreach (var x in EXPemp)
{
    Console.WriteLine($"{x.Name} - {x.Experience}");
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("Names of employees and their salaries in ascending order of salary");
var SortedSalaries = employees.OrderBy(e => e.Salary);
foreach (var s in SortedSalaries)
{
    Console.WriteLine($"{s.Name} - {s.Salary}");
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("Group employees by location ");
var LocationwiseCount = employees.GroupBy(e => e.Location);
foreach (var group in LocationwiseCount)
{
    Console.WriteLine($"{group.Key}: {group.Count()}");
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("Employees whose salary is above the average salary");
var AvgSalary = employees.Average(e => e.Salary);
var aboveAvgSalary = employees.Where(e =>e.Salary > AvgSalary);
foreach (var a in aboveAvgSalary)
{
    Console.WriteLine(a.Name);
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("Top 3 highest-paid employees");

var Top3Salaries = employees.OrderByDescending(e => e.Salary).Take(3);
foreach (var top in Top3Salaries)
{
    Console.WriteLine($"{top.Name} - {top.Salary}");
}


//PRODUCT TASKS
Console.WriteLine("--------------------------------------------------");
Console.WriteLine("Product Tasks");
Console.WriteLine("All products with stock less than 20");
var stocksLessThan20 = products.Where(s => s.Stock < 20);
foreach (var item in stocksLessThan20)
{
    Console.WriteLine($"{item.Name} - {item.Stock}");
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("All products belonging to the “Fashion” category");
var fashionProd = products.Where(c => c.Category == "Fashion");
foreach (var f in fashionProd)
{
    Console.WriteLine($"{f.Name}");
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("Product names and prices where price is greater than 10,000");
var priceRange = products.Where(p => p.Price >= 10000);
foreach (var r in priceRange)
{
    Console.WriteLine($"{r.Name} - {r.Price}");
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("All product names sorted by price (descending)");
var sortedOrder = products.OrderByDescending(p => p.Price);
foreach (var item in sortedOrder)
{
    Console.WriteLine(item.Name);
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("Most expensive product in each category");
var mostExpInCategory = products.GroupBy(p => p.Category).Select(g => g.OrderByDescending(g => g.Price).First());
foreach (var item in mostExpInCategory)
{
    Console.WriteLine($"{item.Category} - {item.Name} - {item.Price}");
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("Total stock per category");
var totalStockPerCategory = products.GroupBy(p => p.Category);
foreach (var item in totalStockPerCategory)
{
    Console.WriteLine($"{item.Key} - {item.Sum(p => p.Stock)}");    
}
Console.WriteLine("--------------------------------------------------");
Console.WriteLine("Products whose name starts with ‘S’");
var nameStartsWithS = products.Where(p => p.Name.StartsWith("S"));
foreach (var item in nameStartsWithS)
{
    Console.WriteLine($"{item.Name}");
}
Console.WriteLine("--------------------------------------------------");
Console.WriteLine("Average price of products in each category");

var avgPriceEachCategory = products.GroupBy(p => p.Category);
foreach (var item in avgPriceEachCategory)
{
    Console.WriteLine($"{item.Key} - {item.Average(p => p.Price)}");
}

//STUDENT TASKS
Console.WriteLine("--------------------------------------------------");
Console.WriteLine("Student Tasks");
Console.WriteLine("Highest scorer in each course");
var HighestScorerinEachCourse = students.GroupBy(s => s.Course);
foreach(var item in HighestScorerinEachCourse)
{
    Console.WriteLine($"{item.Key} - {item.OrderByDescending(s => s.Marks).First().Name} - {item.OrderByDescending(s => s.Marks).First().Marks}");
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("Average marks of all students city-wise");
var avgMarksCityWise = students.GroupBy(s => s.City);
foreach(var item in avgMarksCityWise)
{
    Console.WriteLine($"{item.Key} - {item.Average(s => s.Marks)}");
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("Names and marks of students ranked by marks");
var display = students.OrderByDescending(s => s.Marks);
foreach(var item in display)
{
    Console.WriteLine($"{item.Name} - {item.Marks}");
}

Console.WriteLine("--------------------------------------------------");
Console.WriteLine("OrderTasks");
//OrderTasks
Console.WriteLine("Total order amount per month");
var totalOrderAmountPerMonth = orders.GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month });
foreach(var item in totalOrderAmountPerMonth)
{
    Console.WriteLine($"{item.Key.Year} - {item.Key.Month} - {item.Sum(p => p.Amount)}");
}

Console.WriteLine("---------------------------------------------------");
Console.WriteLine("Customer who spent the most in total");

//Show the customer who spent the most in total.
var spentmost = orders.GroupBy(p => p.CustomerId).Select(g => new
{
    CustomerId = g.Key,
    TotalSpent = g.Sum(o => o.Amount)

}).OrderByDescending(p => p.TotalSpent).Take(1);

foreach(var item in spentmost)
{
    Console.WriteLine($"{item.CustomerId}-- {item.TotalSpent}");
}
Console.WriteLine("---------------------------------------------------");
Console.WriteLine("Orders grouped by customer and show total amount spent");
var displayOrders = orders.GroupBy(o => o.CustomerId);
foreach(var item in displayOrders)
{
    Console.WriteLine($"{item.Key} -- {item.Sum(o => o.Amount)}");
}

Console.WriteLine("---------------------------------------------------");
Console.WriteLine("Top 2 orders with the highest amount");
var top2orders = orders.OrderByDescending(o => o.Amount).Take(2);
foreach(var item in top2orders)
{
    Console.WriteLine($"{item.CustomerId} -- {item.Amount}");
}
