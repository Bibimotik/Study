using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using SampleMVVM.Models;
using SampleMVVM.ViewModels;
using SampleMVVM.Views;
using System.Data.Entity;

namespace SampleMVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : Application
    {
        private List<Course> tickets; 

        private void OnStartup(object sender, StartupEventArgs e)
        {
            using (var context = new CourseContext())
            {
                if (!context.Tickets.Any())
                {
                    tickets = new List<Course>()
                {
                    new Course("Онкология", "высшая", "Онколог","Иван Иванович", 30),
                    new Course("Реанимация", "высшая", "Реаниматолог","Петр Иванович", 30),
                    new Course("Инфекционная", "средняя", "Врач общий","Евгений Евгеньевич", 30),
                    new Course("Детская", "высшая", "Врач общий","Михаил Михайлович", 30)
                };

                    context.Tickets.AddRange(tickets); 
                    context.SaveChanges(); 
                }
                else
                {
                    tickets = context.Tickets.ToList(); 
                }
            }

            MainView view = new MainView(); 
            MainViewModel viewModel = new MainViewModel(tickets); 
            view.DataContext = viewModel;
            view.Show();
        }
    }
}