Imports LibraryManager.Actors
Imports LibraryManager.Collections
Imports LibraryManager.State

Namespace Forms
    Public Class BorrowingForm
        Implements IBorrowingFormActions

        Private borrowingMachine As BorrowingMachine
        Private currentBorrowingContext As CurrentBorrowingContext
        Private initialKeyHasBeenPressed As Boolean = False

        Private Sub initialize() Handles MyBase.Loaded

            currentBorrowingContext = New CurrentBorrowingContext
            Dim errorMessages As String = currentBorrowingContext.Load

            Me.Hide()
            If (errorMessages.Equals("")) Then
                Me.DataContext = currentBorrowingContext
                borrowingMachine = New BorrowingMachine(Me, currentBorrowingContext)
                RefocusOnTextBox()
                Me.Show()
            Else
                MessageBox.Show( _
                                 "The following errors were found:" + vbCrLf + vbCrLf + errorMessages + vbCrLf + _
                                 "Shutting down....")
                Application.Current.Shutdown()
            End If
        End Sub

        Public Sub RefocusOnTextBox() Implements IBorrowingFormActions.RefocusOnTextBox
            CodeTextBox.IsEnabled = True
            CodeTextBox.Focus()
            CodeTextBox.SelectAll()
        End Sub

        Public Sub DisableCodeEntry() Implements IBorrowingFormActions.DisableCodeEntry
            CodeTextBox.IsEnabled = False
        End Sub

        Public Sub DisplayMessageInTextBox (ByVal message As String, ByVal size As Integer, ByVal textColor As Color) _
            Implements IBorrowingFormActions.DisplayMessageInTextBox
            CodeTextBox.FontSize = size
            CodeTextBox.Text = message
            CodeTextBox.Foreground = New SolidColorBrush (textColor)
            RefocusOnTextBox()
        End Sub

        Public Sub ChangeFormColor (ByVal color As Color) Implements IBorrowingFormActions.ChangeFormColor
            Me.Background = New SolidColorBrush (color)
        End Sub

        Public Sub DisplayGameInfo (ByVal titleText As String, ByVal mainText As String) _
            Implements IBorrowingFormActions.DisplayGameInfo
            GameLabelTitle.Text = titleText
            GameLabel.Text = mainText
        End Sub

        Public Sub DisplayBorrowerInfo (ByVal titleText As String, ByVal mainText As String) _
            Implements IBorrowingFormActions.DisplayBorrowerInfo
            BorrowerLabelTitle.Text = titleText
            BorrowerLabel.Text = mainText
        End Sub


        Public Function GetCodeTextBoxText() As String Implements IBorrowingFormActions.GetCodeTextBoxText
            Return CodeTextBox.Text
        End Function

        Private Sub CodeTextBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles CodeTextBox.KeyDown
            If ((Keyboard.Modifiers <> ModifierKeys.Control AndAlso e.Key <> Key.Escape AndAlso Not initialKeyHasBeenPressed)) Then
                CodeTextBox.Clear()
                CodeTextBox.Foreground = New SolidColorBrush(Colors.Black)
                CodeTextBox.FontSize = Constants.LARGE_MESSAGE_FONT_SIZE
                initialKeyHasBeenPressed = True
            End If

        End Sub

        Private Sub CodeTextBox_KeyUp (ByVal sender As Object, ByVal e As KeyEventArgs) Handles CodeTextBox.KeyUp
            If ((Keyboard.Modifiers = ModifierKeys.Control) And (e.Key = Key.R)) Then
                MessageBox.Show(Logger.Report(), "Basic Stats")
                RefocusOnTextBox()
            ElseIf e.Key = Key.Return Then
                initialKeyHasBeenPressed = False
                If _
                    (CodeTextBox.Text.ToUpper.StartsWith(Constants.GAME_PREFIX) Or _
                     CodeTextBox.Text.ToUpper.StartsWith(Constants.BORROWER_PREFIX)) Then
                    Dim code As New SimpleBarcode(CodeTextBox.Text)
                    If (code.Category = Enums.BarcodeCategory.Unknown) Then
                        borrowingMachine.ReactToUnknownBarcode()
                    ElseIf (code.Category = Enums.BarcodeCategory.Borrower) Then
                        If (currentBorrowingContext.ContainsBorrower(code)) Then
                            Dim borrower As Borrower = currentBorrowingContext.GetBorrower(code)
                            If (borrower.HasGameOut) Then
                                borrowingMachine.ReactToBorrowerWithOneGameOutBarcodeEntry()
                            Else
                                borrowingMachine.ReactToBorrowerWithNoGameOutBarcodeEntry()
                            End If
                        Else
                            borrowingMachine.ReactToUnregisteredBorrowerBarcodeEntry()
                        End If
                    ElseIf (code.Category = Enums.BarcodeCategory.Game) Then
                        If (currentBorrowingContext.ContainsGame(code)) Then
                            Dim libraryItem As Game = currentBorrowingContext.GetGame(code)
                            If (libraryItem.IsCheckedOut) Then
                                borrowingMachine.ReactToCheckedOutGameBarcodeEntry()
                            Else
                                borrowingMachine.ReactToCheckedInGameBarcodeEntry()
                            End If
                        Else
                            borrowingMachine.ReactToUnregisteredGameBarcodeEntry()

                        End If

                    End If

                Else
                    borrowingMachine.ReactToTextEntry()
                End If
                CodeTextBox.SelectAll()
            ElseIf e.Key = Key.Escape Then
                borrowingMachine.ReactToCancelEntry()
            End If

        End Sub
    End Class
End Namespace