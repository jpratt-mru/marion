Imports LibraryManager.Actors
Imports LibraryManager.Collections
'
' State pattern Context class
'
Namespace State
    Public Class BorrowingMachine

        Private m_unregisteredName As String
        Property UnregisteredName() As String
            Get
                Return m_unregisteredName
            End Get
            Set(ByVal value As String)
                m_unregisteredName = value
            End Set
        End Property

        Private m_unregisteredBarcode As SimpleBarcode
        Property UnregisteredBarcode() As SimpleBarcode
            Get
                Return m_unregisteredBarcode
            End Get
            Set(ByVal value As SimpleBarcode)
                m_unregisteredBarcode = value
            End Set
        End Property

        Private m_currentGame As Game
        Property CurrentGame() As Game
            Get
                Return m_currentGame
            End Get
            Set(ByVal value As Game)
                m_currentGame = value
            End Set
        End Property

        Private m_currentBorrower As Borrower
        Property CurrentBorrower() As Borrower
            Get
                Return m_currentBorrower
            End Get
            Set(ByVal value As Borrower)
                m_currentBorrower = value
            End Set
        End Property

        Private m_waitingForGameBarcodeState As BorrowingMachineState
        ReadOnly Property WaitingForGameBarcodeState() As BorrowingMachineState
            Get
                Return m_waitingForGameBarcodeState
            End Get
        End Property

        Private m_waitingForBorrowerBarcodeState As BorrowingMachineState
        ReadOnly Property WaitingForBorrowerBarcodeState() As BorrowingMachineState
            Get
                Return m_waitingForBorrowerBarcodeState
            End Get
        End Property

        Private m_waitingForUnregisteredGameNameState As BorrowingMachineState
        ReadOnly Property WaitingForUnregisteredGameNameState() As BorrowingMachineState
            Get
                Return m_waitingForUnregisteredGameNameState
            End Get
        End Property

        Private m_waitingForUnregisteredGameOwnerNameState As BorrowingMachineState
        ReadOnly Property WaitingForUnregisteredGameOwnerNameState() As BorrowingMachineState
            Get
                Return m_waitingForUnregisteredGameOwnerNameState
            End Get
        End Property

        Private m_waitingForUnregisteredBorrowerNameState As BorrowingMachineState
        ReadOnly Property WaitingForUnregisteredBorrowerNameState() As BorrowingMachineState
            Get
                Return m_waitingForUnregisteredBorrowerNameState
            End Get
        End Property

        Private m_currentState As BorrowingMachineState
        Property CurrentState() As BorrowingMachineState
            Get
                Return m_currentState
            End Get
            Set(ByVal value As BorrowingMachineState)
                m_currentState = value
                m_currentState.DisplayInitialStateLayout()
            End Set
        End Property

        Public Sub New(ByVal borrowingForm As IBorrowingFormActions, ByVal currentBorrowingContext As CurrentBorrowingContext)
            m_waitingForGameBarcodeState = New WaitingForGameBarcodeState(Me, borrowingForm, currentBorrowingContext)
            m_waitingForBorrowerBarcodeState = New WaitingForBorrowerBarcodeState(Me, borrowingForm, currentBorrowingContext)
            m_waitingForUnregisteredGameNameState = New WaitingForUnregisteredGameNameState(Me, borrowingForm, currentBorrowingContext)
            m_waitingForUnregisteredGameOwnerNameState = New WaitingForUnregisteredGameOwnerNameState(Me, borrowingForm, currentBorrowingContext)
            m_waitingForUnregisteredBorrowerNameState = New WaitingForUnregisteredBorrowerNameState(Me, borrowingForm, currentBorrowingContext)

            m_currentState = m_waitingForGameBarcodeState
            m_currentState.DisplayInitialStateLayout()
        End Sub

        Public Sub ReactToUnknownBarcode()
            m_currentState.ReactToUnknownBarcode()
        End Sub

        Public Sub ReactToCheckedOutGameBarcodeEntry()
            m_currentState.ReactToCheckedOutGameBarcodeEntry()
        End Sub

        Public Sub ReactToCheckedInGameBarcodeEntry()
            m_currentState.ReactToCheckedInGameBarcodeEntry()
        End Sub

        Public Sub ReactToUnregisteredBorrowerBarcodeEntry()
            m_currentState.ReactToUnregisteredBorrowerBarcodeEntry()
        End Sub

        Public Sub ReactToBorrowerWithOneGameOutBarcodeEntry()
            m_currentState.ReactToBorrowerWithOneGameOutBarcodeEntry()
        End Sub

        Public Sub ReactToBorrowerWithNoGameOutBarcodeEntry()
            m_currentState.ReactToBorrowerWithNoGameOutBarcodeEntry()
        End Sub

        Public Sub ReactToUnregisteredGameBarcodeEntry()
            m_currentState.ReactToUnregisteredGameBarcodeEntry()
        End Sub

        Public Sub ReactToCancelEntry()
            m_currentState.ReactToCancelEntry()
        End Sub

        Public Sub ReactToTextEntry()
            m_currentState.ReactToTextEntry()
        End Sub

        Public Sub DisplayInitialStateLayout()
            m_currentState.DisplayInitialStateLayout()
        End Sub

    End Class
End Namespace