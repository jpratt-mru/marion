Imports LibraryManager.Actors
Imports System.Windows.Threading
Imports LibraryManager.Collections

Namespace State
    Public Class WaitingForUnregisteredGameOwnerNameState
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
            borrowingMachine.CurrentState = borrowingMachine.WaitingForBorrowerBarcodeState
        End Sub

        Public Overrides Sub DisplayInitialStateLayout()
            borrowingForm.ChangeFormColor(Colors.LightBlue)
            borrowingForm.DisplayMessageInTextBox("Enter New Library Item's Owner's Name", Constants.SMALL_MESSAGE_FONT_SIZE, Colors.Blue)
            borrowingForm.DisplayGameInfo("New Item (" + borrowingMachine.UnregisteredBarcode.ToString + "):", borrowingMachine.UnregisteredName)
            borrowingForm.DisplayBorrowerInfo("", "")
        End Sub

        Public Overrides Sub ReactToUnknownBarcode()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_OWNER_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToCancelEntry()
            borrowingMachine.CurrentState = borrowingMachine.WaitingForUnregisteredGameNameState
        End Sub

        Public Overrides Sub ReactToTextEntry()

            Dim gameOwnerName As String = borrowingForm.GetCodeTextBoxText.Trim

            If (gameOwnerName.Length = 0) Then
                borrowingForm.DisplayMessageInTextBox("Owner's name can't be blank", Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
                borrowingForm.DisableCodeEntry()
                clearBorrowingFormTimer.Start()
            Else
                Dim newlyRegisteredGame As New Game(borrowingMachine.UnregisteredBarcode, borrowingMachine.UnregisteredName, gameOwnerName)
                currentBorrowingContext.Add(newlyRegisteredGame)
                borrowingMachine.CurrentGame = newlyRegisteredGame
                borrowingForm.DisplayGameInfo("New Game Library Item:", newlyRegisteredGame.Name)
                borrowingForm.DisableCodeEntry()
                clearAndChangeStateTimer.Start()

                ' Note remove following comment if using BorrowingMachineTests
                'borrowingMachine.CurrentState = borrowingMachine.WaitingForBorrowerBarcodeState

            End If

        End Sub

        Public Overrides Sub ReactToBorrowerWithOneGameOutBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_OWNER_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToBorrowerWithNoGameOutBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_OWNER_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToUnregisteredBorrowerBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_OWNER_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToCheckedInGameBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_OWNER_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToCheckedOutGameBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_OWNER_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToUnregisteredGameBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_OWNER_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub


    End Class
End Namespace