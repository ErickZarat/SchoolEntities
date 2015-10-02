Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports BusinessObjects.Helpers
Imports Infrastructure.Helpers
Imports Modules.Persons.ViewModel

Namespace Modules.Persons.ViewModel
    Public Class NewPersonViewModel
        Inherits ViewModelBase

        Public _person As Person
        Private _radioCheckedEmpl As Boolean
        Private _radioCheckedStud As Boolean
        Public _okButton As ICommand
        Public _cancelButton As ICommand
        Public _resetButton As ICommand
        Public _Ventana As NewPerson
        Public _ToEdit As Boolean

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
                If Me._ToEdit = True Then
                    Dim personToEdit = (From x In DataContext.DBEntities.Person
                                        Where x.PersonID = _person.PersonID
                                        Select x).First()
                    personToEdit.FirstName = _person.FirstName
                    personToEdit.LastName = _person.LastName
                    personToEdit.HireDate = _person.HireDate
                    personToEdit.EnrollmentDate = _person.EnrollmentDate
                    DataContext.DBEntities.SaveChanges()

                Else
                    DataContext.DBEntities.Person.Add(_person)
                    DataContext.DBEntities.SaveChanges()
                End If
                
                _Ventana.Close()
            Catch ex As Exception
                MsgBox("No se ha podido ingresar la persona", MsgBoxStyle.Critical)
            End Try
        End Sub

        Sub CancelCommand()
            Me._Ventana.Close()
        End Sub

        Sub ResetCommand()
            Me._person = New Person
        End Sub

        Sub New(ByRef newVentana As NewPerson, persona As Person, toEdit As Boolean)
            Me._Ventana = newVentana
            Me._person = persona
            Me._ToEdit = toEdit

            If Me._person.HireDate Is Nothing And toEdit = True Then
                RadioCheckedStud = True
            ElseIf Me._person.EnrollmentDate Is Nothing And toEdit = True Then
                RadioCheckedEmpl = True
            End If
        End Sub
    End Class
End Namespace