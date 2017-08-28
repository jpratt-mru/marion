Namespace Actors
    Public Class Borrower
        Inherits BarcodedThing
        Implements IComparable(Of Borrower)

        Public Overrides Property Barcode() As SimpleBarcode
            Get
                Return m_barcode
            End Get
            Set(ByVal value As SimpleBarcode)
                If (value Is Nothing) Then
                    Throw New ArgumentNullException("Can't create a borrower with a null barcode.")
                ElseIf (Not value.Equals(m_barcode)) Then
                    If (value.Category <> Enums.BarcodeCategory.Borrower) Then
                        Throw New ArgumentException("Can't make a borrower without a borrower barcode type.")
                    Else
                        m_barcode = value
                    End If
                End If
            End Set
        End Property

        Private m_firstName As String
        Public Property FirstName() As String
            Get
                Return m_firstName
            End Get
            Set(ByVal value As String)
                If (value Is Nothing Or value.Trim.Length = 0) Then
                    Throw New ArgumentNullException("Borrower's first name can't be empty.")
                Else
                    value = value.Replace(Constants.SPLIT_DELIMITER, "")
                    value = StrConv(value, VbStrConv.ProperCase)
                    If (m_firstName <> value) Then
                        m_firstName = value
                    End If
                End If
            End Set
        End Property

        Private m_lastName As String
        Public Property LastName() As String
            Get
                Return m_lastName
            End Get
            Set(ByVal value As String)
                If (value Is Nothing Or value.Trim.Length = 0) Then
                    Throw New ArgumentNullException("Borrower's last name can't be empty.")
                Else
                    value = value.Replace(Constants.SPLIT_DELIMITER, "")
                    value = StrConv(value, VbStrConv.ProperCase)
                    If (m_lastName <> value) Then
                        m_lastName = value
                    End If
                End If
            End Set
        End Property

        Private m_hasGameOut As Boolean
        Property HasGameOut() As Boolean
            Get
                Return m_hasGameOut
            End Get
            Set(ByVal value As Boolean)
                If (m_hasGameOut <> value) Then
                    m_hasGameOut = value
                End If
            End Set
        End Property

        Public Sub New(ByVal borrower As Borrower)
            MyBase.new(borrower)
            Me.FirstName = borrower.FirstName
            Me.LastName = borrower.LastName
            Me.HasGameOut = borrower.HasGameOut
        End Sub

        Public Sub New(ByVal barcode As SimpleBarcode, ByVal firstName As String, ByVal lastName As String)
            MyBase.New()
            Initialize(barcode, firstName, lastName)
        End Sub

        Public Sub New(ByVal fileString As String)
            MyBase.New(fileString)
            Dim splitFileString As String() = fileString.Split(Constants.SPLIT_DELIMITER)
            If (splitFileString.Count <> 3) Then
                Throw New ArgumentException("Incorrect number of tokens in the file string for the Borrower constructor.")
            Else
                Dim barcode As New SimpleBarcode(splitFileString(0).Trim)
                Dim firstName As String = splitFileString(1).Trim
                Dim lastName As String = splitFileString(2).Trim
                Initialize(barcode, firstName, lastName)
            End If
        End Sub

        Private Sub Initialize(ByVal barcode As SimpleBarcode, ByVal firstName As String, ByVal lastName As String)
            Me.FirstName = firstName
            Me.LastName = lastName
            Me.Barcode = barcode
            Me.HasGameOut = False
        End Sub

        Public Overrides Function ToString() As String
            Return (FirstName + " " + LastName + "(" + Barcode.Suffix + ")")
        End Function

        Public Overrides Function ToFileString() As String
            Return (Barcode.ToString + Constants.SPLIT_DELIMITER + FirstName + Constants.SPLIT_DELIMITER + LastName)
        End Function

        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            If ReferenceEquals(Nothing, obj) Then Return False
            If ReferenceEquals(Me, obj) Then Return True
            If (Not obj.GetType.Equals(GetType(Borrower))) Then Return False
            Dim otherBorower As Borrower = CType(obj, Borrower)
            Return otherBorower.Barcode.Equals(Me.Barcode)
        End Function

        Public Overrides Function GetHashCode() As Integer
            If Barcode IsNot Nothing Then Return Barcode.GetHashCode()
            Return 0
        End Function

        Public Function CompareTo(ByVal other As Borrower) As Integer Implements System.IComparable(Of Borrower).CompareTo
            Return Barcode.CompareTo(other.Barcode)
        End Function

    End Class
End Namespace