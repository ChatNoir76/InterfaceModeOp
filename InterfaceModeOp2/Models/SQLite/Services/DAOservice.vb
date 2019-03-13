Module DAOservice
    ''' <summary>
    ''' Renvoie la date au format Text SQLite
    ''' </summary>
    Public Function DateToString(ByVal maDate As DateTime)
        Dim maDateStr As New System.Text.StringBuilder
        With maDateStr
            .Append(Format(maDate.Year, "0000"))
            .Append("-")
            .Append(Format(maDate.Month, "00"))
            .Append("-")
            .Append(Format(maDate.Day, "00"))
            .Append(" ")
            .Append(Format(maDate.Hour, "00"))
            .Append(":")
            .Append(Format(maDate.Minute, "00"))
            .Append(":")
            .Append(Format(maDate.Second, "00"))
        End With
        Return maDateStr.ToString
    End Function
End Module
