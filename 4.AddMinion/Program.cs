using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace _4.AddMinion
{
    class Program
    {
        const string SQLConnectionString = "Server =.; Database=MinionsDB; Integrated Security = true;";
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(SQLConnectionString))
            {
                connection.Open();
                //SELECT Id FROM Villains WHERE Name = @Name
                //SELECT Id FROM Minions WHERE Name = @Name
                //INSERT INTO MinionsVillains(MinionId, VillainId) VALUES(@villainId, @minionId)
                //INSERT INTO Villains(Name, EvilnessFactorId)  VALUES(@villainName, 4)
                //INSERT INTO Minions(Name, Age, TownId) VALUES(@nam, @age, @townId)
                //INSERT INTO Towns(Name) VALUES(@townName)
                //SELECT Id FROM Towns WHERE Name = @townName
                string[] input = Console.ReadLine().Split();
                string minionName = input[1];
                int age = int.Parse(input[2]);
                string town = input[3];

                string[] villianInfo = Console.ReadLine().Split();
                string villianName = villianInfo[1];

                string townIdQuery = @"SELECT Id FROM Towns WHERE Name = @townName";
                using var commandTown = new SqlCommand(townIdQuery, connection);
                commandTown.Parameters.AddWithValue("@townName", town);
                int? townId = (int)commandTown.ExecuteScalar(); //nullable int

                if (townId == null)
                {
                    string createTown = @"INSERT INTO Towns(Name) VALUES(@townName)";
                    using var commandInsertTown = new SqlCommand(createTown, connection);
                    commandTown.Parameters.AddWithValue("@townName", town);
                    commandTown.ExecuteNonQuery();
                    Console.WriteLine($"Town {town}> was added to the database.");

                }

                string villianIdQuery = @"SELECT Id FROM Villains WHERE Name = @Name";
                using var commandVillianId = new SqlCommand(villianIdQuery, connection);
                commandVillianId.Parameters.AddWithValue("@Name", villianName);
                int? villianId = (int)commandVillianId.ExecuteScalar();

                if (villianId == null)
                {
                    string createVillian = @"INSERT INTO Villains(Name, EvilnessFactorId)  VALUES(@villainName, 4)";
                    using var commandCreateVillian = new SqlCommand(createVillian, connection);
                    commandVillianId.Parameters.AddWithValue("@villainName", villianName);
                    commandCreateVillian.ExecuteNonQuery();
                   
                }







            }
        }
    }
}
