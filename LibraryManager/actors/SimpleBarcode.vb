Namespace Actors
    Public Class SimpleBarcode
        Implements IComparable(Of SimpleBarcode)

        Private m_prefix As String
        ReadOnly Property Prefix() As String
            Get
                Return m_prefix
            End Get
        End Property

        Private m_suffix As String
        ReadOnly Property Suffix() As String
            Get
                Return m_suffix
            End Get
        End Property

        Private m_category As Enums.BarcodeCategory
        ReadOnly Property Category() As Enums.BarcodeCategory
            Get
                Return m_category
            End Get
        End Property

        Private m_barcodeString As String
        Private Property BarcodeString() As String
            Get
                Return m_barcodeString
            End Get
            Set(ByVal value As String)
                If (value Is Nothing Or value.Trim.Length = 0) Then
                    Throw New ArgumentNullException("Can't have a blank barcode.")
                ElseIf (m_barcodeString <> value) Then
                    m_barcodeString = value.Trim.ToUpper
                    SetPrefixAndSuffix(m_barcodeString)
                    m_category = DetermineCategory(m_barcodeString)
                End If
            End Set
        End Property

        Public Sub New(ByVal barcodeString As String)
            Me.BarcodeString = barcodeString
        End Sub

        Public Sub New(ByVal simpleBarcode As SimpleBarcode)
            Me.BarcodeString = simpleBarcode.ToString
        End Sub

        Private Sub SetPrefixAndSuffix(ByVal barcodeString As String)
            If (Not barcodeString.Contains(Constants.PREFIX_SUFFIX_SEPARATOR)) Then
                m_prefix = ""
                m_suffix = ""
            Else
                m_prefix = ExtractPrefix(barcodeString)
                m_suffix = ExtractSuffix(barcodeString)
            End If
        End Sub

        Private Function DetermineCategory(ByVal barcodeString As String) As Enums.BarcodeCategory
            If IsValidGameCode(barcodeString) Then
                Return Enums.BarcodeCategory.Game
            ElseIf IsValidBorrowerCode(barcodeString) Then
                Return Enums.BarcodeCategory.Borrower
            Else
                Return Enums.BarcodeCategory.Unknown
            End If
        End Function

        Private Function IsValidBorrowerCode(ByVal barcodeString As String) As Boolean
            Return HasValidPrefix(Constants.BORROWER_PREFIX) AndAlso _
                   SuffixIsProperLength(Constants.MIN_NUMBER_DIGITS_IN_BORROWER_SUFFIX, Constants.MAX_NUMBER_DIGITS_IN_BORROWER_SUFFIX) AndAlso _
                   SuffixIsNumeric()
        End Function

        Private Function IsValidGameCode(ByVal barcodeString As String) As Boolean
            Return HasValidPrefix(Constants.GAME_PREFIX) AndAlso _
                   SuffixIsProperLength(Constants.MAX_NUMBER_DIGITS_IN_GAME_SUFFIX, Constants.MAX_NUMBER_DIGITS_IN_GAME_SUFFIX) AndAlso _
                   SuffixIsNumeric()
        End Function

        Private Function HasValidPrefix(ByVal validPrefix As String) As Boolean
            Return (Me.Prefix.Equals(validPrefix))
        End Function

        Private Function SuffixIsProperLength(ByVal suffixMinLength As Integer, ByVal suffixMaxLength As Integer) As Boolean
            Return (Me.Suffix.Length >= suffixMinLength) AndAlso (Me.Suffix.Length <= suffixMaxLength)
        End Function

        Private Function SuffixIsNumeric() As Boolean
            Return IsNumeric(Me.Suffix)
        End Function

        Private Function ExtractSuffix(ByVal barcodeString As String) As String
            Dim separatorPosition As Integer = barcodeString.IndexOf(Constants.PREFIX_SUFFIX_SEPARATOR)
            Dim charsUntilEnd As Integer = barcodeString.Length - separatorPosition - 1
            Return barcodeString.Substring(separatorPosition + 1, charsUntilEnd)
        End Function

        Private Function ExtractPrefix(ByVal barcodeString As String) As String
            Dim separatorPosition As Integer = barcodeString.IndexOf(Constants.PREFIX_SUFFIX_SEPARATOR)
            Return barcodeString.Substring(0, separatorPosition)
        End Function

        Public Overrides Function ToString() As String
            Return BarcodeString.ToString()
        End Function

        Public Function CompareTo(ByVal other As SimpleBarcode) As Integer Implements IComparable(Of SimpleBarcode).CompareTo
            Return BarcodeString.CompareTo(other.BarcodeString)
        End Function

        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            If ReferenceEquals(Nothing, obj) Then Return False
            If ReferenceEquals(Me, obj) Then Return True
            If (Not obj.GetType.Equals(GetType(SimpleBarcode))) Then Return False
            Dim otherBarcode As SimpleBarcode = CType(obj, SimpleBarcode)
            Return otherBarcode.ToString.Equals(Me.BarcodeString)
        End Function

        Public Overrides Function GetHashCode() As Integer
            If BarcodeString IsNot Nothing Then Return BarcodeString.GetHashCode()
            Return 0
        End Function

    End Class
End Namespace