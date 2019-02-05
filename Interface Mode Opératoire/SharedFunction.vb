Public Class SF
    'SF = Shared Function
    ''' <summary>
    ''' Tri une collection
    ''' </summary>
    ''' <param name="Col">Collection à trier</param>
    ''' <param name="Ascendant">Facultatif.Par défaut ordre ascendant</param>
    ''' <returns>Retourne la collection triée</returns>
    Overloads Shared Function Tri(ByRef Col As Collection, Optional ByVal Ascendant As Boolean = True) As Collection
        Dim fin As Integer = Col.Count
        Dim clef As Boolean
        Dim T As String

        'récupération dans tableau
        Dim table(0 To fin - 1)
        For i = 0 To fin - 1
            table(i) = Col.Item(i + 1)
        Next i

        Col.Clear()

        Select Case Ascendant
            Case True
                'tri du tableau ASC
                Do
                    clef = False
                    For i = 0 To fin - 2
                        If table(i) > table(i + 1) Then
                            T = table(i)
                            table(i) = table(i + 1)
                            table(i + 1) = T
                            clef = True
                            Exit For
                        End If
                    Next i
                Loop While clef = True
            Case False
                'tri du tableau DSC
                Do
                    clef = False
                    For i = 0 To fin - 2
                        If table(i) < table(i + 1) Then
                            T = table(i)
                            table(i) = table(i + 1)
                            table(i + 1) = T
                            clef = True
                            Exit For
                        End If
                    Next i
                Loop While clef = True
        End Select


        On Error Resume Next
        For Each element As String In table
            Col.Add(element, element)
        Next

        Return Col
    End Function

End Class
