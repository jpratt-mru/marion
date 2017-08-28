Imports LibraryManager.Collections

Namespace State
    Public Class WaitingForUnregisteredGameNameState
        Inherits BorrowingMachineState

        Public Sub New(ByVal borrowingMachine As BorrowingMachine, ByVal borrowingForm As IBorrowingFormActions, ByVal currentBorrowingContext As CurrentBorrowingContext)
            MyBase.New(borrowingMachine, borrowingForm, currentBorrowingContext)
        End Sub

        Public Overrides Sub DisplayInitialStateLayout()
            borrowingForm.ChangeFormColor(Colors.Azure)
            borrowingForm.DisplayMessageInTextBox("Enter New Library Item's Name", Constants.SMALL_MESSAGE_FONT_SIZE, Colors.Blue)
            borrowingForm.DisplayGameInfo("New Item's Barcode:", borrowingMachine.UnregisteredBarcode.ToString)
            borrowingForm.DisplayBorrowerInfo("", "")
        End Sub

        Public Overrides Sub ReactToUnknownBarcode()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_NAME_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToCancelEntry()
            borrowingMachine.CurrentState = borrowingMachine.WaitingForGameBarcodeState
        End Sub

        Public Overrides Sub ReactToTextEntry()
            Dim gameNameText As String = borrowingForm.GetCodeTextBoxText.Trim

            If (gameNameText.Length = 0) Then
                borrowingForm.DisplayMessageInTextBox("Name can't be blank", Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
                borrowingForm.DisableCodeEntry()
                clearBorrowingFormTimer.Start()
            Else
                borrowingMachine.UnregisteredName = borrowingForm.GetCodeTextBoxText
                borrowingMachine.CurrentState = borrowingMachine.WaitingForUnregisteredGameOwnerNameState
            End If
        End Sub

        Public Overrides Sub ReactToBorrowerWithOneGameOutBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_NAME_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToBorrowerWithNoGameOutBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_NAME_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToUnregisteredBorrowerBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_NAME_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToCheckedInGameBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_NAME_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToCheckedOutGameBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_NAME_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub

        Public Overrides Sub ReactToUnregisteredGameBarcodeEntry()
            DisplayNotificationAndPause(Constants.NOT_VALID_GAME_NAME_TEXT_WARNING, Constants.LARGE_MESSAGE_FONT_SIZE, Colors.Red)
        End Sub


    End Class
End Namespace