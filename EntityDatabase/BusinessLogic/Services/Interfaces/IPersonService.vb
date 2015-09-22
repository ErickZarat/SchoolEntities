Namespace BusinessLogic.Services.Interfaces
    Public Interface IPersonService
        Function GetAllPersons() As IQueryable(Of Person)
    End Interface
End Namespace