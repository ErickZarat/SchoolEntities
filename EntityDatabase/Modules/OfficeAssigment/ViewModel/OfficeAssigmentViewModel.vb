Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Namespace Modules.OfficeAssigment.ViewModels
    Public Class OfficeAssigmentViewModel
        Inherits ViewModelBase

        Private _office As ObservableCollection(Of OfficeAssignment)

        Private dataAccess As IOfficeAssigmentService

        Public Property OfficeAssigment As ObservableCollection(Of OfficeAssignment)
            Get
                Return Me._office
            End Get
            Set(value As ObservableCollection(Of OfficeAssignment))
                Me._office = value
                OnPropertyChanged("OfficeAssigment")
            End Set
        End Property

        Private Function GetAllCourses() As IQueryable(Of OfficeAssignment)
            Return dataAccess.GetAllOfficeAssigments
        End Function

        Sub New()
            'inicializo variable de cursos
            Me._office = New ObservableCollection(Of OfficeAssignment)
            'registrar servicio
            ServiceLocator.RegisterService(Of IOfficeAssigmentService)(New OfficeAssigmentService)
            'inicializa acceso de datos del servicio
            Me.dataAccess = GetService(Of IOfficeAssigmentService)()

            For Each element In Me.GetAllCourses
                Me._office.Add(element)
            Next

        End Sub
    End Class
End Namespace