using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace lab09
{
    public interface IAirplaneRepository
    {
        void AddAirplane(Airplane airplane);
        void UpdateAirplane(Airplane airplane);
        void DeleteAirplane(Airplane airplane);
        Airplane GetAirplaneById(int id);
        IEnumerable<Airplane> GetAllAirplanes();
        Task<List<Airplane>> GetAirplanesSortedByPassengerSeatsAscendingAsync();
    }

    public interface ICrewMemberRepository
    {
        void AddCrewMember(CrewMember crewMember);
        void UpdateCrewMember(CrewMember crewMember);
        void DeleteCrewMember(CrewMember crewMember);
        CrewMember GetCrewMemberById(int id);
        IEnumerable<CrewMember> GetAllCrewMembers();
        List<CrewMember> GetCrewMembersSortedByAgeAscending();
        List<CrewMember> GetCrewMembersSortedByExperienceDescending();
    }
    
    public interface IUnitOfWork : IDisposable
    {
        IAirplaneRepository Airplanes { get; }
        ICrewMemberRepository CrewMembers { get; }
        void Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly Model1 context;
        private IAirplaneRepository airplanes;
        private ICrewMemberRepository crewMembers;

        public UnitOfWork()
        {
            context = new Model1(this);
        }

        public IAirplaneRepository Airplanes
        {
            get
            {
                if (airplanes == null)
                {
                    airplanes = new AirplaneRepository(context);
                }
                return airplanes;
            }
        }

        public ICrewMemberRepository CrewMembers
        {
            get
            {
                if (crewMembers == null)
                {
                    crewMembers = new CrewMemberRepository(context);
                }
                return crewMembers;
            }
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }


    public class AirplaneRepository : IAirplaneRepository
    {
        private readonly Model1 context;

        public AirplaneRepository(Model1 context)
        {
            this.context = context;
        }

        public void AddAirplane(Airplane airplane)
        {
            context.Airplanes.Add(airplane);
        }

        public void UpdateAirplane(Airplane airplane)
        {
            context.Entry(airplane).State = EntityState.Modified;
        }

        public void DeleteAirplane(Airplane airplane)
        {
            context.Airplanes.Remove(airplane);
        }

        public Airplane GetAirplaneById(int id)
        {
            return context.Airplanes.Find(id);
        }

        public IEnumerable<Airplane> GetAllAirplanes()
        {
            return context.Airplanes.ToList();
        }

        public Task<List<Airplane>> GetAirplanesSortedByPassengerSeatsAscendingAsync()
        {
            return context.Airplanes.OrderBy(a => a.PassengerSeats).ToListAsync();
        }
    }

    public class CrewMemberRepository : ICrewMemberRepository
    {
        private readonly Model1 context;

        public CrewMemberRepository(Model1 context)
        {
            this.context = context;
        }

        public void AddCrewMember(CrewMember crewMember)
        {
            context.CrewMembers.Add(crewMember);
        }

        public void UpdateCrewMember(CrewMember crewMember)
        {
            context.Entry(crewMember).State = EntityState.Modified;
        }

        public void DeleteCrewMember(CrewMember crewMember)
        {
            context.CrewMembers.Remove(crewMember);
        }

        public CrewMember GetCrewMemberById(int id)
        {
            return context.CrewMembers.Find(id);
        }

        public IEnumerable<CrewMember> GetAllCrewMembers()
        {
            return context.CrewMembers.ToList();
        }

        public List<CrewMember> GetCrewMembersSortedByAgeAscending()
        {
            return context.CrewMembers.OrderBy(c => c.Age).ToList();
        }

        public List<CrewMember> GetCrewMembersSortedByExperienceDescending()
        {
            return context.CrewMembers.OrderByDescending(c => c.Experience).ToList();
        }
    }

    public class Model1 : DbContext, IAirplaneRepository, ICrewMemberRepository
    {
        private readonly IUnitOfWork unitOfWork;

        public Model1(IUnitOfWork unitOfWork)
            : base("name=Model1")
        {
            this.unitOfWork = unitOfWork;
        }

        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<CrewMember> CrewMembers { get; set; }

        public void AddAirplane(Airplane airplane)
        {
            unitOfWork.Airplanes.AddAirplane(airplane);
        }

        public void UpdateAirplane(Airplane airplane)
        {
            unitOfWork.Airplanes.UpdateAirplane(airplane);
        }

        public void DeleteAirplane(Airplane airplane)
        {
            unitOfWork.Airplanes.DeleteAirplane(airplane);
        }

        public Airplane GetAirplaneById(int id)
        {
            return unitOfWork.Airplanes.GetAirplaneById(id);
        }

        public IEnumerable<Airplane> GetAllAirplanes()
        {
            return unitOfWork.Airplanes.GetAllAirplanes();
        }

        public Task<List<Airplane>> GetAirplanesSortedByPassengerSeatsAscendingAsync()
        {
            return unitOfWork.Airplanes.GetAirplanesSortedByPassengerSeatsAscendingAsync();
        }

        public void AddCrewMember(CrewMember crewMember)
        {
            unitOfWork.CrewMembers.AddCrewMember(crewMember);
        }

        public void UpdateCrewMember(CrewMember crewMember)
        {
            unitOfWork.CrewMembers.UpdateCrewMember(crewMember);
        }

        public void DeleteCrewMember(CrewMember crewMember)
        {
            unitOfWork.CrewMembers.DeleteCrewMember(crewMember);
        }

        public CrewMember GetCrewMemberById(int id)
        {
            return unitOfWork.CrewMembers.GetCrewMemberById(id);
        }

        public IEnumerable<CrewMember> GetAllCrewMembers()
        {
            return unitOfWork.CrewMembers.GetAllCrewMembers();
        }

        public List<CrewMember> GetCrewMembersSortedByAgeAscending()
        {
            return unitOfWork.CrewMembers.GetCrewMembersSortedByAgeAscending();
        }

        public List<CrewMember> GetCrewMembersSortedByExperienceDescending()
        {
            return unitOfWork.CrewMembers.GetCrewMembersSortedByExperienceDescending();
        }

        public void Commit()
        {
            unitOfWork.Commit();
        }
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