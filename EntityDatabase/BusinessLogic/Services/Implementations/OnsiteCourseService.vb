Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Namespace BusinessLogic.Services.Implementations
    Public Class OnsiteCourseService
        Implements IOnsiteCourseService

        Public Function GetAllOnsiteCourses() As IQueryable(Of OnsiteCourse) Implements IOnsiteCourseService.GetAllOnsiteCourses
            Return DataContext.DBEntities.OnsiteCourses
        End Function
    End Class
End Namespace