Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Namespace BusinessLogic.Services.Implementations
    Public Class OnlineCursesService
        Implements IOnlineCursesService

        Public Function GetAllOnlineCurses() As IQueryable(Of OnlineCourse) Implements IOnlineCursesService.GetAllOnlineCurses
            Return DataContext.DBEntities.OnlineCourse
        End Function
    End Class
End Namespace
