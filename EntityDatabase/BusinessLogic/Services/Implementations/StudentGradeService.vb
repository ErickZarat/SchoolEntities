Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Namespace BusinessLogic.Services.Implementations
    Public Class StudentGradeService
        Implements IStudentGradeService

        Public Function GetAllStudentGrade() As IQueryable(Of StudentGrade) Implements IStudentGradeService.GetAllStudentGrade
            Return DataContext.DBEntities.StudentGrades
        End Function
    End Class
End Namespace