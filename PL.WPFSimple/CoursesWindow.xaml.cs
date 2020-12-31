using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using BLAPI;

namespace PL.SimpleWPF
{
    /// <summary>
    /// Interaction logic for CoursesWindow.xaml
    /// </summary>
    public partial class CoursesWindow : Window
    {
        IBL bl;
        BO.Course curCourse;
        public CoursesWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            cbCourseID.DisplayMemberPath = "Number";
            cbCourseID.SelectedValuePath = "ID";
            cbCourseID.SelectedIndex = 0;
            cbCourseID.DataContext = bl.GetAllCourses().ToList();
        }

        private void cbCourseID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curCourse = (cbCourseID.SelectedItem as BO.Course);
            CourseDetailsGrid.DataContext = curCourse;
            courseStudentDataGrid.DataContext = curCourse.Students;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource courseViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("courseViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // courseViewSource.Source = [generic data source]
        }

        private void pbFactorGrades_Click(object sender, RoutedEventArgs e)
        {
            bl.FactorCourseGrades(curCourse.ID, 5);
            RefreshCourseStudentDataGrid();
        }

        private void RefreshCourseStudentDataGrid()
        {
            curCourse = bl.GetAllCourses().First(course => course.ID == curCourse.ID);
            //courseStudentDataGrid.DataContext = bl.GetAllCourses().First(course => course.ID == curCourse.ID).Students;
            CourseDetailsGrid.DataContext = curCourse;
            courseStudentDataGrid.DataContext = curCourse.Students;
        }
    }
}
