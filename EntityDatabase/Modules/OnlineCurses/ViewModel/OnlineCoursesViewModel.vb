Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Namespace Modules.OnlineCourses.ViewModels
    Public Class OnlineCoursesViewModel
        Inherits ViewModelBase

        Private _onlineCourse As ObservableCollection(Of OnlineCourse)

        Private dataAccess As IOnlineCursesService

        Public Property OnlineCourses As ObservableCollection(Of OnlineCourse)
            Get
                Return Me._onlineCourse
            End Get
            Set(value As ObservableCollection(Of OnlineCourse))
                Me._onlineCourse = value
                OnPropertyChanged("OnlineCourses")
            End Set
        End Property

        Private Function GetAllOnlineCourses() As IQueryable(Of OnlineCourse)
            Return dataAccess.GetAllOnlineCurses
        End Function

        Sub New()
            'inicializo variable de cursos
            Me._onlineCourse = New ObservableCollection(Of OnlineCourse)
            'registrar servicio
            ServiceLocator.RegisterService(Of IOnlineCursesService)(New OnlineCursesService)
            'inicializa acceso de datos del servicio
            Me.dataAccess = GetService(Of IOnlineCursesService)()

            For Each element In Me.GetAllOnlineCourses
                Me._onlineCourse.Add(element)
            Next

        End Sub

    End Class
End Namespace