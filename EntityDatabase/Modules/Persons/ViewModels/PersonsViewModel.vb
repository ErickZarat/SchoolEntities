Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports BusinessObjects.Helpers
Imports Infrastructure.Helpers

Namespace Modules.Persons.ViewModel
    Public Class PersonsViewModel
        Inherits ViewModelBase
#Region "Declare"
        Private _persons As ObservableCollection(Of Person)
        Private dataAccess As IPersonService
        Private _insertPerson As Person
        Private _insertButtonCommand As ICommand
        Private _deleteButtonCommand As ICommand
        Private _selectedRow As Person
#End Region

#Region "Delete Properties"
        Public Property DeleteButtonCommand As ICommand
            Get
                If _deleteButtonCommand Is Nothing Then
                    _deleteButtonCommand = New RelayCommand(AddressOf DeletePersonDB)
                End If
                Return _deleteButtonCommand
            End Get
            Set(value As ICommand)
                _deleteButtonCommand = value
            End Set
        End Property

        Sub DeletePersonDB()
            If SelectedRow IsNot Nothing Then
                Using context As New SchoolEntities
                    Dim query = (From per In context.Person
                                 Where SelectedRow.PersonID = per.PersonID
                                 Select per).FirstOrDefault
                    context.Person.Remove(query)
                    context.SaveChanges()
                    refresh()
                End Using
            End If
        End Sub

        Public Property SelectedRow As Person
            Get
                Return _selectedRow
            End Get
            Set(value As Person)
                _selectedRow = value
            End Set
        End Property
#End Region

#Region "Insert Properties"
        Public Property InsertCommand As ICommand
            Get
                If Me._insertButtonCommand Is Nothing Then
                    Me._insertButtonCommand = New RelayCommand(AddressOf InsertPersonDB)
                End If
                Return Me._insertButtonCommand
            End Get
            Set(value As ICommand)
                _insertButtonCommand = value
            End Set
        End Property

        Public Property InsertPerson As Person
            Get
                Return _insertPerson
            End Get
            Set(value As Person)
                _insertPerson = value
                OnPropertyChanged("InsertPerson")
            End Set
        End Property

        Sub InsertPersonDB()
            Using context As New SchoolEntities
                Dim newp As New NewPerson
                newp.ShowDialog()
                '_insertPerson = New Person
                'InsertPerson.FirstName = "Mario"
                'InsertPerson.LastName = "Perez"
                'InsertPerson.EnrollmentDate = Date.Now

                'context.Person.Add(InsertPerson)
                'context.SaveChanges()
                'Debug.WriteLine("se agrego la persona")
                refresh()
            End Using
        End Sub
#End Region

#Region "List"

        Public Property Persons As ObservableCollection(Of Person)
            Get
                Return Me._persons
            End Get
            Set(value As ObservableCollection(Of Person))
                Me._persons = value
                OnPropertyChanged("Persons")
            End Set
        End Property

        ' Function to get all departments from service
        Private Function GetAllPersons() As IQueryable(Of Person)
            Return Me.dataAccess.GetAllPersons
        End Function

        Sub refresh()
            'Initialize property variable of departments
            Me._persons.Clear()
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IPersonService)(New PersonService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IPersonService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllPersons
                Me._persons.Add(element)
            Next
        End Sub

        Sub New()
            Me._persons = New ObservableCollection(Of Person)
            refresh()
        End Sub
#End Region
    End Class
End Namespace