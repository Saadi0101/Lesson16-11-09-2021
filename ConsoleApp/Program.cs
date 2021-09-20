using System;
using System.Data.SqlClient;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var conString = @"Data source=.\SQLI; Initial catalog=Academy; Integrated security=true";
            SqlConnection sqlConnection = new SqlConnection(conString);
            sqlConnection.Open();

            var command = "";
            Console.WriteLine("Доступниые команды:\nInsert\nSelectById\nSelectAll\nUpdate\nDelete\nExit");
            while (true)
            {
                Console.Write("Введите команду:");
                command = Console.ReadLine();

                if (command == "Exit")
                {
                    sqlConnection.Close();
                    return;
                }

                else if (command == "Insert")
                {
                    Console.Write("Введите Last Name:");
                    var lastName = Console.ReadLine();
                    Console.Write("Введите First Name:");
                    var firstName = Console.ReadLine();
                    Console.Write("Введите Middle Name:");
                    var middleName = Console.ReadLine();
                    Console.Write("Введите Birthday:");
                    var birthDay = Convert.ToDateTime(Console.ReadLine());
                    Insert(sqlConnection, new Person {LastName = lastName, FirstName = firstName, MiddleName = middleName, BirthDay = birthDay} );
                    
                }

                else if (command == "SelectById")
                {
                    Console.Write("Введите Id:");
                    int.TryParse(Console.ReadLine(), out int Id);
                    SelectById(sqlConnection, Id);
                }

                else if (command == "SelectAll")
                {
                    SelectAll(sqlConnection);
                }

                else if (command == "Update")
                {
                    Console.Write("Введите Id:");
                    int.TryParse(Console.ReadLine(), out int Id);
                    Console.Write("Введите Last Name:");
                    var lastName = Console.ReadLine();
                    Console.Write("Введите First Name:");
                    var firstName = Console.ReadLine();
                    Console.Write("Введите Middle Name:");
                    var middleName = Console.ReadLine();
                    Console.Write("Введите Birthday:");
                    var birthDay = Convert.ToDateTime(Console.ReadLine());
                    Update(sqlConnection, Id, new Person { LastName = lastName, FirstName = firstName, MiddleName = middleName, BirthDay = birthDay });
                }

                else if (command == "Delete")
                {
                    Console.Write("Введите Id:");
                    int.TryParse(Console.ReadLine(), out int Id);
                    Delete(sqlConnection, Id);
                }

                else
                {
                    Console.WriteLine("The command is wrong!");
                }
            }
        }
        
        static void SelectAll(SqlConnection sqlConnection)
        {
            var sqlQuery = "SELECT * FROM PERSON";
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var sqlReader = sqlCommand.ExecuteReader();

            while (sqlReader.Read())
            {
                Console.WriteLine($"ID: {sqlReader.GetValue(0)}, " +
                    $"LASTNAME: {sqlReader.GetValue(1)}, " +
                    $"FIRSTNAME: {sqlReader.GetValue(2)}, " +
                    $"MIDDLENAME: {sqlReader.GetValue(3)}, " +
                    $"BIRTHDAY: {sqlReader.GetValue(4)}");
            }
            sqlReader.Close();
        }
        static void SelectById(SqlConnection sqlConnection, int id)
        {
            var sqlQuery = $"Select * from Person where id={id}";
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var sqlReader = sqlCommand.ExecuteReader();

            while (sqlReader.Read())
            {
                Console.WriteLine($"ID: {sqlReader.GetValue(0)}, " +
                    $"LASTNAME: {sqlReader.GetValue(1)}, " +
                    $"FIRSTNAME: {sqlReader.GetValue(2)}, " +
                    $"MIDDLENAME: {sqlReader.GetValue(3)}, " +
                    $"BIRTHDAY: {sqlReader.GetValue(4)}");
            }
            sqlReader.Close();
        }
        static void Insert(SqlConnection sqlConnection, Person person)
        {
            var sqlQuery = $"insert into Person(last_name, first_name, middle_name, birth_day) " +
                $"values('{person.LastName}', '{person.FirstName}', '{person.MiddleName}','{person.BirthDay}')";
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var result = sqlCommand.ExecuteNonQuery();

            if (result > 0)
            {
                Console.WriteLine("Person успешно добавлен!");
            }
        } 
        static void Update(SqlConnection sqlConnection, int id, Person person)
        {
           
            var sqlQuery = $"update Person set Last_Name='{person.LastName}', First_Name='{person.FirstName}', " +
                $"Middle_Name='{person.MiddleName}', Birth_Day='{person.BirthDay}' where id={id}";
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine($"Персона id: {id} успешно обновленно!");
            }
            else
            {
                Console.WriteLine("Errrrror");
            }
        }
        static void Delete(SqlConnection sqlConnection, int id)
        {
            var sqlQuery = $"Delete Person where id={id}";
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var result = sqlCommand.ExecuteNonQuery();

            if (result > 0)
            {
                Console.WriteLine($"Персона с id: {id} успешено удаленно!");   
            }
        }
    }

    public class Person
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDay { get; set; }

    }
}
