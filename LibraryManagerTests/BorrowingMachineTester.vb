Imports LibraryManager.Collections
Imports LibraryManager
Imports LibraryManager.State
Imports LibraryManager.Actors


Public Class BorrowingMachineTester
    'Private m_borrowers As Borrowers
    'Property Borrowers() As Borrowers
    '    Get
    '        Return m_borrowers
    '    End Get
    '    Set(ByVal value As Borrowers)
    '        m_borrowers = value
    '    End Set
    'End Property

    'Private m_games As Games
    'Property Games() As Games
    '    Get
    '        Return m_games
    '    End Get
    '    Set(ByVal value As Games)
    '        m_games = value
    '    End Set
    'End Property

    'Private m_outstandingLoans As OutstandingLoans
    'Public Property OutstandingLoans() As OutstandingLoans
    '    Get
    '        Return m_outstandingLoans
    '    End Get
    '    Set(ByVal value As OutstandingLoans)
    '        m_outstandingLoans = value
    '    End Set
    'End Property

    Private m_borrowingMachine As BorrowingMachine
    Public Property BorrowingMachine() As BorrowingMachine
        Get
            Return m_borrowingMachine
        End Get
        Set(ByVal value As BorrowingMachine)
            m_borrowingMachine = value
        End Set
    End Property

    Private m_borrowingFormStub As BorrowingFormStub
    Public Property BorrowingFormStub() As BorrowingFormStub
        Get
            Return m_borrowingFormStub
        End Get
        Set(ByVal value As BorrowingFormStub)
            m_borrowingFormStub = value
        End Set
    End Property

    Private m_currentBorrowingContext As CurrentBorrowingContext
    Public Property CurrentBorrowingContext() As CurrentBorrowingContext
        Get
            Return m_currentBorrowingContext
        End Get
        Set(ByVal value As CurrentBorrowingContext)
            m_currentBorrowingContext = value
        End Set
    End Property

    Public Sub New()
        m_borrowingFormStub = New BorrowingFormStub
        m_currentBorrowingContext = New CurrentBorrowingContext()
        m_borrowingMachine = New BorrowingMachine(m_borrowingFormStub, m_currentBorrowingContext)
    End Sub

    Public Sub SimulateStringInput(ByVal input As String)

        m_borrowingFormStub.stubbedCodeTextBoxText = input
        If (input.StartsWith(Constants.GAME_PREFIX) Or input.StartsWith(Constants.BORROWER_PREFIX)) Then
            Dim code As New SimpleBarcode(input)
            If (code.Category = Enums.BarcodeCategory.Unknown) Then
                m_borrowingMachine.ReactToUnknownBarcode()
            ElseIf (code.Category = Enums.BarcodeCategory.Borrower) Then
                If (CurrentBorrowingContext.ContainsBorrower(code)) Then
                    Dim borrower As Borrower = CurrentBorrowingContext.GetBorrower(code)
                    If (borrower.HasGameOut) Then
                        m_borrowingMachine.ReactToBorrowerWithOneGameOutBarcodeEntry()
                    Else
                        m_borrowingMachine.ReactToBorrowerWithNoGameOutBarcodeEntry()
                    End If
                Else
                    m_borrowingMachine.ReactToUnregisteredBorrowerBarcodeEntry()
                End If
            ElseIf (code.Category = Enums.BarcodeCategory.Game) Then
                If (CurrentBorrowingContext.ContainsGame(code)) Then
                    Dim libraryItem As Game = CurrentBorrowingContext.GetGame(code)
                    If (libraryItem.IsCheckedOut) Then
                        m_borrowingMachine.ReactToCheckedOutGameBarcodeEntry()
                    Else
                        m_borrowingMachine.ReactToCheckedInGameBarcodeEntry()
                    End If
                Else
                    m_borrowingMachine.ReactToUnregisteredGameBarcodeEntry()

                End If

            End If

        Else
            m_borrowingMachine.ReactToTextEntry()
        End If
    End Sub

    Public Sub SimulateEscapeInput()
        m_borrowingMachine.ReactToCancelEntry()
    End Sub

End Class
