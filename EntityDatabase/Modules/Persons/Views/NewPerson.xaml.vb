Imports Modules.Persons.ViewModel
Public Class NewPerson
    Sub New(personParam As Person, edit As Boolean)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.MainGrid.DataContext = New NewPersonViewModel(Me, personParam, edit)
    End Sub
End Class
