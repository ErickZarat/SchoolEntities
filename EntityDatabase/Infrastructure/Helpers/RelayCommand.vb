Namespace Infrastructure.Helpers
    Public Class RelayCommand
        Implements ICommand
#Region "Declarations"
        Private ReadOnly _CanExecute As Func(Of Boolean)
        Private ReadOnly _Execute As Action(Of Object)
#End Region
#Region "Constructors"
        Public Sub New(ByVal execute As Action(Of Object))
            Me.New(execute, Nothing)
        End Sub
        Public Sub New(ByVal execute As Action(Of Object), ByVal canExecute As Func(Of Boolean))
            If execute Is Nothing Then
                Throw New ArgumentNullException("execute")
            End If
            _Execute = execute
            _CanExecute = canExecute
        End Sub
#End Region
#Region "ICommand"
        Public Custom Event CanExecuteChanged As EventHandler Implements System.Windows.Input.ICommand.CanExecuteChanged
            AddHandler(ByVal value As EventHandler)
                If _CanExecute IsNot Nothing Then
                    AddHandler CommandManager.RequerySuggested, value
                End If
            End AddHandler
            RemoveHandler(ByVal value As EventHandler)
                If _CanExecute IsNot Nothing Then
                    RemoveHandler CommandManager.RequerySuggested, value
                End If
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As System.EventArgs)
                CommandManager.InvalidateRequerySuggested()
            End RaiseEvent
        End Event
        Public Function CanExecute(ByVal parameter As Object) As Boolean Implements System.Windows.Input.ICommand.CanExecute
            If _CanExecute Is Nothing Then
                Return True
            Else
                Return _CanExecute.Invoke
            End If
        End Function
        Public Sub Execute(ByVal parameter As Object) Implements System.Windows.Input.ICommand.Execute
            _Execute(parameter)
        End Sub
#End Region
    End Class
End Namespace