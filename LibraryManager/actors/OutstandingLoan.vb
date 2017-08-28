Imports System.ComponentModel

Namespace Actors
    Public Class OutstandingLoan
        Inherits BarcodedThing
        Implements IComparable(Of OutstandingLoan)
        Implements INotifyPropertyChanged

        Public Overrides Property Barcode() As SimpleBarcode
            Get
                Return BorrowedItem.Barcode
            End Get
            Set(ByVal value As SimpleBarcode)
                If (value Is Nothing) Then
                    Throw New ArgumentNullException("Can't create an outstanding loan with a null barcode.")
                ElseIf (Not value.Equals(BorrowedItem.Barcode)) Then
                    If (value.Category <> Enums.BarcodeCategory.Game) Then
                        Throw New ArgumentException("Can't make an outstanding loan without a game barcode type.")
                    Else
                        m_barcode = BorrowedItem.Barcode
                        BorrowedItem.Barcode = value
                    End If
                End If
            End Set
        End Property

        Private m_borrowedItem As Game
        Property BorrowedItem() As Game
            Get
                Return m_borrowedItem
            End Get
            Set(ByVal value As Game)
                If value Is Nothing Then
                    Throw New ArgumentNullException("Borrowed item can't be null object.")
                ElseIf (Not value.Equals(m_borrowedItem)) Then
                    m_borrowedItem = value
                End If
            End Set
        End Property

        Protected m_checkOutTime As DateTime
        ReadOnly Property CheckOutTime() As DateTime
            Get
                Return m_checkOutTime
            End Get
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub OnPropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        ReadOnly Property DisplayedCheckOutTime() As String
            Get
                Dim timeOut As String = Format(CheckOutTime, "HH:mm")
                Dim dayOfWeek As String = CheckOutTime.DayOfWeek.ToString.Substring(0, 2)
                Dim formattedTimeAndDay As String = timeOut + " (" + dayOfWeek + ")"
                Return formattedTimeAndDay
            End Get
        End Property

        ReadOnly Property DisplayedGameInfo() As String
            Get
                Dim gameName As String = BorrowedItem.Name
                Dim gameBarcodeSuffix As String = BorrowedItem.Barcode.Suffix

                If (gameName.Length > Constants.MAX_CHECKOUT_DISPLAY_GAME_LENGTH) Then
                    gameName = gameName.Substring(0, Constants.MAX_CHECKOUT_DISPLAY_GAME_LENGTH - 3) + "..."
                End If
                Dim displayedText As String = gameName + " (" + gameBarcodeSuffix + ")"

                Return displayedText
            End Get
        End Property

        ReadOnly Property DisplayedBorrowerInfo() As String
            Get
                Dim borrowerName As String = BorrowedItem.Borrower.FirstName + " " + BorrowedItem.Borrower.LastName
                Dim borrowerBarcodeSuffix As String = BorrowedItem.Borrower.Barcode.Suffix

                If (borrowerName.Length > Constants.MAX_CHECKOUT_DISPLAY_BORROWER_LENGTH) Then
                    borrowerName = borrowerName.Substring(0, Constants.MAX_CHECKOUT_DISPLAY_BORROWER_LENGTH - 3) + "..."
                End If

                Dim displayedText As String = borrowerName + " (" + borrowerBarcodeSuffix + ")"

                Return displayedText
            End Get
        End Property

        Public Sub New(ByVal outstandingLoan As OutstandingLoan)
            MyBase.New()
            Me.BorrowedItem = New Game(outstandingLoan.BorrowedItem)
            m_checkOutTime = outstandingLoan.CheckOutTime
        End Sub

        Public Sub New(ByVal borrowedItem As Game)
            MyBase.New()
            Me.BorrowedItem = borrowedItem
            m_checkOutTime = Now
        End Sub

        Public Sub New(ByVal borrowedItem As Game, ByVal timeOut As DateTime)
            Me.BorrowedItem = New Game(borrowedItem)
            m_checkOutTime = timeOut
        End Sub

        Public Sub New(ByVal fileString As String)
            MyBase.New(fileString)
            Dim splitFileString As String() = fileString.Split(Constants.SPLIT_DELIMITER)
            If (splitFileString.Count <> 7) Then
                Throw New ArgumentException("Incorrect number of tokens in the file string for the Outstanding Loan constructor.")
            Else
                Dim dateTimeString As String = splitFileString(0).Trim
                Dim gameBarcodeString As String = splitFileString(1).Trim
                Dim gameName As String = splitFileString(2).Trim
                Dim gameOwner As String = splitFileString(3).Trim
                Dim borrowerBarcodeString As String = splitFileString(4).Trim
                Dim borrowerFirstName As String = splitFileString(5).Trim
                Dim borrowerLastName As String = splitFileString(6).Trim
                Dim borrowedItem As New Game(New SimpleBarcode(gameBarcodeString), gameName, gameOwner)
                Dim borrower As New Borrower(New SimpleBarcode(borrowerBarcodeString), borrowerFirstName, borrowerLastName)
                Dim borrowingDateTime As DateTime = Convert.ToDateTime(dateTimeString)
                borrowedItem.Borrower = borrower
                Initialize(borrowedItem, borrowingDateTime)
            End If
        End Sub

        Private Sub Initialize(ByVal borrowedItem As Game, ByVal checkOutTime As DateTime)
            Me.BorrowedItem = New Game(borrowedItem)
            Me.m_checkOutTime = checkOutTime
        End Sub

        Public Function CompareTo(ByVal other As OutstandingLoan) As Integer Implements IComparable(Of OutstandingLoan).CompareTo
            Return BorrowedItem.CompareTo(other.BorrowedItem)
        End Function

        Public Overloads Function Equals(ByVal other As OutstandingLoan) As Boolean
            If ReferenceEquals(Nothing, other) Then Return False
            If ReferenceEquals(Me, other) Then Return True
            Return Equals(other.m_borrowedItem, m_borrowedItem)
        End Function

        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            If ReferenceEquals(Nothing, obj) Then Return False
            If ReferenceEquals(Me, obj) Then Return True
            If (Not obj.GetType.Equals(GetType(OutstandingLoan))) Then Return False
            Dim otherOuststandingLoan As OutstandingLoan = CType(obj, OutstandingLoan)
            Return otherOuststandingLoan.Barcode.Equals(Me.Barcode)
        End Function

        Public Overrides Function GetHashCode() As Integer
            If BorrowedItem IsNot Nothing Then Return BorrowedItem.GetHashCode()
            Return 0
        End Function

        ' For writing to the text file
        Public Overrides Function ToFileString() As String
            Return (CheckOutTime.ToString + Constants.SPLIT_DELIMITER + BorrowedItem.Barcode.ToString + Constants.SPLIT_DELIMITER + BorrowedItem.Name + Constants.SPLIT_DELIMITER + BorrowedItem.Owner + Constants.SPLIT_DELIMITER + BorrowedItem.Borrower.Barcode.ToString + Constants.SPLIT_DELIMITER + BorrowedItem.Borrower.FirstName + Constants.SPLIT_DELIMITER + BorrowedItem.Borrower.LastName)
        End Function

        Public Overrides Function ToString() As String
            Return DisplayedCheckOutTime.PadRight(10 + Constants.CHECKOUT_DISPLAY_GUTTER_WIDTH) + _
                   DisplayedGameInfo.PadRight(Constants.CHECKOUT_DISPLAY_GAME_FIELD_WIDTH + Constants.CHECKOUT_DISPLAY_GUTTER_WIDTH) + _
                   DisplayedBorrowerInfo.PadRight(Constants.CHECKOUT_DISPLAY_BORROWER_FIELD_WIDTH + Constants.CHECKOUT_DISPLAY_GUTTER_WIDTH)
        End Function

    End Class
End Namespace