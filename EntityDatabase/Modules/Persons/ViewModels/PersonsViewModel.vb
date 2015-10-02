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
        Public Shadows newp As NewPerson
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
                DataContext.DBEntities.Person.Remove((From per In DataContext.DBEntities.Person
                                 Where SelectedRow.PersonID = per.PersonID
                                 Select per).FirstOrDefault)
                DataContext.DBEntities.SaveChanges()
                refresh()
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
                newp = New NewPerson
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


#Region "new person"
        Public _person As New Person
        Private _radioCheckedEmpl As Boolean
        Private _radioCheckedStud As Boolean
        Public _okButton As ICommand
        Public _cancelButton As ICommand
        Public _resetButton As ICommand

        Public Property FirstName As String
            Get
                Return Me._person.FirstName
            End Get
            Set(value As String)
                Me._person.FirstName = value
                OnPropertyChanged("FirstName")
            End Set
        End Property

        Public Property LastName As String
            Get
                Return Me._person.LastName
            End Get
            Set(value As String)
                Me._person.LastName = value
                OnPropertyChanged("LastName")
            End Set
        End Property

        Public Property RadioCheckedEmpl As Boolean
            Get
                Return Me._radioCheckedEmpl
            End Get
            Set(value As Boolean)
                Me._radioCheckedEmpl = value
                OnPropertyChanged("RadioCheckedEmpl")
                _person.HireDate = Date.Now
                _person.EnrollmentDate = Nothing
            End Set
        End Property

        Public Property RadioCheckedStud As Boolean
            Get
                Return Me._radioCheckedStud
            End Get
            Set(value As Boolean)
                Me._radioCheckedStud = value
                OnPropertyChanged("RadioCheckedStud")
                _person.HireDate = Nothing
                _person.EnrollmentDate = Date.Now
            End Set
        End Property

        Public ReadOnly Property OkButton As ICommand
            Get
                If Me._okButton Is Nothing Then
                    Me._okButton = New RelayCommand(AddressOf OkCommand)
                End If
                Return Me._okButton
            End Get
        End Property

        Public ReadOnly Property CancelButton As ICommand
            Get
                If Me._cancelButton Is Nothing Then
                    Me._cancelButton = New RelayCommand(AddressOf CancelCommand)
                End If
                Return Me._cancelButton
            End Get
        End Property

        Public ReadOnly Property ResetButton As ICommand
            Get
                If Me._resetButton Is Nothing Then
                    Me._resetButton = New RelayCommand(AddressOf ResetCommand)
                End If
                Return _resetButton
            End Get
        End Property

        Sub OkCommand()
            Try
                DataContext.DBEntities.Person.Add(_person)
                DataContext.DBEntities.SaveChanges()
                newp.Close()
            Catch ex As Exception
                MsgBox("No se ha podido ingresar la persona", MsgBoxStyle.Critical)
            End Try
        End Sub

        Sub CancelCommand()
            newp.Close()
        End Sub

        Sub ResetCommand()
            _person = New Person
        End Sub
#End Region
    End Class
End Namespace