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
        Private _updateButtonCommand As ICommand
        Private _toEdit As Boolean
        Private _radioCheckedEmpl As Boolean
        Private _radioCheckedStud As Boolean

#End Region

#Region "Update"
        Public ReadOnly Property UpdateButtonCommand As ICommand
            Get
                If _updateButtonCommand Is Nothing Then
                    _updateButtonCommand = New RelayCommand(AddressOf UpdatePersonDB)
                End If
                Return _updateButtonCommand
            End Get
        End Property

        Sub UpdatePersonDB()
            Me._toEdit = True
            InsertPerson = SelectedRow
            If Me.InsertPerson.HireDate Is Nothing And _toEdit = True Then
                RadioCheckedStud = True
            ElseIf Me.InsertPerson.EnrollmentDate Is Nothing And _toEdit = True Then
                RadioCheckedEmpl = True
            End If
        End Sub
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

        Sub InsertPersonDB()
            If Me._ToEdit = True Then
                Dim personToEdit = (From x In DataContext.DBEntities.Person
                                    Where x.PersonID = _insertPerson.PersonID
                                    Select x).First()
                personToEdit.FirstName = _insertPerson.FirstName
                personToEdit.LastName = _insertPerson.LastName
                personToEdit.HireDate = _insertPerson.HireDate
                personToEdit.EnrollmentDate = _insertPerson.EnrollmentDate
                DataContext.DBEntities.SaveChanges()

            Else
                DataContext.DBEntities.Person.Add(InsertPerson)
                DataContext.DBEntities.SaveChanges()
            End If
            InsertPerson = New Person
            _toEdit = False
            refresh()
        End Sub

        Public Property InsertPerson As Person
            Get
                If Me._insertPerson Is Nothing Then
                    Me._insertPerson = New Person
                End If
                Return Me._insertPerson
            End Get
            Set(value As Person)
                _insertPerson = value
                OnPropertyChanged("InsertPerson")
            End Set
        End Property

        Public Property RadioCheckedEmpl As Boolean
            Get
                Return Me._radioCheckedEmpl
            End Get
            Set(value As Boolean)
                Me._radioCheckedEmpl = value
                OnPropertyChanged("RadioCheckedEmpl")
                _insertPerson.HireDate = Date.Now
                _insertPerson.EnrollmentDate = Nothing
            End Set
        End Property

        Public Property RadioCheckedStud As Boolean
            Get
                Return Me._radioCheckedStud
            End Get
            Set(value As Boolean)
                Me._radioCheckedStud = value
                OnPropertyChanged("RadioCheckedStud")
                _insertPerson.HireDate = Nothing
                _insertPerson.EnrollmentDate = Date.Now
            End Set
        End Property
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
            _insertPerson = New Person
            refresh()
        End Sub
#End Region
    End Class
End Namespace