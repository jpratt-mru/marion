Imports System.ComponentModel
Imports LibraryManager.Actors
Imports System.Collections.Specialized


Namespace Collections
    Public Class CurrentBorrowingContext
        Implements INotifyPropertyChanged

        Private borrowers As NonvolatileContextCollection(Of Borrower)
        Private games As NonvolatileContextCollection(Of Game)
        Private outstandingLoans As NonvolatileContextCollection(Of OutstandingLoan)
        Private sortedLoans As List(Of OutstandingLoan)

        ReadOnly Property OutstandingLoansDisplay() As List(Of OutstandingLoan)
            Get
                Dim sortedByTimeList As New SortedList(Of DateTime, OutstandingLoan)
                For Each loan As OutstandingLoan In outstandingLoans.Values
                    sortedByTimeList.Add(loan.CheckOutTime, loan)
                Next
                Return sortedByTimeList.Values.ToList
            End Get
        End Property

        ReadOnly Property CheckOutTimes() As StringCollection
            Get
                Return GetSortedCheckOutTimes()
            End Get
        End Property

        Private Function GetSortedCheckOutTimes() As StringCollection
            Dim times As New StringCollection
            For Each outstandingLoan As OutstandingLoan In sortedLoans
                times.Add(outstandingLoan.DisplayedCheckOutTime)
            Next
            Return times
        End Function

        ReadOnly Property CheckedOutGameNames() As StringCollection
            Get
                Return GetSortedCheckedOutGameNames()
            End Get
        End Property

        Private Function GetSortedCheckedOutGameNames() As StringCollection
            Dim gameNames As New StringCollection
            For Each outstandingLoan As OutstandingLoan In sortedLoans
                gameNames.Add(outstandingLoan.DisplayedGameInfo)
            Next
            Return gameNames
        End Function

        ReadOnly Property CheckedOutGameBorrowers() As StringCollection
            Get
                Return GetSortedBorrowerNames()
            End Get
        End Property

        Private Function GetSortedBorrowerNames() As StringCollection
            Dim borrowerNames As New StringCollection
            For Each outstandingLoan As OutstandingLoan In sortedLoans
                borrowerNames.Add(outstandingLoan.DisplayedBorrowerInfo)
            Next
            Return borrowerNames
        End Function

        Private Function GetOutstandingLoansSortedByTime() As List(Of OutstandingLoan)
            Dim sortedByTimeList As New SortedList(Of DateTime, OutstandingLoan)
            For Each loan As OutstandingLoan In outstandingLoans.Values
                sortedByTimeList.Add(loan.CheckOutTime, loan)
            Next
            OnPropertyChanged("CheckOutTimes")
            Return New List(Of OutstandingLoan)(sortedByTimeList.Values)
        End Function

        Public Sub New()
            borrowers = New NonvolatileContextCollection(Of Borrower)(Constants.BORROWER_FILE_NAME)
            games = New NonvolatileContextCollection(Of Game)(Constants.GAME_LIBRARY_ITEMS_FILE_NAME)
            outstandingLoans = New NonvolatileContextCollection(Of OutstandingLoan)(Constants.OUTSTANDING_LOANS_FILE_NAME)
            SortedLoans = GetOutstandingLoansSortedByTime()
        End Sub

        Public Sub New(ByVal otherBorrowingContext As CurrentBorrowingContext)
            borrowers = New NonvolatileContextCollection(Of Borrower)(otherBorrowingContext.borrowers)
            games = New NonvolatileContextCollection(Of Game)(otherBorrowingContext.games)
            outstandingLoans = New NonvolatileContextCollection(Of OutstandingLoan)(otherBorrowingContext.outstandingLoans)
            SortedLoans = GetOutstandingLoansSortedByTime()
        End Sub

        ' Game and borrower must exist
        Public Sub CheckOut(ByVal gameBarcode As SimpleBarcode, ByVal borrowerSimpleBarcode As SimpleBarcode)
            If (gameBarcode Is Nothing) Then
                Throw New ArgumentNullException("Game barcode can't be null.")
            ElseIf (Not games.ContainsKey(gameBarcode)) Then
                Throw New ArgumentException("Can't check out an unregisterd game.")
            ElseIf (borrowerSimpleBarcode Is Nothing) Then
                Throw New ArgumentNullException("Borrower barcode can't be null.")
            ElseIf (Not borrowers.ContainsKey(borrowerSimpleBarcode)) Then
                Throw New ArgumentException("Can't check out a game to an unregistered user.")
            Else
                Dim borrower As Borrower = borrowers(borrowerSimpleBarcode)
                Dim itemGoingOut As Game = games(gameBarcode)
                borrower.HasGameOut = True
                itemGoingOut.Borrower = borrower

                Dim outstandingLoan As New OutstandingLoan(itemGoingOut)
                If (Not outstandingLoans.ContainsKey(gameBarcode)) Then
                    outstandingLoans.Add(gameBarcode, outstandingLoan)
                    OnPropertyChanged("OutstandingLoansDisplay")
                    Logger.Log(DisplayTime(outstandingLoan.CheckOutTime) + " " + "CHECKOUT: " + itemGoingOut.ToFileString + " to " + borrower.ToFileString)
                End If

                Save()
            End If

        End Sub

        'Private Sub TriggerOutstandingLoanRefresh()
        '    SortedLoans = GetOutstandingLoansSortedByTime()
        '    'OnPropertyChanged("CheckedOutGameNames")
        '    'OnPropertyChanged("CheckedOutGameBorrowers")
        '    'OnPropertyChanged("CheckOutTimes")
        'End Sub

        ' Game and borrower must exist
        Public Sub CheckIn(ByVal gameBarcode As SimpleBarcode)
            If (gameBarcode Is Nothing) Then
                Throw New ArgumentNullException("Game barcode can't be null.")
            ElseIf (Not games.ContainsKey(gameBarcode)) Then
                Throw New ArgumentException("Can't check out an unregisterd game.")
            Else
                Dim loanComingIn As OutstandingLoan = outstandingLoans(gameBarcode)
                Dim gameComingIn As Game = loanComingIn.BorrowedItem
                Dim loanedItemSimpleBarcode As SimpleBarcode = gameComingIn.Barcode
                Dim loanedItemBorrower As Borrower = CurrentBorrower(loanedItemSimpleBarcode)
                loanedItemBorrower.HasGameOut = False
                games(loanedItemSimpleBarcode).Borrower = Nothing
                Logger.Log(DisplayTime(DateTime.Now) + " " + "CHECKIN: " + gameComingIn.ToFileString + " from " + loanedItemBorrower.ToFileString)
                outstandingLoans.Remove(loanedItemSimpleBarcode)
                OnPropertyChanged("OutstandingLoansDisplay")
                Save()
            End If

        End Sub

        Public Function ContainsBorrower(ByVal borrowerBarcode As SimpleBarcode) As Boolean
            Return borrowers.ContainsKey(borrowerBarcode)
        End Function

        Public Function ContainsGame(ByVal gameBarcode As SimpleBarcode) As Boolean
            Return games.ContainsKey(gameBarcode)
        End Function

        Public Function CurrentBorrower(ByVal gameBarcode As SimpleBarcode) As Borrower
            Dim borrowedGame As Game = games(gameBarcode)
            Dim borrowedGameBorrower As Borrower = borrowedGame.Borrower
            If (borrowedGameBorrower Is Nothing) Then
                Return Nothing
            Else
                Dim borrowedGameBorrowerBarcode As SimpleBarcode = borrowedGameBorrower.Barcode
                Return (borrowers(borrowedGameBorrowerBarcode))
            End If
        End Function

        Public Function CurrentItemOut(ByVal borrowerBarcode As SimpleBarcode) As Game
            If (borrowers(borrowerBarcode).HasGameOut) Then
                Dim allOutstandingLoans As List(Of OutstandingLoan) = outstandingLoans.Values.ToList
                For Each oustandingLoan In allOutstandingLoans
                    Dim borrower As Borrower = oustandingLoan.BorrowedItem.Borrower
                    If (borrower.Barcode.Equals(borrowerBarcode)) Then
                        Return oustandingLoan.BorrowedItem
                    End If
                Next
                Return Nothing
            Else
                Return Nothing
            End If
        End Function

        Public Function GetBorrower(ByVal borrowerBarcode As SimpleBarcode) As Borrower
            Return borrowers(borrowerBarcode)
        End Function

        Public Function GetGame(ByVal gameBarcode As SimpleBarcode) As Game
            Return games(gameBarcode)
        End Function

        ' Add a new game to the collection
        Public Sub Add(ByVal game As Game)
            games.Add(game.Barcode, game)
            games.Save()
            Logger.Log(DisplayTime(DateTime.Now) + " " + "ADDED GAME: " + game.ToString)
        End Sub

        ' Add a new borrower to the collection
        Public Sub Add(ByVal borrower As Borrower)
            borrowers.Add(borrower.Barcode, borrower)
            borrowers.Save()
            Logger.Log(DisplayTime(DateTime.Now) + " " + "ADDED BORROWER: " + borrower.ToString)
        End Sub

        ' Load context from files
        Public Function Load() As String
            Dim errorMessages As String = ""

            errorMessages += borrowers.Load("borrower")
            errorMessages += games.Load("game")
            errorMessages += outstandingLoans.Load("outstandingLoan")
            If (errorMessages = "") Then
                CheckOutOutstandingLoans()
            End If
            Return errorMessages
        End Function

        Private Sub CheckOutOutstandingLoans()
            For Each outstandingLoan In outstandingLoans.Values.ToList
                Dim gameSimpleBarcode As SimpleBarcode = outstandingLoan.BorrowedItem.Barcode
                Dim borrowerSimpleBarcode As SimpleBarcode = outstandingLoan.BorrowedItem.Borrower.Barcode
                CheckOut(gameSimpleBarcode, borrowerSimpleBarcode)
            Next
        End Sub

        ' Save context to files
        Private Sub Save()
            borrowers.Save()
            games.Save()
            outstandingLoans.Save()
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub OnPropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If ReferenceEquals(Nothing, obj) Then Return False
            If ReferenceEquals(Me, obj) Then Return True
            If (Not obj.GetType.Equals(GetType(CurrentBorrowingContext))) Then Return False
            Dim otherContext As CurrentBorrowingContext = CType(obj, CurrentBorrowingContext)
            Dim borrowersAreSame As Boolean = otherContext.borrowers.Equals(Me.borrowers)
            Dim gamesAreSame As Boolean = otherContext.games.Equals(Me.games)
            Dim outstandingLoansAreSame As Boolean = otherContext.outstandingLoans.Equals(Me.outstandingLoans)
            Return borrowersAreSame AndAlso gamesAreSame AndAlso outstandingLoansAreSame
        End Function

        Public Overrides Function GetHashCode() As Integer
            Dim hashCode As Integer = 0
            If borrowers IsNot Nothing Then hashCode = hashCode Xor borrowers.GetHashCode()
            If games IsNot Nothing Then hashCode = hashCode Xor games.GetHashCode()
            If outstandingLoans IsNot Nothing Then hashCode = hashCode Xor outstandingLoans.GetHashCode()
            Return hashCode
        End Function

        Private Function DisplayTime(ByVal d As DateTime) As String
            Dim timeOut As String = Format(d, "HH:mm")
            Dim dayOfWeek As String = d.DayOfWeek.ToString.Substring(0, 2)
            Dim formattedTimeAndDay As String = timeOut + " (" + dayOfWeek + ")"
            Return formattedTimeAndDay
        End Function

    End Class

End Namespace