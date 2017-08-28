Imports LibraryManager.Actors
Imports System.Windows.Threading
Imports LibraryManager.Collections

Namespace State
    Public Class WaitingForUnregisteredBorrowerNameState
        Inherits BorrowingMachineState

        Protected WithEvents clearAndChangeStateTimer As DispatcherTimer

        Public Sub New(ByVal borrowingMachine As BorrowingMachine, ByVal borrowingForm As IBorrowingFormActions, ByVal currentBorrowingContext As CurrentBorrowingContext)
            MyBase.New(borrowingMachine, borrowingForm, currentBorrowingContext)
            clearAndChangeStateTimer = New DispatcherTimer(DispatcherPriority.Normal)
            clearAndChangeStateTimer.Interval = New TimeSpan(0, 0, Constants.CLEAR_FORM_TIMER_INTERVAL)
            AddHandler clearAndChangeStateTimer.Tick, AddressOf State_Timer_Tick
        End Sub

        Private Sub State_Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clearAndChangeStateTimer.Tick
            clearAndChangeStateTimer.Stop()
            borrowingMachine.CurrentState = borrowingMachine.WaitingForGameBarcodeState
        End Sub

        Public Overrides Sub DisplayInitialStateLayout()
            borrowingForm.ChangeFormColor(Colors.LightBlue)
            borrowingForm.DisplayMessageInTextBox("Please enter new borrower's first and last names", Constants.SMALL_MESSAGE_FONT_SIZE, Colors.Blue)
            borrowingForm.DisplayGameInfo("", "")
            borrowingForm.DisplayBorrowerInfo("New Borrower's Barcode:", borrowingMachine.UnregisteredBarcode.ToString)
        End Sub

        Public Overrides Sub ReactToUnknownBarcode()
            DisplayNotificationAndPause(Constants.NOT_VALID_BORROWER_NAME_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToCancelEntry()
            borrowingMachine.CurrentState = borrowingMachine.WaitingForBorrowerBarcodeState
        End Sub

        Public Overrides Sub ReactToTextEntry()
            Dim nameText As String = borrowingForm.GetCodeTextBoxText.Trim
            Dim borrowerNameText As String() = nameText.Split

            If (nameText.Length = 0) Then
                borrowingForm.DisplayMessageInTextBox("Name can't be blank", Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
                borrowingForm.DisableCodeEntry()
                clearBorrowingFormTimer.Start()
            ElseIf (borrowerNameText.Count = 1) Then
                borrowingForm.DisplayMessageInTextBox("Need a first AND last name", Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
                borrowingForm.DisableCodeEntry()
                clearBorrowingFormTimer.Start()
            ElseIf (borrowerNameText.Count = 2) Then
                Dim firstName As String = borrowerNameText(0).Trim
                Dim lastName As String = borrowerNameText(1).Trim
                Dim borrowerToRegister As New Borrower(borrowingMachine.UnregisteredBarcode, firstName, lastName)
                borrowerToRegister.HasGameOut = True
                borrowingMachine.CurrentGame.Borrower = borrowerToRegister
                currentBorrowingContext.Add(borrowerToRegister)
                currentBorrowingContext.CheckOut(borrowingMachine.CurrentGame.Barcode, borrowerToRegister.Barcode)

                'borrowingForm.RefreshItemsOutDisplay()

                borrowingForm.DisplayGameInfo("Checking out:", borrowingMachine.CurrentGame.Name)
                borrowingForm.DisplayBorrowerInfo("To:", firstName & " " & lastName)
                borrowingForm.DisplayMessageInTextBox("Game Checked Out", Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Black)

                borrowingMachine.CurrentGame = Nothing
                borrowingMachine.CurrentBorrower = Nothing

                borrowingForm.DisableCodeEntry()
                clearAndChangeStateTimer.Start()

                ' Note remove following comment if using BorrowingMachineTests
                'borrowingMachine.CurrentState = borrowingMachine.WaitingForGameBarcodeState
            Else
                borrowingForm.DisplayMessageInTextBox("Need a first and last name only", Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
                borrowingForm.DisableCodeEntry()
                clearBorrowingFormTimer.Start()
            End If


        End Sub

        Public Overrides Sub ReactToBorrowerWithOneGameOutBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_BORROWER_NAME_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToBorrowerWithNoGameOutBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_BORROWER_NAME_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToUnregisteredBorrowerBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_BORROWER_NAME_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToCheckedInGameBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_BORROWER_NAME_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToCheckedOutGameBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_BORROWER_NAME_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToUnregisteredGameBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_BORROWER_NAME_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub
    End Class
End Namespace