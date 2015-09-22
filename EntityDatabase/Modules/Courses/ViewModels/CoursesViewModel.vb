Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Namespace Modules.Courses.ViewModels
    Public Class CoursesViewModel
        Inherits ViewModelBase

        Private _courses As ObservableCollection(Of Course)

        Private dataAccess As ICoursesService

        Public Property Courses As ObservableCollection(Of Course)
            Get
                Return Me._courses
            End Get
            Set(value As ObservableCollection(Of Course))
                Me._courses = value
                OnPropertyChanged("Courses")
            End Set
        End Property

        Private Function GetAllCourses() As IQueryable(Of Course)
            Return dataAccess.GetAllCourses
        End Function

        Sub New()
            'inicializo variable de cursos
            Me._courses = New ObservableCollection(Of Course)
            'registrar servicio
            ServiceLocator.RegisterService(Of ICoursesService)(New CoursesService)
            'inicializa acceso de datos del servicio
            Me.dataAccess = GetService(Of ICoursesService)()

            For Each element In Me.GetAllCourses
                Me._courses.Add(element)
            Next

        End Sub
    End Class
End Namespace