Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports BusinessObjects.Helpers
Imports Infrastructure.Helpers

Namespace Modules.Persons.ViewModel
    Public Class NewPersonViewModel
        Inherits ViewModelBase


        Private _FirstName As String
        Private _LastName As String
        Private _radioCheckedEmpl As Boolean
        Private _radioCheckedStud As Boolean

        Public Property FirstName As String
            Get
                Return Me._FirstName
            End Get
            Set(value As String)
                Me._FirstName = value
            End Set
        End Property

        Public Property LastName As String
            Get
                Return Me._LastName
            End Get
            Set(value As String)
                Me._LastName = value
            End Set
        End Property

        Public Property RadioCheckedEmpl As Boolean
            Get
                Return Me._radioCheckedEmpl
            End Get
            Set(value As Boolean)
                Me._radioCheckedEmpl = value
                OnPropertyChanged("RadioCheckedEmpl")
                Debug.WriteLine(RadioCheckedEmpl)
                Debug.WriteLine(RadioCheckedStud)
            End Set
        End Property

        Public Property RadioCheckedStud As Boolean
            Get
                Return Me._radioCheckedStud
            End Get
            Set(value As Boolean)
                Me._radioCheckedStud = value
                OnPropertyChanged("RadioCheckedStud")
                Debug.WriteLine(RadioCheckedEmpl)
                Debug.WriteLine(RadioCheckedStud)
            End Set
        End Property
    End Class
End Namespace