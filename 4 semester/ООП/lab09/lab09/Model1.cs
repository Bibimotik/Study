using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace lab09
{
    public class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<CrewMember> CrewMembers { get; set; }
    }

    public class Airplane
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public int PassengerSeats { get; set; }
        public int YearOfManufacture { get; set; }
        public decimal CargoCapacity { get; set; }
        public DateTime LastMaintenanceDate { get; set; }

        public ICollection<CrewMember> CrewMembers { get; set; }
    }

    public class CrewMember
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }

        public int AirplaneID { get; set; }
        public Airplane Airplane { get; set; }
    }
}