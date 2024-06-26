using System.Data.Entity;
using System.Windows;
using System.Windows.Input;
using SampleMVVM.Commands;
using SampleMVVM.Models;

namespace SampleMVVM.ViewModels
{
    public class TicketRepository
    {
        public void UpdateTicket(Course ticket)
        {
            using (var context = new CourseContext())
            {
                context.Entry(ticket).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
    
    class TicketViewModel : ViewModelBase
    {
        private readonly TicketRepository _ticketRepository;
        public Course Ticket { get; private set; }

        public TicketViewModel(Course ticket)
        {
            Ticket = ticket;
            _ticketRepository = new TicketRepository();
        }

        public string Department
        {
            get { return Ticket.Department; }
            set
            {
                Ticket.Department = value;
                OnPropertyChanged(nameof(Department));
                SaveChanges();
            }
        }

        public string DoctorCategory
        {
            get { return Ticket.DoctorCategory; }
            set
            {
                Ticket.DoctorCategory = value;
                OnPropertyChanged(nameof(DoctorCategory));
                SaveChanges();
            }
        }

        public string Specialization
        {
            get { return Ticket.Specialization; }
            set
            {
                Ticket.Specialization = value;
                OnPropertyChanged(nameof(Specialization));
                SaveChanges();
            }
        }

        public string FIO
        {
            get { return Ticket.FIO; }
            set
            {
                Ticket.FIO = value;
                OnPropertyChanged(nameof(FIO));
                SaveChanges();
            }
        }

        public int Count
        {
            get { return Ticket.Count; }
            set
            {
                if (value >= 0 && value <= 30)
                {
                    Ticket.Count = value;
                    OnPropertyChanged(nameof(Count));
                    SaveChanges();
                }
                else
                {
                    if (value < 0)
                    {
                        MessageBox.Show("Талонов нет!");
                    }
                    if (value > 30)
                    {
                        MessageBox.Show("Всего 30 талонов!");
                    }
                }
            }
        }

        private void SaveChanges()
        {
            _ticketRepository.UpdateTicket(Ticket);
        }

        #region Commands

        private DelegateCommand getItemCommand;
        public ICommand GetItemCommand
        {
            get
            {
                if (getItemCommand == null)
                {
                    getItemCommand = new DelegateCommand(GetItem);
                }
                return getItemCommand;
            }
        }

        private void GetItem()
        {
            Count++;
        }

        private DelegateCommand giveItemCommand;
        public ICommand GiveItemCommand
        {
            get
            {
                if (giveItemCommand == null)
                {
                    giveItemCommand = new DelegateCommand(GiveItem, CanGiveItem);
                }
                return giveItemCommand;
            }
        }

        private void GiveItem()
        {
            Count--;
        }

        private bool CanGiveItem()
        {
            return Count > 0;
        }

        #endregion
    }
}
