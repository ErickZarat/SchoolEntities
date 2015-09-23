Imports Modules.Departments.ViewModels
Imports Modules.Courses.ViewModels
Imports Modules.Persons.ViewModel
Imports Modules.OfficeAssigment.ViewModels
Imports Modules.OnlineCourses.ViewModels
Imports Modules.OnsiteCourses.ViewModels
Imports Modules.StudentsGrade.ViewModel

Class MainWindow
    Sub New()

        InitializeComponent()

        Me.DepartamenstUserControl.MainGrid.DataContext = New DepartmentsViewModel()
        Me.CoursesUserControl.MainGrid.DataContext = New CoursesViewModel
        Me.PersonsUserControl.MainGrid.DataContext = New PersonsViewModel()
        Me.OfficeAssigList.MainGrid.DataContext = New OfficeAssigmentViewModel()
        Me.OnlineCourseUserControl.MainGrid.DataContext = New OnlineCoursesViewModel()
        Me.OnsiteCoursesUserControl.MainGrid.DataContext = New OnsiteCursesViewModel()
        Me.StudenGradesUserControl.MainGrid.DataContext = New StudentsGradesViewModel()

    End Sub

End Class
