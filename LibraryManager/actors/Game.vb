Namespace Actors
    Public Class Game
        Inherits BarcodedThing
        Implements IComparable(Of Game)

        Public Overrides Property Barcode() As SimpleBarcode
            Get
                Return m_barcode
            End Get
            Set(ByVal value As SimpleBarcode)
                If (value Is Nothing) Then
                    Throw New ArgumentNullException("Can't create a game with a null barcode.")
                ElseIf (Not value.Equals(m_barcode)) Then
                    If (value.Category <> Enums.BarcodeCategory.Game) Then
                        Throw New ArgumentException("Can't make a game without a game barcode type.")
                    Else
                        m_barcode = value
                    End If
                End If
            End Set
        End Property

        Private m_gameName As String
        Property Name() As String
            Get
                Return m_gameName
            End Get
            Set(ByVal value As String)
                If (value Is Nothing Or value.Trim.Length = 0) Then
                    Throw New ArgumentNullException("Game's name can't be empty.")
                Else
                    value = value.Replace(Constants.SPLIT_DELIMITER, "")
                    value = StrConv(value, VbStrConv.ProperCase)
                    If (m_gameName <> value) Then
                        m_gameName = value
                    End If
                End If
            End Set
        End Property

        ReadOnly Property ShortenedName() As String
            Get
                If (Name.Length >= Constants.SHORTENED_NAME_LENGTH) Then
                    Return Name.Substring(0, Constants.SHORTENED_NAME_LENGTH) + "..."
                Else
                    Return Name
                End If
            End Get
        End Property

        Private m_owner As String
        Property Owner() As String
            Get
                Return m_owner
            End Get
            Set(ByVal value As String)
                If (value Is Nothing Or value.Trim.Length = 0) Then
                    Throw New ArgumentNullException("Game owner's name can't be empty.")
                Else
                    value = value.Replace(Constants.SPLIT_DELIMITER, "")
                    value = StrConv(value, VbStrConv.ProperCase)
                    If (m_owner <> value) Then
                        m_owner = value
                    End If
                End If
            End Set
        End Property

        Private m_borrower As Borrower
        Property Borrower() As Borrower
            Get
                Return m_borrower
            End Get
            Set(ByVal value As Borrower)
                If (value Is Nothing) Then
                    m_borrower = Nothing
                ElseIf (Not value.Equals(m_borrower)) Then
                    m_borrower = value
                End If
            End Set
        End Property

        ReadOnly Property IsCheckedOut() As Boolean
            Get
                Return Borrower IsNot Nothing
            End Get
        End Property

        Public Sub New(ByVal game As Game)
            MyBase.New(game)
            If (game.Borrower Is Nothing) Then
                Me.Borrower = Nothing
            Else
                Me.Borrower = New Borrower(game.Borrower)
            End If
            Me.Name = game.Name
            Me.Owner = game.Owner
        End Sub

        Public Sub New(ByVal barcode As SimpleBarcode, ByVal gameName As String, ByVal ownerName As String)
            MyBase.New()
            Initialize(barcode, gameName, ownerName)
        End Sub

        Public Sub New(ByVal fileString As String)
            MyBase.New(fileString)
            Dim splitFileString As String() = fileString.Split(Constants.SPLIT_DELIMITER)
            If (splitFileString.Count <> 3) Then
                Throw New ArgumentException("Incorrect number of tokens in the file string for Game constructor.")
            Else
                Dim barcode As New SimpleBarcode(splitFileString(0).Trim)
                Dim gameName As String = splitFileString(1).Trim
                Dim ownerName As String = splitFileString(2).Trim
                Initialize(barcode, gameName, ownerName)
            End If
        End Sub

        Private Sub Initialize(ByVal barcode As SimpleBarcode, ByVal gameName As String, ByVal ownerName As String)
            Me.Barcode = New SimpleBarcode(barcode)
            Me.Name = gameName
            Me.Owner = ownerName
            Me.Borrower = Nothing
        End Sub


        Public Overrides Function ToString() As String
            Return (Name + " (" + Barcode.Suffix + ")")
        End Function

        Public Overrides Function ToFileString() As String
            Return (Barcode.ToString + Constants.SPLIT_DELIMITER + Name + Constants.SPLIT_DELIMITER + Owner)
        End Function

        Public Function CompareTo(ByVal other As Game) As Integer Implements IComparable(Of Game).CompareTo
            Return Barcode.CompareTo(other.Barcode)
        End Function

        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            If ReferenceEquals(Nothing, obj) Then Return False
            If ReferenceEquals(Me, obj) Then Return True
            If (Not obj.GetType.Equals(GetType(Game))) Then Return False
            Dim otherGame As Game = CType(obj, Game)
            Return otherGame.Barcode.Equals(Me.Barcode)
        End Function

        Public Overrides Function GetHashCode() As Integer
            If Barcode IsNot Nothing Then Return Barcode.GetHashCode()
            Return 0
        End Function

    End Class
End Namespace