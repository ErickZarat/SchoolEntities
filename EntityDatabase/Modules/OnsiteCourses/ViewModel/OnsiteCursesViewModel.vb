Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Namespace Modules.OnsiteCourses.ViewModels
    Public Class OnsiteCursesViewModel
        Inherits ViewModelBase

        Private _onsiteCourse As ObservableCollection(Of OnsiteCourse)

        Private dataAccess As IOnsiteCourseService

        Public Property OnsiteCourses As ObservableCollection(Of OnsiteCourse)
            Get
                Return Me._onsiteCourse
            End Get
            Set(value As ObservableCollection(Of OnsiteCourse))
                Me._onsiteCourse = value
                OnPropertyChanged("OnlineCourses")
            End Set
        End Property

        Private Function GetAllOnsiteCourses() As IQueryable(Of OnsiteCourse)
            Return dataAccess.GetAllOnsiteCourses
        End Function

        Sub New()
            'inicializo variable de cursos
            Me._onsiteCourse = New ObservableCollection(Of OnsiteCourse)
            'registrar servicio
            ServiceLocator.RegisterService(Of IOnsiteCourseService)(New OnsiteCourseService)
            'inicializa acceso de datos del servicio
            Me.dataAccess = GetService(Of IOnsiteCourseService)()

            For Each element In Me.GetAllOnsiteCourses
                Me._onsiteCourse.Add(element)
            Next

        End Sub

    End Class
End Namespace