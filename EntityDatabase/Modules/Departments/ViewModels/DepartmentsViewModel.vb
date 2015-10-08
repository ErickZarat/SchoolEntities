Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports BusinessObjects.Helpers
Imports Infrastructure.Helpers

Namespace Modules.Departments.ViewModels
    Public Class DepartmentsViewModel
        Inherits ViewModelBase

#Region "Declare"
        Private _departments As ObservableCollection(Of Department)
        Private dataAccess As IDepartmentService
        Private dataAccessPerson As IPersonService
        Private _insertDepa As Department
        Private _selectedRow As Department
        Private _toEdit As Boolean
        Private _personslist As ObservableCollection(Of Integer)
        Private _InsertCommand As ICommand
        Private _UpdateButtonCommand As ICommand
        Private _DeleteButtonCommand As ICommand

#End Region



#Region "Update"
        Public Property SelectedRow As Department
            Get
                Return _selectedRow
            End Get
            Set(value As Department)
                _selectedRow = value
            End Set
        End Property

        Public ReadOnly Property UpdateButtonCommand As ICommand
            Get
                If Me._UpdateButtonCommand Is Nothing Then
                    Me._UpdateButtonCommand = New RelayCommand(AddressOf UpdateCommand)
                End If
                Return Me._UpdateButtonCommand
            End Get
        End Property

        Sub UpdateCommand()
            _toEdit = True
            InsertDepartment = SelectedRow
        End Sub
#End Region

#Region "Insert"
        Public Property InsertDepartment As Department
            Get
                If _insertDepa Is Nothing Then
                    _insertDepa = New Department
                End If
                Return _insertDepa
            End Get
            Set(value As Department)
                _insertDepa = value
            End Set
        End Property



        Public ReadOnly Property InsertCommand As ICommand
            Get
                If Me._InsertCommand Is Nothing Then
                    Me._InsertCommand = New RelayCommand(AddressOf AddCommand)
                End If
                Return Me._InsertCommand
            End Get
        End Property

        Sub AddCommand()

            If _toEdit = True Then
                Dim dep = (From d In DataContext.DBEntities.Department
                          Where d.DepartmentID = InsertDepartment.DepartmentID
                          Select d).First
                dep = InsertDepartment
                DataContext.DBEntities.SaveChanges()
            Else
                InsertDepartment.StartDate = Date.Now
                DataContext.DBEntities.Department.Add(InsertDepartment)
                DataContext.DBEntities.SaveChanges()
            End If

            InsertDepartment = Nothing
            _toEdit = False
        End Sub
#End Region
#Region "Delete"
        Public ReadOnly Property DeleteButtonCommand As ICommand
            Get
                If _DeleteButtonCommand Is Nothing Then
                    _DeleteButtonCommand = New RelayCommand(AddressOf removeCommand)
                End If
                Return Me._DeleteButtonCommand
            End Get
        End Property

        Sub removeCommand()
            DataContext.DBEntities.Department.Remove((From d In DataContext.DBEntities.Department
                              Where d.DepartmentID = SelectedRow.DepartmentID
                              Select d).First)
            DataContext.DBEntities.SaveChanges()
        End Sub

#End Region
#Region "List"

        Public Property Departments As ObservableCollection(Of Department)
            Get
                Return Me._departments
            End Get
            Set(value As ObservableCollection(Of Department))
                Me._departments = value
                OnPropertyChanged("Departments")
            End Set
        End Property

        ' Function to get all departments from service
        Private Function GetAllDepartments() As IQueryable(Of Department)
            Return Me.dataAccess.GetAllDepartments
        End Function

        Sub Refresh()
            Me._departments.Clear()
            Me._personslist.Clear()
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IDepartmentService)(New DepartmentService)
            ServiceLocator.RegisterService(Of IPersonService)(New PersonService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IDepartmentService)()
            Me.dataAccessPerson = GetService(Of IPersonService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllPersons
                Me._personslist.Add(element.PersonID)
            Next
            For Each element In Me.GetAllDepartments
                Me._departments.Add(element)
            Next

        End Sub
        ''' <summary>
        ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Persons As ObservableCollection(Of Integer)
            Get
                Return Me._personslist
            End Get
            Set(value As ObservableCollection(Of Integer))
                Me._personslist = value
                OnPropertyChanged("Persons")
            End Set
        End Property

        ' Function to get all departments from service
        Private Function GetAllPersons() As IQueryable(Of Person)
            Return Me.dataAccessPerson.GetAllPersons
        End Function

        Sub New()
            'Initialize property variable of departments
            Me._departments = New ObservableCollection(Of Department)
            Me._personslist = New ObservableCollection(Of Integer)
            Refresh()
        End Sub
#End Region
    End Class
End Namespace

