Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel

Namespace Modules.StudentsGrade.ViewModel
    Public Class StudentsGradesViewModel
        Inherits ViewModelBase

        Private _StudentsGrades As ObservableCollection(Of StudentGrade)
        Private dataAccess As IStudentGradeService

        Public Property StudentsGrades As ObservableCollection(Of StudentGrade)
            Get
                Return Me._StudentsGrades
            End Get
            Set(value As ObservableCollection(Of StudentGrade))
                Me._StudentsGrades = value
                OnPropertyChanged("StudentsGrades")
            End Set
        End Property

        ' Function to get all departments from service
        Private Function GetAllStudentsGrades() As IQueryable(Of StudentGrade)
            Return Me.dataAccess.GetAllStudentGrade
        End Function

        Sub New()
            'Initialize property variable of departments
            Me._StudentsGrades = New ObservableCollection(Of StudentGrade)
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IStudentGradeService)(New StudentGradeService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IStudentGradeService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllStudentsGrades
                Me._StudentsGrades.Add(element)
            Next
        End Sub
    End Class
End Namespace

