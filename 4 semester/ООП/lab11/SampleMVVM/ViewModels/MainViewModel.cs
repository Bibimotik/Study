using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

using SampleMVVM.Commands;
using System.Collections.ObjectModel;
using SampleMVVM.Models;

namespace SampleMVVM.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public ObservableCollection<TicketViewModel> TicketList { get; set; } 

        #region Constructor

        public MainViewModel(List<Course> tickets)
        {
            TicketList = new ObservableCollection<TicketViewModel>(tickets.Select(b => new TicketViewModel(b)));
        }

        #endregion
    }
}