Imports Modules.Persons.ViewModel
Public Class NewPerson
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.MainGrid.DataContext = New NewPersonViewModel(Me)
    End Sub
End Class
